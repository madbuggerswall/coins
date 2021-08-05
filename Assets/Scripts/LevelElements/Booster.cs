using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour {
	[SerializeField] float force = 36;

	float entryTime;
	float stuckThreshold = 4;
	void Awake() {
	}

	void OnTriggerEnter(Collider other) {
		StartCoroutine(changeColor(Color.gray, Color.white));
		entryTime = Time.time;
		Debug.Log(entryTime);
	}

	void OnTriggerStay(Collider other) {
		other.attachedRigidbody.AddForce(transform.forward * force);
		if (Time.time - entryTime > stuckThreshold) {
			Debug.Log("Stuck " + entryTime);
			other.attachedRigidbody.AddForce(-Vector3.right * force);
		}
	}

	void OnTriggerExit(Collider other) {
		StartCoroutine(changeColor(Color.white, Color.gray));
	}

	IEnumerator changeColor(Color first, Color last) {
		float interpolant = 0;
		while (true) {
			// material.color = Color.Lerp(first, last, interpolant);
			if (interpolant > 1) break;
			interpolant += Time.deltaTime * 6;
			yield return new WaitForEndOfFrame();
		}
	}
}
