using System;
using Reflex.Attributes;
using TMPro;
using UI.Presenters;
using UniRx;
using UnityEngine;

namespace UI.Views
{
    public class HitPointsView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _hitPointsText;

        private CompositeDisposable _subscriptions;

        [Inject]
        public void Construct(IHitPointsPresenter presenter)
        {
            _subscriptions = new CompositeDisposable();
            presenter.HitPointsText.Subscribe(OnHitPointsTextChanged).AddTo(_subscriptions);
        }

        private void OnHitPointsTextChanged(string text)
        {
            _hitPointsText.text = text;
        }

        private void OnDestroy()
        {
            _subscriptions.Dispose();
        }
    }
}