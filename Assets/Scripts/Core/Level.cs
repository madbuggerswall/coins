using UnityEngine;
using System;

[Serializable]
public class Level {
	[SerializeField] readonly string levelPath;
	[SerializeField] readonly string levelName;
	[SerializeField] readonly int levelNumber;

	[SerializeField] int attempts;
	[SerializeField] bool isCompleted;
	[SerializeField] bool isUnlocked;
	[SerializeField] bool[] completion = { false, false, false };

	public Level(string levelPath, string levelName, int levelNumber) {
		this.levelName = levelName;
		this.levelNumber = levelNumber;
		this.levelPath = levelPath;

		isUnlocked = (levelNumber == 1);
	}

	public void incrementAttempts() { attempts++; }
	public void unlock() { isUnlocked = true; }

	// Getters
	public string getLevelPath() { return levelPath; }
	public string getLevelName() { return levelName; }
	public int getLevelNumber() { return levelNumber; }
	public bool[] getCompletion() { return completion; }
	public bool getIsCompleted() { return isCompleted; }
	public bool getIsUnlocked() { return isUnlocked; }
}