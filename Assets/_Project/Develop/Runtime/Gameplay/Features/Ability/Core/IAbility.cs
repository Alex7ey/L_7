using Assets._Project.Develop.Runtime.Gameplay.Features.Ability.Core;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Ability
{
    public interface IAbility
    {
        void Use(AbilityInputData input);
    }
}
