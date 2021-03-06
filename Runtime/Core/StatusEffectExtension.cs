using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace mactinite.Status
{
    public class StatusEffectExtension : MonoBehaviour, IEffectable
    {

        public List<StatusEffect> statusEffects = new List<StatusEffect>();

        private void Update()
        {
            for (int i = statusEffects.Count - 1; i >= 0; i--)
            {
                statusEffects[i].OnTick(gameObject);
                if (statusEffects[i].hasPopped)
                {
                    statusEffects.RemoveAt(i);
                }
            }
        }

        public void ApplyStatusEffect(StatusEffect effect)
        {
            var existingEffectIndex = GetStatusIndex(effect);
            if (effect.stacks || existingEffectIndex == -1)
            {
                statusEffects.Add(effect.Clone());
            }
            else if (effect.stacks == false)
            {
                statusEffects[existingEffectIndex].Reset(gameObject);
            }
        }

        // checks to see if status effect is in the status effect list and returns a bool
        public bool IsStatusEffectActive(StatusEffect effect){
            return GetStatusIndex(effect) != -1;
        }
        
        int GetStatusIndex(StatusEffect effect)
        {
            // need to check if an effect of this type is in the list, a simple contains check won't work
            int index = statusEffects.FindLastIndex((statusEffect) =>
            {
                if (effect.name == statusEffect.name)
                {
                    return true;
                }

                return false;
            });

            return index;
        }
    }
}

