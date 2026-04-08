using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.MainHero;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States.FindTarget
{
    public class FindDistanceToTargetState : State, IUpdatableState
    {
        private readonly Entity _source;
        private readonly Entity _target;
        private readonly float _radiusDetecting;

        private float _distanceToTarget;
        private ReactiveVariable<bool> _isTargetReached;

        public FindDistanceToTargetState(Entity entity, TowerHolderService towerHolderService)
        {
            _source = entity;
            _target = towerHolderService.Tower;
            _radiusDetecting = entity.RadiusDetecting.Value;
            _isTargetReached = entity.IsTargetReached;

            entity.CurrentTarget.Value = towerHolderService.Tower;///
        }

        public void Update(float deltaTime)
        {
            if (_target.GameObject == null)
                return;

            _distanceToTarget = (_target.Transform.position - _source.Transform.position).magnitude;

            if (_distanceToTarget <= _radiusDetecting)
            {
                _isTargetReached.Value = true;
                return;
            }

            _isTargetReached.Value = false;
        }

        public override void Exit()
        {
            base.Exit();

            _isTargetReached.Value = false;

            _distanceToTarget = 0;
        }
    }
}
