using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.User.RemoveUserOutletRole
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
            Delete("/user/role/{userId}/{role}");
        }

        public override async Task HandleAsync(Request req, CancellationToken c)
        {
            var userOutlet = await _dbContext.UserOutlet.Where(x => x.UserId == _cu.UserId && x.IsCurrent == true).FirstOrDefaultAsync(c);
            if (userOutlet == null)
            {
                throw new Exception("Could not find outlet for user");
            }

            var role = await _dbContext.Role.FirstOrDefaultAsync(x => x.Name == req.Role);
            if (role == null)
            {
                throw new Exception("Could not find role");
            }

            var roleEntity = await _dbContext.UserRoleOutlet
                .Where(x => x.UserId == req.UserId && x.RoleId == role.RoleId && x.OutletId == userOutlet.OutletId)
                .FirstOrDefaultAsync(c);

            if (roleEntity == null)
            {
                throw new Exception("Could not find role to remove");
            }

            _dbContext.UserRoleOutlet.Remove(roleEntity);
            await _dbContext.SaveChangesAsync();
            await SendNoContentAsync();
        }
    }
}