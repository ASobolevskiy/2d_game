using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Systems.Visual.Animation
{
    public sealed class MovingAnimationSystem : ReactiveSystem<GameEntity>
    {
        private static readonly int s_animState = Animator.StringToHash("AnimState");

        public MovingAnimationSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            var matches = new[]
            {
                GameMatcher.IsMoving
            };
            return context.CreateCollector(GameMatcher.AllOf(matches));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasAnimator;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var movingEntity in entities)
            {
                movingEntity.animator.Value.SetInteger(s_animState, movingEntity.isMoving.Value ? 1 : 0);
            }
        }
    }
}