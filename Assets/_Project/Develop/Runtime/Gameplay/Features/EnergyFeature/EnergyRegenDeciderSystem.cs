using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.TeleportFeature
{
    public class EnergyRegenDeciderSystem : IInitializableSystem, IUpdatableSystem
    {
        private ICompositeCondition _canRegenerateEnergy;
        private ReactiveVariable<bool> _inRegenEnergyProcess;

        private ReactiveVariable<float> _energyRegenPercent;

        private ReactiveEvent<float> _energyRegenEvent;
        private ReactiveEvent _energyRegenRequest;

        public void OnInit(Entity entity)
        {
            _canRegenerateEnergy = entity.CanRegenerateEnergy;
            _energyRegenPercent = entity.EnergyRegenPercent;
            _energyRegenEvent = entity.EnergyRegenEvent;
            _inRegenEnergyProcess = entity.InRegenEnergyProcess;
            _energyRegenRequest = entity.EnergyRegenRequest;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_canRegenerateEnergy.Evaluate() == false)
                return;

            if (_inRegenEnergyProcess.Value == true)
                return;

            _energyRegenRequest?.Invoke();
            _energyRegenEvent?.Invoke(_energyRegenPercent.Value);
        }
    }
}
