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
        if (collision.gameObject.tag == "MainCamera") { print("You got hit in the head, Bro!"); }
        else
        {
            GetComponent<AudioSource>().Play();
            Destroy(this.transform.gameObject);
        }
    }
}
