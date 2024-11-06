using Kayord.Pos.Data;
using Kayord.Pos.DTO;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Printer.Create;

public class Endpoint : Endpoint<Request, PrinterDTO>
{
    private readonly AppDbContext _dbContext;

    public Endpoint(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public override void Configure()
    {
        Post("/printer");
    }

    public override async Task HandleAsync(Request req, CancellationToken ct)
    {
        Entities.Printer entity = new Entities.Printer()
        {
            OutletId = req.OutletId,
            Port = req.Port,
            PrinterName = req.PrinterName,
            IPAddress = req.IPAddress,
            IsEnabled = true,
            LineCharacters = req.LineCharacters
        };
        await _dbContext.Printer.AddAsync(entity);
        await _dbContext.SaveChangesAsync();

        var result = await _dbContext.Printer.ProjectToDto().FirstOrDefaultAsync(x => x.Id == entity.Id);
        if (result == null)
        {
            await SendNotFoundAsync();
            return;
        }

        await SendAsync(result);
    }
}