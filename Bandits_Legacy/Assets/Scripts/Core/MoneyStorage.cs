using UniRx;

namespace Core
{
    public sealed class MoneyStorage
    {
        public IReadOnlyReactiveProperty<long> Money => _money;
        
        private readonly ReactiveProperty<long> _money = new LongReactiveProperty();

        public void AddMoney(int amount)
        {
            _money.Value += amount;
        }
    }
}