using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckEnemies : MonoBehaviour
{
    public int enemies;
    public GameManager gameManager;
    public static CheckEnemies CheckEnemies1;
    private void Awake()
    {
        CheckEnemies1 = this;
    }
    private void Update()
    {
        
           
        if (enemies == 13)
        {
            gameManager.win();
            Destroy(gameObject);
        }
    }
}
