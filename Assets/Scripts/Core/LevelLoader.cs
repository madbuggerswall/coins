using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;


public class LevelLoader {
	int mainMenuIndex = 0;

	public void loadMainMenu() { loadLevel(mainMenuIndex); }
	public void loadLevel(int index) { SceneManager.LoadScene(index, LoadSceneMode.Single); }
	public void loadLastPuzzle() {
		// TODO [PlayerStats & SaveManager needs to be implmented]
	}
	public void loadNextLevel() {
		loadLevel(SceneManager.GetActiveScene().buildIndex + 1);
	}
	public void restartLevel() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}