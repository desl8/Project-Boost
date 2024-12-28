using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
	Rigidbody rb;
	float Thrust = 1000f;
	float RotateSpeed = 50f;
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		ProcessThrust();
		ProcessRotation();
	}

	void ProcessThrust()
	{
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space))
		{
			rb.AddRelativeForce(Vector3.up * Time.deltaTime * Thrust);
		}
	}

	void ProcessRotation()
	{
		if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && !(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))) //Unbiased directional detection
		{
			rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ; //Force input rotation
			transform.Rotate(Vector3.forward * Time.deltaTime * RotateSpeed);
			rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ; //Manually unset RotZ freeze. Why is there no method for this!? FML
		}
		if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
		{
			rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ;
			transform.Rotate(Vector3.forward * Time.deltaTime * -RotateSpeed);
			rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
		}
	}
}
