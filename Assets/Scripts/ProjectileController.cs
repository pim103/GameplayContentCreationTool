using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private float projectileSpeed = 10f;
    private float projectileDuration = 5f;
    private int projectileDamage = 10;

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
