using UnityEngine;
using System.Collections;

//Stores the Drone's states for access.

public class DroneManager : MonoBehaviour {

    public bool IsShocked { get; set; }
    public bool IsTracking { get; set; }       //A Drone is either Tracking (true) or Roaming (false)

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
