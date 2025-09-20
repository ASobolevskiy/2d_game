using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Systems.Visual.Animation
{
    public sealed class AttackAnimationSystem : ReactiveSystem<GameEntity>
    {
        private static readonly int s_attack = Animator.StringToHash("Attack");

        public AttackAnimationSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            var matches = new[]
            {
                GameMatcher.Animator,
                GameMatcher.AttackRequested
            };
            return context.CreateCollector(GameMatcher.AllOf(matches));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasAnimator &&
                   entity.isAttackRequested &&
                   !entity.isAttackDelaying &&
                   !entity.isDead;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.animator.Value.SetTrigger(s_attack);
            }
        }
    }
}