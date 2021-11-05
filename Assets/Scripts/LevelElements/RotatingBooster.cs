using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoosterRotation {
	half = 180,
	quarter = 90
}

public class RotatingBooster : Booster {
	[SerializeField] protected bool isClockwise;
	[SerializeField] protected BoosterRotation boosterRotation;

	void Awake() {
		LevelManager.getInstance().events.playerShotValid.AddListener(rotate);
	}

	protected void rotate() {
		if (isClockwise)
			transform.eulerAngles += Vector3.up * (float) boosterRotation;
		else
			transform.eulerAngles -= Vector3.up * (float) boosterRotation;
	}
}
