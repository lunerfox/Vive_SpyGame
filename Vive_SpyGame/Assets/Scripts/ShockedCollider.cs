using UnityEngine;
using System.Collections;

public class ShockedCollider : MonoBehaviour {

    public float shockTime;         //Amount of time to set when the robot is shocked.
    private float timeTilEnable;    //This time is > 0 when the robot is shocked. Counts down to 0 and enables the robot again

    ParticleSystem shockParticle;

    public GameObject electricBuzz;
    AudioSource AS_electricBuzz;

    // Use this for initialization
    void Start()
    {
        shockParticle = GetComponentInChildren<ParticleSystem>();
        AS_electricBuzz = electricBuzz.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeTilEnable > 0) timeTilEnable -= Time.deltaTime;
        else
        {
            GetComponent<NavMeshAgent>().Resume();
            GetComponent<BoxCollider>().enabled = true;
            shockParticle.Stop();
            AS_electricBuzz.Stop();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        print("Drone Hit by " + collision.collider.gameObject.name);
        if (collision.gameObject.tag == "Bullet(Clone)")
        {
            GetComponent<NavMeshAgent>().Stop();
            GetComponent<BoxCollider>().enabled = false;
            timeTilEnable = shockTime;
            shockParticle.Play();
            AS_electricBuzz.Play();
            Destroy(collision.collider.gameObject);
        }
        
    }
}
