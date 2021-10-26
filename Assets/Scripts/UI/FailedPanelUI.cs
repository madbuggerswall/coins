using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FailedPanelUI : MonoBehaviour {
	[SerializeField] Button restart;
	[SerializeField] Button mainMenu;

	void Awake() {
		restart.onClick.AddListener(restartScene);
		mainMenu.onClick.AddListener(loadMainMenu);
	}

	// TODO 
	void restartScene() {
		GameManager.getInstance().levelLoader.restartLevel();
	}

	void loadMainMenu() {
		GameManager.getInstance().levelLoader.loadMainMenu();
	}
}
