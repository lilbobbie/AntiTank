using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public float health;
    public int scorePoints;

    public GameObject projectile;
    public float projectileSpeed;
    public Transform attackPoint;

    public bool damageTaken;
    public float invulnerabiltyTime;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        //Check for sight and attackrange
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if(!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
        }
        if(playerInSightRange && !playerInAttackRange)
        {
            ChasePlayer();
        }
        if(playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    private void Patrolling()
    {
        if (!walkPointSet)
        {
            SearchWalkPoint();
        }
        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if(distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }
    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
        {
            walkPointSet = true;
        }
    }
    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    private void AttackPlayer()
    {
        //Make sure enemy doesn't move
        agent.SetDestination(transform.position);

        var lookPos = player.position;
        lookPos.y = -10.5f;
        transform.LookAt(lookPos);

        if (!alreadyAttacked)
        {
            //Atack code here
            Rigidbody rb = Instantiate(projectile, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);


            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        gameObject.GetComponentInChildren<Healthbar>().TakeDamage(damage);
        if(health == 0)
        {
            GameManager.Instance.score += scorePoints;
            Invoke(nameof(DestroyEnemy), .5f);
        }
    }
    private void DestroyEnemy()
    {
        EnemySpawn.Instance.currentSpawns--;
        GameObject.FindGameObjectWithTag("Player").GetComponent<VRGaze>().GVROff();
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet" && !damageTaken)
        {
            damageTaken = true;
            TakeDamage(collision.gameObject.GetComponent<CustomBullet>().explosionDamage);
            if(health <= collision.gameObject.GetComponent<CustomBullet>().explosionDamage)
            {
                Invoke("ResetDamage", 0f);
            }
            else
            {
                Invoke("ResetDamage", invulnerabiltyTime);
            }
        }
    }

    private void ResetDamage()
    {
        damageTaken = false;
    }

    /**public void OnMouseOver()
    {
        GameObject.FindWithTag("Player").GetComponent<VRGaze>().GVROn();
    }

    public void OnMouseExit()
    {
        GameObject.FindWithTag("Player").GetComponent<VRGaze>().GVROff();
    }**/

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
