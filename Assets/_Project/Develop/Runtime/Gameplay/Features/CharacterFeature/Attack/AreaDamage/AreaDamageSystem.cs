using Assets._Project.Develop.Runtime.Gameplay.Common;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AreaDamage
{
    public class AreaDamageSystem : IInitializableSystem, IDisposableSystem
    {
        private Buffer<Entity> _contactEntities;
        private ReactiveVariable<float> _damageAmount;

        private IDisposable _attackAreaDamageRequest;
        private ICompositeCondition _canStartAttack;

        private Entity _entity;

        public void OnInit(Entity entity)
        {
            _entity = entity;
            _damageAmount = entity.InstantAttackDamage;
            _canStartAttack = entity.CanStartAttack;
            _contactEntities = entity.ContactEntitiesBuffer;
            _attackAreaDamageRequest = entity.StartAttackRequest.Subscribe(DealDamage);
        }

        public void DealDamage()
        {
            if (_canStartAttack.Evaluate() == false)
                return;

            for (int i = 0; i < _contactEntities.Count; i++)
            {
                Entity contactEntity = _contactEntities.Items[i];

                if (EntitiesHelper.CanTakeDamageFrom(_entity, contactEntity))
                    contactEntity.TakeDamageRequest.Invoke(_damageAmount.Value);
            }
        }

        public void OnDispose()
        {
            _attackAreaDamageRequest.Dispose();
        }
    }
}
