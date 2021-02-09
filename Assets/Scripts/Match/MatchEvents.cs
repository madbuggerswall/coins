using UnityEngine;
using UnityEngine.Events;

public class MatchEvents : GameEvents{
	public UnityEvent playerTurnPassed;
	public UnityEvent matchEnded;
	UI ui;

	public MatchEvents(Match match) : base(){

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