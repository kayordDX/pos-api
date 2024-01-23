using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.User.GetRoles
{
    public class Endpoint : Endpoint<Request, List<string>>
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
            Get("/user/getroles");
            AllowAnonymous();
        }   
 
        public override async Task HandleAsync(Request req, CancellationToken ct)  
        {
         
              var userRoles = await _dbContext.UserRole
                .Include(ur => ur.Role)
                .Where(ur => ur.UserId == _cu.UserId)
                .Select(ur => ur.Role.Name)
                .ToListAsync();
            if (userRoles == null)
             {
                await SendNotFoundAsync();
                return;
             }
                await SendAsync(userRoles);
                return;
        }
    }
    }