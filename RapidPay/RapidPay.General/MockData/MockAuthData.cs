using RapidPay.General.Models;

namespace RapidPay.General.MockData
{
    public static class MockAuthData
    {
        public static List<User> Users = new List<User>()
        {
            new User() { UserId = "gmanrique", Email = "gmanrique.422@gmail.com", Password = "qazw3579"},
            new User() { UserId = "gfmanriquev", Email = "gfmanriquev@gmail.com", Password = "edcr3579"}
        };
    }
}
