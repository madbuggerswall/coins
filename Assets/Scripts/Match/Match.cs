using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// [DefaultExecutionOrder(-64)]
public class Match : MonoBehaviour {
	CoinSet coinSet;
	public MatchEvents events;

	[SerializeField] Player playerLeft;
	[SerializeField] Player playerRight;
	Player activePlayer;

	[SerializeField] ushort winningScore = 5;

	[SerializeField] Blocker blockerLeft;
	[SerializeField] Blocker blockerRight;

	MatchState.State state;


	void Awake() {
		coinSet = FindObjectOfType<CoinSet>();
		events = new MatchEvents(this);
		playerLeft = new Player();
		playerRight = new Player();
		state = new MatchState.CoinToss(this);
		coinSet.events.shotEnded.AddListener(() => events.playerShotEnded.Invoke());
	}

	void passTurnToOtherPlayer() {
		if (activePlayer == playerLeft) {
			activePlayer = playerRight;
			blockLeftPost(false);
		} else {
			blockLeftPost(true);
			activePlayer = playerLeft;
		}
	}

	public void blockLeftPost(bool value) {
		blockerLeft.block(value);
		blockerRight.block(!value);
	}

	public void passTurn() {
		activePlayer.restoreShotsLeft();
		passTurnToOtherPlayer();
		startResettingCoins();
		events.playerTurnPassed.Invoke();
	}

	public void startResettingCoins() {
		StartCoroutine(resetCoins());
	}

	public bool isPlayerLeftActive() { return activePlayer == playerLeft; }

	IEnumerator resetCoins() {
		yield return coinSet.getFormation().resetCoins(coinSet.getCoins(), (activePlayer == playerLeft));
		setState(new MatchState.PlayerTurn(this));
	}

	// Setters & Getters
	public void setState(MatchState.State state) { this.state = state; }
	public void setActivePlayer(Player player) { activePlayer = player; }
	public CoinSet getCoinSet() { return coinSet; }
	public Player getPlayerLeft() { return playerLeft; }
	public Player getPlayerRight() { return playerRight; }
	public Player getActivePlayer() { return activePlayer; }
	public ushort getWinningScore() { return winningScore; }
}