using Assets._Project.Develop.Runtime.Gameplay.Features.ExplosionFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.Shooting;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class ProjectileShoter : IShooter
    {
        private ProjectileEntityFactory _projectileEntityFactory;

        public ProjectileShoter(ProjectileEntityFactory projectileEntityFactory)
        {
            _projectileEntityFactory = projectileEntityFactory;
        }

        public void Shoot(Vector3 position)
        {
            _projectileEntityFactory.CreateExplosionEntity(position);
        }
    }
}