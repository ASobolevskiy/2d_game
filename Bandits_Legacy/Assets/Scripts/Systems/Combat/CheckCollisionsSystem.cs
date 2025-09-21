using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using NUnit.Framework;
using UnityEngine;

namespace Systems.Combat
{
    public sealed class CheckCollisionsSystem : ReactiveSystem<GameEntity>
    {
        public CheckCollisionsSystem(Contexts contexts) : base(contexts.game)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.AttackCollisionCheck);
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasAttacking &&
                   entity.hasWeapon;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var weapon = entity.weapon;
                var attackPoint = weapon.AttackPoint;
                var colliders = new List<Collider2D>();
                Physics2D.OverlapCircle(attackPoint.position, 1, new ContactFilter2D(), colliders);
                if (colliders.Count != 0)
                {
                    foreach (var collider in colliders)
                    {
                        if (collider.gameObject == entity.sceneView.Value)
                            continue;
                        if (!collider.gameObject.TryGetComponent(out EntityLink linked)) 
                            continue;
                        if (linked.entity is GameEntity { isDead: false } collidedEntity)
                        {
                            collidedEntity.ReplaceTakeDamage(weapon.Damage);
                        }
                    }
                }
                entity.isAttackCollisionCheck = false;
            }
        }
    }
}