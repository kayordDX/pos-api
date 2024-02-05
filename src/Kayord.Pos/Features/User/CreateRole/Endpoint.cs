using Kayord.Pos.Data;

namespace Kayord.Pos.Features.Role.Create
{
    public class Endpoint : Endpoint<Request>
    {
        private readonly AppDbContext _dbContext;

        public Endpoint(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Post("/role/createRole");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var newRole = new Entities.Role
            {
                Name = req.Name,
                Description = req.Description
            };

            _dbContext.Role.Add(newRole);
            await _dbContext.SaveChangesAsync();
            await SendNoContentAsync();
        }
    }
}