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
		restart.onClick.AddListener(restartScene);
		home.onClick.AddListener(loadMainMenu);
	}

	void OnEnable() { pauseGame(true); }
	void OnDisable() { pauseGame(false); }

	void restartScene() {
		SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
	}

	void loadMainMenu() {
		SceneManager.LoadSceneAsync("Main Menu", LoadSceneMode.Single);
	}

	void pauseGame(bool paused) {
		if (paused)
			Time.timeScale = 0;
		else
			Time.timeScale = 1;
	}
}
