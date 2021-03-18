using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsibleObstacle : MonoBehaviour {
	Rigidbody rigidBody;

	Vector3 initialPosition;
	Vector3 collapsedPosition;
	float collapseSpeed = 2;

	void Awake() {
		rigidBody = GetComponentInChildren<Rigidbody>();
		initialPosition = transform.position;
		Collider collider = GetComponentInChildren<Collider>();
		collapsedPosition = initialPosition - 2 * Vector3.up * collider.bounds.extents.y - Vector3.up; 
	}

	public void startCollapsing() { StartCoroutine(collapse()); }
	public void startUncollapsing() { StartCoroutine(uncollapse()); }

	IEnumerator collapse() {
		Vector3 initialPosition = transform.position;
		float interpolant = 0;
		while (true) {
			Vector3 position = Vector3.Lerp(initialPosition, collapsedPosition, interpolant);
			rigidBody.MovePosition(position);

			if (interpolant > 1f) break;
			interpolant += Time.deltaTime * collapseSpeed;
			yield return new WaitForEndOfFrame();
		}
	}

	IEnumerator uncollapse() {
		Vector3 initialPosition = transform.position;
		float interpolant = 0;
		while (true) {
			Vector3 position = Vector3.Lerp(initialPosition, this.initialPosition, interpolant);
			rigidBody.MovePosition(position);

			if (interpolant > 1f) break;
			interpolant += Time.deltaTime * collapseSpeed;
			yield return new WaitForEndOfFrame();
		}
	}
}
