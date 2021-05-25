using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionUI : MonoBehaviour {
	Animation animPlayer;

	StageManager stageManager;

	const float marginVert = 40;
	const float marginHor = 40;

	[SerializeField] RectTransform levelsParent;
	[SerializeField] RectTransform levelPanelPrefab;

	[SerializeField] Button returnButton;

	void Awake() {
		animPlayer = GetComponent<Animation>();
		stageManager = GameManager.getInstance().stageManager;
		initializeLevelList();

		returnButton.onClick.AddListener(() => {
			animPlayer.Play("hideLevelSelectionPanel");
			FindObjectOfType<MainMenuUI>().GetComponent<Animation>().Play("displayMainMenuPanel");
		});
	}

	void initializeLevelList() {
		List<Level> levels = stageManager.getStage().getLevels();
		for (int i = 0; i < levels.Count; i++) {
			// Create panel and set parent
			GameObject levelPanel = Instantiate(levelPanelPrefab.gameObject);
			levelPanel.SetActive(true);
			levelPanel.transform.SetParent(levelsParent);
			levelPanel.GetComponent<LevelUI>().setLevel(levels[i]);
			RectTransform levelPanelTransform = levelPanel.GetComponent<RectTransform>();

			// Set position to prefab
			levelPanelTransform.position = levelPanelPrefab.position;
			levelPanelTransform.rotation = levelPanelPrefab.rotation;
			levelPanelTransform.sizeDelta = levelPanelPrefab.sizeDelta;
			levelPanelTransform.localScale = levelPanelPrefab.localScale;

			// Set L/R position
			float prefabX = levelPanelPrefab.anchoredPosition.x;
			float prefabY = levelPanelPrefab.anchoredPosition.y;
			float prefabHeight = levelPanelPrefab.rect.height;
			float prefabWidth = levelPanelPrefab.rect.width;

			if (i % 2 == 0) {
				levelPanelTransform.anchoredPosition = new Vector2(prefabX, prefabY - (i / 2) * (prefabHeight + marginVert));
			} else {
				float posX = prefabX + prefabWidth + marginHor;
				levelPanelTransform.anchoredPosition = new Vector2(posX, prefabY - (i / 2) * (prefabHeight + marginVert));
			}
		}
	}
}
