using System;
using System.Collections.Generic;
using Entitas;

namespace Systems.GameCycle
{
    public class PauseUnpauseSystem : ReactiveSystem<GameCycleEntity>
    {
        public Action<bool> OnPauseRequested;
        
        public PauseUnpauseSystem(IContext<GameCycleEntity> context) : base(context)
        {
        }

        protected override ICollector<GameCycleEntity> GetTrigger(IContext<GameCycleEntity> context)
        {
            return context.CreateCollector(GameCycleMatcher.Pause);
        }

        protected override bool Filter(GameCycleEntity entity)
        {
            return entity.hasPause;
        }

        protected override void Execute(List<GameCycleEntity> entities)
        {
            foreach (var entity in entities)
            {
                OnPauseRequested?.Invoke(entity.pause.PauseRequired);
            }
        }
    }
}