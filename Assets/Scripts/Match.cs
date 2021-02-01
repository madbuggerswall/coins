using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Match : MonoBehaviour {
	static Match instance;
	public UnityEvent coinSelected;
	public UnityEvent coinDeselected;
	public UnityEvent coinShot;

	void Awake() {
		assertSingleton();

		coinSelected = new UnityEvent();
		coinDeselected = new UnityEvent();
		coinShot = new UnityEvent();
	}

	// Singleton utilities
	void assertSingleton() {
		if (instance == null) { instance = this; } else { Destroy(gameObject); }
	}

	public static Match getInstance() {
		return instance;
	}
}