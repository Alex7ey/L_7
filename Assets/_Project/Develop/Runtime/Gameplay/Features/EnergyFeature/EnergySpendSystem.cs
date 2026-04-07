using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.TeleportFeature
{
    public class EnergySpendSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveVariable<float> _currentEnergy;
        private IDisposable _energySpendEvent;

        public void OnInit(Entity entity)
        {
            _currentEnergy = entity.CurrentEnergy;
            _energySpendEvent = entity.EnergySpendEvent.Subscribe(Spend);
        }

        private void Spend(float cost)
        {
            if (cost > _currentEnergy.Value)
                throw new ArgumentException("Not enough energy!");

            _currentEnergy.Value = MathF.Max(_currentEnergy.Value - cost, 0);
            Debug.Log($"Потрачено {cost} энергии");
        }

        public void OnDispose()
        {
            _energySpendEvent.Dispose();
        }
    }
}
