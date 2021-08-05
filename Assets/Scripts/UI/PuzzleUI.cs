using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuzzleUI : MonoBehaviour {
	[SerializeField] Button pause;
	[SerializeField] GameObject pauseMenu;
	[SerializeField] GameObject shotsLeft;
	[SerializeField] GameObject faulPanel;
	[SerializeField] GameObject completedPanel;
	[SerializeField] GameObject failedPanel;
	ShotsLeftUI shotsLeftUI;

	void Awake() {
		shotsLeftUI = new ShotsLeftUI(shotsLeft, LevelManager.getInstance().getPlayer().getShotsLeft());
		resetPlayerShotsUI();
		initializeListeners();
		pause.onClick.AddListener(() => pauseMenu.GetComponent<PauseMenu>().pauseGame(!pauseMenu.activeSelf));
	}

	void initializeListeners() {
		Events events = LevelManager.getInstance().events;

		events.playerFouled.AddListener(() => showFaulPanel());
		events.playerContinuesTurn.AddListener(() => setShotsLeft(FindObjectOfType<Puzzle>().getPlayer().getShotsLeft()));
		events.playerHasNoShotsLeft.AddListener(() => resetPlayerShotsUI());
		events.playerScored.AddListener(() => {
			completedPanel.SetActive(true);
			pause.enabled = false;
		});
		events.playerHasNoShotsLeft.AddListener(() => {
			failedPanel.SetActive(true);
			pause.enabled = false;
		});
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

	public void showFaulPanel() { StartCoroutine(enableFaulPanelFor(1)); }
}