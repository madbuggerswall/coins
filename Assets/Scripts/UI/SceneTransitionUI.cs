using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTransitionUI : MonoBehaviour {
	static SceneTransitionUI instance;
	Animation animPlayer;

	bool toggle = false;
	void Awake() {
		assertSingleton();
		animPlayer = GetComponent<Animation>();
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.Space)) {
			toggle = !toggle;
			if (toggle) lighten();
			else darken();
		}
	}

	// Singleton 
	public static SceneTransitionUI getInstance() { return instance; }
	void assertSingleton() { if (instance == null) { instance = this; } else { Destroy(gameObject); } }

	public void lighten() {
		animPlayer.Play("LightenSceneTransition");
	}

	public void darken() {
		animPlayer.Play("DarkenSceneTransition");
	}
}
