using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.StagesFeature
{
    public class PreperationTriggerService
    {
        private ReactiveVariable<bool> _startButtonPress = new();

        public IReadOnlyVariable<bool> StartButtonPress => _startButtonPress;

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                _startButtonPress.Value = true;
                return;
            }  
        }

        public void Cleanup()
        {
            _startButtonPress.Value = false;
        }
    }
}
