using System;
using Reflex.Attributes;
using TMPro;
using UI.Presenters;
using UniRx;
using UnityEngine;

namespace UI.Views
{
    public class MoneyView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _moneyText;

        private CompositeDisposable _subscriptions;
        
        [Inject]
        public void Construct(IMoneyPresenter presenter)
        {
            _subscriptions = new CompositeDisposable();
            presenter.MoneyText.Subscribe(OnMoneyTextChanged).AddTo(_subscriptions);
        }

        private void OnMoneyTextChanged(string text)
        {
            _moneyText.text = text;
        }

        private void OnDestroy()
        {
            _subscriptions.Dispose();
        }
    }
}