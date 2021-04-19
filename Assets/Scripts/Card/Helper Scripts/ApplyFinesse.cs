using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyFinesse : MonoBehaviour {
	bool left;

	Rigidbody rigidBody;
	Vector3 direction;
	Vector3 torqueDirection;

	void Awake() {
		rigidBody = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		float speed = rigidBody.velocity.magnitude;
		if (speed > 0.01f) {
			rigidBody.AddForce(direction * speed);
			rigidBody.AddTorque(torqueDirection * speed);
		}
	}

	public void setDirection(bool isLeft) {
		direction = isLeft ? Vector3.forward : -Vector3.forward;
		torqueDirection = isLeft ? Vector3.up : -Vector3.up;
	}
}
