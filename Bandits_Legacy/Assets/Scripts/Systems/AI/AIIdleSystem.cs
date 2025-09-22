using Entitas;
using UnityEngine;
using Utils;

namespace Systems.AI
{
    public sealed class AIIdleSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _idleAgents;
        
        public AIIdleSystem(Contexts contexts)
        {
            _idleAgents = contexts.game.GetGroup(GameMatcher
                .AllOf(GameMatcher.AIAgent, GameMatcher.IdleState, GameMatcher.SceneView)
                .NoneOf(GameMatcher.Dead));
        }
        public void Execute()
        {
            foreach (var entity in _idleAgents)
            {
                var currentFaceDirection = entity.faceDirection.Value;
                var currentTurnTimer = entity.currentTurnAroundTimer.Value;
                currentTurnTimer -= Time.deltaTime;
                if (currentTurnTimer <= 0)
                {
                    entity.ReplaceFaceDirection(currentFaceDirection == FaceDirectionEnum.Left
                        ? FaceDirectionEnum.Right
                        : FaceDirectionEnum.Left);
                    entity.ReplaceCurrentTurnAroundTimer(entity.idleStateTurnAroundTimer.Value);
                }
                else
                {
                    entity.ReplaceCurrentTurnAroundTimer(currentTurnTimer);
                }
            }
        }
    }
}