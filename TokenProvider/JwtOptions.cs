namespace TokenProvider;

public class JwtOptions()
{
    public required string SecretKey { get; init; }
    public required double RefreshTokenExpiredTimePerDays { get; init; }
    public required double AccessRefreshTokenExpiredTimePerMinutes { get; init; }
    public required string CookieName { get; init; }
}