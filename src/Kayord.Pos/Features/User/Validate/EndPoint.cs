using Kayord.Pos.Data;
using Kayord.Pos.Services;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.User.Validate

{
    public class Endpoint : Endpoint<Request, Response>
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
            Get("/User/Validate");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            
            // Check User Exists Poes
            var user = _dbContext.User.FirstOrDefault(x=>x.UserId == _cu.UserId);
            if(user == null)
            {
                user.Email = req.Email;
                user.UserId = req.UserId;
                user.Image = req.Image;
                user.Name = req.Name;
                _dbContext.User.Add(user);
                _dbContext.SaveChangesAsync();
            }     
            user = _dbContext.User.FirstOrDefault(x=>x.UserId == _cu.UserId);
            //TODO: - Double Check if details match and update if not
            var defaultRole = _dbContext.UserRole.FirstOrDefault(x=>x.UserId== _cu.UserId);
            if(defaultRole == null)
            {
                defaultRole.RoleId = 1;
                defaultRole.UserId = _cu.UserId;
                _dbContext.UserRole.Add(defaultRole);
                _dbContext.SaveChangesAsync();
            }

            var userRoles = await _dbContext.UserRole
                .Include(ur => ur.Role)
                .Where(ur => ur.UserId == user.UserId)
                .Select(ur => ur.Role.Name)
                .ToListAsync();
            Response r = new Response();
            r.UserId = user.UserId;
            r.UserRoles = userRoles;
            await SendAsync(r);
        }
    }
}