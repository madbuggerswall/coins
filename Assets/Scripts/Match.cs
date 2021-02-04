using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// [DefaultExecutionOrder(-64)]
public class Match : MonoBehaviour {
	CoinSet coinSet;

	[SerializeField] Player playerLeft;
	[SerializeField] Player playerRight;
	Player activePlayer;

	public UnityEvent playerScored;
	public UnityEvent playerShotEnded;

	MatchState state;

	ushort winningScore = 5;

	void Awake() {
		coinSet = FindObjectOfType<CoinSet>();
		playerLeft = new Player();
		playerRight = new Player();

		playerScored = new UnityEvent();
		playerShotEnded = new UnityEvent();

		state = new CoinToss(this);
	}

	void passTurnToOtherPlayer() {
		if (activePlayer == playerLeft)
			activePlayer = playerRight;
		else
			activePlayer = playerLeft;
	}

	public void passTurn() {
		Debug.Log("passTurn");

		activePlayer.restoreShotsLeft();
		passTurnToOtherPlayer();
		StartCoroutine(resetCoins());
	}

	public IEnumerator resetCoins() {
		yield return coinSet.getFormation().resetCoins(coinSet.getCoins());
		setState(new PlayerTurn(this));
	}

	// Setters & Getters
	public void setState(MatchState state) { this.state = state; }
	public void setActivePlayer(Player player) { activePlayer = player; }
	public CoinSet getCoinSet() { return coinSet; }
	public Player getPlayerLeft() { return playerLeft; }
	public Player getPlayerRight() { return playerRight; }
	public Player getActivePlayer() { return activePlayer; }
	public ushort getWinningScore() { return winningScore; }
}