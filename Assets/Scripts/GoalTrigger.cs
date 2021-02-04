using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour {
	Collider trigger;
	bool scored = false;

	[SerializeField] float dragMultiplier = 8;
	void Awake() {
		trigger = GetComponent<Collider>();
	}


	void OnTriggerEnter(Collider other) {
		// IDEA: Zoom in
		other.GetComponent<Coin>().multiplyDrag(dragMultiplier);
	}

	void OnTriggerStay(Collider other) {
		if (trigger.bounds.Contains(other.transform.position) && !scored) {
			scored = true;
			FindObjectOfType<Match>().playerScored.Invoke();
		}
	}

	void OnTriggerExit(Collider other) {
		other.GetComponent<Coin>().multiplyDrag(1f / dragMultiplier);
		scored = false;
	}
}
