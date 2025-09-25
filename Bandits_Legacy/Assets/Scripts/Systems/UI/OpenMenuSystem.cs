using System;
using System.Collections.Generic;
using Controllers;
using Entitas;
using Reflex.Core;

namespace Systems.UI
{
    public class OpenMenuSystem : ReactiveSystem<UIEntity>
    {
        private readonly PopupController _popupController;
        private bool _menuShown;
        private readonly GameCycleContext _gcContext;
        
        public OpenMenuSystem(Contexts contexts, Container sceneContainer) : base(contexts.uI)
        {
            _popupController = sceneContainer.Resolve<PopupController>();
            _gcContext = contexts.gameCycle;
        }

        protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
        {
            return context.CreateCollector(UIMatcher.MenuOpenRequested);
        }

        protected override bool Filter(UIEntity entity)
        {
            return entity.isMenuOpenRequested;
        }

        protected override void Execute(List<UIEntity> entities)
        {
            foreach (var entity in entities)
            {
                if(!_menuShown)
                {
                    _popupController.ShowMenu();
                    _gcContext.ReplacePause(true);
                    _menuShown = true;
                }
                else
                {
                    _popupController.HideMenu();
                    _gcContext.ReplacePause(false);
                    _menuShown = false;
                }

                entity.isMenuOpenRequested = false;
            }
        }
    }
}