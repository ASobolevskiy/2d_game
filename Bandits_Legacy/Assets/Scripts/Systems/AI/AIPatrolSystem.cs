using Entitas;
using UnityEngine;

namespace Systems.AI
{
    public sealed class AIPatrolSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _patrollingAiAgents;
        
        public AIPatrolSystem(Contexts contexts)
        {
            _patrollingAiAgents = contexts.game.GetGroup(GameMatcher.AllOf(new[]
            {
                GameMatcher.AIAgent,
                GameMatcher.PatrolState,
                GameMatcher.SceneView
            }).NoneOf(GameMatcher.Dead));
        }
        
        public void Execute()
        {
            foreach (var entity in _patrollingAiAgents.GetEntities())
            {
                var patrolPoints = entity.patrolData.PatrolPoints;
                var currentPosition = (Vector2)entity.sceneView.Value.transform.position;
                var desiredPosition = patrolPoints[entity.patrolData.CurrentTargetIndex];
                var direction = (desiredPosition - currentPosition);
                var distance = direction.sqrMagnitude;
                
                if (distance <= 0.5f)
                {
                    entity.ReplacePatrolData(entity.patrolData.CurrentTargetIndex == 0 ? 1 : 0, patrolPoints);
                }
                else
                {
                    entity.ReplaceDirection(direction.normalized);
                }
            }
        }
    }
}