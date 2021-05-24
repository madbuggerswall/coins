using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour {
	static readonly Color32 white = new Color32(0xFF, 0xFF, 0xFF, 0xCC);
	static readonly Color32 yellow = new Color32(0xFF, 0xFB, 0x5F, 0xCC);

	[SerializeField] RectTransform levelInfo;
	[SerializeField] RectTransform lockedPanel;
	[SerializeField] Text levelNumber;

	Image[] stars;
	Button playButton;

	Level level;

	void Awake() {
		stars = levelInfo.GetComponentsInChildren<Image>();
		playButton = GetComponent<Button>();

		playButton.onClick.AddListener(() => { });
	}

	void lockLevel(bool isUnlocked) {
		lockedPanel.gameObject.SetActive(!isUnlocked);
		levelInfo.gameObject.SetActive(isUnlocked);
	}

	void setStars(bool[] completion) {
		for (int i = 0; i < completion.Length; i++) {
			if (completion[i]) {
				stars[i].color = yellow;
			} else {
				stars[i].color = white;
			}
		}
	}

	public void setLevel(Level level) {
		this.level = level;

		lockLevel(level.getIsUnlocked());
		setStars(level.getCompletion());
		levelNumber.text = level.getLevelNumber().ToString();

	}
}
