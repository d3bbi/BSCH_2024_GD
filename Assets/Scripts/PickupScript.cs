using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public float scoreValue;
    public GameManagerScript gameManager;
    public GameObject collectedEffect;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerScript>(); // find the game manager and get the script
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player")) // If the player collides with the pickup
        {
            gameManager.AddScore(scoreValue);
            Instantiate(collectedEffect, transform.position, transform.rotation); // Create the effect
            Destroy(gameObject);
        }
    }
}
