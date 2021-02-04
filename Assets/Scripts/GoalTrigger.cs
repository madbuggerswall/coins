using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour {
	Collider trigger;

	void Awake() {
		trigger = GetComponent<Collider>();
	}

	void OnTriggerEnter(Collider other) {
		GameObject.FindObjectOfType<Match>().playerScored.Invoke();
		Debug.Log("Goal");
	}

	private void OnTriggerStay(Collider other) {
		if (trigger.bounds.Contains(other.transform.position)) {
			Debug.Log("Scored");
		}
	}
}
