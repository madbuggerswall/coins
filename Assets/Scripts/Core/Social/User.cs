using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

class User {
	string username;
	string email;

	Stats stats;
	Achievements achievements;
	List<Level> completedLevels;
}

[Serializable]
class Level {
	readonly string levelPath;
	readonly string levelName;
	readonly int levelNumber;

	[SerializeField] int attempts;
	[SerializeField] bool isCompleted;
	[SerializeField] bool isUnlocked;
	[SerializeField] bool[] completion = { false, false, false };

	Level() { }
	public Level(string levelPath, string levelName, int levelNumber) {
		this.levelName = levelName;
		this.levelNumber = levelNumber;
		this.levelPath = levelPath;
	}
}

[Serializable]
class Stage {
	readonly string stageName;
	readonly string stageNumber;
	
	bool isUnlocked;
	bool isCompleted;

	List<Level> levels;
}