using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyFinesse : MonoBehaviour {
	[SerializeField] bool left;
	
	Rigidbody rigidBody;
	Vector3 direction;

	void Awake() {
		rigidBody = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		float speed = rigidBody.velocity.magnitude;
		if (speed > 0.01f)
			rigidBody.AddForce(-Vector3.forward * speed * 1f);
	}
}
