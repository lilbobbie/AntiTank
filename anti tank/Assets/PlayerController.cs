using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public bool isDamaged;
    public float health;
    public float invulnerabilityTime;

    private void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "EnemyBullet")
        {
            TakeDamage(collision.gameObject.GetComponent<CustomBullet>().explosionDamage);
        }
    }
    public void TakeDamage(int damage)
    {
        if (!isDamaged)
        {
            isDamaged = true;
            health = health - damage;
            Invoke("ResetDamage", invulnerabilityTime);
        }
    }
    private void ResetDamage()
    {
        isDamaged = false;
    }
}
