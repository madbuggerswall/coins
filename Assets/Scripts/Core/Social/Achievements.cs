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
		shootNCoins = new ShootNCoins(stats);
		hitNObstacles = new HitNObstacles(stats);
		playNCards = new PlayNCards(stats);
		collectNCollectibles = new CollectNCollectibles(stats);
		commitNFouls = new CommitNFouls(stats);
		if (SaveManager.exists(FilePath.achievements)) {
			Achievements achievements = SaveManager.load<Achievements>(FilePath.achievements);

			shootNCoins.loadProgress(achievements.shootNCoins);
			hitNObstacles.loadProgress(achievements.hitNObstacles);
			playNCards.loadProgress(achievements.playNCards);
			collectNCollectibles.loadProgress(achievements.collectNCollectibles);
			commitNFouls.loadProgress(achievements.commitNFouls);
		}
	}
}

[Serializable]
public abstract class Achievement {
	protected Stats stats;
	[SerializeField] protected bool unlocked;

	protected Achievement() { }
	protected Achievement(Stats stats) { this.stats = stats; }

	public abstract void check();
	public abstract string getDescription();
	public void loadProgress(Achievement achievement) { unlocked = achievement.unlocked; }
}

[Serializable]
public abstract class TieredAchievement : Achievement {
	protected List<int> tiers;
	[SerializeField] protected int tierCompleted;

	protected TieredAchievement(Stats stats) : base(stats) { tierCompleted = -1; }

	public float getProgress() {
		return ((float) tierCompleted + 1) / (float) tiers.Count;
	}
	public void loadProgress(TieredAchievement achievement) {
		unlocked = achievement.unlocked;
		tierCompleted = achievement.tierCompleted;
	}
}
