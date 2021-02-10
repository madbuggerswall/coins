using UnityEngine;
using UnityEngine.Events;

public class PuzzleEvents : GameEvents {
	public UnityEvent cardPlayed;
	public UnityEvent cardApplied;
	public UnityEvent puzzleEnded;

	public PuzzleEvents(Puzzle puzzle) : base() {
		cardPlayed = new UnityEvent();
		cardApplied = new UnityEvent();
		puzzleEnded = new UnityEvent();

		CoinSet coinSet = GameObject.FindObjectOfType<CoinSet>();
		cardPlayed.AddListener(() => coinSet.setState(new StationaryState(coinSet)));
		cardApplied.AddListener(() => coinSet.setState(new AimState(coinSet)));

		PuzzleUI puzzleUI = GameObject.FindObjectOfType<PuzzleUI>();

		playerFouled.AddListener(() => puzzleUI.showFaulPanel());
		playerFouled.AddListener(() => puzzleUI.resetPlayerShotsUI());
		playerScored.AddListener(() => puzzleUI.resetPlayerShotsUI());
		playerContinuesTurn.AddListener(() => puzzleUI.setShotsLeft(puzzle.getPlayer().getShotsLeft()));
		playerHasNoShotsLeft.AddListener(() => puzzleUI.resetPlayerShotsUI());
		playerScored.AddListener(() => puzzleUI.showGoalPanel());
		puzzleEnded.AddListener(() => puzzleUI.showWinPanel());
	}
}