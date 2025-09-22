using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Systems.AI
{
    public sealed class AIReactToEnemySightedSystem : ReactiveSystem<GameEntity>
    {
        public AIReactToEnemySightedSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.EnemySighted.AddedOrRemoved());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isAIAgent &&
                   entity.hasSceneView &&
                   entity.hasIdleStateTurnAroundTimer;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.hasEnemySighted)
                {
                    entity.isIdleState = false;
                    if(entity.isPatrolState)
                    {
                        entity.isPatrolState = false;
                        entity.RemovePatrolData();
                    }
                    entity.ReplaceChaseState(entity.enemySighted.Enemy);
                    entity.ReplaceCurrentTurnAroundTimer(0);
                    entity.isAbleToMove = true;
                    entity.ReplaceSpeed(entity.chasingSpeed.Value);
                }
                else
                {
                    if(entity.hasChaseState)
                        entity.RemoveChaseState();
                    entity.isIdleState = true;
                    entity.isAbleToMove = false;
                    entity.ReplaceCurrentTurnAroundTimer(entity.idleStateTurnAroundTimer.Value);
                }
            }
        }
    }
}