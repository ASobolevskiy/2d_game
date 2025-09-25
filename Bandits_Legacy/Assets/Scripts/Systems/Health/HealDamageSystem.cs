using System.Collections.Generic;
using Entitas;

namespace Systems.Health
{
    public sealed class HealDamageSystem : ReactiveSystem<GameEntity>
    {
        public HealDamageSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            var matches = new[]
            {
                GameMatcher.HealDamage,
                GameMatcher.HitPoints
            };
            return context.CreateCollector(GameMatcher.AllOf(matches).NoneOf(GameMatcher.Dead));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasHitPoints &&
                   entity.hasHealDamage;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var maxHp = entity.maxHitpoints.Value;
                var currentHp = entity.hitPoints.Value;
                var lostHp = maxHp - currentHp;
                var healedDamage = entity.healDamage.Amount;
                if (healedDamage > lostHp)
                    healedDamage = lostHp;
                entity.ReplaceHitPoints(currentHp + healedDamage);
                entity.RemoveHealDamage();
            }
        }
    }
}