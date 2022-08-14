using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBullet : MonoBehaviour
{
    //Assignables
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatIsEnemies;

    //Stats
    [Range(0f, 1f)]
    public float bounciness;
    public bool useGravity;

    //Damage
    public int explosionDamage;
    public float explosionRange;

    //Lifetime
    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouch = true;

    int collisions;
    PhysicMaterial physics_mat;

    private void Start()
    {
        Setup();
    }

    private void Update()
    {
        //When to explode
        if(collisions > maxCollisions)
        {
            Explode();
        }

        //Count down lifetime
        maxLifetime -= Time.deltaTime;
        if(maxLifetime <= 0)
        {
            Explode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {


        //Count up collisions
        collisions++;

        //Explode if bullet hits an enemy
        if(collision.collider.CompareTag("Enemy") && explodeOnTouch)
        {
            Explode();
        }

    }

    private void Explode()
    {
        //Instantiate explosion
        if(explosion != null)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }

        //Check for enemies
        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, whatIsEnemies);
        for (int i = 0; i < enemies.Length; i++)
        {
            //enemies[i].GetComponent<PlayerController>().TakeDamage(explosionDamage);
        }

        //Little delay to make sure everything works fine
        Invoke("Delay", 0.05f);
    }

    private void Delay()
    {
        Destroy(gameObject);
    }

    private void Setup()
    {
        //Create new physic material
        physics_mat = new PhysicMaterial();
        physics_mat.bounciness = bounciness;
        physics_mat.frictionCombine = PhysicMaterialCombine.Minimum;
        physics_mat.bounceCombine = PhysicMaterialCombine.Maximum;
        //Assign material to collider
        GetComponent<SphereCollider>().material = physics_mat;
        
        //Set gravity
        rb.useGravity = useGravity;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, explosionRange);
    }
}
