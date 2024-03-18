using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) // If the player collides with the checkpoint
        {
           GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>().spawnPoint = transform; // Set the game manager's spawn point to this checkpoint 
            gameObject.GetComponent<Animator>().SetTrigger("CheckpointTriggered");
        }
    }
}
