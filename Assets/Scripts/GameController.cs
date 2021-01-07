using System;
using System.Collections;
using System.Collections.Generic;
using Games.Global.Weapons;
using Player;
using UnityEngine;
using Weapons;

public class GameController : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    public void Start()
    {
        StartCoroutine(WaitForWeaponsLoad());
    }

    private IEnumerator WaitForWeaponsLoad()
    {
        yield return WeaponList.InitWeaponList();

        player.InitPlayer();
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        GameObject target = ObjectPooler.SharedInstance.GetPooledObject(1);
        target.transform.position = Vector3.zero + Vector3.up * 2;
        
        Entity entity = target.GetComponent<Entity>();
        entity.Player = player.gameObject;

        target.SetActive(true);
    }
}
