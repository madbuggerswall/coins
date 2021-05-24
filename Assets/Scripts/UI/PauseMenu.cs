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
			GameManager.getInstance().levelLoader.restartLevel();
		});

		home.onClick.AddListener(() => {
			pauseGame(false);
			GameManager.getInstance().levelLoader.loadMainMenu();
		});

		sound.onClick.AddListener(() => {
			SoundManager.getInstance().toggleSound();
			toggleSoundImage();
		});
		music.onClick.AddListener(() => {
			SoundManager.getInstance().toggleMusic();
			toggleMusicImage();
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

	void toggleSoundImage() {
		GameObject soundOn = sound.transform.GetChild(0).gameObject;
		GameObject soundOff = sound.transform.GetChild(1).gameObject;
		soundOn.SetActive(!soundOn.activeSelf);
		soundOff.SetActive(!soundOff.activeSelf);
	}

	void toggleMusicImage() {
		GameObject musicOn = music.transform.GetChild(0).gameObject;
		GameObject musicOff = music.transform.GetChild(1).gameObject;
		musicOn.SetActive(!musicOn.activeSelf);
		musicOff.SetActive(!musicOff.activeSelf);
	}
}
