using System.Collections.Generic;
using Entitas;
using Reflex.Core;
using Utils.Randomizers;

namespace Systems.Utility
{
    public class CheckForDropSystem : ReactiveSystem<EventsEntity>
    {
        private readonly EventsContext _context;
        private readonly CollectibleDropRandomizer _randomizer;
        
        public CheckForDropSystem(Contexts contexts, Container sceneContainer) : base(contexts.events)
        {
            _context = contexts.events;
            _randomizer = sceneContainer.Resolve<CollectibleDropRandomizer>();
        }

        protected override ICollector<EventsEntity> GetTrigger(IContext<EventsEntity> context)
        {
            return context.CreateCollector(EventsMatcher.DropCheckRequested);
        }

        protected override bool Filter(EventsEntity entity)
        {
            return entity.isDropCheckRequested &&
                   entity.hasPosition;
        }

        protected override void Execute(List<EventsEntity> entities)
        {
            foreach (var entity in entities)
            {
                var collectible = _randomizer.RandomizeDrop();
                if (collectible != null)
                {
                    var spawnRequestEntity = _context.CreateEntity();
                    spawnRequestEntity.AddPosition(entity.position.Value);
                    spawnRequestEntity.AddCollectiblePrefab(collectible);
                    spawnRequestEntity.isCollectibleSpawnRequested = true;
                }
                entity.Destroy();
            }
        }
    }
}