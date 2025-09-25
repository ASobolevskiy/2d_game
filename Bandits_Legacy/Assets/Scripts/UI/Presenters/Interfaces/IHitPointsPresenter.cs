using UniRx;

namespace UI.Presenters
{
    public interface IHitPointsPresenter
    {
        public IReadOnlyReactiveProperty<string> HitPointsText { get; }

        public void ChangeHitPointsText(string text);
    }
}