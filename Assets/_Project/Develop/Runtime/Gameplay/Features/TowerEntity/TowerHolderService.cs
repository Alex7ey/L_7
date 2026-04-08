using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.TowerEntity;
using Assets._Project.Develop.Runtime.UI;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.MainHero
{
    public class TowerHolderService : IInitializable, IDisposable
    {
        private EntitiesLifeContext _entitiesLifeContext;
        private ReactiveEvent<Entity> _heroRegistred = new();

        private Entity _tower;

        public TowerHolderService(EntitiesLifeContext entitiesLifeContext)
        {
            _entitiesLifeContext = entitiesLifeContext;
        }

        public IReadOnlyEvent<Entity> HeroRegistred => _heroRegistred;

        public Entity Tower => _tower;

        public void Initialize()
        {
            _entitiesLifeContext.Added += OnEntityAdded;
        }

        private void OnEntityAdded(Entity entity)
        {
            if (entity.HasComponent<IsTower>())
            {
                _entitiesLifeContext.Added -= OnEntityAdded;
                _tower = entity;
                _heroRegistred?.Invoke(_tower);
            }
        }

        public void Dispose()
        {
            _entitiesLifeContext.Added -= OnEntityAdded;
        }
    }
}
