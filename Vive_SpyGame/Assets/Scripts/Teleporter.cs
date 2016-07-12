using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour
{

    public GameObject particle;
    public GameObject spyCamera;
    private bool TeleporterOnFloor;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Rigidbody>().velocity.magnitude < 0.5 && TeleporterOnFloor)
        {
            //print("Teleporter now playing particle effect");
            Quaternion rotation = particle.transform.rotation;
            rotation.eulerAngles = Vector3.up;
            particle.GetComponent<ParticleSystem>().Play();
            spyCamera.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
            spyCamera.transform.rotation = FindObjectOfType<SteamVR_TestThrow>().transform.rotation;
        }
        else
        {
            particle.GetComponent<ParticleSystem>().Stop();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")    TeleporterOnFloor = true;
        else                                        TeleporterOnFloor = false;
    }
}
