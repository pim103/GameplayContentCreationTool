using System;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    private int hp;

    public GameObject Player { get; set; }
    
    private void OnEnable()
    {
        hp = 50;    
        hpBar.value = hp;
        hpBar.maxValue = hp;
    }

    public void Update()
    {
        hpBar.transform.LookAt(Player.transform);
        hpBar.transform.Rotate(Vector3.up * 180);
    }

    public void TakeDamage(int damageTaken)
    {
        hp -= damageTaken;
        hpBar.value = hp;

        if (hp <= 0)
        {
            DestroyEntity();
        }
    }

    private void DestroyEntity()
    {
        gameObject.SetActive(false);
    }
}
