using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.RotateFeature
{
    public class RotationSpeed : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class RotationDirection : IEntityComponent
    { 
        public ReactiveVariable<Vector3> Value;
    }

    public class RotationAngle : IEntityComponent
    {
        public ReactiveVariable<Quaternion> Value;
    }

    public class CanRotate : IEntityComponent
    {
        public ICompositeCondition Value;
    }
}
