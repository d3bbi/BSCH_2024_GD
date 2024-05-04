using System.Collections;
using UnityEngine;

public class FP_EnemyAIScript : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float chaseSpeed = 8f;
    public float detectedPlayerTime = 3f;
    public float aggroTime = 5f;

    private Rigidbody2D rb;
    private bool moving = false;
    private Vector2 currentDirection;
    public Animator anim;
    private Transform playerTransform;
    private bool playerDetected = false;
    private bool aggro = false;
    private bool isDead = false;
    public LayerMask gridLayerMask;

    //get the child collider
    public Collider2D childCollider;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponentInChildren<Animator>();
        childCollider = GetComponentInChildren<Collider2D>();
    }

    void FixedUpdate()
    {
        if (playerDetected)
        {
            // MoveTowardsPlayer();
        }
        else if(isDead){
            return;
        }
        else if (!moving)
        {
            // Randomly choose between moving left-right or up-down
            bool moveHorizontal = Random.value < 0.5f;

            if (moveHorizontal)
            {
                // Choose left or right direction
                currentDirection = new Vector2(Random.Range(-1f, 1f), 0f).normalized;
                anim.SetFloat("Horizontal", currentDirection.x);
                anim.SetFloat("Vertical", 0f);
            }
            else
            {
                // Choose up or down direction
                currentDirection = new Vector2(0f, Random.Range(-1f, 1f)).normalized;
                anim.SetFloat("Horizontal", 0f);
                anim.SetFloat("Vertical", currentDirection.y);
            }

            //if it move on the left, change the sprite
            if (currentDirection.x < 0)
            {
                anim.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                anim.transform.localScale = new Vector3(1, 1, 1);
            }

            StartCoroutine(MoveInDirection(currentDirection, Random.Range(1f, 3f))); // Move for 1-3 seconds
        }
    }


    IEnumerator MoveInDirection(Vector2 direction, float duration)
    {
        moving = true;
        rb.velocity = direction * moveSpeed;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            if (Physics2D.Raycast(transform.position, direction, 0.5f, gridLayerMask))
            {
                rb.velocity = Vector2.zero;
                moving = false;
                yield break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        rb.velocity = Vector2.zero;
        moving = false;
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (playerTransform.position - transform.position).normalized;
        float speed = aggro ? chaseSpeed : moveSpeed;
        rb.velocity = direction * speed;

        if (direction.y > 0)
        {
            anim.SetFloat("Horizontal", 0f);
            anim.SetFloat("Vertical", 1f);
        }
        else if (direction.y < 0)
        {
            anim.SetFloat("Horizontal", 0f);
            anim.SetFloat("Vertical", -1f);
        }
        else
        {
            anim.SetFloat("Horizontal", direction.x);
            anim.SetFloat("Vertical", 0f);
                    // Rotate towards the player
            if (direction.x < 0)
            {
                anim.transform.localScale = new Vector3(-1, 1, 1);
            }
            else
            {
                anim.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = true;
            StartCoroutine(DetectTimer());
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Explosion"))
        {
            anim.SetBool("isDead", true);
            isDead = true;
            StartCoroutine(Destroy(2.5f)); // Start a coroutine to respawn after a delay
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerDetected = false;
        }
    }

    IEnumerator DetectTimer()
    {
        yield return new WaitForSeconds(detectedPlayerTime);
        if (playerDetected)
        {
            aggro = true;
        }
    }

    IEnumerator Destroy(float delay)
    {
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(delay);
        anim.SetBool("isDead", false);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

}