using UI.Presenters;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class MenuPopup : MonoBehaviour
    {
        [SerializeField]
        private Button _saveButton;

        [SerializeField]
        private Button _quitButton;

        private MenuPresenter _presenter;
        private CompositeDisposable _subscriptions;

        public void Show(MenuPresenter presenter)
        {
            _subscriptions = new CompositeDisposable();
            _presenter = presenter;
            gameObject.SetActive(true);
            _presenter.SaveCommand.BindTo(_saveButton).AddTo(_subscriptions);
            _presenter.QuitCommand.BindTo(_quitButton).AddTo(_subscriptions);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            _subscriptions.Dispose();
        }
    }
}