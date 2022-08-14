using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public GameObject weapon;
    public int powerUpDuration;
    public float speed;
    public Vector3 direction;
    public Transform seekTarget;
    public float fleeDistance = 5;

    float preTBS;
    float preRT;
    private void Awake()
    {
        preTBS = weapon.GetComponent<RocketLauncher>().timeBetweenShooting;
        preRT = weapon.GetComponent<RocketLauncher>().reloadTime;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, seekTarget.position) < fleeDistance)
        {
            Vector3 newDirection = transform.position - seekTarget.position;
            newDirection = new Vector3(newDirection.x, 0, newDirection.z);
            newDirection = newDirection.normalized;
            direction = newDirection;
        }
        else
        {
            direction = Vector3.zero;
        }
        transform.position += speed * direction * Time.deltaTime;
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
