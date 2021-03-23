
public class Stats {
	int coinsShot;
	int obstaclesHit;
	int cardsPlayed;
	int collectiblesCollected;
	int foulsCommitted;

	public Stats() {
		// Read from .bin to initialize
		// If .bin file doesn't exist initialize a new instance.

		// Add Event listeners for 
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

