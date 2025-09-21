using System.Collections.Generic;
using Entitas;

namespace Systems.Health
{
    public sealed class TakeDamageSystem : ReactiveSystem<GameEntity>
    {
        public TakeDamageSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            var matches = new[]
            {
                GameMatcher.TakeDamage,
                GameMatcher.HitPoints
            };
            return context.CreateCollector(GameMatcher.AllOf(matches).NoneOf(GameMatcher.Dead));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasHitPoints &&
                   entity.hasTakeDamage;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var currentHp = entity.hitPoints.Value;
                var takenDamage = entity.takeDamage.Amount;
                entity.ReplaceHitPoints(currentHp - takenDamage);
                entity.RemoveTakeDamage();
            }
        }
    }
}