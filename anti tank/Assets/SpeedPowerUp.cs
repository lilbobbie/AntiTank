using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : MonoBehaviour
{
    public GameObject weapon;
    public int powerUpDuration;
    public float speed;
    public Vector3 direction;
    public Transform fleeFrom;
    public float fleeDistance = 5f;
    public float wanderRadius = 3f;
    public UnityEngine.AI.NavMeshAgent agent;
    private Vector3 wanderPoint;

    float preTBS;
    float preRT;
    private void Awake()
    {
        preTBS = weapon.GetComponent<RocketLauncher>().timeBetweenShooting;
        preRT = weapon.GetComponent<RocketLauncher>().reloadTime;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, fleeFrom.position) < fleeDistance)
        {
            Vector3 newDirection = transform.position - fleeFrom.position;
            newDirection = new Vector3(newDirection.x, 0, newDirection.z);
            newDirection = newDirection.normalized;
            direction = newDirection;
        }
        else
        {
            Wander();
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

    public void Wander() 
    {
        wanderPoint = RandomWanderPoint();
        if (Vector3.Distance(transform.position, wanderPoint) < 10f)
        {
            wanderPoint = RandomWanderPoint();
        }
        else
        {
            agent.SetDestination(wanderPoint); 
        }
    } 

    public Vector3 RandomWanderPoint()
    {
        Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
        UnityEngine.AI.NavMeshHit navHit;
        UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);
        return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
    }


}
