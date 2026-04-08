using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.EnemysEntity;
using Assets._Project.Develop.Runtime.Gameplay.Features.ExplosionFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.TowerEntity;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class TestGameplay
    {
        private bool _isRunning;
        private TowerFactory _towerFactory;
        private EnemyEntityFactory _enemyFactory;
        private ProjectileEntityFactory _projectileEntityFactory;

        public TestGameplay(TowerFactory towerFactory, EnemyEntityFactory enemyFactory, ProjectileEntityFactory projectileEntityFactory)
        {
            _towerFactory = towerFactory;
            _enemyFactory = enemyFactory;
            _projectileEntityFactory = projectileEntityFactory;
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
                _towerFactory.Create();

            if (Input.GetKeyDown(KeyCode.Alpha2))
                _enemyFactory.Create(Vector3.forward * 5);

            if (Input.GetMouseButtonDown(0))
                Shoot();
        }

        public void Shoot()
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
                _projectileEntityFactory.CreateExplosion(hit.point);
        }
    }
}