using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Systems.Movement
{
    public sealed class UnableToMoveSystem : ReactiveSystem<GameEntity>
    {
        public UnableToMoveSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AbleToMove.Removed());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isMovable &&
                   entity.hasSpeed &&
                   entity.hasSceneView &&
                   entity.hasRigidBody2D;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.rigidBody2D.Value.linearVelocity = Vector2.zero;
                entity.ReplaceDirection(Vector2.zero);
                entity.ReplaceIsMoving(false);
            }
        }
    }
}