using Assets._Project.Develop.Runtime.Configs.Entities;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Stages
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Stages/NewEnemiesStageConfig", fileName = "EnemiesStageConfig")]
    public class EnemiesStageConfig : StageConfig
    {
        [SerializeField] private List<EnemyConfig> _enemyItems;

        public IReadOnlyList<EnemyConfig> EnemyItems => _enemyItems;
    }
}
