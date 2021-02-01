using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-64)]
public class Match : MonoBehaviour {
	static Match instance;
	public UnityEvent coinStatusChanged;
	public UnityEvent coinShot;

	void Awake() {
		assertSingleton();

		coinStatusChanged = new UnityEvent();
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