using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyNavmeshAI : MonoBehaviour
{
    public Transform player;
    public Animator animator;
    public UnityEngine.AI.NavMeshAgent agent;
    public float currentVelocity;
    private bool aggro;
    public bool destinationReached;
    public float destinationThreshold;

    public Transform[] patrolPoints;
    public float aggroTimer;
    public float patrolSpeed;
    public float aggroSpeed;
    public GameObject aggroUI;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        aggro = false;
        destinationReached = true;
        aggroUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentVelocity = agent.velocity.magnitude;
        animator.SetFloat("velocity", currentVelocity);

        if(aggro == true)
        {
            aggroUI.SetActive(true);
            agent.destination = player.position;
        }

        if(aggro == false && destinationReached == true)
        {
            aggroUI.SetActive(false);
            destinationReached = false;
            agent.speed = patrolSpeed;
            agent.destination = patrolPoints[Random.Range(0, patrolPoints.Length)].position;
        }

        if (Vector3.Distance(transform.position, agent.destination)< destinationThreshold)
        {
            destinationReached = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            aggro = true;
            agent.speed = aggroSpeed;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            agent.destination = player.position;
            StopCoroutine("AggroTimer");
            StartCoroutine("AggroTimer");
        }
    }

    IEnumerator AggroTimer()
    {
        yield return new WaitForSeconds(aggroTimer);
        aggro = false;
        destinationReached = true;
    }
}
