using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class ShootNCoins : TieredAchievement {
	public ShootNCoins(Stats stats) : base(stats) {
		tiers = new List<int> { 10, 25, 50, 100, 250, 500, 1000 };
		LevelManager.getInstance().events.coinShot.AddListener(check);
	}

	public override void check() {
		int coinsShot = stats.getCoinsShot();
		int index = tiers.IndexOf(coinsShot);
		Debug.Log("Checked");
		if (index > tierCompleted) {
			tierCompleted = index;
			unlocked = (index == tiers.Count) ? true : false;
			GameObject.FindObjectOfType<AchievementPopupUI>().displayAchievement(getDescription());
		}
	}

	public override string getDescription() {
		return "Shoot " + tiers[tierCompleted] + " coins.";
	}
}