﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]

public class RandomAgent : MonoBehaviour {

    private NavMeshAgent agent;
    public GameObject droneSpotlight;

    private GameObject followTarget;

    const float fadeToRate = 0.8f;
    private float transition = 0;

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
            print("Target Acquired at " + followTarget.transform.position);
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

