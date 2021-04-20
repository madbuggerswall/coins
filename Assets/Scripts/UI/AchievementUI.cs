using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementUI : MonoBehaviour {
	[SerializeField] Image bar;
	[SerializeField] GameObject indicatorParent;
	RectTransform[] indicators;
	TieredAchievement tieredAchievement;

	void Awake() {
		indicators = indicatorParent.GetComponentsInChildren<RectTransform>();
	}

	public void initialize() {
		float barLength = bar.rectTransform.rect.width;

		List<int> tiers = tieredAchievement.getTiers();

		int minTier = tiers[0];
		indicators[0].GetComponentInChildren<Text>().text = minTier.ToString();
		int maxTier = tiers[tiers.Count - 1];
		indicators[indicators.Length - 1].GetComponentInChildren<Text>().text = maxTier.ToString();

		for (int i = 1; i < tiers.Count - 1; i++) {
			float indicatorPosX = Mathf.InverseLerp(minTier, maxTier, tiers[i]) * barLength;
			indicators[i].anchoredPosition.Set(indicatorPosX, indicators[i].anchoredPosition.y);
		}
	}

	public void setTieredAchievement(TieredAchievement tieredAchievement) {
		this.tieredAchievement = tieredAchievement;
	}
}

struct Progress { }
struct Indicator {
	Image indicator;
	Text level;
}

