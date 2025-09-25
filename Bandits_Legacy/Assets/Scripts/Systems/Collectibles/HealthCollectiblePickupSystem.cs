using System.Collections.Generic;
using Entitas;

namespace Systems.Collectibles
{
    public sealed class HealthCollectiblePickupSystem : ReactiveSystem<EventsEntity>
    {
        private readonly IGroup<GameEntity> _playerGroup;
        
        public HealthCollectiblePickupSystem(Contexts contexts) : base(contexts.events)
        {
            var matches = new[]
            {
                GameMatcher.Player,
                GameMatcher.SceneView
            };
            _playerGroup = contexts.game.GetGroup(GameMatcher
                .AllOf(matches)
                .NoneOf(GameMatcher.Dead));
        }

        protected override ICollector<EventsEntity> GetTrigger(IContext<EventsEntity> context)
        {
            return context.CreateCollector(EventsMatcher.HealthCollectibleTriggered.Added());
        }

        protected override bool Filter(EventsEntity entity)
        {
            return entity.hasCollectibleValue;
        }

        protected override void Execute(List<EventsEntity> entities)
        {
            var player = _playerGroup.GetSingleEntity();
            foreach (var entity in entities)
            {
                player.ReplaceHealDamage(entity.collectibleValue.Amount);
                entity.Destroy();
            }
        }
    }
}