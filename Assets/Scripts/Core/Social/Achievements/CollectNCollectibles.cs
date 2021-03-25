using System.Collections.Generic;

public class CollectNCollectibles : TieredAchievement {
	public CollectNCollectibles(Stats stats) {
		this.stats = stats;
		tiers = new List<int> { 10, 25, 50, 100, 250, 500, 1000 };
		LevelManager.getInstance().events.collectibleCollected.AddListener(check);
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