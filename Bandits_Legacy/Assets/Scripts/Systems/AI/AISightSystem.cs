using Entitas;
using UnityEngine;

namespace Systems.AI
{
    public sealed class AISightSystem : IExecuteSystem
    {
        
        private readonly IGroup<GameEntity> _aiAgents;
        private readonly IGroup<GameEntity> _targets;

        public AISightSystem(Contexts contexts)
        {
            _targets = contexts.game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Player, GameMatcher.SceneView)
                .NoneOf(GameMatcher.Dead));

            _aiAgents = contexts.game.GetGroup(GameMatcher
                .AllOf(GameMatcher.AIAgent, GameMatcher.SceneView, GameMatcher.SearchingForEnemy)
                .NoneOf(GameMatcher.Dead));
        }
        
        public void Execute()
        {
            var target = _targets.GetSingleEntity();
            if (target == null) 
                return;
            foreach (var entity in _aiAgents.GetEntities())
            {
                var sightDistance = entity.sightDistance.Value;
                sightDistance *= sightDistance;
                var position = entity.position.Value;
                var targetPosition = target.position.Value;
                var distance = (targetPosition - position).sqrMagnitude;
                if (distance <= sightDistance)
                {
                    entity.ReplaceEnemySighted(target);
                }
                else
                {
                    if(entity.hasEnemySighted)
                        entity.RemoveEnemySighted();
                }
            }
        }
    }
}