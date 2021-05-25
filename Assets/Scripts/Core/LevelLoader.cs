using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;


public class LevelLoader {
	int mainMenuIndex = 0;


	IEnumerator startLoadingLevel(int buildIndex) {
		SceneTransitionUI.getInstance().darken();
		float animLength = SceneTransitionUI.getInstance().GetComponent<Animation>().GetClip("LightenSceneTransition").length;
		yield return new WaitForSeconds(animLength);

		SceneManager.LoadScene(buildIndex);
	}

	public void loadLevel(string scenePath) {
		int buildIndex = SceneUtility.GetBuildIndexByScenePath(scenePath);
		GameManager.getInstance().StartCoroutine(startLoadingLevel(buildIndex));
	}

	public void loadLevel(int buildIndex) {
		GameManager.getInstance().StartCoroutine(startLoadingLevel(buildIndex));
	}

	public void loadMainMenu() { loadLevel("Scenes/Main Menu"); }

	public void loadLastPuzzle() {
		// TODO [PlayerStats & SaveManager needs to be implmented]
	}
	public void loadNextLevel() {
		loadLevel(SceneUtility.GetScenePathByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1));
	}
	public void restartLevel() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}