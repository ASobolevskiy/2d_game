using Sirenix.OdinInspector;
using UnityEngine;
using Utils;

namespace Core
{
    public sealed class Collectible : MonoBehaviour
    {
        [SerializeField]
        private CollectibleTypeEnum _collectibleType;

        [SerializeField, MinValue(0.1), MaxValue(0.5f)]
        private float _dropRate;

        [SerializeField]
        private int _amount;

        public CollectibleTypeEnum CollectibleType => _collectibleType;

        public float DropRate => _dropRate;

        public int Amount => _amount;
    }
}