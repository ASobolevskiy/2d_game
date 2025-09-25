using System;
using Core;
using Reflex.Attributes;
using UniRx;

namespace UI.Presenters
{
    public class MoneyPresenter : IMoneyPresenter, IDisposable
    {
        public IReadOnlyReactiveProperty<string> MoneyText => _moneyText;
        
        private readonly ReactiveProperty<string> _moneyText;
        private readonly IDisposable _subscription;
        
        public MoneyPresenter(MoneyStorage moneyStorage)
        {
            _moneyText = new ReactiveProperty<string>();
            _subscription = moneyStorage.Money.Subscribe(OnMoneyChanged);
        }

        private void OnMoneyChanged(long money)
        {
            _moneyText.Value = money.ToString();
        }

        public void Dispose()
        {
            _moneyText?.Dispose();
            _subscription?.Dispose();
        }
    }
}