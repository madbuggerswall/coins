using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [DefaultExecutionOrder(-64)]
public class Match : CoinGame {
	MatchEvents events;
	[SerializeField] Player playerLeft;
	[SerializeField] Player playerRight;
	[SerializeField] Blocker blockerLeft;
	[SerializeField] Blocker blockerRight;

	MatchState.State state;

	[SerializeField] ushort winningScore = 5;

	void Awake() {
		coinSet = FindObjectOfType<CoinSet>();
		events = new MatchEvents(this);
		playerLeft = new Player();
		playerRight = new Player();
		state = new MatchState.CoinToss(this);

		events.playerShotInGoal.AddListener(() => setPlayerShotInGoal(true));
		events.playerShotEnded.AddListener(evaluateShot);
		coinSet.events.shotEnded.AddListener(() => events.playerShotEnded.Invoke());
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
			events.playerFouled.Invoke();
			passTurn();
		} else if (hasPlayerShotInGoal) {
			events.playerScored.Invoke();
			setState(new MatchState.PlayerScored(this));
		} else if (playerHasShotsLeft()) {
			events.playerContinuesTurn.Invoke();
			continueTurn();
		} else {
			events.playerHasNoShotsLeft.Invoke();
			passTurn();
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
		events.playerTurnPassed.Invoke();
	}
	public void startResettingCoins() {
		StartCoroutine(resetCoins());
	}
	public bool isPlayerLeftActive() { return player == playerLeft; }

	// Setters & Getters
	public override GameEvents getEvents() {
		return events;
	}
	public void setState(MatchState.State state) { this.state = state; }
	public void setPlayer(Player player) { this.player = player; }
	public Player getPlayerLeft() { return playerLeft; }
	public Player getPlayerRight() { return playerRight; }
	public ushort getWinningScore() { return winningScore; }
}