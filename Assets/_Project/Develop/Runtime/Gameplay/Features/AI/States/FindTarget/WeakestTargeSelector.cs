using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.ApplyDamage;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States.FindTarget
{
    public class WeakestTargeSelector : ITargetSelector
    {
        private Entity _source;

        public WeakestTargeSelector(Entity entity)
        {
            _source = entity;
        }

        public Entity SelectTargetFrom(IEnumerable<Entity> targets)
        {
            IEnumerable<Entity> selectedTargets = targets.Where(target =>
            {
                bool result = target.HasComponent<TakeDamageRequest>();

                if (target.TryGetCanApplyDamage(out ICompositeCondition canApplyDamage))
                {
                    result = result && canApplyDamage.Evaluate();
                }

                result = result && (target != _source);

                return result;
            });
        
            if (selectedTargets.Any() == false)
                return null;

            Entity weakestTarget = selectedTargets.First();
            float minHealth = weakestTarget.CurrentHealth.Value;

            foreach (Entity target in selectedTargets)
            {
                float targetHealth = target.CurrentHealth.Value;

                if (targetHealth < minHealth)
                {
                    minHealth = targetHealth;
                    weakestTarget = target;
                }
            }

            return weakestTarget;
        }

    }
}
