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
		stats.onClick.AddListener(() => {
			animation.Play("hideMainMenuPanel");
			FindObjectOfType<StatsUI>().GetComponent<Animation>().Play("displayStatsPanel");
		});
	}

	void loadLevelsPage() {
		GameManager.getInstance().levels.loadLevelsPage();
	}

	void loadOfflineMultiplayer() {

	}
}
