using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Services;

public class UserService
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _cu;

    public UserService(AppDbContext dbContext, CurrentUserService cu)
    {
        _dbContext = dbContext;
        _cu = cu;
    }

    public async Task<List<string>> GetUserRoles()
    {
        return await _dbContext.Database
        .SqlQuery<string>(
        $"""
            SELECT 
                r."Name"
            FROM "UserOutlet" u
            JOIN "UserRoleOutlet" ur
                ON u."OutletId" = ur."OutletId"
            AND u."UserId" = ur."UserId"
            JOIN "Role" r
                ON r."RoleId" = ur."RoleId"
            WHERE u."UserId" = {_cu.UserId}
            AND u."isCurrent" = true
        """
        )
        .ToListAsync();
    }
}