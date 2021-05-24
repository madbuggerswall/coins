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
			
		});
		multiplayer.onClick.AddListener(loadOfflineMultiplayer);

		if (stats == null) { Debug.Log("stats null"); }

		stats.onClick.AddListener(() => {
			animPlayer.Play("hideMainMenuPanel");
			FindObjectOfType<StatsUI>().GetComponent<Animation>().Play("displayStatsPanel");
		});

		achievements.onClick.AddListener(() => {
			animPlayer.Play("hideMainMenuPanel");
			FindObjectOfType<AchievementsUI>().GetComponent<Animation>().Play("displayAchievementsPanel");
		});
	}

	void loadOfflineMultiplayer() {

	}
}
