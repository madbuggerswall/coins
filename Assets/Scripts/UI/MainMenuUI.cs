using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {
	Animation animPlayer;

	[SerializeField] Button play;
	[SerializeField] Button multiplayer;
	[SerializeField] Button onlineMultiplayer;
	[SerializeField] Button stats;
	[SerializeField] Button achievements;
	[SerializeField] Button settings;

	void Awake() {
		animPlayer = GetComponent<Animation>();

		play.onClick.AddListener(() => {
			animPlayer.Play("hideMainMenuPanel");
			FindObjectOfType<LevelSelectionUI>().GetComponent<Animation>().Play("displayLevelSelectionPanel");
		});

		multiplayer.onClick.AddListener(loadOfflineMultiplayer);

		stats.onClick.AddListener(() => {
			animPlayer.Play("hideMainMenuPanel");
			FindObjectOfType<StatsUI>().GetComponent<Animation>().Play("displayStatsPanel");
		});

		achievements.onClick.AddListener(() => {
			animPlayer.Play("hideMainMenuPanel");
			FindObjectOfType<AchievementsUI>().GetComponent<Animation>().Play("displayAchievementsPanel");
		});
	}

	void Start() {
		// Lighten the screen from dark everytime main menu scene is loaded.
		SceneTransitionUI.getInstance().lighten();
	}

	void loadOfflineMultiplayer() {

	}
}
