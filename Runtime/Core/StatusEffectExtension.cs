using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            else if(effect.stacks == false)
            {
                statusEffects[existingEffectIndex].Reset(gameObject);
            }
        }

        int GetStatusIndex(StatusEffect effect)
        {
            if (statusEffects.Contains(effect))
            {
                return statusEffects.IndexOf(effect);
            }
            else
            {
                return -1;
            }
        }
    }
}

