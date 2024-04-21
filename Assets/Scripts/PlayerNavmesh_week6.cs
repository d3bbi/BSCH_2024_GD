using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNavmesh_week6 : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent;
    public float currentVelocity;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentVelocity = agent.velocity.magnitude;

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
