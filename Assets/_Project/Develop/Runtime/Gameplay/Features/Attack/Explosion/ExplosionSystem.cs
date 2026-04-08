using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Attack.Explosion
{
    public class ExplosionSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveEvent _startAttackRequest;
        private ReactiveVariable<bool> _isDead;

        public void OnInit(Entity entity)
        {
            _startAttackRequest = entity.StartAttackRequest;
            _isDead = entity.IsDead;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_isDead.Value)
                return;

            Explode();
        }

        private void Explode()
        {
            _startAttackRequest.Invoke();
            _isDead.Value = true;
        }
    }
}
