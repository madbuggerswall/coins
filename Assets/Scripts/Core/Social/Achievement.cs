using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public abstract class Achievement {
	protected Stats stats;
	[SerializeField] protected bool unlocked;

	protected Achievement() { }
	protected Achievement(Stats stats) { this.stats = stats; }

	public abstract void check();
	public abstract string getDescription();
	public void loadProgress(Achievement achievement) { unlocked = achievement.unlocked; }
}

[Serializable]
public abstract class TieredAchievement : Achievement {
	protected List<int> tiers;
	protected int value;

	[SerializeField] protected int tierCompleted;

	protected TieredAchievement(Stats stats) : base(stats) { tierCompleted = -1; }

	public float getProgress() {
		return ((float) tierCompleted + 1) / (float) tiers.Count;
	}
	public void loadProgress(TieredAchievement achievement) {
		unlocked = achievement.unlocked;
		tierCompleted = achievement.tierCompleted;
	}

	public abstract string getNextTierDescription();
	public int getValue() { return value; }
	public int getTierCompleted() { return tierCompleted; }
	public List<int> getTiers() { return tiers; }
}