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
		play.onClick.AddListener(() => pauseGame(!gameObject.activeSelf));
		restart.onClick.AddListener(() => {
			pauseGame(false);
			GameManager.getInstance().levels.restartLevel();
		});
		home.onClick.AddListener(() => {
			pauseGame(false);
			GameManager.getInstance().levels.loadMainMenu();
		});
	}

	public void pauseGame(bool paused) {
		if (paused) {
			Time.timeScale = 0;
			LevelManager.getInstance().getGame().getCoinSet().disableControls();
			gameObject.SetActive(paused);
		} else {
			Time.timeScale = 1;
			LevelManager.getInstance().getGame().getCoinSet().enableControls();
			gameObject.SetActive(paused);
		}
	}
}
