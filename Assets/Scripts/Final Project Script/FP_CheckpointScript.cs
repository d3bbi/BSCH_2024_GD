using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_CheckpointScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")) // If the player collides with the checkpoint
        {
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<FP_GameManagerScript>().spawnPoint = transform; // Set the game manager's spawn point to this checkpoint 
            gameObject.GetComponent<Animator>().SetTrigger("CheckpointTriggered");
        }
    }
}
