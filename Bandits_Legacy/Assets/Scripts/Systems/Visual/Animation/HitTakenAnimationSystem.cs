using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Systems.Visual.Animation
{
    public sealed class HitTakenAnimationSystem : ReactiveSystem<GameEntity>
    {
        private static readonly int s_hitTaken = Animator.StringToHash("HitTaken");

        public HitTakenAnimationSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.TakeDamage);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasAnimator &&
                   !entity.isDead;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.animator.Value.SetTrigger(s_hitTaken);
            }
        }
    }
}