using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class StageManager {
	[SerializeField] List<Stage> stages;
	[SerializeField] Stage japaneseGarden;

	Level lastLoadedLevel;

	public StageManager() {
		japaneseGarden = new Stage("Scenes/Puzzle Levels/Japanese Garden", "Japanese Garden", 1);
		stages = new List<Stage>();
		stages.Add(japaneseGarden);

		Level level1 = new Level(japaneseGarden.getStagePath(), "Puzzle 1", "First Slings", 1);
		Level level2 = new Level(japaneseGarden.getStagePath(), "Puzzle 2", "Cards", 2);
		Level level3 = new Level(japaneseGarden.getStagePath(), "Puzzle 3", "Naming Convention", 3);
		Level level4 = new Level(japaneseGarden.getStagePath(), "Puzzle 4", "Alice & Bob", 4);
		Level level5 = new Level(japaneseGarden.getStagePath(), "Puzzle 5", "Untrain", 5);
		Level level6 = new Level(japaneseGarden.getStagePath(), "Puzzle 6", "Overtrain", 6);

		japaneseGarden.addLevel(level1);
		japaneseGarden.addLevel(level2);
		japaneseGarden.addLevel(level3);
		japaneseGarden.addLevel(level4);
		japaneseGarden.addLevel(level5);
		japaneseGarden.addLevel(level6);
	}

	// Returns a new StageManager if file does not exist.
	public static StageManager loadFromFile() {
		if (SaveManager.exists(FilePath.stageManager)) {
			StageManager stageManager = SaveManager.load<StageManager>(FilePath.stageManager);
			return stageManager;
		} else {
			return new StageManager();
		}
	}

	public void unlockNextLevel() {
		bool nextLevelUnlocked = japaneseGarden.unlockNextLevel(lastLoadedLevel);
		if (!nextLevelUnlocked) {
			// TODO: Unlock next stage
		}
	}

	// Getters
	public Stage getStage() { return japaneseGarden; }
	public Level getLastLoadedLevel() { return lastLoadedLevel; }
	// Setters
	public void setLastLoadedLevel(Level level) { lastLoadedLevel = level; }
}