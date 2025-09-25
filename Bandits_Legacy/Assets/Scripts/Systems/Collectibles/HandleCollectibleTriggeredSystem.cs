using System.Collections.Generic;
using Entitas;
using Utils;

namespace Systems.Collectibles
{
    public sealed class HandleCollectibleTriggeredSystem : ReactiveSystem<GameEntity>
    {
        private readonly EventsContext _eventsContext;
        
        public HandleCollectibleTriggeredSystem(Contexts contexts) : base(contexts.game)
        {
            _eventsContext = contexts.events;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.CollectibleTriggerEntered.Added());
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.isCollectible;
        }

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                var eventsEntity = _eventsContext.CreateEntity();
                eventsEntity.isHealthCollectibleTriggered = entity.collectibleType.Value == CollectibleTypeEnum.Health;
                eventsEntity.isMoneyCollectibleTriggered = entity.collectibleType.Value == CollectibleTypeEnum.Money;
                eventsEntity.AddCollectibleValue(entity.collectibleValue.Amount);
                entity.isCollectibleTriggerEntered = false;
                entity.isMarkedToDestroy = true;
            }
        }
    }
}