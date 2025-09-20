using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Systems.Visual.Animation
{
    public sealed class JumpAnimationSystem : ReactiveSystem<GameEntity>
    {
        private static readonly int s_grounded = Animator.StringToHash("Grounded");
        private static readonly int s_jump = Animator.StringToHash("Jump");

        public JumpAnimationSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            var matches = new[]
            {
                GameMatcher.Grounded
            };
            return context.CreateCollector(GameMatcher.AllOf(matches));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasGrounded &&
                   entity.hasAnimator &&
                   !entity.isDead;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var jumping in entities)
            {
                var animator = jumping.animator.Value;
                animator.SetBool(s_grounded, jumping.grounded.Value);
            }
        }
    }
}