using Assets._Project.Develop.Runtime.Configs.Entities;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.ExplosionFeature
{
    public class ProjectileEntityFactory
    {
        private readonly EntitiesFactory _entitiesFactory;
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private readonly BrainsFactory _brainsFactory;
        private readonly AIBrainsContext _brainsContext;

        private readonly ProjectileConfig _projectileConfig;

        public ProjectileEntityFactory(DIContainer container)
        {
            _entitiesFactory = container.Resolve<EntitiesFactory>();
            _entitiesLifeContext = container.Resolve<EntitiesLifeContext>();
            _projectileConfig = container.Resolve<ConfigsProviderService>().GetConfig<ProjectileConfig>();
            _brainsFactory = container.Resolve<BrainsFactory>();
            _brainsContext = container.Resolve<AIBrainsContext>();
        }

        public Entity CreateExplosionEntity(Vector3 spawnPosition)
        {
            Entity entity = _entitiesFactory.CreateExplosion(spawnPosition, _projectileConfig);

            _entitiesLifeContext.Add(entity);

            return entity;
        }

        public Entity CreateMineEntity(Vector3 spawnPosition)
        {
            Entity entity = _entitiesFactory.CreateMine(spawnPosition, _projectileConfig);

            StateMachineBrain stateMachineBrain = _brainsFactory.CreateMineEntity(entity);


            _brainsContext.SetFor(entity, stateMachineBrain);
            _entitiesLifeContext.Add(entity);
            return entity;
        }
    }
}
