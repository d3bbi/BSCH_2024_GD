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
    public LayerMask explosionLayerMask;
    public bool isDead = false;
    public bool killedByEnemy = false;
    private Collider2D circleCollider;

    public FP_GameManagerScript gameManager;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // look for a component called Rigidbody2D and assign it to myRb
        anim = GetComponentInChildren<Animator>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FP_GameManagerScript>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
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
        else {
            circleCollider.isTrigger = true;
            movement.x = 0;
            movement.y = 0;
            anim.SetFloat("Speed", 0);
            if (killedByEnemy)
            {
                StartCoroutine(RespawnAfterDelay(2f)); // Start a coroutine to respawn after a delay
            }
        }

    }

    // FixedUpdate is called a fixed amount of times per second
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            isDead = true;
            StartCoroutine(RespawnAfterDelay(2f)); // Start a coroutine to respawn after a delay
        }
    }

    IEnumerator RespawnAfterDelay(float delay)
    {
        anim.SetBool("isDead", true);
        yield return new WaitForSeconds(delay);
        anim.SetBool("isDead", false);
        isDead = false;
        yield return new WaitForSeconds(0.1f);
        circleCollider.isTrigger = false;
        gameManager.Respawn();
    }

}