using Assets._Project.Develop.Runtime.Configs.GameState;
using Assets._Project.Develop.Runtime.Gameplay.Features.Ability;
using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;

namespace Assets._Project.Develop.Runtime.Gameplay.GameStates
{
    public class GameplayState : State, IUpdatableState
    {
        private readonly StageProviderService _stageProviderService;
        private readonly AbilityService _abilityService;
        private readonly StateConfig _stateConfig;

        public GameplayState(
            StageProviderService stageProviderService, 
            IInputService inputService, 
            AbilityService abilityService,
            StateConfig stateConfig)
        {
            _stageProviderService = stageProviderService;
            _abilityService = abilityService;
            _stateConfig = stateConfig;
        }

        public override void Enter()
        {
            base.Enter();

            _stageProviderService.SwitchToNext();
            _stageProviderService.StartCurrent();

            _abilityService.SetAbility(_stateConfig.Ability);
        }

        public void Update(float deltaTime)
        {
            _stageProviderService?.UpdateCurrent(deltaTime);
            _abilityService?.Update(deltaTime);
        }

        public override void Exit()
        {
            base.Exit();

            _abilityService.Dispose();
            _stageProviderService.CleanupCurrent();
        }
    }
}
