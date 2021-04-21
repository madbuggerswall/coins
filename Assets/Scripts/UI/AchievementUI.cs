using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour {

	[SerializeField] Text achievementProgress;
	[SerializeField] Text description;

	[SerializeField] ProgressBar progressBar;
	TieredAchievement tieredAchievement;

	void Awake() {
	}

	public void initialize() {
		progressBar.initializeProgressBar();
	}

	public void setTieredAchievement(TieredAchievement tieredAchievement) {
		this.tieredAchievement = tieredAchievement;
		progressBar.setTieredAchievement(tieredAchievement);
		description.text = tieredAchievement.getNextTierDescription();
		achievementProgress.text = "[ " + (tieredAchievement.getTierCompleted() + 1) + " / " + tieredAchievement.getTiers().Count + " ]";
	}
}

[Serializable]
class ProgressBar {
	static int lowerLimit = 648;
	static int upperLimit = 12;

	[SerializeField] Image progressBar;
	[SerializeField] Text value;
	[SerializeField] Text lowerIndicator;
	[SerializeField] Text upperIndicator;
	TieredAchievement tieredAchievement;

	public void initializeProgressBar() {
		List<int> tiers = tieredAchievement.getTiers();
		int tierCompleted = tieredAchievement.getTierCompleted();
		int currentTierValue;
		int nextTierValue;

		if (tierCompleted == -1) {
			currentTierValue = 0;
			nextTierValue = tiers[0];

			lowerIndicator.text = "0";
			upperIndicator.text = tiers[tierCompleted + 1].ToString();
		} else if (tierCompleted == tiers.Count - 1) {
			currentTierValue = tiers[tierCompleted - 1];
			nextTierValue = tiers[tierCompleted];

			lowerIndicator.text = tiers[tierCompleted - 1].ToString(); ;
			upperIndicator.text = tiers[tierCompleted].ToString();
		} else {
			currentTierValue = tiers[tierCompleted];
			nextTierValue = tiers[tierCompleted + 1];

			lowerIndicator.text = tiers[tierCompleted].ToString();
			upperIndicator.text = tiers[tierCompleted + 1].ToString();
		}

		float ratio = Mathf.InverseLerp(currentTierValue, nextTierValue, tieredAchievement.getValue());
		progressBar.rectTransform.setRight(lowerLimit - (lowerLimit - upperLimit) * ratio);
		value.text = tieredAchievement.getValue().ToString();
	}

	public void setTieredAchievement(TieredAchievement tieredAchievement) { this.tieredAchievement = tieredAchievement; }
}

