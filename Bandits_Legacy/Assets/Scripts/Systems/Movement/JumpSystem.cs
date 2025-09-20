using Entitas;
using UnityEngine;

namespace Systems.Movement
{
    public sealed class JumpSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _canJumpEntities;

        public JumpSystem(Contexts contexts)
        {
            var matches = new[]
            {
                GameMatcher.Movable,
                GameMatcher.AbleToMove,
                GameMatcher.AbleToJump,
                GameMatcher.JumpForce,
                GameMatcher.SceneView,
                GameMatcher.RigidBody2D,
                GameMatcher.Grounded
            };
            _canJumpEntities = contexts.game.GetGroup(GameMatcher.AllOf(matches).NoneOf(GameMatcher.Dead));
        }

        public void Execute()
        {
            foreach (var jumping in _canJumpEntities.GetEntities())
            {
                if (!jumping.isJumpRequested) 
                    continue;
                if (!jumping.isAbleToMove || !jumping.isAbleToJump || !jumping.hasRigidBody2D || !jumping.grounded.Value) 
                    continue;
                var rb = jumping.rigidBody2D.Value;
                var rbLinearVelocity = rb.linearVelocity;
                var newRbLinearVelocity =
                    new Vector2(rbLinearVelocity.x, jumping.jumpForce.Value);
                rb.linearVelocity = newRbLinearVelocity;
                jumping.ReplaceGrounded(false);
                jumping.isJumpRequested = false;
            }
        }
    }
}