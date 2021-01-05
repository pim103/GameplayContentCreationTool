using System;
using System.Collections;
using System.Collections.Generic;
using Games.Global.Weapons;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public void Start()
    {
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        GameObject target = ObjectPooler.SharedInstance.GetPooledObject(1);
        target.transform.position = Vector3.zero + Vector3.up * 2;
        
        Entity entity = target.GetComponent<Entity>();
        entity.Player = player;

        target.SetActive(true);
    }
}
