using System;
using System.Collections.Generic;
using Core;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utils.Randomizers
{
    public class CollectibleDropRandomizer
    {
        private readonly List<Collectible> _prefabs;
        
        public CollectibleDropRandomizer(List<Collectible> prefabs)
        {
            _prefabs = prefabs;
        }

        [CanBeNull]
        public Collectible RandomizeDrop()
        {
            var chance = Random.Range(0f, 1f);
            var collectibleType = RandomizeType();
            var collectibles = _prefabs.FindAll(c => c.CollectibleType == collectibleType);
            Collectible collectibleToDrop = null;
            if (collectibles.Count != 1)
            {
                var index = Random.Range(0, collectibles.Count);
                if (chance <= collectibles[index].DropRate)
                    collectibleToDrop = collectibles[index];
            }
            else
            {
                if (chance <= collectibles[0].DropRate)
                    collectibleToDrop = collectibles[0];
            }
            
            return collectibleToDrop;
        }

        private CollectibleTypeEnum RandomizeType()
        {
            var typesCount = Enum.GetNames(typeof(CollectibleTypeEnum)).Length;
            var result = (CollectibleTypeEnum)Random.Range(0, typesCount);
            return result;
        }
    }
}