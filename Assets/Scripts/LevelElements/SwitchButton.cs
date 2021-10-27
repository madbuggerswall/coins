using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwitchButton : MonoBehaviour {
	[SerializeField] GameObject switchButton;

	[SerializeField] List<ITriggerable> triggerables = new List<ITriggerable>();

	Animation animPlayer;
	Material switchMaterial;

	int coinCount = 0;
	bool isSwitchOn = false;

	const string turnGreen = "turnGreen";
	const string turnRed = "turnRed";

	void Awake() {
		initializeTriggerable();
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
				triggerAll();
				animPlayer.Play(turnGreen);
			} else {
				untriggerAll();
				animPlayer.Play(turnRed);
			}
		}
	}

	void triggerAll() {
		foreach (ITriggerable triggerable in triggerables) {
			triggerable.trigger();
		}
	}

	void untriggerAll() {
		foreach (ITriggerable triggerable in triggerables) {
			triggerable.untrigger();
		}
	}

	void initializeTriggerable() {
		if (triggerables.Count == 0)
			triggerables.AddRange(transform.parent.GetComponentsInChildren<ITriggerable>());
	}
}
