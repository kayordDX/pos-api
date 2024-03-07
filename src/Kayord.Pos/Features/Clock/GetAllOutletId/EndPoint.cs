using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Clock.List
{
    public class Endpoint : Endpoint<Request, List<Entities.User>>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/clock/list");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            List<Entities.User> staffList = new List<Entities.User>();
            List<Entities.User> preStaffList = new List<Entities.User>();

            if (req.StatusId == 1) // Clocked Out
            {
                // Get staff with no corresponding clock records for the day (not clocked in or out)
                // var allStaff = await _dbContext.User
                //     .Where(s => s.OutletId == req.OutletId)
                //     .ToListAsync();

                // Get staff who are clocked in
                var clockedInStaff = await _dbContext.Clock
                    .Where(c => c.OutletId == req.OutletId && c.EndDate == null)
                    .Select(c => c.User)
                    .ToListAsync();

                // foreach (var item in allStaff)
                // {
                //     if (!clockedInStaff.Any(x => x.Id == item.Id))
                //     {
                //         staffList.Add(item);
                //     }
                // }

            }
            else if (req.StatusId == 2) // Clocked In
            {
                staffList = await _dbContext.Clock
                    .Where(c => c.OutletId == req.OutletId && c.EndDate == null)
                    .Select(c => c.User)
                    .ToListAsync();
            }

            await SendAsync(staffList);
        }
    }
}