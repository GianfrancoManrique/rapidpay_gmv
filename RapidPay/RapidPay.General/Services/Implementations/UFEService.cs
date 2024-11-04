using RapidPay.General.Services.Interfaces;

namespace RapidPay.General.Services.Implementations
{
    public sealed class UFEService : IUFEService
    {
        private decimal _fee;

        public UFEService(decimal initialFee = 1.0M)
        {
            _fee = initialFee;
        }

        public decimal GetFee()
        {
            _fee *= (decimal)Random.Shared.NextDouble() * 2;
            return _fee;
        }
    }
}
