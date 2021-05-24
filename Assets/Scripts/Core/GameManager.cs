using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-8)]
public class GameManager : MonoBehaviour {
	static GameManager instance;

	public LevelLoader levelLoader;
	public StageManager stageManager;

	void Awake() {
		assertSingleton();
		levelLoader = new LevelLoader();
		stageManager = StageManager.loadFromFile();

		Application.targetFrameRate = 60;
		DontDestroyOnLoad(this);
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
