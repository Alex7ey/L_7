using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States.Attack
{
    public class ExplosionState : State, IUpdatableState
    {
        private ReactiveEvent _attackRequest;
        private ReactiveVariable<bool> _isDead;

        public ExplosionState(Entity entity)
        {
            _attackRequest = entity.StartAttackRequest;
            _isDead = entity.IsDead;
        }

        public override void Enter()
        {
            base.Enter();

            _attackRequest.Invoke();
            _isDead.Value = true;
        }

        public void Update(float deltaTime)
        {

        }
    }
}
