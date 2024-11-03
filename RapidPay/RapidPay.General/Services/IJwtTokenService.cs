namespace RapidPay.General.Services
{
    public interface IJwtTokenService
    {
        string GenerateToken(string userId);
    }
}
