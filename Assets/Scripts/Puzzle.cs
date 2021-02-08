using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle : MonoBehaviour {
	public UnityEvent playerShotEnded;
	void Awake() {
		playerShotEnded = new UnityEvent();
	}
}
