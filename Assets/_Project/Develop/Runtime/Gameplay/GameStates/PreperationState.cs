using Assets._Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.GameStates
{
    public class PreperationState : State, IUpdatableState
    {
        private readonly PreperationTriggerService _preperationTriggerService;

        public PreperationState(PreperationTriggerService preperationTriggerService)
        {
            _preperationTriggerService = preperationTriggerService;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public void Update(float deltaTime)
        {
            _preperationTriggerService.Update(deltaTime);
        }

        public override void Exit()
        {
            base.Exit();

            _preperationTriggerService.Cleanup();
        }
    }
}
