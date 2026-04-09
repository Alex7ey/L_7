using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.TeleportFeature
{
    public class EndTeleportingProcessSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<float> _currentTime;
        private ReactiveVariable<float> _teleportationTime;
        private ReactiveVariable<bool> _inProcessTeleporting;

        private GameObject _character;
        private ReactiveEvent _teleportEnded;

        public void OnInit(Entity entity)
        {
            _character = entity.GameObject;
            _teleportationTime = entity.TeleportProcessTime;
            _currentTime = entity.TeleportCurrentTime;
            _inProcessTeleporting = entity.InTeleportProcess;
            _teleportEnded = entity.TeleportEnded;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inProcessTeleporting.Value == false)
                return;

            if (_currentTime.Value >= _teleportationTime.Value)
                EndTeleportProcess();
        }

        private void EndTeleportProcess()
        {
            _inProcessTeleporting.Value = false;
            _character.SetActive(true);          
            _teleportEnded?.Invoke();
        }
    }
}
