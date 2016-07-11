using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//To be used with a Gun Object

public class UIAmmoUpdate : MonoBehaviour {

    public GameObject GunObject;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = GunObject.GetComponent<DazeGun>().ammoCount.ToString();
	}
}
