namespace RapidPay.General.Models
{
    public class User
    {
        public string? UserId { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
