using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {
	Animation animation;

	[SerializeField] Button play;
	[SerializeField] Button multiplayer;
	[SerializeField] Button onlineMultiplayer;
	[SerializeField] Button stats;
	[SerializeField] Button achievements;
	[SerializeField] Button settings;

	void Awake() {
		animation = GetComponent<Animation>();

		play.onClick.AddListener(loadLevelsPage);
		multiplayer.onClick.AddListener(loadOfflineMultiplayer);

		if (stats == null) { Debug.Log("stats null"); }

		stats.onClick.AddListener(() => {
			animation.Play("hideMainMenuPanel");
			FindObjectOfType<StatsUI>().GetComponent<Animation>().Play("displayStatsPanel");
		});

		achievements.onClick.AddListener(() => {
			animation.Play("hideMainMenuPanel");
			FindObjectOfType<AchievementsUI>().GetComponent<Animation>().Play("displayAchievementsPanel");
		});
	}

	

	void loadLevelsPage() {
		GameManager.getInstance().levels.loadLevelsPage();
	}

	void loadOfflineMultiplayer() {

	}
}
