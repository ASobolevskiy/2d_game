using UniRx;

namespace UI.Presenters
{
    public interface IMoneyPresenter
    {
        public IReadOnlyReactiveProperty<string> MoneyText { get; }
    }
}