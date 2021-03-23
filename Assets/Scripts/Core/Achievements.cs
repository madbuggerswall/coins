using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achievements {
	ShootNCoins shootNCoins;
	HitNObstacles hitNObstacles;
	PlayNCards playNCards;
	CollectNCollectibles collectNCollectibles;
	CommitNFouls commitNFouls;

	public Achievements(Stats stats) {
		shootNCoins = new ShootNCoins(stats);
		hitNObstacles = new HitNObstacles(stats);
		playNCards = new PlayNCards(stats);
		collectNCollectibles = new CollectNCollectibles(stats);
		commitNFouls = new CommitNFouls(stats);
	}
}

public abstract class Achievement {
	protected Stats stats;
	public abstract void check();
	public abstract string getDescription();
}

public abstract class TieredAchievement : Achievement {
	protected List<int> tiers;
	protected int tierCompleted = -1;

	public float getProgress() {
		return ((float) tierCompleted + 1) / (float) tiers.Count;
	}
}


public class ShootNCoins : TieredAchievement {
	public ShootNCoins(Stats stats) {
		this.stats = stats;
		tiers = new List<int> { 10, 25, 50, 100, 250, 500, 1000 };

		LevelManager.getInstance().events.coinShot.AddListener(check);
	}

	public override void check() {
		int coinsShot = stats.getCoinsShot();
		int index = tiers.IndexOf(coinsShot);
		if (index > tierCompleted) {
			// Invoke achievement completed event.
		}
	}

	public override string getDescription() {
		return "Shoot " + tiers[tierCompleted] + "coins.";
	}
}

public class HitNObstacles : TieredAchievement {
	public HitNObstacles(Stats stats) {
		this.stats = stats;
		tiers = new List<int> { 10, 25, 50, 100, 250, 500, 1000 };
		// Add event listener for hits.
	}

	public override void check() {
		int obstaclesHit = stats.getObstaclesHit();
		int index = tiers.IndexOf(obstaclesHit);
		if (index > tierCompleted) {
			// Invoke achievement completed event.
		}
	}
	public override string getDescription() {
		return "Hit " + tiers[tierCompleted] + "obstacles.";
	}
}

public class PlayNCards : TieredAchievement {
	public PlayNCards(Stats stats) {
		this.stats = stats;
		tiers = new List<int> { 10, 25, 50, 100, 250, 500, 1000 };
		LevelManager.getInstance().events.cardPlayed.AddListener(check);
	}

	public override void check() {
		int cardsPlayed = stats.getCardsPlayed();
		int index = tiers.IndexOf(cardsPlayed);
		if (index > tierCompleted) {
			// Invoke achievement completed event.
		}
	}
	public override string getDescription() {
		return "Play " + tiers[tierCompleted] + "cards.";
	}
}

public class CollectNCollectibles : TieredAchievement {
	public CollectNCollectibles(Stats stats) {
		this.stats = stats;
		tiers = new List<int> { 10, 25, 50, 100, 250, 500, 1000 };
		// Add event listener for collectibles.
	}

	public override void check() {
		int collected = stats.getCollected();
		int index = tiers.IndexOf(collected);
		if (index > tierCompleted) {
			// Invoke achievement completed event.
		}
	}
	public override string getDescription() {
		return "Collect " + tiers[tierCompleted] + "collectibles.";
	}
}

public class CommitNFouls : TieredAchievement {
	public CommitNFouls(Stats stats) {
		this.stats = stats;
		tiers = new List<int> { 10, 25, 50, 100, 250, 500, 1000 };
		LevelManager.getInstance().events.playerFouled.AddListener(check);
	}

	public override void check() {
		int foulsCommitted = stats.getFoulsCommitted();
		int index = tiers.IndexOf(foulsCommitted);
		if (index > tierCompleted) {
			// Invoke achievement completed event.
		}
	}

	public override string getDescription() {
		return "Commit " + tiers[tierCompleted] + "fouls";
	}
}