using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
   
    private void Update()
    {
       //destroy bullet after 2 seconds
        Destroy(gameObject, 6f);
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
            damageable?.TakeDamage(5);
        }
        //destroy bullet on collision
        Destroy(gameObject);
    }

}
