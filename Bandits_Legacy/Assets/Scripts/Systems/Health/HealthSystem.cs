using System.Collections.Generic;
using Entitas;

namespace Systems.Health
{
    public sealed class HealthSystem : ReactiveSystem<GameEntity>
    {
        public HealthSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            var matches = new[]
            {
                GameMatcher.HitPoints
            };
            return context.CreateCollector(GameMatcher.AllOf(matches));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasHitPoints &&
                   !entity.isDead;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.hitPoints.Value <= 0)
                {
                    entity.isDead = true;
                }
            }
        }
    }
}