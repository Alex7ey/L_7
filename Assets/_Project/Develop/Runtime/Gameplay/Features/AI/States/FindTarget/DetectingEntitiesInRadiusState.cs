using Assets._Project.Develop.Runtime.Gameplay.Common;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;


namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States.FindTarget
{
    public class DetectingEntitiesInRadiusState : State, IUpdatableState
    {
        private readonly Buffer<Entity> _contactEntitiesBuffer;
        private readonly Entity _entity;
        private readonly ReactiveVariable<bool> _detectedEntity;

        public DetectingEntitiesInRadiusState(Entity entity)
        {
            _entity = entity;
            _detectedEntity = entity.DetectedEntity;
            _contactEntitiesBuffer = entity.ContactEntitiesBuffer;
        }

        public void Update(float deltaTime)
        {
            if (_contactEntitiesBuffer.Count > 0)
            {
                for (int i = 0; i < _contactEntitiesBuffer.Count; i++)
                {
                    if (EntitiesHelper.CanTakeDamageFrom(_entity, _contactEntitiesBuffer.Items[i]))
                    {
                        _detectedEntity.Value = true;
                        break;
                    }
                }

            }
        }
    }
}
