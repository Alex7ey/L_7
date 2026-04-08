using Assets._Project.Develop.Runtime.Configs.Entities;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.ExplosionFeature
{
    public class ProjectileEntityFactory
    {
        private readonly EntitiesFactory _entitiesFactory;
        private readonly EntitiesLifeContext _entitiesLifeContext;

        private readonly ProjectileConfig _enemyConfig;

        public ProjectileEntityFactory(DIContainer container)
        {
            _entitiesFactory = container.Resolve<EntitiesFactory>();
            _entitiesLifeContext = container.Resolve<EntitiesLifeContext>();
            _enemyConfig = container.Resolve<ConfigsProviderService>().GetConfig<ProjectileConfig>();
        }

        public Entity CreateExplosion(Vector3 spawnPosition)
        {
            Entity entity = _entitiesFactory.CreateExplosion(spawnPosition, _enemyConfig);
            
            _entitiesLifeContext.Add(entity);

            return entity;
        }
    }
}
