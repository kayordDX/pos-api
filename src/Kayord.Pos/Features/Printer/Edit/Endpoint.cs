using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Printer.Edit;

public class Endpoint : Endpoint<Request, PrinterDTO>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Put("/printer");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Entities.Printer? entity = await _dbContext.Printer.FirstOrDefaultAsync(x => x.Id == req.Id);
        if (entity == null)
        {
            await SendNotFoundAsync();
            return;
        }

        entity.Port = req.Port;
        entity.PrinterName = req.PrinterName;
        entity.IPAddress = req.IPAddress;
        entity.LineCharacters = req.LineCharacters;
        entity.IsEnabled = req.IsEnabled;
        entity.DeviceId = req.DeviceId;

        await _dbContext.SaveChangesAsync();
        await SendNoContentAsync();
    }
}