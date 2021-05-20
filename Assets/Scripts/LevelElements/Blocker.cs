using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocker : MonoBehaviour {
	float blockedY = 0.5f;
	float restingY = -0.6f;

	Rigidbody rigidBody;
	new Collider collider;

	void Awake() {
		rigidBody = GetComponent<Rigidbody>();
		collider = GetComponent<Collider>();
	}

	public void block(bool enable) {
		if (enable)
			StartCoroutine(block());
		else
			StartCoroutine(rest());
	}

	IEnumerator block() {
		Vector3 position = rigidBody.position;
		position.y = 0;
		float interpolant = 0;
		while (true) {
			Vector3 target = Vector3.up * Mathf.Lerp(restingY, blockedY, interpolant);

			rigidBody.MovePosition(position + target);
			if (interpolant > 1) break;
			interpolant += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		collider.enabled = true;
	}

	IEnumerator rest() {
		Vector3 position = rigidBody.position;
		position.y = 0;
		float interpolant = 0;
		while (true) {
			Vector3 target = Vector3.up * Mathf.Lerp(blockedY, restingY, interpolant);
			rigidBody.MovePosition(position + target);
			if (interpolant > 1) break;
			interpolant += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
		collider.enabled = false;
	}
}
