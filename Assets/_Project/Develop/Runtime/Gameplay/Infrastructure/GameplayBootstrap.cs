using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
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
        private TestGameplay _gameplay;
        private EntitiesLifeContext _entitiesLifeContext;
        private AIBrainsContext _brainsContext;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs inputSceneArgs = null)
        {
            if (inputSceneArgs is not GameplayInputArgs gameplayInputArgs && inputSceneArgs != null)
                throw new ArgumentException($"{nameof(inputSceneArgs)} is not match with {typeof(GameplayInputArgs)} type");

            GameplayContextRegistration.Process(container);

            _container = container;
            _container.Initialize();
        }

        public override IEnumerator Initialize()
        {
            _gameplay = _container.Resolve<TestGameplay>();
            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
            _brainsContext = _container.Resolve<AIBrainsContext>();

            yield break;
        }

        public override void Run() => _gameplay.Run();

        private void Update()
        {
            _brainsContext?.Update(Time.deltaTime);
            _entitiesLifeContext?.Update(Time.deltaTime);
            _gameplay?.Update();
        }

        private void FixedUpdate()
        {
            _entitiesLifeContext?.FixedUpdate(Time.fixedDeltaTime);
        }
    }
}