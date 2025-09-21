using System;
using System.Collections.Generic;
using Entitas;

namespace Systems.Input
{
    public sealed class ReadAttackInputSystem : ReactiveSystem<InputEntity>
    {
        private readonly IGroup<GameEntity> _playerGroup;
        
        public ReadAttackInputSystem(Contexts contexts) : base(contexts.input)
        {
            var matches = new[]
            {
                GameMatcher.Player,
                GameMatcher.SceneView,
                GameMatcher.Weapon
            };
            var exclude = new[]
            {
                GameMatcher.Dead
            };
            
            _playerGroup = contexts.game.GetGroup(GameMatcher.AllOf(matches).NoneOf(exclude));
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            var matches = new[]
            {
                InputMatcher.AttackInput
            };
            return context.CreateCollector(InputMatcher.AllOf((matches)));
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.hasAttackInput;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (var attackEntity in entities)
            {
                foreach (var gameEntity in _playerGroup.GetEntities())
                {
                    gameEntity.isAttackRequested = attackEntity.attackInput.IsPressed;
                }
            }
        }
    }
}