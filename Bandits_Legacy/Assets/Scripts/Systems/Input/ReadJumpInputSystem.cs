using System.Collections.Generic;
using Entitas;

namespace Systems.Input
{
    public sealed class ReadJumpInputSystem : ReactiveSystem<InputEntity>
    {
        private readonly IGroup<GameEntity> _playerGroup;

        public ReadJumpInputSystem(Contexts contexts) : base(contexts.input)
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
                InputMatcher.JumpInput
            };
            return context.CreateCollector(InputMatcher.AllOf((matches)));
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.hasJumpInput;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (var inputEntity in entities)
            {
                foreach (var entity in _playerGroup)
                {
                    entity.isJumpRequested = inputEntity.jumpInput.IsPressed;
                }
            }
        }
    }
}