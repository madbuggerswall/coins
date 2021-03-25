using System.Collections.Generic;

public class HitNObstacles : TieredAchievement {
	public HitNObstacles(Stats stats) {
		this.stats = stats;
		tiers = new List<int> { 10, 25, 50, 100, 250, 500, 1000 };
		LevelManager.getInstance().events.obstacleHit.AddListener(check);
	}

	public override void check() {
		int obstaclesHit = stats.getObstaclesHit();
		int index = tiers.IndexOf(obstaclesHit);
		if (index > tierCompleted) {
			// Invoke achievement completed event.
		}
	}
	public override string getDescription() {
		return "Hit " + tiers[tierCompleted] + "obstacles.";
	}
}