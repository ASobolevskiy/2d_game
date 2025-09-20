using Entitas;
using UnityEngine;

namespace Systems.Combat
{
    public sealed class AttackDelaySystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _delayers;

        public AttackDelaySystem(Contexts contexts)
        {
            var matches = new[]
            {
                GameMatcher.AttackDelaying,
                GameMatcher.Weapon,
                GameMatcher.AttackDelayTimer
            };
            _delayers = contexts.game.GetGroup(GameMatcher.AllOf(matches));
        }

        public void Execute()
        {
            foreach (var delayer in _delayers.GetEntities())
            {
                if (!delayer.hasAttackDelayTimer || !delayer.isAttackDelaying)
                    continue;
                var currentDelay = delayer.attackDelayTimer.Value;
                currentDelay -= Time.deltaTime;
                if (currentDelay <= 0)
                {
                    delayer.RemoveAttackDelayTimer();
                    delayer.isAttackDelaying = false;
                }
                else
                {
                    delayer.ReplaceAttackDelayTimer(currentDelay);
                }
            }
        }
    }
}