using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_PickupScript : MonoBehaviour
{
    public int bombAmount;
    public BombController bombScript;
    public Animator anim;
    public GameObject collectedEffect;
    private bool full = true;

    // Start is called before the first frame update
    void Start()
    {
        // find the game manager and get the script
        anim = GetComponent<Animator>();
        bombScript = GameObject.FindGameObjectWithTag("Player").GetComponent<BombController>();
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player") && full == true) // If the player collides with the pickup
        {
            Instantiate(collectedEffect, transform.position, transform.rotation); // Create the effect
            bombScript.AddBombs(bombAmount);
            anim.SetBool("Triggered", true);
            StartCoroutine(TurnOffTrigger(2f));
            full = false;
        }
    }

    IEnumerator TurnOffTrigger(float delay)
    {
        yield return new WaitForSeconds(delay);
        anim.SetBool("Triggered", false);
    }
}
