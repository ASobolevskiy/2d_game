using System;
using UniRx;

namespace UI.Presenters
{
    public class MenuPresenter : IDisposable
    {
        public readonly ReactiveCommand SaveCommand;

        public readonly ReactiveCommand QuitCommand;

        private readonly CompositeDisposable _subscriptions;

        public MenuPresenter()
        {
            _subscriptions = new CompositeDisposable();
            SaveCommand = new ReactiveCommand();
            QuitCommand = new ReactiveCommand();
            SaveCommand.Subscribe(OnSaveCommand).AddTo(_subscriptions);
            QuitCommand.Subscribe(OnQuitCommand).AddTo(_subscriptions);
        }

        private void OnSaveCommand(Unit _)
        {
            
        }

        private void OnQuitCommand(Unit _)
        {
            
        }

        public void Dispose()
        {
            _subscriptions.Dispose();
        }
    }
}