using System;
using System.Collections;
using UnityEngine;

namespace Weapons
{
    public class EffectController : MonoBehaviour
    {
        private static EffectController instance;
        
        private void Start()
        {
            instance = this;
        }

        public static void ApplyEffect(Entity target, Effect effect)
        {
            instance.StartCoroutine(RunEffect(target, effect));
        }

        private static IEnumerator RunEffect(Entity target, Effect effect)
        {
            switch (effect.SpecialEffect)
            {
                case SpecialEffect.Heal:
                    target.HealDamages(effect.amount);
                    break;
                case SpecialEffect.Slow:
                    target.ReduceSpeed(effect.amount);
                    break;
            }

            float duration = effect.lifeTime;
            
            while (duration > 0)
            {
                if (effect.SpecialEffect == SpecialEffect.Dot)
                {
                    target.TakeDamage(effect.amount);
                }
                
                yield return new WaitForSeconds(effect.interval);
                duration -= effect.interval;
            }
        }
    }
}