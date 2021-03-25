using System.Collections.Generic;

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