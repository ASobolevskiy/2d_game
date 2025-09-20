using Entitas;
using UnityEngine;
using Utils;

namespace Systems.Movement
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
                GameMatcher.FaceDirection,
                GameMatcher.RigidBody2D
            };

            _movables = contexts.game.GetGroup(GameMatcher.AllOf(matches).NoneOf(GameMatcher.Dead));
        }
        
        public void Execute()
        {
            foreach (var movable in _movables.GetEntities())
            {
                if (!movable.isAbleToMove || !movable.hasSceneView || !movable.hasRigidBody2D)
                    continue;

                var rb = movable.rigidBody2D.Value;
                var rbLinearVelocity = rb.linearVelocity;
                var newRbLinearVelocity =
                    new Vector2(movable.direction.Value.x * movable.speed.Value, rbLinearVelocity.y);
                rb.linearVelocity = newRbLinearVelocity;
                switch (movable.direction.Value.x)
                {
                    case > 0:
                        movable.ReplaceFaceDirection(FaceDirectionEnum.Right);
                        movable.ReplaceIsMoving(true);
                        break;
                    case < 0:
                        movable.ReplaceFaceDirection(FaceDirectionEnum.Left);
                        movable.ReplaceIsMoving(true);
                        break;
                    default:
                        movable.ReplaceIsMoving(false);
                        break;
                }
                movable.ReplacePosition(movable.sceneView.Value.transform.position);
            }
        }
    }
}