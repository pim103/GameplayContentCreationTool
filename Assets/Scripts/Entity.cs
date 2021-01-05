using System;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    private float hp;
    private float maxHp;

    private float speed;
    private float originalSpeed;

    public GameObject Player { get; set; }
    
    private void OnEnable()
    {
        maxHp = hp = 50;
        hpBar.value = hp;
        hpBar.maxValue = maxHp;

        originalSpeed = speed = 5;
    }

    public void Update()
    {
        hpBar.transform.LookAt(Player.transform);
        hpBar.transform.Rotate(Vector3.up * 180);
    }

    public void TakeDamage(float damageTaken)
    {
        hp -= damageTaken;
        hpBar.value = hp;

        if (hp <= 0)
        {
            DestroyEntity();
        }
    }

    public void HealDamages(float damageHeal)
    {
        hp += damageHeal;
        hp = hp > maxHp ? maxHp : hp;
    }

    public void ReduceSpeed(float valueSlow)
    {
        speed -= valueSlow;
        speed = speed < 0 ? 0 : speed;
    }

    public void ResetSpeed()
    {
        speed = originalSpeed;
    }

    private void DestroyEntity()
    {
        gameObject.SetActive(false);
    }
}
