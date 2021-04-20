using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class HitNObstacles : TieredAchievement {
	public HitNObstacles(Stats stats) : base(stats) {
		tiers = new List<int> { 10, 25, 50, 100, 250, 500, 1000 };
		LevelManager.getInstance().events.obstacleHit.AddListener(check);
	}

	public override void check() {
		int obstaclesHit = stats.getObstaclesHit();
		int index = tiers.IndexOf(obstaclesHit);
		if (index > tierCompleted) {
			// Invoke achievement completed event.
			tierCompleted = index;
			unlocked = (index == tiers.Count) ? true : false;
			GameObject.FindObjectOfType<AchievementPopupUI>().displayAchievement(getDescription());
		}
	}
	public override string getDescription() {
		return "Hit " + tiers[tierCompleted] + " obstacles.";
	}
}