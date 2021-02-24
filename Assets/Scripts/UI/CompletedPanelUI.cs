using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CompletedPanelUI : MonoBehaviour {
	[SerializeField] Button restart;
	[SerializeField] Button mainMenu;
	[SerializeField] Button nextLevel;

	void Awake() {
		restart.onClick.AddListener(GameManager.getInstance().levels.restartLevel);
		mainMenu.onClick.AddListener(GameManager.getInstance().levels.loadMainMenu);
		nextLevel.onClick.AddListener(GameManager.getInstance().levels.loadNextLevel);
	}
}
