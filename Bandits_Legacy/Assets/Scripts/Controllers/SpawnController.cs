using System.Collections.Generic;
using Core;
using Reflex.Core;
using UnityEngine;

namespace Controllers
{
    public class SpawnController
    {
        private readonly Container _sceneContainer;
        private readonly List<SpawnPoint> _spawnPoints;
        private readonly List<Enemy> _enemyPrefabs;
        private readonly EventsContext _context;
        
        public SpawnController(Container sceneContainer)
        {
            _sceneContainer = sceneContainer;
            _spawnPoints = sceneContainer.Resolve<List<SpawnPoint>>();
            _enemyPrefabs = sceneContainer.Resolve<List<Enemy>>();
            _context = Contexts.sharedInstance.events;
        }

        public void SpawnUnits()
        {
            if (_spawnPoints == null) 
                return;
            foreach (var spawnPoint in _spawnPoints)
            {
                var position = spawnPoint.Coordinates.position;
                var eventEntity = _context.CreateEntity();
                eventEntity.AddPosition(position);
                    
                if (spawnPoint.IsPlayerSpawnPoint)
                {
                    //Spawn player
                    eventEntity.isPlayerSpawnRequested = true;
                    eventEntity.AddPlayerPrefab(_sceneContainer.Resolve<Player>());
                }
                else
                {
                    //Spawn enemies
                    eventEntity.isEnemySpawnRequested = true;
                    eventEntity.AddEnemyPrefab(GetRandomEnemyPrefab(_enemyPrefabs));
                }
            }
        }
        
        private Enemy GetRandomEnemyPrefab(List<Enemy> prefabs)
        {
            var index = Random.Range(0, prefabs.Count);
            return prefabs[index];
        }
    }
}