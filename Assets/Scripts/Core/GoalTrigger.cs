using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour {
	Collider trigger;
	bool scored = false;

	[SerializeField] float drag = 4;

	void Awake() {
		trigger = GetComponent<Collider>();
	}


	void OnTriggerEnter(Collider other) {
		// IDEA: Zoom in
		other.GetComponent<Coin>().setDrag(drag);
	}

	// Generic events and game-type-specific events.
	void OnTriggerStay(Collider other) {
		if (trigger.bounds.Contains(other.transform.position) && !scored) {
			scored = true;
			LevelManager.getInstance().events.coinShotInGoal.Invoke();
		}
	}

	void OnTriggerExit(Collider other) {
		other.GetComponent<Coin>().setDrag(0);
		scored = false;
	}
}
