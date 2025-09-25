using System;
using System.Collections.Generic;
using Data;
using Entitas;

namespace Systems.SaveLoad
{
    public class SaveSystem : ReactiveSystem<SaveLoadEntity>
    {
        private readonly IGroup<GameEntity> _playerGroup;
        private readonly IGroup<GameEntity> _enemies;
        
        public SaveSystem(Contexts contexts) : base(contexts.saveLoad)
        {
            _playerGroup = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.SceneView));
            _enemies = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Enemy, GameMatcher.SceneView));
        }

        protected override ICollector<SaveLoadEntity> GetTrigger(IContext<SaveLoadEntity> context)
        {
            return context.CreateCollector(SaveLoadMatcher.SaveRequested);
        }

        protected override bool Filter(SaveLoadEntity entity)
        {
            return entity.isSaveRequested;
        }

        protected override void Execute(List<SaveLoadEntity> entities)
        {
            var player = _playerGroup.GetSingleEntity();
            
            foreach (var entity in entities)
            {
                var playerSaveData = CreatePlayerSaveData(player);
                var saveData = CreateEnemiesSaveData(_enemies);
                saveData.Add(playerSaveData);
            }
            
            
        }

        private UnitSaveData CreatePlayerSaveData(GameEntity _player)
        {
            var positionData = new PositionData
            {
                X = _player.position.Value.x,
                Y = _player.position.Value.y
            };
            return new UnitSaveData()
            {
                IsPlayer = true,
                HitPoints = _player.hitPoints.Value,
                Position = positionData
            };
        }

        private List<UnitSaveData> CreateEnemiesSaveData(IGroup<GameEntity> _enemies)
        {
            List<UnitSaveData> saveData = new();
            foreach (var enemy in _enemies)
            {
                var positionData = new PositionData
                {
                    X = enemy.position.Value.x,
                    Y = enemy.position.Value.y
                };
                var data =  new UnitSaveData()
                {
                    IsPlayer = false,
                    HitPoints = enemy.hitPoints.Value,
                    Position = positionData
                };
                saveData.Add(data);
            }

            return saveData;
        }
    }
}