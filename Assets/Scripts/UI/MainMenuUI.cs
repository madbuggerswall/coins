using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour {
	[SerializeField] Button play;
	[SerializeField] Button multiplayer;
	[SerializeField] Button onlineMultiplayer;
	[SerializeField] Button achievements;
	[SerializeField] Button settings;

	void Awake() {
		play.onClick.AddListener(loadLevelsPage);
		multiplayer.onClick.AddListener(loadOfflineMultiplayer);
	}

	void loadLevelsPage() {
		GameManager.getInstance().levels.loadLevelsPage();
	}

	void loadOfflineMultiplayer() {
		
	}
}
