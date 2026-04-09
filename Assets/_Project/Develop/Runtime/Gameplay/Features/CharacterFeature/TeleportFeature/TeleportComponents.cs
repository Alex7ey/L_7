using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.TeleportFeature
{
    public class TeleportRadius : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class TeleportCost : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class TeleportProcessTime: IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
     
    public class TeleportCurrentTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class InTeleportProcess : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }

    public class CanTeleportProcess : IEntityComponent
    {
        public ICompositeCondition Value;
    }

    public class TeleportEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }

    public class TeleportRequest : IEntityComponent
    {
        public ReactiveEvent Value;
    }

    public class TeleportEnded : IEntityComponent
    {
        public ReactiveEvent Value;
    }

    public class TeleportEnergyThreshold : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
}
