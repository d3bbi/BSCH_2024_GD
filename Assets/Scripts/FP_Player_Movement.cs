using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Player_Movement : MonoBehaviour
{
    // speed of the player
    public float speed = 5f;
    // get the Rigidbody
    public Rigidbody2D rb;
    Vector2 movement;
    public Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // look for a component called Rigidbody2D and assign it to myRb
        anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Input.GetAxisRaw("Horizontal");
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Vertical", movement.y);
        anim.SetFloat("Speed", movement.sqrMagnitude);

        if (Input.GetAxis("Horizontal") > 0.1f) // if the player is moving right
        {
            anim.transform.localScale = new Vector3(1, 1, 1); // flip the player sprite
        }
        if (Input.GetAxis("Horizontal") < -0.1f) // if the player is moving left
        {
            anim.transform.localScale = new Vector3(-1, 1, 1); // flip the player sprite
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}