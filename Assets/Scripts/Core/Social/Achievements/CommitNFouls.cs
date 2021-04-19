using System.Collections.Generic;
using System;

[Serializable]
public class CommitNFouls : TieredAchievement {
	public CommitNFouls(Stats stats) : base(stats) {
		tiers = new List<int> { 10, 25, 50, 100, 250, 500, 1000 };
		LevelManager.getInstance().events.playerFouled.AddListener(check);
	}

	public override void check() {
		int foulsCommitted = stats.getFoulsCommitted();
		int index = tiers.IndexOf(foulsCommitted);
		if (index > tierCompleted) {
			// Invoke achievement completed event.
			tierCompleted = index;
			unlocked = (index == tiers.Count) ? true : false;
		}
	}

	public override string getDescription() {
		return "Commit " + tiers[tierCompleted] + "fouls";
	}
}