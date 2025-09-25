using System;
using System.Collections.Generic;
using Core;
using Entitas;
using Reflex.Core;
using UnityEngine;

namespace Systems.Collectibles
{
    public sealed class MoneyCollectiblePickupSystem : ReactiveSystem<EventsEntity>
    {
        private readonly MoneyStorage _moneyStorage;
        
        public MoneyCollectiblePickupSystem(Contexts contexts, Container sceneContainer) : base(contexts.events)
        {
            _moneyStorage = sceneContainer.Resolve<MoneyStorage>();
        }

        protected override ICollector<EventsEntity> GetTrigger(IContext<EventsEntity> context)
        {
            return context.CreateCollector(EventsMatcher.MoneyCollectibleTriggered.Added());
        }

        protected override bool Filter(EventsEntity entity)
        {
            return entity.hasCollectibleValue;
        }

        protected override void Execute(List<EventsEntity> entities)
        {
            foreach (var entity in entities)
            {
                _moneyStorage.AddMoney(entity.collectibleValue.Amount);
                entity.Destroy();
            }
        }
    }
}