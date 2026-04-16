using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature
{
    public class DesktopInput : IInputService
    {
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";

        private const string HorizontalMouseAxis = "Mouse X";

        public bool IsEnabled { get; set; } = true;

        public Vector3 MovementDirection
        {
            get
            {
                if (IsEnabled == false)
                    return Vector3.zero;

                return new Vector3(Input.GetAxisRaw(HorizontalAxisName), 0, Input.GetAxisRaw(VerticalAxisName));
            }
        }

        public float RotationAngle
        {
            get
            {
                if (IsEnabled == false)
                    return 0;

                return Input.GetAxisRaw(HorizontalMouseAxis);
            }
        }

        public bool IsUseAbilityPressed
        {
            get
            {
                if (IsEnabled == false)
                    return false;

                return Input.GetMouseButtonDown(0);
            }
        }

        public Vector3? MousePosition
        {
            get
            {
                if (IsEnabled == false)
                    return null;

                if (EventSystem.current.IsPointerOverGameObject())
                    return null;

                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit))
                    return hit.point;

                return null;
            }
        }

    }
}
