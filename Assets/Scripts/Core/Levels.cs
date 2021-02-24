using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;


public class Levels {
	List<int> puzzles = new List<int>();
	int mainMenuIndex = 0;
	int levelsPageIndex = 1;

	public Levels() {
		initializePuzzleIndices();
	}

	void initializePuzzleIndices() {
		for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++) {
			if (SceneUtility.GetScenePathByBuildIndex(i).Contains("Puzzle Levels")) {
				puzzles.Add(i);
			}
		}
	}

	public void loadMainMenu() { loadLevel(mainMenuIndex); }
	public void loadLevelsPage() { loadLevel(levelsPageIndex); }
	public void loadLevel(int index) { SceneManager.LoadScene(index, LoadSceneMode.Single); }
	public void loadPuzzle(int index) { loadLevel(puzzles[index]); }
	public void loadLastPuzzle() {
		// TODO [PlayerStats & SaveManager needs to be implmented]
	}
	public void loadNextLevel() {
		int index = SceneManager.GetActiveScene().buildIndex + 1;
		if (puzzles.Contains(index))
			loadLevel(index);
	}
	public void restartLevel() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}