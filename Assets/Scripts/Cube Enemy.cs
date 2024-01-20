using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEnemy : MonoBehaviour,IDamageable
{

    
    public float health = 100f;
    public float movementSpeed = 5f;
    public float rotationSpeed = 180f;
    public float minDistance = 3f;
    public float shootCooldown = 2f;
    public float bulletSpeed = 10f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Transform player;
    private float nextShootTime;
    private List<GameObject> bulletPool = new List<GameObject>();

    void Start()
    {
        // Find the player object based on the "Player" tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Set initial shoot time to allow the eye monster to shoot immediately
        nextShootTime = Time.time;

        // Initialize the bullet pool
        InitializeBulletPool();
    }

    void Update()
    {
        // Rotate the eye monster to face the player
        Vector3 lookDirection = player.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(lookDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);

        // Move the eye monster towards the player while maintaining a minimum distance
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > minDistance)
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }

        // Shoot at the player if enough time has passed
        if (Time.time >= nextShootTime)
        {
            Shoot();
        }
        if(health <= 0)
        {
            Destroy(this.gameObject);
        }   
    }

    void InitializeBulletPool()
    {
        // Populate the bullet pool with inactive bullets
        for (int i = 0; i < 10; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab);
            bullet.SetActive(false);
            bulletPool.Add(bullet);
        }
    }

    void Shoot()
    {
        // Calculate the direction from the eye monster to the player
        Vector3 shootDirection = (player.position - firePoint.position).normalized;

        // Instantiate a bullet prefab
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Get the Rigidbody component of the bullet
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        // Apply force to the bullet in the direction of the player
        bulletRb.AddForce(shootDirection * bulletSpeed*10, ForceMode.Impulse);

        // Set the next shoot time based on the cooldown
        nextShootTime = Time.time + shootCooldown;
    }

    GameObject GetInactiveBullet()
    {
        // Find the first inactive bullet in the pool
        foreach (GameObject bullet in bulletPool)
        {
            if (!bullet.activeSelf)
            {
                return bullet;
            }
        }

        // If no inactive bullets are found, instantiate a new one and add it to the pool
        GameObject newBullet = Instantiate(bulletPrefab);
        newBullet.SetActive(false);
        bulletPool.Add(newBullet);

        return newBullet;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
    }
}
