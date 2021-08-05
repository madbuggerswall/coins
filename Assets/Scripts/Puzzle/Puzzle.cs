using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle : MonoBehaviour {
	CoinSet coinSet;
	Player player;
	protected bool hasPlayerShotInGoal;
	Formation formation;

	void Awake() {
		coinSet = CoinSet.getInstance();
		player = LevelManager.getInstance().getPlayer();
		LevelManager.getInstance().events.coinShotInGoal.AddListener(() => setPlayerShotInGoal(true));
		LevelManager.getInstance().events.coinShotEnded.AddListener(evaluateShot);
	}

	void Start() {
		formation = new Formation(coinSet.getCoins());
	}

	IEnumerator resetCoinSet() {
		yield return formation.resetCoinSet();
		LevelManager.getInstance().events.playerContinuesTurn.Invoke();
	}

	void evaluateShot() {
		Events events = LevelManager.getInstance().events;
		if (playerFouled()) {
			events.playerFouled.Invoke();
			setPlayerShotInGoal(false);
			StartCoroutine(resetCoinSet());
		} else if (hasPlayerShotInGoal) {
			events.playerScored.Invoke();
		} else if (playerHasShotsLeft()) {
			events.playerContinuesTurn.Invoke();
		} else {
			events.playerHasNoShotsLeft.Invoke();
		}
	}

	bool playerFouled() {
		if (coinSet.getMechanics().hasPassedThrough())
			return false;
		return true;
	}

	bool playerHasShotsLeft() {
		player.decrementShotsLeft();
		if (player.getShotsLeft() > 0)
			return true;
		return false;
	}

	// Setters & Getters
	public void setPlayerShotInGoal(bool value) { hasPlayerShotInGoal = value; }
	public CoinSet getCoinSet() { return coinSet; }
	public Player getPlayer() { return player; }
	public Formation getFormation() { return formation; }
}