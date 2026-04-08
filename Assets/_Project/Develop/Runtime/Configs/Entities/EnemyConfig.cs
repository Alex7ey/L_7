using Assets._Project.Develop.Runtime.Configs.Gameplay.Entities;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Entities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/NewEnemyConfig", fileName = "EnemyConfig")]
    public class EnemyConfig : EntityConfig
    {
        [field: SerializeField] public float MaxHealth { get; private set; }
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
        [field: SerializeField] public float AttackDamage { get; private set; }
        [field: SerializeField] public float RadiusDetected { get; private set; }
    }
}
