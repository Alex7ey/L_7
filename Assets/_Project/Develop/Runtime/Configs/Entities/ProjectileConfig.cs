using Assets._Project.Develop.Runtime.Configs.Gameplay.Entities;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Entities
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/NewProjectileConfig", fileName = "ProjectileConfig")]
    public class ProjectileConfig : EntityConfig
    {
        [field: SerializeField] public float Damage { get; private set; }
        [field: SerializeField] public float Radius { get; private set; }
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }
    }
}
