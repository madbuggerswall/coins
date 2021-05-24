using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle : CoinGame {
	public PuzzleState.State state;
	Formation formation;
	void Awake() {
		coinSet = FindObjectOfType<CoinSet>();

		LevelManager.getInstance().events.coinShotInGoal.AddListener(() => setPlayerShotInGoal(true));
		LevelManager.getInstance().events.coinShotEnded.AddListener(evaluateShot);
	}

	void Start() {
		state = new PuzzleState.PlayerTurn(this);
		formation = new Formation(coinSet.getCoins());
	}

	IEnumerator resetCoinSet() {
		yield return formation.resetCoinSet();
		LevelManager.getInstance().events.playerContinuesTurn.Invoke();
	}

	protected override void evaluateShot() {
		Events events = LevelManager.getInstance().events;
		if (playerFouled()) {
			events.playerFouled.Invoke();
			setPlayerShotInGoal(false);
			StartCoroutine(resetCoinSet());
		} else if (hasPlayerShotInGoal) {
			events.playerScored.Invoke();
			setState(new PuzzleState.PlayerScored(this));
		} else if (playerHasShotsLeft()) {
			events.playerContinuesTurn.Invoke();
		} else {
			events.playerHasNoShotsLeft.Invoke();
		}
	}

	public void setState(PuzzleState.State state) { this.state = state; }
	public Formation getFormation() { return formation; }
}