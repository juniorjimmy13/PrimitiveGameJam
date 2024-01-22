using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderEnemy : MonoBehaviour,IDamageable
{
    public float movementSpeed = 5f;
    public float rotationSpeed = 180f;
    public float chargeDistance = 5f;
    public float chargeCooldown = 3f;
    public float health = 100f;
    public Material Material1;
    public GameObject explosion;

    private Transform player;
    private bool isCharging = false;
    private float nextChargeTime;

    void Start()
    {
        // Find the player object based on the "Player" tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Set initial charge time to allow the interceptor to charge immediately
        nextChargeTime = Time.time;
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
        // Rotate the pyramidal interceptor to face the player
        Vector3 lookDirection = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        // Move the pyramidal interceptor towards the player
        if (!isCharging)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer > chargeDistance)
            {
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
            else
            {
                // Start charging towards the player
                isCharging = true;
                nextChargeTime = Time.time + chargeCooldown;
            }
        }
        else
        {
            // Charge towards the player for a short duration
            transform.Translate(Vector3.forward * movementSpeed * 2f * Time.deltaTime);

            // Check if the charging duration has ended
            if (Time.time >= nextChargeTime)
            {
                isCharging = false;
            }
        }
        if (health <= 0)
        {
            CheckEnemies.CheckEnemies1.enemies++;
            AudioInstance.AudioInstance1.PlaySpecificAudioClip(AudioInstance.AudioInstance1.explosion);
            GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the pyramidal interceptor collided with the player
        if (other.gameObject.tag =="Player")
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            damageable?.TakeDamage(5);
            //apply force to the player
            AudioInstance.AudioInstance1.PlaySpecificAudioClip(AudioInstance.AudioInstance1.enemyhit);
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 10f);
        }
    }

    public void TakeDamage(float damage)
    {
        
        AudioInstance.AudioInstance1.PlaySpecificAudioClip(AudioInstance.AudioInstance1.enemyhit);
        health -= damage;
        StartCoroutine(flash());
    }
}
