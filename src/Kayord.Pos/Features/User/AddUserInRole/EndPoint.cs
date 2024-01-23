using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Role.AddUserInRole
{
    public class Endpoint : Endpoint<Request, Pos.Entities.UserRole>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Post("/role/adduserinrole");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
           

                var UserRole = new Entities.UserRole
                {
                   RoleId = req.RoleId,
                   UserId = req.UserId
                };

                _dbContext.UserRole.Add(UserRole);
                await _dbContext.SaveChangesAsync();

                var result = await _dbContext.UserRole.FindAsync(UserRole.UserRoleId);

                await SendAsync(result);
          
        }
    }
}