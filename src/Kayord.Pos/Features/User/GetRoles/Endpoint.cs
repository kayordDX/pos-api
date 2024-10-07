using Kayord.Pos.Services;

namespace Kayord.Pos.Features.User.GetRoles
{
    public class Endpoint : Endpoint<Request, List<string>>
    {
        private readonly UserService _userService;

        public Endpoint(UserService userService)
        {
            _userService = userService;
        }

        public override void Configure()
        {
            Get("/user/getRoles");
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var userRoles = await _userService.GetUserRoles();

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