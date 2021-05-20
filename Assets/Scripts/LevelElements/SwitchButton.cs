using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchButton : MonoBehaviour {
	[SerializeField] GameObject switchButton;
	
	[SerializeField] UnityEvent activate;
	[SerializeField] UnityEvent deactivate;

	new Animation animation;
	Material switchMaterial;

	int coinCount = 0;
	bool isSwitchOn = false;

	const string turnGreen = "turnGreen";
	const string turnRed = "turnRed";

	void Awake() {
		switchMaterial = switchButton.GetComponent<Renderer>().material;
		animation = GetComponent<Animation>();
	}

	void OnTriggerEnter(Collider other) {
		coinCount++;
	}

	void OnTriggerExit(Collider other) {
		coinCount--;
		if (coinCount == 0) {
			isSwitchOn = !isSwitchOn;
			if (isSwitchOn) {
				activate.Invoke();
				animation.Play(turnGreen);
			} else {
				deactivate.Invoke();
				animation.Play(turnRed);
			}
		}
	}
}
