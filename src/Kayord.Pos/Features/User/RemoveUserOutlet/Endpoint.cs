using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.User.RemoveUserOutlet
{
    public class Endpoint : Endpoint<Request>
    {
        private readonly AppDbContext _dbContext;
        private readonly CurrentUserService _cu;

        public Endpoint(AppDbContext dbContext, CurrentUserService cu)
        {
            _dbContext = dbContext;
            _cu = cu;
        }

        public override void Configure()
        {
            Delete("/user/outlet/{userId}");
        }

        public override async Task HandleAsync(Request req, CancellationToken c)
        {
            var userOutlet = await _dbContext.UserOutlet.Where(x => x.UserId == _cu.UserId && x.IsCurrent == true).FirstOrDefaultAsync(c);
            if (userOutlet == null)
            {
                throw new Exception("Could not find outlet for user");
            }

            var roleEntity = await _dbContext.UserOutlet
                .Where(x => x.UserId == req.UserId && x.OutletId == userOutlet.OutletId)
                .FirstOrDefaultAsync(c);

            if (roleEntity == null)
            {
                throw new Exception("Could not find user to remove");
            }

            _dbContext.UserOutlet.RemoveRange(roleEntity);
            await _dbContext.SaveChangesAsync();
            await SendNoContentAsync();
        }
    }
}