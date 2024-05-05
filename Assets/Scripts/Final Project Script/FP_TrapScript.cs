using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_TrapScript : MonoBehaviour
{
    public float scoreValue;
    public FP_GameManagerScript gameManager;
    public FP_Player_Movement player;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FP_GameManagerScript>(); // find the game manager and get the script
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<FP_Player_Movement>(); // find the player and get the script
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            gameManager.AddScore(scoreValue);
            player.isDead = true;
            player.killedByEnemy = true;
        }

    }
}
