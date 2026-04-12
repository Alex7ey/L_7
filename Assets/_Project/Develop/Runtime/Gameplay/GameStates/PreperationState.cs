using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.Miner;
using Assets._Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using Assets._Project.Develop.Runtime.Meta.Features.Inventory;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagment;
using Assets._Project.Develop.Runtime.Utilities.DataManagment.DataProvider;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;

namespace Assets._Project.Develop.Runtime.Gameplay.GameStates
{
    public class PreperationState : State, IUpdatableState
    {
        private readonly PreperationTriggerService _preperationTriggerService;

        private readonly MinerController _minerController;

        public PreperationState(
            PreperationTriggerService preperationTriggerService, 
            MinePlacer minePlacer, 
            IInputService inputService)
        {
            _preperationTriggerService = preperationTriggerService;
            _minerController = new(minePlacer, inputService);
        }

        public override void Enter()
        {
            base.Enter();
        }

        public void Update(float deltaTime)
        {
            _preperationTriggerService.Update(deltaTime);
            _minerController.Update(deltaTime);
        }

        public override void Exit()
        {
            base.Exit();

            _preperationTriggerService.Cleanup();
        }
    }
}
