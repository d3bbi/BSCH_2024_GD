using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    public float scoreValue;
    public GameManagerScript gameManager;
    public CharacterControllerScript player;

    public bool damaging;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>(); // find the game manager and get the script
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterControllerScript>(); // find the player and get the script
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            gameManager.AddHealth(scoreValue);
            player.hurt = true;
        }
    }
}
