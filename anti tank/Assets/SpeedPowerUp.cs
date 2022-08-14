using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public GameObject weapon;
    public int powerUpDuration;

    float preTBS;
    float preRT;
    private void Awake()
    {
        preTBS = weapon.GetComponent<RocketLauncher>().timeBetweenShooting;
        preRT = weapon.GetComponent<RocketLauncher>().reloadTime;
    }


    private void OnTriggerEnter(UnityEngine.Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PowerUpManager.Instance.StartCoroutine(PowerUpManager.Instance.PowerUp(powerUpDuration));

            Destroy(this.gameObject);
        }
    }
}
