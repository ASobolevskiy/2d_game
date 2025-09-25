using System.Collections.Generic;
using Entitas;
using Reflex.Core;
using UI.Presenters;

namespace Systems.UI
{
    public class PlayerHitPointsUISystem : ReactiveSystem<UIEntity>
    {
        private readonly IHitPointsPresenter _hitPointsPresenter;
        
        public PlayerHitPointsUISystem(Contexts contexts, Container sceneContainer) : base(contexts.uI)
        {
            _hitPointsPresenter = sceneContainer.Resolve<IHitPointsPresenter>();
        }

        protected override ICollector<UIEntity> GetTrigger(IContext<UIEntity> context)
        {
            return context.CreateCollector(UIMatcher.PlayerHitPoints);
        }

        protected override bool Filter(UIEntity entity)
        {
            return entity.hasPlayerHitPoints;
        }

        protected override void Execute(List<UIEntity> entities)
        {
            foreach (var entity in entities)
            {
                _hitPointsPresenter.ChangeHitPointsText(entity.playerHitPoints.Value.ToString());
            }
        }
    }
}