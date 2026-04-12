using UnityEngine;

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

        public bool AttackKeyPress
        {
            get
            {
                if (IsEnabled == false)
                    return false;

                return Input.GetMouseButtonDown(0);
            }
        }

        public bool MinePlaceButtonPress
        {
            get
            {
                if (IsEnabled == false)
                    return false;

                return Input.GetMouseButtonDown(1);
            }
        }

    }
}
