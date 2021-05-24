using UnityEngine;
using System;

[Serializable]
public class StageManager {
	[SerializeField] Stage japaneseGarden;
	Level lastLoadedLevel;

	public StageManager() {
		japaneseGarden = new Stage("Scenes/Puzzle Levels/Japanese Garden", "Japanese Garden", 1);

		Level level1 = new Level("/Puzzle 1", "First Slings", 1);
		Level level2 = new Level("/Puzzle 2", "Cards", 2);
		Level level3 = new Level("/Puzzle 3", "Triggered", 3);

		japaneseGarden.addLevel(level1);
		japaneseGarden.addLevel(level2);
		japaneseGarden.addLevel(level3);
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

	public Stage getStage() { return japaneseGarden; }
}