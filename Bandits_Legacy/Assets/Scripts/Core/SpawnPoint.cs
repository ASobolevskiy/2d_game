using System;
using UnityEngine;

namespace Core
{
    [Serializable]
    public sealed class SpawnPoint
    {
        [SerializeField]
        private Transform _coordinates;

        [SerializeField]
        private bool _isPlayerSpawnPoint;

        public Transform Coordinates => _coordinates;

        public bool IsPlayerSpawnPoint => _isPlayerSpawnPoint;
    }
}