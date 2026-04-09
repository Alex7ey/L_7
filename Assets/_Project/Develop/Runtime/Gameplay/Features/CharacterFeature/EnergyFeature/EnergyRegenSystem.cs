using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.TeleportFeature
{
    public class EnergyRegenSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveVariable<float> _currentEnergy;
        private ReactiveVariable<float> _maxEnergy;

        private ICompositeCondition _canRegenerateEnergy;

        private IDisposable _energyRegenEvent;

        public void OnInit(Entity entity)
        {
            _maxEnergy = entity.MaxEnergy;
            _currentEnergy = entity.CurrentEnergy;
            _canRegenerateEnergy = entity.CanRegenerateEnergy;

            _energyRegenEvent = entity.EnergyRegenEvent.Subscribe(RegenEnergy);
        }

        private void RegenEnergy(float energyPercent)
        {
            if (_canRegenerateEnergy.Evaluate() == false)
                return;

            float percentRegenEnergy = energyPercent / 100 * _maxEnergy.Value;
            float newEnergy = _currentEnergy.Value + percentRegenEnergy;

            _currentEnergy.Value = MathF.Min(newEnergy, _maxEnergy.Value);

            Debug.Log($"+ {percentRegenEnergy} энергии, всего: { _currentEnergy.Value}");
        }

        public void OnDispose()
        {
            _energyRegenEvent?.Dispose();
        }
    }
}
