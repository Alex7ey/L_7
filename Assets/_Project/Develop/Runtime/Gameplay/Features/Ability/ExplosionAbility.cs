using Assets._Project.Develop.Runtime.Gameplay.Features.Ability.Core;
using Assets._Project.Develop.Runtime.Gameplay.Features.ExplosionFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using UnityEngine;


namespace Assets._Project.Develop.Runtime.Gameplay.Features.Ability
{
    public class ExplosionAbility : IAbility
    {
        private readonly ProjectileEntityFactory _projectileEntityFactory;
        private readonly IInputService _inputService;

        public ExplosionAbility(
            ProjectileEntityFactory projectileEntityFactory,
            IInputService inputService)
        {
            _projectileEntityFactory = projectileEntityFactory;
            _inputService = inputService;
        }

        public void Use(AbilityInputData input)
        {
            if (input.MousePosition == null)
                return;

            _projectileEntityFactory.CreateExplosionEntity((Vector3)_inputService.MousePosition);
        }
    }
}
