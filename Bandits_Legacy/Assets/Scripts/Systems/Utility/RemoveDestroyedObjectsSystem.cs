using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using Entitas.VisualDebugging.Unity;

namespace Systems.Utility
{
    public class RemoveDestroyedObjectsSystem : ReactiveSystem<GameEntity>
    {
        public RemoveDestroyedObjectsSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.MarkedToDestroy);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isMarkedToDestroy &&
                   entity.hasSceneView;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var go = entity.sceneView.Value;
                go.Unlink();
                go.DestroyGameObject();
                entity.Destroy();
            }
        }
    }
}