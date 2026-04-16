using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature
{
    public interface IInputService
    {
        bool IsEnabled { get; set; }

        bool IsUseAbilityPressed { get; }

        float RotationAngle { get; }

        Vector3 MovementDirection { get; }

        Vector3? MousePosition {  get; }
    }
}
