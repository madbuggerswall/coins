using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsibleObstacle : MonoBehaviour {
	Rigidbody rigidBody;

	Vector3 uncollapsedPosition;
	Vector3 collapsedPosition;
	float collapseSpeed = 2;

	void Awake() {
		rigidBody = GetComponentInChildren<Rigidbody>();
		uncollapsedPosition = rigidBody.position;
		Debug.Log(uncollapsedPosition);
		Collider collider = GetComponentInChildren<Collider>();
		collapsedPosition = uncollapsedPosition - 2 * Vector3.up * collider.bounds.extents.y - Vector3.up;
	}

	public void startCollapsing() { StartCoroutine(collapse()); }
	public void startUncollapsing() { StartCoroutine(uncollapse()); }

	IEnumerator collapse() {
		float interpolant = Mathf.InverseLerp(uncollapsedPosition.y, collapsedPosition.y, transform.position.y); ;
		while (true) {
			Vector3 position = Vector3.Lerp(uncollapsedPosition, collapsedPosition, interpolant);
			rigidBody.MovePosition(position);

			if (interpolant > 1f) break;
			interpolant += Time.deltaTime * collapseSpeed;
			yield return new WaitForEndOfFrame();
		}
	}

	IEnumerator uncollapse() {
		float interpolant = Mathf.InverseLerp(uncollapsedPosition.y, collapsedPosition.y, transform.position.y);
		Debug.Log(interpolant);
		while (true) {
			Vector3 position = Vector3.Lerp(collapsedPosition, uncollapsedPosition, interpolant);
			rigidBody.MovePosition(position);

			if (interpolant > 1f) break;
			interpolant += Time.deltaTime * collapseSpeed;
			yield return new WaitForEndOfFrame();
		}
		Debug.Log(rigidBody.position);
	}
}
