using UnityEngine;
using System.Collections;
[RequireComponent(typeof(NavMeshAgent))]

public class WaypointAgent : MonoBehaviour {

    private NavMeshAgent agent;
    public Vector3[] waypoints;
    private int curIndex;

    // Use this for initialization
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        curIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

        // Check if we've reached the destination
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
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
        print("Drone moving to new Destination");
        Vector3 newDest = waypoints[curIndex];
        NavMeshHit hit;
        bool hasDestination = NavMesh.SamplePosition(newDest, out hit, 100f, 1);
        if (hasDestination)
        {
            agent.SetDestination(hit.position);
        }

        curIndex++;
        if (curIndex == waypoints.Length) {curIndex = 0;}
    }
}


