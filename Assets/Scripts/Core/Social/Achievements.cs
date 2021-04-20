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

	List<Achievement> achievements = new List<Achievement>();

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
		achievements.Add(shootNCoins);
		achievements.Add(hitNObstacles);
		achievements.Add(playNCards);
		achievements.Add(collectNCollectibles);
		achievements.Add(commitNFouls);
	}

	public static Achievements loadFromFile() {
		if (SaveManager.exists(FilePath.achievements)) {
			Achievements achievements = SaveManager.load<Achievements>(FilePath.achievements);

			return achievements;
		} else {
			return new Achievements();
		}
	}

	public List<Achievement> getAchievements() { return achievements; }
}
