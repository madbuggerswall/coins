using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle : CoinGame {
	public PuzzleState.State state;

	void Awake() {
		coinSet = FindObjectOfType<CoinSet>();
		player = new Player();
		state = new PuzzleState.PlayerTurn(this);

		LevelManager.getInstance().events.coinShotInGoal.AddListener(() => setPlayerShotInGoal(true));
		LevelManager.getInstance().events.coinShotEnded.AddListener(evaluateShot);
	}

	protected override void evaluateShot() {
		Events events = LevelManager.getInstance().events;

		if (playerFouled()) {
			events.playerFouled.Invoke();
		} else if (hasPlayerShotInGoal) {
			events.playerScored.Invoke();
			setState(new PuzzleState.PlayerScored(this));
		} else if (playerHasShotsLeft()) {
			events.playerContinuesTurn.Invoke();
			continueTurn();
		} else {
			events.playerHasNoShotsLeft.Invoke();
		}
	}
	public void setState(PuzzleState.State state) { this.state = state; }
}
