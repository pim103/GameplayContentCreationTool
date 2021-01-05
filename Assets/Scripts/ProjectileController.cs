using System;
using System.Collections;
using UnityEngine;
using Weapons;

public class ProjectileController : MonoBehaviour
{
    public float projectileSpeed = 10f;
    public float projectileDuration = 5f;
    public int projectileDamage = 10;
    public Effect effect;

    private Coroutine currentDelay;

    private void OnEnable()
    {
        currentDelay = StartCoroutine(DelayBeforeDisappear());
    }

    private void Update()
    {
        transform.position += transform.forward * projectileSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        int layerGround = LayerMask.NameToLayer("ground");
        int layerEntity = LayerMask.NameToLayer("Entity");

        if (other.gameObject.layer == layerGround)
        {
            EndOfProjectile();
        }
        
        if (other.gameObject.layer == layerEntity)
        {
            EndOfProjectile();

            Entity entity = other.GetComponent<Entity>();
            entity.TakeDamage(projectileDamage);

            if (effect != null)
            {
                EffectController.ApplyEffect(entity, effect);
            }
        }

    }

    private IEnumerator DelayBeforeDisappear()
    {
        yield return new WaitForSeconds(projectileDuration);
        EndOfProjectile();
    }

    private void EndOfProjectile()
    {
        if (currentDelay != null)
        {
            StopCoroutine(currentDelay);
        }

        gameObject.SetActive(false);   
    }
}
