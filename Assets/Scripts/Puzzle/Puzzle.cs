using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle : CoinGame {
	public PuzzleEvents events;
	public PuzzleState.State state;
	void Awake() {
		coinSet = FindObjectOfType<CoinSet>();
		events = new PuzzleEvents(this);
		player = new Player();
		state = new PuzzleState.PlayerTurn(this);

		events.playerShotInGoal.AddListener(() => setPlayerShotInGoal(true));
		events.playerShotEnded.AddListener(evaluateShot);
		coinSet.events.shotEnded.AddListener(() => events.playerShotEnded.Invoke());
	}

	protected override void evaluateShot() {
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
