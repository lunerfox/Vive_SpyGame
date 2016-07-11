using UnityEngine;
using System.Collections;

public class BulletImpact : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        print("Bullet Hit by " + collision.collider.gameObject.name);
        GetComponent<AudioSource>().Play();
    }
}
