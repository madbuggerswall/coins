using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-64)]
public class Match : MonoBehaviour {
	static Match instance;

	CoinSet coinSet;

	[SerializeField] Player playerLeft;
	[SerializeField] Player playerRight;
	Player activePlayer;

	public UnityEvent playerScored;
	public UnityEvent playerFouled;
	public UnityEvent playerShotEnded;

	MatchState state;

	ushort winningScore = 5;

	void Awake() {
		assertSingleton();
		coinSet = FindObjectOfType<CoinSet>();
		playerLeft = new Player();
		playerRight = new Player();

		playerScored = new UnityEvent();
		playerFouled = new UnityEvent();
		playerShotEnded = new UnityEvent();

		state = new CoinToss(this);
	}

	public void passTurn() {
		if (activePlayer == playerLeft)
			activePlayer = playerRight;
		else
			activePlayer = playerLeft;
	}

	// Singleton utilities
	void assertSingleton() { if (instance == null) { instance = this; } else { Destroy(gameObject); } }
	public static Match getInstance() { return instance; }

	// Setters & Getters
	public void setState(MatchState state) { this.state = state; }
	public void setActivePlayer(Player player) { activePlayer = player; }
	public CoinSet getCoinSet() { return coinSet; }
	public Player getPlayerLeft() { return playerLeft; }
	public Player getPlayerRight() { return playerRight; }
	public Player getActivePlayer() { return activePlayer; }
}