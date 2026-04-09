using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.TeleportFeature
{
    public class CurrentEnergy : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class MaxEnergy : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class EnergyRegenDelay : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class EnergyRegenCurrentTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class EnergyRegenPercent : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class InRegenEnergyProcess : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }

    public class CanRegenerateEnergy : IEntityComponent
    {
        public ICompositeCondition Value;
    }

    public class EnergyRegenEvent : IEntityComponent
    {
        public ReactiveEvent<float> Value;
    }

    public class EnergyRegenRequest : IEntityComponent
    {
        public ReactiveEvent Value;
    }

    public class EnergySpendEvent : IEntityComponent 
    {
        public ReactiveEvent<float> Value;
    } 
}
