using System;
using Entitas.Unity;
using UnityEngine;

namespace Core
{
    public class TriggerCollisionComponent : MonoBehaviour
    {
        private GameEntity _gameEntity;

        public void SetGameEntity(GameEntity entity)
        {
            _gameEntity = entity;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponent(out EntityLink linked))
                return;
            if (linked.entity is GameEntity { isPlayer: true })
            {
                _gameEntity.isCollectibleTriggerEntered = true;
            }
        }
    }
}