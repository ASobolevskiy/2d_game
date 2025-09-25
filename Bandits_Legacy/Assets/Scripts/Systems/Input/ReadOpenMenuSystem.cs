using System.Collections.Generic;
using Entitas;
using Reflex.Core;

namespace Systems.Input
{
    public class ReadOpenMenuSystem : ReactiveSystem<InputEntity>
    {
        private readonly UIContext _uiContext;
        
        
        public ReadOpenMenuSystem(Contexts contexts) : base(contexts.input)
        {
            _uiContext = contexts.uI;
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context)
        {
            return context.CreateCollector(InputMatcher.OpenMenu);
        }

        protected override bool Filter(InputEntity entity)
        {
            return entity.hasOpenMenu &&
                   entity.openMenu.IsPressed;
        }

        protected override void Execute(List<InputEntity> entities)
        {
            foreach (var entity in entities)
            {
                _uiContext.isMenuOpenRequested = true;
            }
        }
    }
}