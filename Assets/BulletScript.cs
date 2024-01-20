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

}
