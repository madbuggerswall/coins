using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionUI : MonoBehaviour {
	static SceneTransitionUI instance;

	bool toggle = false;

	void OnEnable() {
		Debug.Log("OnEnable SceneTransitionUI");
		assertSingleton();
	}

	// Singleton 
	public static SceneTransitionUI getInstance() { return instance; }
	void assertSingleton() { if (instance == null) { instance = this; } else { Destroy(gameObject); } }

	public void lighten() {
		GetComponent<Animation>().Play("LightenSceneTransition");
	}

	public void darken() {
		GetComponent<Animation>().Play("DarkenSceneTransition");
	}
}
