using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs
{
    [CreateAssetMenu(menuName = "Configs/MovementSystemConfig", fileName = "MovementSystemConfig")]
    public class MovementSystemConfig : ScriptableObject
    {
        [SerializeField] private List<MovementSystem> _configs;

        public float GetSpeed(MovementSystemType movementSystemType)
            => _configs.First(config => config.Type == movementSystemType).Speed;

        public float GetRotationSpeed(MovementSystemType movementSystemType)
            => _configs.First(config => config.Type == movementSystemType).RotationSpeed;

        [Serializable]
        private class MovementSystem
        {
            [field: SerializeField] public MovementSystemType Type { get; private set; }
            [field: SerializeField] public float Speed { get; private set; }
            [field: SerializeField] public float RotationSpeed { get; private set; }
        }
    }
}
