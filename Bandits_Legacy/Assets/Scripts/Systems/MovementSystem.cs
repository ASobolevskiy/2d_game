using Entitas;
using UnityEngine;
using Utils;

namespace Systems
{
    public sealed class MovementSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _movables;

        public MovementSystem(Contexts contexts)
        {
            var matches = new[]
            {
                GameMatcher.Movable,
                GameMatcher.AbleToMove,
                GameMatcher.Speed,
                GameMatcher.Direction,
                GameMatcher.SceneView,
                GameMatcher.FaceDirection
            };

            _movables = contexts.game.GetGroup(GameMatcher.AllOf(matches));
        }
        
        public void Execute()
        {
            foreach (var movable in _movables)
            {
                if (!movable.isAbleToMove || !movable.hasSceneView)
                    continue;
                if (!movable.sceneView.Value.TryGetComponent(out Rigidbody2D rb)) 
                    continue;
                var rbLinearVelocity = rb.linearVelocity;
                var newRbLinearVelocity =
                    new Vector2(movable.direction.Value.x * movable.speed.Value, rbLinearVelocity.y);
                rb.linearVelocity = newRbLinearVelocity;
                switch (rbLinearVelocity.x)
                {
                    case > 0:
                        movable.ReplaceFaceDirection(FaceDirectionEnum.Right);
                        break;
                    case < 0:
                        movable.ReplaceFaceDirection(FaceDirectionEnum.Left);
                        break;
                }
                movable.ReplacePosition(movable.sceneView.Value.transform.position);
            }
        }
    }
}