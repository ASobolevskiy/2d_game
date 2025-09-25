using UniRx;

namespace UI.Presenters
{
    public class HitPointsPresenter : IHitPointsPresenter
    {
        public IReadOnlyReactiveProperty<string> HitPointsText => _hitPointsText;

        private readonly ReactiveProperty<string> _hitPointsText = new();
        
        public void ChangeHitPointsText(string text)
        {
            _hitPointsText.Value = text;
        }
    }
}