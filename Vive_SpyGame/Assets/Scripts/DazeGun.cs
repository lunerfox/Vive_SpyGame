using UnityEngine;
using System.Collections;

public class DazeGun : MonoBehaviour
{
    public Transform BulletSpawnPoint;
    public GameObject BulletPrefab;
    public int StartingAmmo;
    public float BulletSpeed;
    public SteamVR_TrackedObject trackedObj;

    public int ammoCount { get; set; }

    void Start() {
        ammoCount = StartingAmmo;
    }

    // Update is called once per frame
    void Update() {
        var device = SteamVR_Controller.Input((int)trackedObj.index);
        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            //print("Right Controller has pulled the trigger");
            if (ammoCount > 0)
            {
                GameObject bullet = GameObject.Instantiate(BulletPrefab, BulletSpawnPoint.position, BulletSpawnPoint.rotation) as GameObject;
                bullet.GetComponent<Rigidbody>().velocity = BulletSpawnPoint.transform.up * BulletSpeed;
                GetComponent<AudioSource>().Play();
                //Haptic Feedback
                device.TriggerHapticPulse(3000);
                ammoCount--;
            }
        }
    }
}
