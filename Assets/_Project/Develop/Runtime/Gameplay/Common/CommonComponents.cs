using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Common
{
    public class RigidbodyComponent : IEntityComponent
    {
        public Rigidbody Value;
    }

    public class CharacterControllerComponent : IEntityComponent
    {
        public CharacterController Value;
    }

    public class TransformComponent : IEntityComponent
    {
        public Transform Value;
    }

    public class GameObjectComponent : IEntityComponent
    {
        public GameObject Value;
    }

    public class CurrentTarget : IEntityComponent
    {
        public ReactiveVariable<Entity> Value;
    }

    public class CurrentTargetPosition : IEntityComponent
    {
        public ReactiveVariable<Vector3> Value;
    }

    public class IsTargetReached : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }
}
