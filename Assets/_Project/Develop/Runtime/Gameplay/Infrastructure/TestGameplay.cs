using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class TestGameplay
    {
        private Entity _entity;
        private bool _isRunning;
        private EntitiesFactory _entitiesFactory;
        private BrainsFactory _brainsFactory;

        public TestGameplay(EntitiesFactory entitiesFactory, BrainsFactory brainsFactory)
        {
            _entitiesFactory = entitiesFactory;
            _brainsFactory = brainsFactory;
        }

        public void Run()
        {
            _isRunning = true;
        }

        public void Update()
        {
            if (_isRunning == false)
                return;

            if (Input.GetKeyDown(KeyCode.Alpha1))
                CreateRandomTeleportingEntityTest();

            if (Input.GetKeyDown(KeyCode.Alpha2))
                CreateToTargetTeleportingEntityTest();

            if (Input.GetKeyDown(KeyCode.Alpha3))
                CreateHeroTest();
        }

        private void CreateRandomTeleportingEntityTest()
        {
            _entity = _entitiesFactory.CreateEntity(Vector3.zero);
            _brainsFactory.CreateRandomTeleportingEntity(_entity);
        }

        private void CreateToTargetTeleportingEntityTest()
        {
            _entity = _entitiesFactory.CreateEntity(Vector3.zero);
            _brainsFactory.CreateToTargetTeleportingEntity(_entity);

            CreateEntityEnemy(new Vector3(0, 0, 5));
            CreateEntityEnemy(new Vector3(0, 0, -4), 80);
        }

        private void CreateHeroTest()
        {
            _entity = _entitiesFactory.CreateHero(Vector3.zero);
            _brainsFactory.CreateMainHeroBrain(_entity);

            CreateEntityEnemy(new Vector3(0, 0, 5));
            CreateEntityEnemy(new Vector3(0, 0, -5));
        }

        private void CreateEntityEnemy(Vector3 spawnPosition, float hp = 100)
        {
            _entitiesFactory.CreateEntityEnemy(spawnPosition, hp);
        }
    }
}