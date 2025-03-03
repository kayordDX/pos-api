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
            r.name
        FROM user_outlet u
        JOIN user_role_outlet ur
            ON u.outlet_id = ur.outlet_id
        AND u.user_id = ur.user_id
        JOIN role r
            ON r.role_id = ur.role_id
        WHERE u.user_id = {_cu.UserId}
        AND u.is_current = true  
        """
        )
        .ToListAsync();
    }
}