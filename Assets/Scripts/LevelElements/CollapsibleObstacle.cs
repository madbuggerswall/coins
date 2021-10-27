using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollapsibleObstacle : MonoBehaviour, ITriggerable {
	Rigidbody rigidBody;

	Vector3 uncollapsedPosition;
	Vector3 collapsedPosition;
	float collapseSpeed = 4;

	[SerializeField] bool isInverse;
	void Awake() {
		rigidBody = GetComponentInChildren<Rigidbody>();
		uncollapsedPosition = rigidBody.position;
		Collider collider = GetComponentInChildren<Collider>();
		collapsedPosition = uncollapsedPosition - 2 * Vector3.up * collider.bounds.extents.y - Vector3.up;
		if (isInverse) StartCoroutine(collapse());
	}

	public void trigger() {
		if (isInverse)
			StartCoroutine(uncollapse());
		else
			StartCoroutine(collapse());
	}

	public void untrigger() {
		if (isInverse)
			StartCoroutine(collapse());
		else
			StartCoroutine(uncollapse());
	}

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
		while (true) {
			Vector3 position = Vector3.Lerp(collapsedPosition, uncollapsedPosition, interpolant);
			rigidBody.MovePosition(position);

			if (interpolant > 1f) break;
			interpolant += Time.deltaTime * collapseSpeed;
			yield return new WaitForEndOfFrame();
		}
	}
}
