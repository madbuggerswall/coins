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

	[SerializeField] Blocker blockerLeft;
	[SerializeField] Blocker blockerRight;

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

	public void blockLeftPost(bool value) {
		blockerLeft.block(value);
		blockerRight.block(!value);
	}

	void passTurnToOtherPlayer() {
		if (activePlayer == playerLeft) {
			activePlayer = playerRight;
			blockLeftPost(false);
			FindObjectOfType<UI>().passTurn(false);
		} else {
			blockLeftPost(true);
			activePlayer = playerLeft;
			FindObjectOfType<UI>().passTurn(true);
		}
	}

	public void passTurn() {
		activePlayer.restoreShotsLeft();
		passTurnToOtherPlayer();
		startResettingCoins();
	}

	public void startResettingCoins() {
		StartCoroutine(resetCoins());
	}

	IEnumerator resetCoins() {
		yield return coinSet.getFormation().resetCoins(coinSet.getCoins(), (activePlayer == playerLeft));
		setState(new PlayerTurn(this));
	}

	// UI
	public void resetActivePlayerShotsUI() {
		if (activePlayer == playerLeft)
			FindObjectOfType<UI>().resetShotsLeftL();
		else
			FindObjectOfType<UI>().resetShotsLeftR();
	}
	public void setActivePlayerScore() {
		if (activePlayer == playerLeft)
			FindObjectOfType<UI>().setScoreL(activePlayer.getScore());
		else
			FindObjectOfType<UI>().setScoreR(activePlayer.getScore());
	}
	public void setActivePlayerShotsLeft() {
		if (activePlayer == playerLeft)
			FindObjectOfType<UI>().setShotsLeftL(activePlayer.getShotsLeft());
		else
			FindObjectOfType<UI>().setShotsLeftR(activePlayer.getShotsLeft());
	}
	public void showWinPanel(){
		StartCoroutine(FindObjectOfType<UI>().enableWinPanelAfter(activePlayer == playerLeft, 1));
	}
	public void showGoalPanel() { StartCoroutine(FindObjectOfType<UI>().enableGoalPanelFor(1)); }
	public void showFaulPanel() { StartCoroutine(FindObjectOfType<UI>().enableFaulPanelFor(1)); }
	
	// Setters & Getters
	public void setState(MatchState state) { this.state = state; }
	public void setActivePlayer(Player player) { activePlayer = player; }
	public CoinSet getCoinSet() { return coinSet; }
	public Player getPlayerLeft() { return playerLeft; }
	public Player getPlayerRight() { return playerRight; }
	public Player getActivePlayer() { return activePlayer; }
	public ushort getWinningScore() { return winningScore; }
}