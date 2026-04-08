using Assets._Project.Develop.Runtime.Configs.Gameplay.Entities;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Entities/NewTowerConfig", fileName = "TowerConfig")]
    public class TowerConfig : EntityConfig
    {
        [field: SerializeField] public float MaxHealth { get; private set; }
        [field: SerializeField] public Vector3 SpawnPosition { get; private set; }
    }
}
