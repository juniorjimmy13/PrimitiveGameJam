using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollerBombs : MonoBehaviour,IDamageable
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 180f;
    public float explosionRadius = 3f;
    public float timeBeforeExplosion = 2f;
    public float damage = 20f;
    public float maxHealth = 20f;
    public Material Material1;
    public GameObject explosion;

    private Transform player;
    private bool isRolling = false;

    void Start()
    {
        // Find the player object based on the "Player" tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    IEnumerator flash()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        Material material = meshRenderer.material;
        meshRenderer.material = Material1;


        yield return new WaitForSeconds(0.1f);
        meshRenderer.material = material;
    }


    void Update()
    {
        // Rotate the sphere enemy to face the player
        Vector3 lookDirection = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        // Move the sphere enemy towards the player
        if (!isRolling)
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
        else
        {
            // Continue rolling towards the player
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
        if (maxHealth <= 0)
        {
            CheckEnemies.CheckEnemies1.enemies++;
            AudioInstance.AudioInstance1.PlaySpecificAudioClip(AudioInstance.AudioInstance1.explosion);
            GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // Check if the sphere enemy collided with the player
        if (other.gameObject.CompareTag("Player"))
        {
            // Start rolling towards the player
            isRolling = true;

            // Schedule the explosion after a certain time
            Invoke("Explode", timeBeforeExplosion);
        }
    }

    void Explode()
    {
        // Find all the colliders on the sphere enemy's explosion radius
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        // Go through all the colliders
        foreach (Collider nearbyObject in colliders)
        {
            // Check if the collider is an enemy
            IDamageable enemy = nearbyObject.GetComponent<IDamageable>();
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (enemy != null)
            {
                if(rb != null)
                {
                    rb.AddExplosionForce(1000f, transform.up, explosionRadius);
                    enemy.TakeDamage(damage);
                }
            }
        }
        GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity);
        CheckEnemies.CheckEnemies1.enemies++;
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        AudioInstance.AudioInstance1.PlaySpecificAudioClip(AudioInstance.AudioInstance1.enemyhit);
        maxHealth -= damage;
        StartCoroutine(flash());
    }
}
