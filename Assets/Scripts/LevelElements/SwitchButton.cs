using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchButton : MonoBehaviour {
	[SerializeField] GameObject switchButton;

	[SerializeField] CollapsibleObstacle collapsibleObstacle;

	Animation animPlayer;
	Material switchMaterial;

	int coinCount = 0;
	bool isSwitchOn = false;

	const string turnGreen = "turnGreen";
	const string turnRed = "turnRed";

	void Awake() {
		initializeCollapsibleObstacle();
		switchMaterial = switchButton.GetComponent<Renderer>().material;
		animPlayer = GetComponent<Animation>();
	}

	void OnTriggerEnter(Collider other) {
		coinCount++;
	}

	void OnTriggerExit(Collider other) {
		coinCount--;
		if (coinCount == 0) {
			isSwitchOn = !isSwitchOn;
			if (isSwitchOn) {
				collapsibleObstacle.startCollapsing();
				animPlayer.Play(turnGreen);
			} else {
				collapsibleObstacle.startUncollapsing();
				animPlayer.Play(turnRed);
			}
		}
	}

	void initializeCollapsibleObstacle() {
		if (collapsibleObstacle == null)
			collapsibleObstacle = transform.parent.GetComponentInChildren<CollapsibleObstacle>();
	}
}
