using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelPanelUI : MonoBehaviour {
	[SerializeField] Button level1;
	[SerializeField] Button level2;
	[SerializeField] Button level3;
	[SerializeField] Button level4;

	void Awake() {
		level1.onClick.AddListener(() => GameManager.getInstance().levels.loadPuzzle(0));
		level2.onClick.AddListener(() => GameManager.getInstance().levels.loadPuzzle(1));
		level3.onClick.AddListener(() => GameManager.getInstance().levels.loadPuzzle(2));
		level4.onClick.AddListener(() => GameManager.getInstance().levels.loadPuzzle(3));
	}
}
