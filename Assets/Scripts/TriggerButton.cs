using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerButton : MonoBehaviour {
	[SerializeField] UnityEvent triggerEnter;
	[SerializeField] UnityEvent triggerExit;

	int coinCount = 0;
	void OnTriggerEnter(Collider other) {
		coinCount++;
		triggerEnter.Invoke();
	}

	void OnTriggerExit(Collider other) {
		coinCount--;
		if (coinCount == 0)
			triggerExit.Invoke();
	}
}
