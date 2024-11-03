namespace RapidPay.General.Services
{
    public sealed class UFEService
    {
        private static readonly Lazy<UFEService> _instance = new Lazy<UFEService>(() => new UFEService(1.0M));
        private decimal _fee;

        private UFEService(decimal initialFee)
        {
            _fee = initialFee;
        }

        public static UFEService Instance => _instance.Value;

        public decimal GetFee()
        {
            _fee *= (decimal)Random.Shared.NextDouble() * 2;
            return _fee;
        }
    }
}
