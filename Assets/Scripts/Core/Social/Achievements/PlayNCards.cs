using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class PlayNCards : TieredAchievement {
	public PlayNCards(Stats stats) : base(stats) {
		tiers = new List<int> { 10, 25, 50, 100, 250, 500, 1000 };
		LevelManager.getInstance().events.cardPlayed.AddListener(check);
	}

	public override void check() {
		value = stats.getCardsPlayed();
		int index = tiers.IndexOf(value);
		if (index > tierCompleted) {
			// Invoke achievement completed event.
			tierCompleted = index;
			unlocked = (index == tiers.Count) ? true : false;
			GameObject.FindObjectOfType<AchievementPopupUI>().displayAchievement(getDescription());
		}
	}
	
	public override string getDescription() {
		return "Play " + tiers[tierCompleted] + " cards.";
	}
	
	public override string getNextTierDescription() {
		return "Play " + tiers[tierCompleted+1] + " cards.";
	}
}