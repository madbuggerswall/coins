using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class CollectNCollectibles : TieredAchievement {
	public CollectNCollectibles(Stats stats) : base(stats) {
		tiers = new List<int> { 10, 25, 50, 100, 250, 500, 1000 };
		LevelManager.getInstance().events.collectibleCollected.AddListener(check);
	}

	public override void check() {
		value = stats.getCollectiblesFound();
		int index = tiers.IndexOf(value);
		if (index > tierCompleted) {
			// Invoke achievement completed event.
			tierCompleted = index;
			unlocked = (index == tiers.Count) ? true : false;
			GameObject.FindObjectOfType<AchievementPopupUI>().displayAchievement(getDescription());
		}
	}

	public override string getDescription() {
		return "Collect " + tiers[tierCompleted] + " collectibles.";
	}
	public override string getNextTierDescription() {
		if (tierCompleted + 1 >= tiers.Count)
			return "Completed.";
		else
			return "Collect " + tiers[tierCompleted + 1] + " collectibles.";
	}
}