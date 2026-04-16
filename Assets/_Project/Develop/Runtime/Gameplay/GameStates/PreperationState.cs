using Assets._Project.Develop.Runtime.Configs.GameState;
using Assets._Project.Develop.Runtime.Gameplay.Features.Ability;
using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;

namespace Assets._Project.Develop.Runtime.Gameplay.GameStates
{
    public class PreperationState : State, IUpdatableState
    {
        private readonly PreperationTriggerService _preperationTriggerService;
        private readonly AbilityService _abilityService;
        private readonly StateConfig _stateConfig;

        public PreperationState(
            PreperationTriggerService preperationTriggerService,
            IInputService inputService,
            AbilityService abilityService,
            StateConfig stateConfig)
        {
            _preperationTriggerService = preperationTriggerService;
            _abilityService = abilityService;
            _stateConfig = stateConfig;
        }

        public override void Enter()
        {
            base.Enter();

            _abilityService.SetAbility(_stateConfig.Ability);
        }

        public void Update(float deltaTime)
        {
            _preperationTriggerService.Update(deltaTime);
            _abilityService?.Update(deltaTime);
        }

        public override void Exit()
        {
            base.Exit();

            _preperationTriggerService.Cleanup();
            _abilityService.Dispose();
        }
    }
}
