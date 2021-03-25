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
