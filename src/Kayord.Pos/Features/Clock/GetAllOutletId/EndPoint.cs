using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Clock.List
{
    public class Endpoint : Endpoint<Request, List<Pos.Entities.Staff>>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/clock/list");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            List<Pos.Entities.Staff> staffList = new List<Pos.Entities.Staff>();
            List<Pos.Entities.Staff> prestaffList = new List<Pos.Entities.Staff>();

            if (req.StatusId == 1) // Clocked Out
            {
                // Get staff with no corresponding clock records for the day (not clocked in or out)
                var AllStaff = await _dbContext.Staff
                    .Where(s => s.OutletId == req.OutletId)
                    .ToListAsync();

                // Get staff who are clocked in
                var clockedInStaff = await _dbContext.Clock
                    .Where(c => c.OutletId == req.OutletId && c.EndDate == null)
                    .Select(c => c.Staff)
                    .ToListAsync();

                foreach (var item in AllStaff)
                {
                    if (!clockedInStaff.Any(x => x.Id == item.Id))
                    {
                        staffList.Add(item);
                    }
                }

            }
            else if (req.StatusId == 2) // Clocked In
            {
                staffList = await _dbContext.Clock
                    .Where(c => c.OutletId == req.OutletId && c.EndDate == null)
                    .Select(c => c.Staff)
                    .ToListAsync();
            }

            await SendAsync(staffList);
        }
    }
}