using UnityEngine;
using UnityEngine.Events;

public class MatchEvents {
	public UnityEvent playerShotInGoal;
	public UnityEvent playerShotEnded;
	public UnityEvent playerFouled;
	public UnityEvent playerScored;
	public UnityEvent playerContinuesTurn;
	public UnityEvent playerHasNoShotsLeft;
	public UnityEvent playerTurnPassed;
	public UnityEvent matchEnded;
	UI ui;

	public MatchEvents(Match match) {
		playerShotInGoal = new UnityEvent();
		playerShotEnded = new UnityEvent();
		playerFouled = new UnityEvent();
		playerScored = new UnityEvent();
		playerContinuesTurn = new UnityEvent();
		playerHasNoShotsLeft = new UnityEvent();
		playerTurnPassed = new UnityEvent();
		matchEnded = new UnityEvent();

		ui = GameObject.FindObjectOfType<UI>();

		// UI
		playerFouled.AddListener(() => ui.showFaulPanel());
		playerFouled.AddListener(() => ui.resetActivePlayerShotsUI(match.isPlayerLeftActive()));
		playerScored.AddListener(() => ui.resetActivePlayerShotsUI(match.isPlayerLeftActive()));
		playerContinuesTurn.AddListener(() => ui.setActivePlayerShotsLeft(match.isPlayerLeftActive(), match.getPlayer().getShotsLeft()));
		playerHasNoShotsLeft.AddListener(() => ui.resetActivePlayerShotsUI(match.isPlayerLeftActive()));
		playerScored.AddListener(() => ui.showGoalPanel());
		playerScored.AddListener(() => ui.setActivePlayerScore(match.isPlayerLeftActive(), match.getPlayer().getScore()));
		playerTurnPassed.AddListener(() => ui.passTurn(match.isPlayerLeftActive()));
		matchEnded.AddListener(() => ui.showWinPanel(match.isPlayerLeftActive()));
	}
}