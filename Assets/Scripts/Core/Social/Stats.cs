using System;
using UnityEngine;

[Serializable]
public class Stats {
	[SerializeField] int coinsShot;
	[SerializeField] int obstaclesHit;
	[SerializeField] int cardsPlayed;
	[SerializeField] int collectiblesCollected;
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
			collectiblesCollected = stats.collectiblesCollected;
			foulsCommitted = stats.foulsCommitted;
		} else {
			coinsShot = 0;
			obstaclesHit = 0;
			cardsPlayed = 0;
			collectiblesCollected = 0;
			foulsCommitted = 0;
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
	public void incrementCollected() { collectiblesCollected++; }
	public void incrementFoulsCommitted() { foulsCommitted++; }

	// Getters
	public int getCoinsShot() { return coinsShot; }
	public int getObstaclesHit() { return obstaclesHit; }
	public int getCardsPlayed() { return cardsPlayed; }
	public int getCollected() { return collectiblesCollected; }
	public int getFoulsCommitted() { return foulsCommitted; }
}

public class PlayerData {
	// Unlocked levels and their completion ranks
}

