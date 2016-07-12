using UnityEngine;
using System.Collections;
[RequireComponent(typeof(NavMeshAgent))]

public class WaypointAgent : MonoBehaviour {

    DroneManager Drone;
    private NavMeshAgent agent;
    public Vector3[] waypoints;
    private int curIndex;
    public GameObject droneSpotlight;
    private GameObject followTarget;

    //NOT USED AT THIS TIME;
    const float fadeToRate = 0.8f;
    private float transition = 0;


    // Use this for initialization
    void Start()
    {
        Drone = GetComponent<DroneManager>();
        agent = GetComponent<NavMeshAgent>();
        curIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

        // Check if we've reached the destination
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

        if (Drone.IsShocked)
        {
            print("Drone is shocked. Resetting");
            followTarget = null;
            droneSpotlight.GetComponent<Light>().color = Color.white;
        }

    }

    private void NewDestination()
    {
        print("Drone moving to new Destination");
        Vector3 newDest = waypoints[curIndex];
        NavMeshHit hit;
        
        if (followTarget != null)
        {
            print("Target Acquired at " + followTarget.transform.position);
            newDest = followTarget.transform.position;
        }
        else
        {
            curIndex++;
            if (curIndex == waypoints.Length) { curIndex = 0; }
        }

        bool hasDestination = NavMesh.SamplePosition(newDest, out hit, 100f, 1);
        if (hasDestination)
        {
            agent.SetDestination(hit.position);
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        //print("Drone sees something");
        if (other.gameObject.tag == "MainCamera")
        {
            if (transition <= 1) transition += Time.deltaTime * fadeToRate;
            droneSpotlight.GetComponent<Light>().color = Color.red;
            print("Waypoint Drone is now engaged");
            followTarget = other.gameObject;
        }
    }

}


