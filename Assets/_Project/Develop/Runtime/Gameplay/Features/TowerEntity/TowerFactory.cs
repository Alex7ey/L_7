using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagment;
using UnityEngine;


namespace Assets._Project.Develop.Runtime.Gameplay.Features.TowerEntity
{
    public class TowerFactory
    {
        private readonly EntitiesFactory _entitiesFactory;
        private readonly EntitiesLifeContext _lifeContext;
        private readonly TowerConfig _towerConfig;

        public TowerFactory(DIContainer container)
        {
            _entitiesFactory = container.Resolve<EntitiesFactory>();
            _lifeContext = container.Resolve<EntitiesLifeContext>();
            _towerConfig = container.Resolve<ConfigsProviderService>().GetConfig<TowerConfig>();
        }

        public Entity Create()
        {
            Entity entity = _entitiesFactory.CreateTower(_towerConfig);

            _lifeContext.Add(entity);

            return null;
        }
    }
}
