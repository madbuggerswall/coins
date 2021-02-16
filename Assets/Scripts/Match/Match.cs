using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [DefaultExecutionOrder(-64)]
public class Match : CoinGame {
	[SerializeField] Player playerLeft;
	[SerializeField] Player playerRight;
	[SerializeField] Blocker blockerLeft;
	[SerializeField] Blocker blockerRight;

	MatchState.State state;

	[SerializeField] ushort winningScore = 5;

	void Awake() {
		coinSet = FindObjectOfType<CoinSet>();
		playerLeft = new Player();
		playerRight = new Player();
		state = new MatchState.CoinToss(this);

		LevelManager.getInstance().events.coinShotInGoal.AddListener(() => setPlayerShotInGoal(true));
		LevelManager.getInstance().events.coinShotEnded.AddListener(evaluateShot);
	}

	void passTurnToOtherPlayer() {
		if (player == playerLeft) {
			player = playerRight;
			blockLeftPost(false);
		} else {
			blockLeftPost(true);
			player = playerLeft;
		}
	}
	IEnumerator resetCoins() {
		yield return coinSet.getFormation().resetCoins(coinSet.getCoins(), (player == playerLeft));
		setState(new MatchState.PlayerTurn(this));
	}

	// From PlayerTurnState
	protected override void evaluateShot() {
		if (playerFouled()) {
			LevelManager.getInstance().events.playerFouled.Invoke();
			passTurn();
		} else if (hasPlayerShotInGoal) {
			LevelManager.getInstance().events.playerScored.Invoke();
			setState(new MatchState.PlayerScored(this));
		} else if (playerHasShotsLeft()) {
			LevelManager.getInstance().events.playerContinuesTurn.Invoke();
			continueTurn();
		} else {
			LevelManager.getInstance().events.playerHasNoShotsLeft.Invoke();
			passTurn();
			LevelManager.getInstance().events.playerTurnPassed.Invoke();
		}
	}


	// Match functions
	public void blockLeftPost(bool value) {
		blockerLeft.block(value);
		blockerRight.block(!value);
	}
	public void passTurn() {
		player.restoreShotsLeft();
		passTurnToOtherPlayer();
		startResettingCoins();
	}
	public void startResettingCoins() {
		StartCoroutine(resetCoins());
	}
	public bool isPlayerLeftActive() { return player == playerLeft; }

	// Setters & Getters
	public void setState(MatchState.State state) { this.state = state; }
	public void setPlayer(Player player) { this.player = player; }
	public Player getPlayerLeft() { return playerLeft; }
	public Player getPlayerRight() { return playerRight; }
	public ushort getWinningScore() { return winningScore; }
}