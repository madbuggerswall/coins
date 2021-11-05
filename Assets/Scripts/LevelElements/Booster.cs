using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour {
	// Default is 36
	protected static float force = 36;

	void OnTriggerEnter(Collider other) {
		// StartCoroutine(changeColor(green, brightGreen));
	}

	void OnTriggerStay(Collider other) {
		other.attachedRigidbody.AddForceAtPosition(transform.forward * force, other.ClosestPoint(transform.position));
	}

	void OnTriggerExit(Collider other) {
		// StartCoroutine(changeColor(brightGreen, green));
	}

	protected IEnumerator changeColor(Color first, Color last) {
		float interpolant = 0;
		ParticleSystem.MainModule mainModule = GetComponentInChildren<ParticleSystem>().main;
		while (true) {
			mainModule.startColor = Color.Lerp(first, last, interpolant);
			if (interpolant > 1) break;
			interpolant += Time.deltaTime * 6;
			yield return new WaitForEndOfFrame();
		}
	}
}
