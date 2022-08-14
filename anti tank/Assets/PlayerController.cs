using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject player;
    public bool isDamaged;
    public float health;
    public float invulnerabilityTime;
    public GameObject Healthbar;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.gamePaused)
            {
                GameManager.Instance.UpdateGameState(GameState.Play);
            }
            else
            {
                GameManager.Instance.UpdateGameState(GameState.Pause);
            }
        }
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
            Healthbar.GetComponent<Healthbar>().TakeDamage(damage);
            if(health <= 0)
            {
                GameManager.Instance.UpdateGameState(GameState.GameOver);
            }
            Invoke("ResetDamage", invulnerabilityTime);
        }
    }
    private void ResetDamage()
    {
        isDamaged = false;
    }
}
