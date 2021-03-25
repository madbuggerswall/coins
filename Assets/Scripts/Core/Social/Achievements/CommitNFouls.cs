using System.Collections.Generic;

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