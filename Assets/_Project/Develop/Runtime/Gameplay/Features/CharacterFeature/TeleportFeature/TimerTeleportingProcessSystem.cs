using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using System;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.TeleportFeature
{
    public class TimerTeleportingProcessSystem : IInitializableSystem, IUpdatableSystem, IDisposableSystem
    {
        private ReactiveVariable<float> _currentTime;
        private ReactiveVariable<bool> _inTeleportProcess;
        private IDisposable _teleportEvents;

        public void OnInit(Entity entity)
        {
            _currentTime = entity.TeleportCurrentTime;
            _inTeleportProcess = entity.InTeleportProcess;
            _teleportEvents = entity.TeleportEvent.Subscribe(StartTeleportProcess);
        }

        private void StartTeleportProcess()
        {
            _inTeleportProcess.Value = true;
            _currentTime.Value = 0;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inTeleportProcess.Value == false)
                return;

            _currentTime.Value += deltaTime;
        }

        public void OnDispose()
        {
            _teleportEvents.Dispose();
        }
    }
}
