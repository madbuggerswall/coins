using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuzzleUI : MonoBehaviour {
	[SerializeField] Button pause;
	[SerializeField] GameObject pauseMenu;
	[SerializeField] GameObject shotsLeft;
	[SerializeField] GameObject goalPanel;
	[SerializeField] GameObject faulPanel;
	[SerializeField] GameObject winPanel;
	ShotsLeftUI shotsLeftUI;

	void Awake() {
		shotsLeftUI = new ShotsLeftUI(shotsLeft);
		resetPlayerShotsUI();
		initializeListeners();
		pause.onClick.AddListener(() => pauseMenu.SetActive(!pauseMenu.activeSelf));
	}

	void initializeListeners() {
		Events events = LevelManager.getInstance().events;

		events.playerFouled.AddListener(() => showFaulPanel());
		events.playerFouled.AddListener(() => resetPlayerShotsUI());
		events.playerScored.AddListener(() => resetPlayerShotsUI());
		events.playerContinuesTurn.AddListener(() => setShotsLeft(FindObjectOfType<Puzzle>().getPlayer().getShotsLeft()));
		events.playerHasNoShotsLeft.AddListener(() => resetPlayerShotsUI());
		events.playerScored.AddListener(() => showGoalPanel());
		events.sessionEnded.AddListener(() => showWinPanel());
	}

	IEnumerator enableWinPanelAfter(float seconds) {
		winPanel.GetComponentInChildren<Text>().text = "WON";
		yield return new WaitForSeconds(seconds);
		winPanel.SetActive(true);
	}
	IEnumerator enableGoalPanelFor(float seconds) {
		goalPanel.SetActive(true);
		yield return new WaitForSeconds(seconds);
		goalPanel.SetActive(false);
	}
	IEnumerator enableFaulPanelFor(float seconds) {
		faulPanel.SetActive(true);
		yield return new WaitForSeconds(seconds);
		faulPanel.SetActive(false);
	}


	// Interface
	public void setShotsLeft(int shotsLeft) { shotsLeftUI.setShotsLeft(shotsLeft); }
	public void resetShotsLeft() { shotsLeftUI.resetShotsLeft(); }
	public void resetPlayerShotsUI() {
		resetShotsLeft();
	}

	public void showWinPanel() {
		StartCoroutine(enableWinPanelAfter(1));
	}
	public void showGoalPanel() { StartCoroutine(enableGoalPanelFor(1)); }
	public void showFaulPanel() { StartCoroutine(enableFaulPanelFor(1)); }
}