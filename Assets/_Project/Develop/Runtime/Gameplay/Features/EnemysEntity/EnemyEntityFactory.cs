using Assets._Project.Develop.Runtime.Configs.Entities;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using UnityEngine;


namespace Assets._Project.Develop.Runtime.Gameplay.Features.EnemysEntity
{
    public class EnemyEntityFactory
    {
        private readonly EntitiesFactory _entitiesFactory;
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private readonly EnemyConfig _enemyConfig;
        private readonly BrainsFactory _brainsFactory;
        private readonly AIBrainsContext _brainsContext;

        public EnemyEntityFactory(DIContainer container)
        {
            _entitiesFactory = container.Resolve<EntitiesFactory>();
            _entitiesLifeContext = container.Resolve<EntitiesLifeContext>();
            _enemyConfig = container.Resolve<ConfigsProviderService>().GetConfig<EnemyConfig>();
            _brainsFactory = container.Resolve<BrainsFactory>();
            _brainsContext = container.Resolve<AIBrainsContext>();
        }

        public Entity Create(Vector3 spawnPosition)
        {
            Entity entity = _entitiesFactory.CreateEnemy(spawnPosition, _enemyConfig);
            StateMachineBrain stateMachineBrain = _brainsFactory.CreateEnemyBrain(entity);

            _brainsContext.SetFor(entity, stateMachineBrain);
            _entitiesLifeContext.Add(entity);

            return entity;
        }
    }
}
