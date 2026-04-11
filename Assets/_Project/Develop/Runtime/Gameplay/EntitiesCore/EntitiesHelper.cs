using Assets._Project.Develop.Runtime.Gameplay.Features.ApplyDamage;
using Assets._Project.Develop.Runtime.Gameplay.Features.TeamsFeature;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.EntitiesCore
{
    public class EntitiesHelper
    {
        public static bool TryTakeDamageFrom(Entity source, Entity damageable, float damage)
        {
            if (CanTakeDamageFrom(source, damageable) == false)
                return false;

            damageable.TakeDamageRequest.Invoke(damage);

            return true;
        }

        public static bool CanTakeDamageFrom(Entity source, Entity damageable)
        {
            if (damageable.TryGetTakeDamageRequest(out ReactiveEvent<float> takeDamageRequest) == false)
                return false;

            if (source.TryGetTeam(out ReactiveVariable<Teams> sourceTeam) && damageable.TryGetTeam(out ReactiveVariable<Teams> damageableTeam))
            {
                if (sourceTeam.Value == damageableTeam.Value)
                    return false;
            }

            return true;
        }
    }
}
