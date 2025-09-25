using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Systems.Health
{
    public sealed class HealthSystem : ReactiveSystem<GameEntity>
    {
        private EventsContext _eventsContext;
        
        public HealthSystem(Contexts contexts) : base(contexts.game)
        {
            _eventsContext = contexts.events;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            var matches = new[]
            {
                GameMatcher.HitPoints
            };
            return context.CreateCollector(GameMatcher.AllOf(matches));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.hasHitPoints &&
                   !entity.isDead;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.hitPoints.Value > 0) 
                    continue;
                if (entity.isEnemy)
                {
                    var eventEntity = _eventsContext.CreateEntity();
                    eventEntity.isDropCheckRequested = true;
                    var position = entity.position.Value;
                    var collectibleSpawnPosition = new Vector2(position.x, position.y + 2);
                    eventEntity.AddPosition(collectibleSpawnPosition);
                }
                entity.isDead = true;
            }
        }
    }
}