using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
	Rigidbody rigidBody;

	void Awake() {
		rigidBody = GetComponent<Rigidbody>();
	}

	public void setDrag(float drag) { rigidBody.drag = drag; }
}
