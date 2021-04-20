using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementsUI : MonoBehaviour {
	[SerializeField] RectTransform achievementsParent;

	[SerializeField] Achievements achievements;
	Stats stats;
	void Awake() {
		stats = Stats.loadFromFile();
		achievements = Achievements.loadFromFile();

		GameObject achievementUIPrefab = achievementsParent.GetComponentInChildren<AchievementUI>().gameObject;
		List<Achievement> achievementList = achievements.getAchievements();
		for (int i = 0; i < achievementList.Count; i++) {
			RectTransform prefabRectTransform = achievementUIPrefab.GetComponent<RectTransform>();
			Vector3 position = prefabRectTransform.anchoredPosition + Vector2.up * prefabRectTransform.rect.height;

			GameObject achievementPanel = Instantiate(achievementUIPrefab);
			achievementPanel.transform.SetParent(achievementsParent);
			RectTransform rectTransform = achievementPanel.GetComponent<RectTransform>();
			rectTransform.position = prefabRectTransform.position;
			rectTransform.rotation = prefabRectTransform.rotation;
			rectTransform.sizeDelta = prefabRectTransform.sizeDelta;
			rectTransform.localScale = prefabRectTransform.localScale;
			rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -prefabRectTransform.rect.height * i);

			achievementPanel.GetComponent<AchievementUI>().setTieredAchievement((TieredAchievement) achievementList[i]);
		}
	}
}
