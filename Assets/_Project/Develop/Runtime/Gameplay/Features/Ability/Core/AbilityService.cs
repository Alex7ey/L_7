using Assets._Project.Develop.Runtime.Gameplay.Features.Ability.Core;
using System;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Ability
{
    public class AbilityService : IDisposable
    {
        private readonly AbilityInputService _abilityInputService;
        private readonly AbilityFactory _abilityFactory;

        private IAbility _currentAbility;

        public AbilityService(AbilityFactory abilityFactory, AbilityInputService abilityInputService)
        {
            _abilityFactory = abilityFactory;
            _abilityInputService = abilityInputService;
        }

        public void Update(float deltaTime)
        {
            if (_abilityInputService.IsUseAbilityPressed)
                _currentAbility?.Use(_abilityInputService.GetInputData());
        }

        public void SetAbility(AbilityType ability)
        {
            switch (ability)
            {
                case AbilityType.Explosion:
                    _currentAbility = _abilityFactory.CreateExplosionAbility();
                    break;

                case AbilityType.MinePlacement:
                    _currentAbility = _abilityFactory.CreateMinePlacement();
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(ability), $"Unknown ability type: {ability}");
            }
        }

        public void Dispose()
        {
            _currentAbility = null;
        } 
    }
}
