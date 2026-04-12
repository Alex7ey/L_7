using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.Shooting;
using Assets._Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;

namespace Assets._Project.Develop.Runtime.Gameplay.GameStates
{
    public class GameplayState : State, IUpdatableState
    {
        private readonly StageProviderService _stageProviderService;
        private readonly ProjectileShoter _projectileShooter;

        private readonly ShooterController _shooterController;

        public GameplayState(StageProviderService stageProviderService, ProjectileShoter projectileShooter, IInputService inputService)
        {
            _stageProviderService = stageProviderService;
            _projectileShooter = projectileShooter;

            _shooterController = new(_projectileShooter, inputService);
        }

        public override void Enter()
        {
            base.Enter();

            _stageProviderService.SwitchToNext();
            _stageProviderService.StartCurrent();
        }

        public void Update(float deltaTime)
        {
            _stageProviderService?.UpdateCurrent(deltaTime);
            _shooterController?.Update(deltaTime);
        }

        public override void Exit()
        {
            base.Exit();

            _stageProviderService.CleanupCurrent();
        }
    }
}
