using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerableBooster : MonoBehaviour, ITriggerable {
	[SerializeField] float force = 36;
	[SerializeField] float stuckThreshold = 4;
	[SerializeField] bool isInverse;
	float entryTime;

	ParticleSystem particles;
	BoxCollider boxCollider;

	void Awake() {
		particles = GetComponentInChildren<ParticleSystem>();
		boxCollider = GetComponent<BoxCollider>();
		particles.gameObject.SetActive(isInverse);
		boxCollider.enabled = isInverse;
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

	public void trigger() {
		boxCollider.enabled = !isInverse;
		particles.gameObject.SetActive(!isInverse);
	}
	public void untrigger() {
		boxCollider.enabled = isInverse;
		particles.gameObject.SetActive(isInverse);
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
