using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Systems.AI
{
    public sealed class AIAttackSystem : ReactiveSystem<GameEntity>
    {
        public AIAttackSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AttackState.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return !entity.isDead &&
                   !entity.isAttackDelaying &&
                   entity.hasWeapon &&
                   entity.isAIAgent;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.ReplaceDirection(Vector2.zero);
                entity.isAttackRequested = true;
                entity.ReplaceCurrentPeaceTimer(entity.peaceTimer.Value);
            }
        }
    }
}