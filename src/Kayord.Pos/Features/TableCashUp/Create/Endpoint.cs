using Kayord.Pos.Data;

namespace Kayord.Pos.Features.TableCashUp.Create
{
    public class Endpoint : Endpoint<Request, Pos.Entities.TableCashUp>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Post("/tableCashUp");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            Pos.Entities.TableCashUp entity = new Pos.Entities.TableCashUp()
            {
                TableBookingId = req.TableBookingId,
                SalesAmount = req.SalesAmount,
                TotalAmount = req.TotalAmount,
                CashUpDate = DateTime.Now,
                OutletId = req.OutletId,
            };

            await _dbContext.TableCashUp.AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            var result = await _dbContext.TableCashUp.FindAsync(entity.Id);
            if (result == null)
            {
                await SendNotFoundAsync();
                return;
            }

            await SendAsync(result);
        }
    }
}