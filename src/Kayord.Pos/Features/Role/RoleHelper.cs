using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Features.Role;

public static class RoleHelper
{
    public static async Task<List<int>> GetDivisionsForRolesOnly(string? roleIds, AppDbContext _dbContext, string? userId)
    {
        int outletId = await Helper.GetUserOutlet(_dbContext, userId ?? "");
        return await GetDivisionsForRolesOnly(roleIds, _dbContext, outletId, userId);
    }
    public static async Task<List<int>> GetDivisionsForRolesOnly(string? roleIds, AppDbContext _dbContext, int outletId, string? userId)
    {
        List<int> divisionIds = new();
        if (roleIds != null)
        {
            var rolesToCheck = roleIds.Split(",").Select(int.Parse).ToList();
            var canViewRole = await _dbContext.UserRoleOutlet.Where(x => x.UserId == userId && x.OutletId == outletId && rolesToCheck.Contains(x.RoleId)).FirstOrDefaultAsync();
            if (canViewRole == null)
            {
                throw new Exception("You do not have access to view role");
            }
            divisionIds = await _dbContext.RoleDivision.Where(x => rolesToCheck.Contains(x.RoleId)).Select(x => x.DivisionId).ToListAsync();
        }
        return divisionIds;
    }

    // public static string GetAppRoleFromRole(Entities.Role role)
    // {
    //     if (role.isBackOffice == false && role.isFrontLine == false)
    //     {
    //         return "guest";
    //     }
    //     if (role.isBackOffice == true && role.isFrontLine == true)
    //     {
    //         return "manager";
    //     }
    //     if (role.isBackOffice == true && role.isFrontLine == false)
    //     {
    //         return "back";
    //     }
    //     if (role.isBackOffice == false && role.isFrontLine == true)
    //     {
    //         return "front";
    //     }
    //     return "guest";
    // }

    public static async Task<List<int>> GetDivisionsForRoles(string? roleIds, AppDbContext _dbContext, string? userId)
    {
        int outletId = await Helper.GetUserOutlet(_dbContext, userId ?? "");
        return await GetDivisionsForRoles(roleIds, _dbContext, outletId, userId);
    }

    public static async Task<List<int>> GetDivisionsForRoles(string? roleIds, AppDbContext _dbContext, int outletId, string? userId)
    {
        List<int> divisionIds = new();
        if (roleIds != null)
        {
            var rolesToCheck = roleIds.Split(",").Select(int.Parse).ToList();
            var canViewRole = await _dbContext.UserRoleOutlet.Where(x => x.UserId == userId && x.OutletId == outletId && rolesToCheck.Contains(x.RoleId)).FirstOrDefaultAsync();
            if (canViewRole == null)
            {
                throw new Exception("You do not have access to view role");
            }
            divisionIds = await _dbContext.RoleDivision.Where(x => rolesToCheck.Contains(x.RoleId)).Select(x => x.DivisionId).ToListAsync();
        }
        // Get Role From User
        else
        {
            var roles = await _dbContext.UserRoleOutlet
                .Where(x => x.OutletId == outletId)
                .Select(x => x.RoleId)
                .ToListAsync();

            divisionIds = await _dbContext.RoleDivision
                .Where(x => roles.Contains(x.RoleId))
                .Select(x => x.DivisionId)
                .Distinct()
                .ToListAsync();
        }
        return divisionIds;
    }
}