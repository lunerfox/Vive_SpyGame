using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]

public class RandomAgent : MonoBehaviour {

    private NavMeshAgent agent;
    private GameObject followTarget;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

        // Check if we've reached the destination or if we've spotted anyone
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance || followTarget != null)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    //Done
                    NewDestination();
                }
            }
        }
    }

    private void NewDestination()
    {
        Vector3 newDest = Random.insideUnitSphere * 500f + new Vector3(139, 86f, -172f);
        NavMeshHit hit;

        if (followTarget != null)
        {
            print("Target Acquired");
            newDest = followTarget.transform.position;
        }
        
        bool hasDestination = NavMesh.SamplePosition(newDest, out hit, 100f, 1);
        if (hasDestination)
        {
            agent.SetDestination(hit.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "MainCamera")
        {
            print("Random Drone is now engaged");
            followTarget = other.gameObject;
        }
    }
}

