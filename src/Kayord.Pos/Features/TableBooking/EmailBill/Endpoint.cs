using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Kayord.Pos.Features.TableOrder.GetBill;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using QuestPDF.Fluent;

namespace Kayord.Pos.Features.TableBooking.EmailBill
{
    public class Endpoint : Endpoint<Request, bool>
    {
        private readonly AppDbContext _dbContext;
        private readonly CurrentUserService _user;
        private readonly IEmailSender _emailSender;

        public Endpoint(AppDbContext dbContext, CurrentUserService user, IEmailSender emailSender)
        {
            _dbContext = dbContext;
            _user = user;
            _emailSender = emailSender;
        }

        public override void Configure()
        {
            Post("/tableBooking/emailBill");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            TableOrder.GetBill.Request request = new() { TableBookingId = req.TableBookingId };
            var bill = await Bill.Get(request, _dbContext);

            List<Item> items = new();
            foreach (var order in bill.OrderItems)
            {
                List<SubItem> subItems = new();

                foreach (var extra in order.OrderItemExtras ?? [])
                {
                    subItems.Add(new SubItem { Name = $"+ {extra.Extra.Name}", Price = extra.Extra.Price });
                }
                foreach (var option in order.OrderItemOptions ?? [])
                {
                    subItems.Add(new SubItem { Name = $"> {option.Option.Name}", Price = option.Option.Price });
                }
                items.Add(new Item { Name = order.MenuItem.Name, Price = order.MenuItem.Price, Items = subItems });
            }
            foreach (var adjustment in bill.Adjustments ?? [])
            {
                items.Add(new Item { Name = adjustment.AdjustmentType.Name, Price = adjustment.Amount });
            }

            var tableBooking = await _dbContext.TableBooking.FindAsync(req.TableBookingId);
            if (tableBooking == null)
            {
                await SendNotFoundAsync();
                return;
            }

            var salesPeriod = await _dbContext.SalesPeriod.FindAsync(tableBooking.SalesPeriodId);
            if (salesPeriod == null)
            {
                await SendNotFoundAsync();
                return;
            }

            var outlet = await _dbContext.Outlet.Include(x => x.Business).FirstOrDefaultAsync(x => x.Id == salesPeriod.OutletId);
            if (outlet == null)
            {
                await SendNotFoundAsync();
                return;
            }

            // Generate PDF
            PdfRequest pdfRequest = new()
            {
                Balance = bill.Balance,
                VAT = bill.VAT,
                PaymentReceived = bill.PaymentsReceived.Sum(s => s.Amount),
                TableBookingId = req.TableBookingId,
                TipAmount = bill.TipAmount,
                TotalExVAT = bill.TotalExVAT,
                Total = bill.Total,
                BillDate = bill.BillDate,
                Items = items,
                OutletName = $"{outlet.Business.Name} {outlet.Name}",
                VATNumber = outlet.VATNumber,
                Logo = outlet.Logo
            };
            BillPdf billPdf = new(pdfRequest);
            var document = billPdf.Generate();

            await using var stream = new MemoryStream();
            document.GeneratePdf(stream);

            // document.GeneratePdf("./test.pdf");

            AttachmentCollection attachment = new()
            {
                { $"Invoice{pdfRequest.TableBookingId}.pdf", stream.ToArray() }
            };

            await _emailSender.SendEmailAsync(req.Email, req.Name, $"{outlet.Business.Name} {outlet.Name} Invoice #{pdfRequest.TableBookingId} {pdfRequest.BillDate}",
            $"""
            Dear {req.Name},

            Thank you for choosing {outlet.Business.Name} {outlet.Name}.
            We appreciate your recent visit.

            Please find the attached invoice for your reference.

            If you have any questions or need further assistance, feel free to reach out.

            Best regards,
            {outlet.Business.Name} {outlet.Name}
            """, attachment);

            // Send Email
            await SendAsync(true);
        }
    }
}