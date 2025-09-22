using Entitas;
using UnityEngine;

namespace Systems.AI
{
    public sealed class AIChaseSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _chasingAgents;

        public AIChaseSystem(Contexts contexts)
        {
            _chasingAgents = contexts.game.GetGroup(GameMatcher
                .AllOf(GameMatcher.AIAgent, GameMatcher.ChaseState, GameMatcher.SceneView)
                .NoneOf(GameMatcher.Dead));
        }

        public void Execute()
        {
            foreach ( var entity in _chasingAgents.GetEntities())
            {
                var targetPosition = entity.chaseState.Target.position.Value;
                var currentPosition = entity.position.Value;
                var direction = (targetPosition - currentPosition);
                entity.ReplaceDirection(new Vector2(direction.normalized.x, 0));
                var attackRange = entity.weapon.Range;
                if (!(direction.sqrMagnitude <= attackRange)) 
                    continue;
                if (entity.isAttackDelaying) 
                    continue;
                entity.isSearchingForEnemy = false;
                entity.RemoveChaseState();
                entity.isAttackState = true;
            }
        }
    }
}