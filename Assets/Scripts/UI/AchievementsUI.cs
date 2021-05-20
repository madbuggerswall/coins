using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementsUI : MonoBehaviour {
	[SerializeField] RectTransform achievementsParent;
	[SerializeField] Achievements achievements;
	Stats stats;

	[SerializeField] Button returnButton;
	new Animation animation;

	void Awake() {
		stats = Stats.loadFromFile();
		achievements = Achievements.loadFromFile();
		animation = GetComponent<Animation>();

		returnButton.onClick.AddListener(() => {
			animation.Play("hideAchievementsPanel");
			FindObjectOfType<MainMenuUI>().GetComponent<Animation>().Play("displayMainMenuPanel");
		});

		listAchievements();
	}

	void listAchievements() {
		// List colors.
		Color32 darkColor = new Color32(0x00, 0x00, 0x00, 0x52);
		Color32 lightColor = new Color32(0x55, 0x055, 0x55, 0x52);
		
		GameObject achievementUIPrefab = achievementsParent.GetComponentInChildren<AchievementUI>().gameObject;
		List<Achievement> achievementList = achievements.getAchievements();

		for (int i = 0; i < achievementList.Count; i++) {
			RectTransform prefabRectTransform = achievementUIPrefab.GetComponent<RectTransform>();

			// Create panel from the panel in the scene
			GameObject achievementPanel = Instantiate(achievementUIPrefab);
			achievementPanel.transform.SetParent(achievementsParent);

			// Position the panel
			RectTransform rectTransform = achievementPanel.GetComponent<RectTransform>();
			rectTransform.position = prefabRectTransform.position;
			rectTransform.rotation = prefabRectTransform.rotation;
			rectTransform.sizeDelta = prefabRectTransform.sizeDelta;
			rectTransform.localScale = prefabRectTransform.localScale;
			rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x, -i * prefabRectTransform.rect.height);

			// Set color
			achievementPanel.GetComponent<Image>().color = (i % 2 == 0) ? darkColor : lightColor;

			// Initialize panel content
			achievementPanel.GetComponent<AchievementUI>().setTieredAchievement((TieredAchievement) achievementList[i]);
			achievementPanel.GetComponent<AchievementUI>().initialize();
		}

		// Adjust container size
		float bottom = achievementList.Count * achievementUIPrefab.GetComponent<RectTransform>().rect.height;
		bottom -= achievementsParent.rect.height;
		achievementsParent.setBottom(-bottom);

		// Disable original gameobject
		achievementUIPrefab.SetActive(false);
	}
}
