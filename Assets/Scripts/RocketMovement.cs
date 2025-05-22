using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour
{
	Rigidbody rb;
	AudioSource audioSource;
	[SerializeField] float Thrust = 1000f;
	[SerializeField] float RotateSpeed = 50f;
	[SerializeField] AudioClip mainEngine;
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update()
	{
		ProcessThrust();
		ProcessRotation();
		ManageAudio();
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
			rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ; //Manually unset RotZ freeze. Why is there no method for this!? ToT
		}
		if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && !(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
		{
			rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionZ;
			transform.Rotate(Vector3.forward * Time.deltaTime * -RotateSpeed);
			rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionZ;
		}
	}
	void ManageAudio()
	{
		if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space)) || (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) ^ (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
		{
			if(!audioSource.isPlaying)
			{
				audioSource.PlayOneShot(mainEngine);
			}
		}
		else
		{
			if(audioSource.isPlaying)
			{
				audioSource.Stop();
			}
		}
	}
}
