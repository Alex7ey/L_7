using Assets._Project.Develop.Runtime.Gameplay.Features.EnemysEntity;
using Assets._Project.Develop.Runtime.Gameplay.Features.ExplosionFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.TowerEntity;
using Assets._Project.Develop.Runtime.Utilities.Timer;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class ProjectileShooter
    {
        private ProjectileEntityFactory _projectileEntityFactory;

        public ProjectileShooter(ProjectileEntityFactory projectileEntityFactory)
        {
            _projectileEntityFactory = projectileEntityFactory;
        }

        public void Update(float deltaTime)
        {
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