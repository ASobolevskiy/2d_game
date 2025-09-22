using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Systems.AI
{
    public sealed class AIPeaceSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _calmedAgents;
        public AIPeaceSystem(Contexts contexts)
        {
            _calmedAgents = contexts.game.GetGroup(GameMatcher
                .AllOf(GameMatcher.AIAgent, GameMatcher.SceneView)
                .NoneOf(GameMatcher.Dead, GameMatcher.SearchingForEnemy));
        }

        public void Execute()
        {
            foreach (var entity in _calmedAgents.GetEntities())
            {
                var currentTimer = entity.currentPeaceTimer.Value;
                currentTimer -= Time.deltaTime;
                entity.ReplaceCurrentPeaceTimer(currentTimer);
                if (currentTimer <= 0)
                {
                    entity.isSearchingForEnemy = true;
                }
                else
                {
                    entity.isAttackState = false;
                }
            }
        }
    }
}