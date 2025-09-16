using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using Reflex.Attributes;
using UnityEngine;

namespace Systems
{
    public sealed class PlayerSpawnSystem : ReactiveSystem<EventsEntity>
    {
        [Inject]
        private Transform _worldTransform;

        private readonly Contexts _contexts;
        
        public PlayerSpawnSystem(Contexts contexts) : base(contexts.events)
        {
            _contexts = contexts;
        }

        protected override ICollector<EventsEntity> GetTrigger(IContext<EventsEntity> context)
        {
            var matches = new[]
            {
                EventsMatcher.PlayerSpawnRequested
            };
            return context.CreateCollector(EventsMatcher.AllOf((matches)));
        }

        protected override bool Filter(EventsEntity entity)
        {
            return entity.isPlayerSpawnRequested &&
                   entity.hasPosition &&
                   entity.hasPlayerPrefab;
        }

        protected override void Execute(List<EventsEntity> entities)
        {
            foreach (var entity in entities)
            {
                var prefab = entity.playerPrefab.Value;
                var gameEntity = _contexts.game.CreateEntity();
                gameEntity.AddPosition(entity.position.Value);
                gameEntity.isPlayer = true;

                var gameObject = Object.Instantiate(prefab.gameObject, gameEntity.position.Value, Quaternion.identity);
                gameObject.transform.SetParent(_worldTransform);
                gameObject.Link(gameEntity);
                gameEntity.AddSceneView(gameObject);
                entity.Destroy();
            }   
        }
    }
}