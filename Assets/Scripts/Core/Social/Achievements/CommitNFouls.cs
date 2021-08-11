using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class CommitNFouls : TieredAchievement {
	public CommitNFouls(Stats stats) : base(stats) {
		tiers = new List<int> { 10, 25, 50, 100, 250, 500, 1000 };
		LevelManager.getInstance().events.playerFouled.AddListener(check);
	}

	public override void check() {
		value = stats.getFoulsCommitted();
		int index = tiers.IndexOf(value);
		if (index > tierCompleted) {
			// Invoke achievement completed event.
			tierCompleted = index;
			unlocked = (index == tiers.Count) ? true : false;
			GameObject.FindObjectOfType<AchievementPopupUI>().displayAchievement(getDescription());
		}
	}

	public override string getDescription() {
		return "Commit " + tiers[tierCompleted] + " fouls.";
	}

	public override string getNextTierDescription() {
		if (tierCompleted + 1 >= tiers.Count)
			return "Completed.";
		else
			return "Commit " + tiers[tierCompleted + 1] + " fouls.";
	}
}