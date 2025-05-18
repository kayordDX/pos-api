using System.Text.Json;
using FirebaseAdmin.Auth;
using Kayord.Pos.Data;
using Microsoft.EntityFrameworkCore;

namespace Kayord.Pos.Services;

public class UserService
{
    private readonly AppDbContext _dbContext;
    private readonly CurrentUserService _cu;
    private readonly HttpClient _httpClient;

    public UserService(AppDbContext dbContext, CurrentUserService cu, HttpClient httpClient)
    {
        _dbContext = dbContext;
        _cu = cu;
        _httpClient = httpClient;
    }

    public async Task<List<string>> GetUserRoles()
    {
        return await _dbContext.Database
        .SqlQuery<string>(
        $"""
            SELECT 
                DISTINCT rt.name
            FROM user_outlet u
            JOIN user_role_outlet ur
                ON u.outlet_id = ur.outlet_id
                AND u.user_id = ur.user_id
            JOIN role r
                ON r.role_id = ur.role_id
            JOIN role_type rt
                ON rt.id = r.role_type_id
            WHERE u.user_id = {_cu.UserId}
            AND u.is_current = true
        """
        )
        .ToListAsync();
    }

    public async Task<string> GetCustomToken(string userId)
    {
        string customToken = await FirebaseAuth.DefaultInstance.CreateCustomTokenAsync(userId);
        return customToken;
    }

    public async Task<Token> GetIdToken(string userId)
    {
        string customToken = await GetCustomToken(userId);
        string key = "AIzaSyAoHwOFNJ0_ag0Ly4YZzGzdW_n5_NjC2uE";
        string uri = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithCustomToken?key={key}";

        var fireBaseLoginInfo = new FireBaseLoginInfo
        {
            Token = customToken,
            ReturnSecureToken = true
        };
        var requestResult = await _httpClient.PostAsJsonAsync(uri, fireBaseLoginInfo,
            new JsonSerializerOptions()
            { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        if (!requestResult.IsSuccessStatusCode)
        {
            throw new Exception("Failed to get token");
        }

        var token = await requestResult.Content.ReadFromJsonAsync<Token>();
        if (token == null)
        {
            throw new Exception("Failed to get token");
        }

        return token;
    }
}

public class FireBaseLoginInfo
{
    public string Token { get; set; } = string.Empty;
    public bool ReturnSecureToken { get; set; }
}

public class Token
{
    public string Kind { get; set; } = string.Empty;
    public string IdToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public int ExpiresIn { get; set; }
    public bool IsNewUser { get; set; }
}
