using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-8)]
public class GameManager : MonoBehaviour {
	static GameManager instance;

	public Levels levels;

	void Awake() {
		assertSingleton();
		levels = new Levels();

		Application.targetFrameRate = 60;
	}

	void OnApplicationPause(bool pauseStatus) {

	}

	void OnApplicationFocus(bool focusStatus) {

	}

	void OnApplicationQuit() {

	}

	// Singleton 
	public static GameManager getInstance() { return instance; }
	void assertSingleton() { if (instance == null) { instance = this; } else { Destroy(gameObject); } }
}
