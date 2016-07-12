using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteamVR_TrackedObject))]
public class SteamVR_TestThrow : MonoBehaviour
{
	public GameObject prefab;
	public Rigidbody attachPoint;
    public GameObject cameraRig;
    public GameObject cameraScreen;

    SteamVR_TrackedObject trackedObj;
	FixedJoint joint;
    GameObject go;
    

    void Awake()
	{
		trackedObj = GetComponent<SteamVR_TrackedObject>();
	}

	void FixedUpdate()
	{
		var device = SteamVR_Controller.Input((int)trackedObj.index);
		if (joint == null && go == null && device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
		{
			go = GameObject.Instantiate(prefab);
			go.transform.position = attachPoint.transform.position;

			joint = go.AddComponent<FixedJoint>();
			joint.connectedBody = attachPoint;
		}
		else if (joint != null && go != null && device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
		{
			go = joint.gameObject;
			var rigidbody = go.GetComponent<Rigidbody>();
			Object.DestroyImmediate(joint);
			joint = null;
			//Object.Destroy(go, 15.0f);

			// We should probably apply the offset between trackedObj.transform.position
			// and device.transform.pos to insert into the physics sim at the correct
			// location, however, we would then want to predict ahead the visual representation
			// by the same amount we are predicting our render poses.

			var origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
			if (origin != null)
			{
				rigidbody.velocity = origin.TransformVector(device.velocity);
				rigidbody.angularVelocity = origin.TransformVector(device.angularVelocity);
			}
			else
			{
				rigidbody.velocity = device.velocity;
				rigidbody.angularVelocity = device.angularVelocity;
			}

			rigidbody.maxAngularVelocity = rigidbody.angularVelocity.magnitude;
            //Destroy(go, 15.0f);
        }
        else if (joint == null && go != null && go.GetComponent<Rigidbody>().velocity.magnitude < 0.5)
        {
            //device.TriggerHapticPulse(3000);
            go.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            //print("Teleporter has stopped moving at " + go.transform.position);
            cameraScreen.transform.localScale = new Vector3(1, 1, 1);
            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Grip)) {
                print("Teleporting");
                GetComponent<AudioSource>().Play();
                cameraRig.transform.position = go.transform.position;
                Destroy(go);
                cameraScreen.transform.localScale = new Vector3(0, 0, 0);
            }
            if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                print("Teleport Cancelled");
                Destroy(go);
                cameraScreen.transform.localScale = new Vector3(0, 0, 0);
            }
        }
	}
}
