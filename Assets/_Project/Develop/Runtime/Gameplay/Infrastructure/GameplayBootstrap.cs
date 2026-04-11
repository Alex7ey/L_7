using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using Assets._Project.Develop.Runtime.Gameplay.Features.TowerEntity;
using Assets._Project.Develop.Runtime.Gameplay.GameStates;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.SceneManagment;
using System;
using System.Collections;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : Bootstrap
    {
        private DIContainer _container;
        private EntitiesLifeContext _entitiesLifeContext;
        private AIBrainsContext _brainsContext;
        private GameplayStatesContext _gameplayStateContext;

        private ProjectileShooter _projectileShooter;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs inputSceneArgs = null)
        {
            if (inputSceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(inputSceneArgs)} is not match with {typeof(GameplayInputArgs)} type");

            GameplayContextRegistration.Process(container, gameplayInputArgs);

            _container = container;
            _container.Initialize();
        }

        public override IEnumerator Initialize()
        {
            _projectileShooter = _container.Resolve<ProjectileShooter>();
            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
            _brainsContext = _container.Resolve<AIBrainsContext>();

            _gameplayStateContext = _container.Resolve<GameplayStatesContext>();

            _container.Resolve<TowerFactory>().Create();

            yield break;
        }

        public override void Run() => _gameplayStateContext.Run();

        private void Update()
        {
            _brainsContext?.Update(Time.deltaTime);
            _entitiesLifeContext?.Update(Time.deltaTime);
            _gameplayStateContext?.Update(Time.deltaTime);

            _projectileShooter?.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _entitiesLifeContext?.FixedUpdate(Time.fixedDeltaTime);
        }
    }
}