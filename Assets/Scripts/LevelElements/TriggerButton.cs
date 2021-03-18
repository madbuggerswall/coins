using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerButton : MonoBehaviour {
	[SerializeField] UnityEvent triggerEnter;
	[SerializeField] UnityEvent triggerExit;

	[SerializeField] Transform button;
	Vector3 initialButtonPos;
	Vector3 collapsedButtonPos;

	[SerializeField] float collapseSpeed;
	int coinCount = 0;

	void Start() {
		initialButtonPos = button.position;
		collapsedButtonPos = initialButtonPos - Vector3.up * .2f;
	}

	void OnTriggerEnter(Collider other) {
		coinCount++;
		triggerEnter.Invoke();
		StartCoroutine(collapse());

	}

	void OnTriggerExit(Collider other) {
		coinCount--;
		if (coinCount == 0) {
			triggerExit.Invoke();
			StartCoroutine(uncollapse());
		}
	}

	IEnumerator collapse() {
		Vector3 initialPosition = button.position;
		float interpolant = Mathf.InverseLerp(initialButtonPos.y, collapsedButtonPos.y, button.transform.position.y);
		while (true) {
			Vector3 position = Vector3.Lerp(initialPosition, collapsedButtonPos, interpolant);
			button.position = position;

			if (interpolant > 1f) break;
			interpolant += Time.deltaTime * collapseSpeed;
			yield return new WaitForEndOfFrame();
		}
	}

	IEnumerator uncollapse() {
		Vector3 initialPosition = button.position;
		float interpolant = 0;

		while (true) {
			Vector3 position = Vector3.Lerp(initialPosition, initialButtonPos, interpolant);
			button.position = position;

			if (interpolant > 1f) break;
			interpolant += Time.deltaTime * collapseSpeed;
			yield return new WaitForEndOfFrame();
		}
	}
}
