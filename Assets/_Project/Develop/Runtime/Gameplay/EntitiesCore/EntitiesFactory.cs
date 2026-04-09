using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Configs.Entities;
using Assets._Project.Develop.Runtime.Gameplay.Common;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Gameplay.Features.ApplyDamage;
using Assets._Project.Develop.Runtime.Gameplay.Features.AreaDamage;
using Assets._Project.Develop.Runtime.Gameplay.Features.Attack;
using Assets._Project.Develop.Runtime.Gameplay.Features.Attack.Explosion;
using Assets._Project.Develop.Runtime.Gameplay.Features.ContactTakeDamage;
using Assets._Project.Develop.Runtime.Gameplay.Features.LifeCycle;
using Assets._Project.Develop.Runtime.Gameplay.Features.LifeCycleFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.RotateFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.Sensors;
using Assets._Project.Develop.Runtime.Gameplay.Features.TeamsFeature;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.EntitiesCore
{
    public class EntitiesFactory
    {
        private readonly DIContainer _container;

        private readonly EntitiesLifeContext _entitiesLifeContext;
        private readonly MonoEntitiesFactory _monoEntitiesFactory;

        public EntitiesFactory(DIContainer container)
        {
            _container = container;
            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
            _monoEntitiesFactory = _container.Resolve<MonoEntitiesFactory>();
        }

        public Entity CreateTower(TowerConfig config)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, config.SpawnPosition, "Entities/TowerEntity");

            entity
           .AddMaxHealth(new ReactiveVariable<float>(config.MaxHealth))
           .AddCurrentHealth(new ReactiveVariable<float>(config.MaxHealth))
           .AddIsDead()
           .AddInDeathProcess()
           .AddTakeDamageRequest()
           .AddTakeDamageEvent()

           .AddIsTower()
           .AddTeam(new ReactiveVariable<Teams>(Teams.MainHero))
           ;

            ICompositeCondition mustDie = new CompositeCondition()
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            ICompositeCondition canApplyDamage = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            entity
               .AddMustDie(mustDie)
               .AddMustSelfRelease(mustSelfRelease)
               .AddCanApplyDamage(canApplyDamage);

            entity
                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext));

            return entity;
        }

        public Entity CreateExplosion(Vector3 position, ProjectileConfig projectileConfig)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/ExplosionEntity");

            entity
                .AddContactsDetectingMask(1 << LayerMask.NameToLayer("Characters"))
                .AddContactCollidersBuffer(new Buffer<Collider>(64))
                .AddContactEntitiesBuffer(new Buffer<Entity>(64))
                .AddRadiusDetecting(new ReactiveVariable<float>(projectileConfig.Radius))

                .AddTeam(new ReactiveVariable<Teams>(Teams.MainHero))

                .AddIsDead()
                .AddDeathMask(1 << LayerMask.NameToLayer("Characters"))
                .AddIsTouchDeathMask()

                .AddInstantAttackDamage(new ReactiveVariable<float>(projectileConfig.Damage))
                .AddInAttackProcess()
                .AddStartAttackEvent()
                .AddStartAttackRequest();

            ICompositeCondition mustDie = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsTouchDeathMask.Value));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value));

            ICompositeCondition canStartAttack = new CompositeCondition()
              .Add(new FuncCondition(() => entity.IsDead.Value == false));

            entity
                .AddMustDie(mustDie)
                .AddCanStartAttack(canStartAttack)
                .AddMustSelfRelease(mustSelfRelease);

            entity
                .AddSystem(new ContactsDetectingSystem())
                .AddSystem(new ContactsEntitiesFilterSystem(_container.Resolve<CollidersRegistryService>()))

                .AddSystem(new DeathSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext))

                .AddSystem(new AreaDamageSystem())
                .AddSystem(new ExplosionSystem())

                ;

            return entity;
        }

        public Entity CreateEnemy(Vector3 position, EnemyConfig config)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/EnemyEntity");

            entity
               .AddMaxHealth(new ReactiveVariable<float>(config.MaxHealth))
               .AddCurrentHealth(new ReactiveVariable<float>(config.MaxHealth))

               .AddTeam(new ReactiveVariable<Teams>(Teams.Enemies))

               .AddIsDead()
               .AddInDeathProcess()

               .AddTakeDamageRequest()
               .AddTakeDamageEvent()

               .AddAttackCanceledEvent()
               .AddAttackProcessCurrentTime()

               .AddIsTargetReached()
               .AddCurrentTarget()

               .AddRadiusDetecting(new ReactiveVariable<float>(config.RadiusDetected))

               .AddContactsDetectingMask(1 << LayerMask.NameToLayer("Characters"))
               .AddContactCollidersBuffer(new Buffer<Collider>(64))
               .AddContactEntitiesBuffer(new Buffer<Entity>(64))

               .AddInstantAttackDamage(new ReactiveVariable<float>(config.AttackDamage))
               .AddInAttackProcess()
               .AddStartAttackEvent()
               .AddStartAttackRequest()

               .AddMoveDirection()
               .AddMoveSpeed(new ReactiveVariable<float>(config.MovementSpeed))
               .AddIsMoving()

               .AddRotationAngle(new ReactiveVariable<Quaternion>(Quaternion.identity))
               .AddRotationSpeed(new ReactiveVariable<float>(config.RotationSpeed))

               ;
            ICompositeCondition canMove = new CompositeCondition()
             .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition canRotate = new CompositeCondition()
             .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition mustDie = new CompositeCondition()
             .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
             .Add(new FuncCondition(() => entity.IsDead.Value))
             .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            ICompositeCondition canApplyDamage = new CompositeCondition()
            .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition canStartAttack = new CompositeCondition()
             .Add(new FuncCondition(() => entity.IsDead.Value == false))
             .Add(new FuncCondition(() => entity.InAttackProcess.Value == false))
             .Add(new FuncCondition(() => entity.IsMoving.Value == false));

            ICompositeCondition mustCancelAttack = new CompositeCondition(LogicOperations.Or)
             .Add(new FuncCondition(() => entity.IsDead.Value))
             .Add(new FuncCondition(() => entity.IsMoving.Value));

            entity
                .AddCanMove(canMove)
                .AddCanRotate(canRotate)

                .AddMustDie(mustDie)
                .AddMustSelfRelease(mustSelfRelease)

                .AddCanApplyDamage(canApplyDamage)
                .AddCanStartAttack(canStartAttack)
                .AddMustCancelAttack(mustCancelAttack);

            entity
               .AddSystem(new RigidbodyMovementSystem())
               .AddSystem(new RigidbodyRotateSystem())

               .AddSystem(new ApplyDamageSystem())
               .AddSystem(new AreaDamageSystem())

               .AddSystem(new ContactsDetectingSystem())
               .AddSystem(new ContactsEntitiesFilterSystem(_container.Resolve<CollidersRegistryService>()))

               .AddSystem(new DeathSystem())
               .AddSystem(new SelfReleaseSystem(_entitiesLifeContext));
            ;

            return entity;
        }

        public Entity CreateProjectile(Vector3 position, Vector3 direction, float damage)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/Projectile");

            entity
                .AddMoveDirection(new ReactiveVariable<Vector3>(direction))
                .AddMoveSpeed(new ReactiveVariable<float>(10))
                .AddIsMoving()
                .AddRotationDirection(new ReactiveVariable<Vector3>(direction))
                .AddRotationSpeed(new ReactiveVariable<float>(9999))
                .AddIsDead()
                .AddRotationAngle(new ReactiveVariable<Quaternion>(Quaternion.identity))
                .AddContactsDetectingMask(1 << LayerMask.NameToLayer("Characters"))
                .AddContactCollidersBuffer(new Buffer<Collider>(64))
                .AddContactEntitiesBuffer(new Buffer<Entity>(64))
                .AddBodyContactDamage(new ReactiveVariable<float>(damage))
                .AddDeathMask(1 << LayerMask.NameToLayer("Characters"))
                .AddIsTouchDeathMask();

            ICompositeCondition canMove = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition canRotate = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition mustDie = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsTouchDeathMask.Value));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value));

            entity
                .AddCanMove(canMove)
                .AddCanRotate(canRotate)

                .AddMustDie(mustDie)
                .AddMustSelfRelease(mustSelfRelease);

            entity
                .AddSystem(new RigidbodyMovementSystem())
                .AddSystem(new RigidbodyRotateSystem())
                .AddSystem(new ContactsDetectingSystem())
                .AddSystem(new ContactsEntitiesFilterSystem(_container.Resolve<CollidersRegistryService>()))
                .AddSystem(new DealDamageOnContactSystem())
                .AddSystem(new DeathMaskTouchDetectorSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext));

            _entitiesLifeContext.Add(entity);

            return entity;
        }

        private void AddEnergyFeature(Entity entity)
        {
            entity
                .AddCurrentEnergy(new ReactiveVariable<float>(35))
                .AddMaxEnergy(new ReactiveVariable<float>(100))
                .AddEnergyRegenPercent(new ReactiveVariable<float>(15))
                .AddEnergyRegenDelay(new ReactiveVariable<float>(3))
                .AddEnergyRegenCurrentTime()
                .AddInRegenEnergyProcess()
                .AddEnergyRegenEvent()
                .AddEnergyRegenRequest()
                .AddEnergySpendEvent();
        }

        private void AddLifeCycleFeature(Entity entity)
        {
            entity
              .AddMaxHealth(new ReactiveVariable<float>(100))
              .AddCurrentHealth(new ReactiveVariable<float>(100))
              .AddIsDead()
              .AddInDeathProcess()
              .AddTakeDamageRequest()
              .AddTakeDamageEvent();
        }

        private void AddAreaDamageFeature(Entity entity)
        {
            entity
                .AddInstantAttackDamage(new ReactiveVariable<float>(60))
                .AddInAttackProcess()
                .AddStartAttackEvent()
                .AddStartAttackRequest();
        }

        private void AddSensorFeature(Entity entity)
        {
            entity
                .AddContactsDetectingMask(1 << LayerMask.NameToLayer("Characters"))
                .AddContactCollidersBuffer(new Buffer<Collider>(64))
                .AddContactEntitiesBuffer(new Buffer<Entity>(64))
                .AddRadiusDetecting(new ReactiveVariable<float>(5));
        }

        private Entity CreateEmpty() => new Entity();
    }
}
