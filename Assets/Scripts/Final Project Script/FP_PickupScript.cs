using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_PickupScript : MonoBehaviour
{
    public float scoreValue;
    public int bombAmount;
    public FP_GameManagerScript gameManager;
    public BombController bombScript;
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        // find the game manager and get the script
        anim = GetComponent<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FP_GameManagerScript>();
        bombScript = GameObject.FindGameObjectWithTag("Bomb").GetComponent<BombController>();
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player")) // If the player collides with the pickup
        {
            gameManager.AddScore(scoreValue);
            bombScript.AddBombs(bombAmount);
            anim.SetBool("Triggered", true);
            StartCoroutine(TurnOffTrigger(2f));
        }
    }

    IEnumerator TurnOffTrigger(float delay)
    {
        yield return new WaitForSeconds(delay);
        anim.SetBool("Triggered", false);
    }
}
