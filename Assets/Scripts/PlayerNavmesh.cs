using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNavmesh : MonoBehaviour
{
    public Animator animator;
    public UnityEngine.AI.NavMeshAgent agent;
    public float currentVelocity;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentVelocity = agent.velocity.magnitude;
        animator.SetFloat("velocity", currentVelocity);
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GetComponent<UnityEngine.AI.NavMeshAgent>().destination = hit.point;
            }
        }

    }
}
