using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
	[SerializeField] Button play;
	[SerializeField] Button restart;
	[SerializeField] Button sound;
	[SerializeField] Button music;
	[SerializeField] Button home;

	void Awake() {
		play.onClick.AddListener(() => gameObject.SetActive(!gameObject.activeSelf));
		restart.onClick.AddListener(GameManager.getInstance().levels.restartLevel);
		home.onClick.AddListener(GameManager.getInstance().levels.loadMainMenu);
	}

	void OnEnable() {
		Debug.Log("Pause Enable");
		pauseGame(true);
	}
	void OnDisable() {
		Debug.Log("Pause Disable");
		pauseGame(false);
	}

	void pauseGame(bool paused) {
		if (paused) {
			Time.timeScale = 0;
			LevelManager.getInstance().getGame().getCoinSet().disableControls();
		} else {
			Time.timeScale = 1;
			LevelManager.getInstance().getGame().getCoinSet().enableControls();
		}
	}
}
