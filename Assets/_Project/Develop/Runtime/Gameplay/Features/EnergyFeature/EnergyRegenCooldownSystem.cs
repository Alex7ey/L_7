using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.TeleportFeature
{
    public class EnergyRegenCooldownSystem : IInitializableSystem, IUpdatableSystem, IDisposableSystem
    {
        private ReactiveVariable<float> _currentTime = new();
        private ReactiveVariable<float> _initialTime;
        private ReactiveVariable<bool> _inRegenEnergyProcess;

        private IDisposable _energyRegenRequest;

        private ICompositeCondition _canRegenerateEnergy;

        public void OnInit(Entity entity)
        {
            _initialTime = entity.EnergyRegenDelay;
            _canRegenerateEnergy = entity.CanRegenerateEnergy;
            _inRegenEnergyProcess = entity.InRegenEnergyProcess;
            _energyRegenRequest = entity.EnergyRegenRequest.Subscribe(OnStartCooldown);

            _currentTime.Value = _initialTime.Value;
        }

        private void OnStartCooldown()
        {
            _currentTime.Value = _initialTime.Value;
            _inRegenEnergyProcess.Value = true;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_canRegenerateEnergy.Evaluate() == false)
                return;

            if (_inRegenEnergyProcess.Value == false)
                return;

            _currentTime.Value -= deltaTime;

            if (CooldownIsOver())
                _inRegenEnergyProcess.Value = false;
        }

        private bool CooldownIsOver() => _currentTime.Value <= 0;

        public void OnDispose()
        {
            _energyRegenRequest?.Dispose();
        }
    }
}
