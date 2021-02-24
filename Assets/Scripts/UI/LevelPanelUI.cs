using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanelUI : MonoBehaviour {
	Button[] levels;

	void Awake() {
		levels = GetComponentsInChildren<Button>();

		for (int i = 0; i < levels.Length; i++) {
			levels[i].onClick.AddListener(() => GameManager.getInstance().levels.loadPuzzle(i));
			Debug.Log(levels[i].gameObject.name + " | i: " + i);
		}
	}
}
