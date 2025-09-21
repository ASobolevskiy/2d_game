using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Systems.Input
{
    public sealed class ReadMoveInputSystem : ReactiveSystem<InputEntity>
    {
        private readonly IGroup<GameEntity> _playerGroup;
        
        public ReadMoveInputSystem(Contexts contexts) : base(contexts.input)
        {
            var matches = new[]
            {
                GameMatcher.Player,
                GameMatcher.SceneView,
                GameMatcher.Movable,
                GameMatcher.Direction
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
                InputMatcher.MoveInput
            };
            return context.CreateCollector(InputMatcher.AllOf((matches)));
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.hasMoveInput;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (var inputEntity in entities)
            {
                var inputDirection = inputEntity.moveInput.Value;
                foreach (var entity in _playerGroup)
                {
                    var directionToMove = new Vector2(inputDirection.x, entity.direction.Value.y);
                    entity.ReplaceDirection(directionToMove);
                }
            }
        }
    }
}