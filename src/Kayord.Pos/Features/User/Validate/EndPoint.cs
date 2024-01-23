using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.User.Validate

{
    public class Endpoint : Endpoint<Request, Response>
    {
        private readonly AppDbContext _dbContext;


        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Post("/User/Validate");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            // Check User Exists Poes
            var user = await _dbContext.User.FirstOrDefaultAsync(x => x.UserId == req.UserId);
            if (user == null)
            {
                await _dbContext.User.AddAsync(new Entities.User
                {
                    Email = req.Email ?? "",
                    UserId = req.UserId ?? "",
                    Image = req.Image ?? "",
                    Name = req.Name ?? "",
                });
                await _dbContext.SaveChangesAsync();
            }
            user = await _dbContext.User.FirstOrDefaultAsync(x => x.UserId == req.UserId);
            //TODO: - Double Check if details match and update if not
            var defaultRole = await _dbContext.UserRole.FirstOrDefaultAsync(x => x.UserId == req.UserId);
            if (defaultRole == null)
            {
                await _dbContext.UserRole.AddAsync(new Entities.UserRole
                {
                    RoleId = 1,
                    UserId = req.UserId ?? ""
                });
                await _dbContext.SaveChangesAsync();
            }

            var userRoles = await _dbContext.UserRole
                .Include(ur => ur.Role)
                .Where(ur => ur.UserId == req.UserId)
                .Select(ur => ur.Role.Name)
                .ToListAsync();

            Response r = new()
            {
                UserId = req.UserId ?? "",
                UserRoles = userRoles
            };

            await SendAsync(r);
        }
    }
}