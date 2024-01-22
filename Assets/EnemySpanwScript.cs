using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpanwScript : MonoBehaviour
{
    public GameObject[] enemies;
    bool spawned = false;
    public GameObject spwanpos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider collision)
    {
        if(!spawned && collision.gameObject.tag == "Player")
        {
            spawned = true;
            spwanpos.transform.position = transform.position;
            foreach (GameObject enemy in enemies)
            {
                enemy.SetActive(true);
            }
        }
    }
}
