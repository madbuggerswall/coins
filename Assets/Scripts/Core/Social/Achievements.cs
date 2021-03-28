using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Achievements {
	[SerializeField] ShootNCoins shootNCoins;
	[SerializeField] HitNObstacles hitNObstacles;
	[SerializeField] PlayNCards playNCards;
	[SerializeField] CollectNCollectibles collectNCollectibles;
	[SerializeField] CommitNFouls commitNFouls;

	Achievements() { }

	public void initialize(Stats stats) {
		if (SaveManager.exists(FilePath.achievements)) {
			Achievements achievements = SaveManager.load<Achievements>(FilePath.achievements);

			shootNCoins = achievements.shootNCoins;
			hitNObstacles = achievements.hitNObstacles;
			playNCards = achievements.playNCards;
			collectNCollectibles = achievements.collectNCollectibles;
			commitNFouls = achievements.commitNFouls;
		} else {
			shootNCoins = new ShootNCoins(stats);
			hitNObstacles = new HitNObstacles(stats);
			playNCards = new PlayNCards(stats);
			collectNCollectibles = new CollectNCollectibles(stats);
			commitNFouls = new CommitNFouls(stats);
		}

	}
}

[Serializable]
public abstract class Achievement {
	protected Stats stats;
	[SerializeField] protected bool unlocked;
	public abstract void check();
	public abstract string getDescription();
}

[Serializable]
public abstract class TieredAchievement : Achievement {
	protected List<int> tiers;
	[SerializeField] protected int tierCompleted = -1;

	public float getProgress() {
		return ((float) tierCompleted + 1) / (float) tiers.Count;
	}
}
