using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Systems.Visual.Animation
{
    public sealed class DeathAnimationSystem : ReactiveSystem<GameEntity>
    {
        private static readonly int s_dead = Animator.StringToHash("Death");

        public DeathAnimationSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            var matches = new[]
            {
                GameMatcher.Dead
            };
            return context.CreateCollector(GameMatcher.AllOf(matches));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isDead &&
                   entity.hasAnimator;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.animator.Value.SetTrigger(s_dead);
            }
        }
    }
}