using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BoosterRotation {
	half = 180,
	quarter = 90
}

public class RotatingBooster : MonoBehaviour {
	[SerializeField] float force = 36;
	[SerializeField] float stuckThreshold = 4;
	[SerializeField] bool isClockwise;
	[SerializeField] BoosterRotation boosterRotation;
	float entryTime;

	void Awake() {
		// LevelManager.getInstance().events.playerContinuesTurn.AddListener(rotate);
		LevelManager.getInstance().events.playerShotValid.AddListener(rotate);
	}

	void OnTriggerEnter(Collider other) {
		StartCoroutine(changeColor(Color.gray, Color.white));
		entryTime = Time.time;
	}

	void OnTriggerStay(Collider other) {
		other.attachedRigidbody.AddForce(transform.forward * force);
		if (Time.time - entryTime > stuckThreshold) {
			other.attachedRigidbody.AddForce(-Vector3.right * force);
		}
	}

	void OnTriggerExit(Collider other) {
		StartCoroutine(changeColor(Color.white, Color.gray));
	}

	void rotate() {
		if (isClockwise)
			transform.eulerAngles += Vector3.up * (float) boosterRotation;
		else
			transform.eulerAngles -= Vector3.up * (float) boosterRotation;
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
