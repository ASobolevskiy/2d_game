using UI.Presenters;
using UI.Views;
using UnityEngine;

namespace Controllers
{
    public class PopupController : MonoBehaviour
    {
        [SerializeField]
        private MenuPopup _menuPopup;

        public void ShowMenu()
        {
            _menuPopup.Show(new MenuPresenter());
        }

        public void HideMenu()
        {
            _menuPopup.Hide();
        }
    }
}