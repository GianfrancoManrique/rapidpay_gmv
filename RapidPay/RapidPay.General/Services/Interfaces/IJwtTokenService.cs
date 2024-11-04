namespace RapidPay.General.Services.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(string userId);
    }
}
