using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Ability.Core
{
    public class AbilityInputService
    {
        private readonly IInputService _inputService;

        public bool IsUseAbilityPressed => _inputService.IsUseAbilityPressed;

        public AbilityInputService(IInputService inputService)
        {
            _inputService = inputService;
        }

        public AbilityInputData GetInputData() => new()
        {
            MovementDirection = _inputService.MovementDirection,
            MousePosition = _inputService.MousePosition
        };
    }
}
