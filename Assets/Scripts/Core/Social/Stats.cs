using System;
using UnityEngine;

[Serializable]
public class Stats {
	[SerializeField] int coinsShot;
	[SerializeField] int obstaclesHit;
	[SerializeField] int cardsPlayed;
	[SerializeField] int collectiblesFound;
	[SerializeField] int foulsCommitted;

	Stats() { }

	// Initializer needed in order to contruct after serialization.
	public void initialize() {
		constructFromFile();
		subscribeToEvents();
	}

	void constructFromFile() {
		if (SaveManager.exists(FilePath.stats)) {
			Stats stats = SaveManager.load<Stats>(FilePath.stats);
			coinsShot = stats.coinsShot;
			obstaclesHit = stats.obstaclesHit;
			cardsPlayed = stats.cardsPlayed;
			collectiblesFound = stats.collectiblesFound;
			foulsCommitted = stats.foulsCommitted;
		} else {
			coinsShot = 0;
			obstaclesHit = 0;
			cardsPlayed = 0;
			collectiblesFound = 0;
			foulsCommitted = 0;
		}
	}

	public static Stats loadFromFile() {
		if (SaveManager.exists(FilePath.stats)) {
			Stats stats = SaveManager.load<Stats>(FilePath.stats);
			return stats;
		} else {
			return new Stats();
		}
	}

	void subscribeToEvents() {
		LevelManager.getInstance().events.coinShot.AddListener(incrementCoinsShot);
		LevelManager.getInstance().events.cardPlayed.AddListener(incrementCardsPlayed);
		LevelManager.getInstance().events.playerFouled.AddListener(incrementFoulsCommitted);
		LevelManager.getInstance().events.obstacleHit.AddListener(incrementObstaclesHit);
		LevelManager.getInstance().events.collectibleCollected.AddListener(incrementCollected);
	}

	public void incrementCoinsShot() { coinsShot++; }
	public void incrementObstaclesHit() { obstaclesHit++; }
	public void incrementCardsPlayed() { cardsPlayed++; }
	public void incrementCollected() { collectiblesFound++; }
	public void incrementFoulsCommitted() { foulsCommitted++; }

	// Getters
	public int getCoinsShot() { return coinsShot; }
	public int getObstaclesHit() { return obstaclesHit; }
	public int getCardsPlayed() { return cardsPlayed; }
	public int getCollectiblesFound() { return collectiblesFound; }
	public int getFoulsCommitted() { return foulsCommitted; }
}

public class PlayerData {
	// Unlocked levels and their completion ranks
}

