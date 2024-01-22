using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    PlayerMovement playerScript;
    public GameObject player;
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public Transform spawnPoint;
    public GameObject tut;

    enum GameState
    {
        Playing,
        GameOver,
        Win
    }

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerMovement>();
        Invoke("switchofftut", 60f);
       
    }
    private void Update()
    {
        
        if (playerScript.currentHealth <= 0)
        {
            gameOverScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            player.SetActive(false);
            

        }
        //quit application
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
        

    }
    public void restart()
    {

        player.SetActive(true);
        player.transform.position = spawnPoint.position;
        playerScript.currentHealth = playerScript.maxHealth;
        gameOverScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;


    }
    public void win()
    {
        winScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        player.SetActive(false);
    }
    void switchofftut()
    {
        tut.SetActive(false);
    }
}  

        
