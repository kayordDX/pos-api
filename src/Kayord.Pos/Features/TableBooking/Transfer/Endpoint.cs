using Kayord.Pos.Data;
using Kayord.Pos.Entities;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.TableBooking.Transfer;

public class Endpoint : Endpoint<Request>
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _user;

    public Endpoint(AppDbContext dbContext, CurrentUserService user)
    {
        _dbContext = dbContext;
        _user = user;
    }

    public override void Configure()
    {
        Post("/tableBooking/transfer");
        Policies(Constants.Policy.Manager);
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        var tableBooking = await _dbContext.TableBooking.FirstOrDefaultAsync(x => x.Id == req.TableBookingId && x.CloseDate == null);
        if (tableBooking == null)
        {
            ValidationContext.Instance.ThrowError("Could not find active table booking");
        }

        TableBookingTransfer entity = new()
        {
            ByUserId = _user.UserId ?? "",
            FromUserId = tableBooking.UserId,
            ToUserId = req.TransferUserId,
            TableBookingId = req.TableBookingId
        };

        tableBooking.UserId = req.TransferUserId;

        await _dbContext.TableBookingTransfer.AddAsync(entity, ct);
        await _dbContext.SaveChangesAsync(ct);
        await Send.NoContentAsync();
    }
}