using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class Stage {
	[SerializeField] readonly string stagePath;
	[SerializeField] readonly string stageName;
	[SerializeField] readonly int stageNumber;

	[SerializeField] bool isUnlocked;
	[SerializeField] bool isCompleted;

	[SerializeField] List<Level> levels;

	public Stage(string stagePath, string stageName, int stageNumber) {
		this.stagePath = stagePath;
		this.stageName = stageName;
		this.stageNumber = stageNumber;

		isUnlocked = (stageNumber == 1);
		isCompleted = false;

		levels = new List<Level>();
	}

	public void addLevel(Level level) { levels.Add(level); }
	public bool unlockNextLevel(Level level) {
		int index = levels.IndexOf(level) + 1;
		if(index >= levels.Count){
			return false;
		}else{
			levels[index].unlock();
			return true;
		}
	}
	// Getters
	public List<Level> getLevels() { return levels; }
	public string getStagePath() { return stagePath; }
}