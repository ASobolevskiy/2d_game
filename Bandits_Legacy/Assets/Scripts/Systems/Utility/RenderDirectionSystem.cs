using System.Collections.Generic;
using System.Linq.Expressions;
using Entitas;
using UnityEngine;
using Utils;

namespace Systems.Utility
{
    public sealed class RenderDirectionSystem : ReactiveSystem<GameEntity>
    {
        public RenderDirectionSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            var matches = new[]
            {
                GameMatcher.FaceDirection
            };
            return context.CreateCollector(GameMatcher.AllOf(matches));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasFaceDirection &&
                   entity.hasSceneView;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.sceneView.Value.transform.localScale = entity.faceDirection.Value == FaceDirectionEnum.Left
                    ? new Vector3(1.0f, 1.0f, 1.0f)
                    : new Vector3(-1.0f, 1.0f, 1.0f);
            }
        }
    }
}