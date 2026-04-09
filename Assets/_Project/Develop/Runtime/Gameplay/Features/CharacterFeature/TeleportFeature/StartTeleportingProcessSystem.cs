using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.TeleportFeature
{
    public class StartTeleportingProcessSystem : IInitializableSystem, IDisposableSystem
    {
        private GameObject _character;

        private ReactiveEvent _teleportEvents;
        private IDisposable _teleportRequest;

        private ICompositeCondition _canTeleportProcess;

        public void OnInit(Entity entity)
        {
            _character = entity.GameObject;
            _canTeleportProcess = entity.CanTeleportProcess;
            _teleportEvents = entity.TeleportEvent;

            _teleportRequest = entity.TeleportRequest.Subscribe(StartTeleportProcess);
        }

        private void StartTeleportProcess()
        {
            if (_canTeleportProcess.Evaluate() == false)
                return;

            _teleportEvents.Invoke();

            _character.SetActive(false);
        }

        public void OnDispose()
        {
            _teleportRequest.Dispose();
        }
    }
}
