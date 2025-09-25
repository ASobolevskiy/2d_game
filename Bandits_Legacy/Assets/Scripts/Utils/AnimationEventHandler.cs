using Entitas.Unity;
using Entitas.VisualDebugging.Unity;
using UnityEngine;

namespace Utils
{
    public sealed class AnimationEventHandler : MonoBehaviour
    {
        private GameEntity _gameEntity;

        public void SetGameEntity(GameEntity entity)
        {
            _gameEntity = entity;
        }

        public void ReceiveEvent(string key)
        {
            HandleEvent(key);
        }

        private void HandleEvent(string key)
        {
            switch (key)
            {
                case "AttackHit":
                    if (_gameEntity is { hasAttacking: true })
                        _gameEntity.isAttackCollisionCheck = true;
                    break;
                case "AttackComplete":
                    if (_gameEntity is { hasAttacking: true })
                        _gameEntity.RemoveAttacking();
                    break;
                case "DeathComplete":
                    if (_gameEntity is { isDead: true, isPlayer: false }) 
                        _gameEntity.isMarkedToDestroy = true;
                    break;
                default:
                    return;
            }
        }
    }
}