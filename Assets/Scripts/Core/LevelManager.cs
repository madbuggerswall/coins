using UnityEngine;
using System;

[DefaultExecutionOrder(-4)]
public class LevelManager : MonoBehaviour {
	static LevelManager instance;
	public Events events;

	public Func<CoinGame> getGame;
	Puzzle puzzle;
	Match match;

	void Awake() {
		assertSingleton();
		events = new Events();
		selectGameType();


		// Debug
		events.coinStatusChanged.AddListener(() => Debug.Log("coinStatusChanged"));
		events.coinShot.AddListener(() => Debug.Log("coinShot"));
		events.coinShotInGoal.AddListener(() => Debug.Log("coinShotInGoal"));
		events.coinShotEnded.AddListener(() => Debug.Log("coinShotEnded"));
		events.playerFouled.AddListener(() => Debug.Log("playerFouled"));
		events.playerScored.AddListener(() => Debug.Log("playerScored"));
		events.playerContinuesTurn.AddListener(() => Debug.Log("playerContinuesTurn"));
		events.playerTurnPassed.AddListener(() => Debug.Log("playerTurnPassed"));
		events.playerHasNoShotsLeft.AddListener(() => Debug.Log("playerHasNoShotsLeft"));
		events.sessionEnded.AddListener(() => Debug.Log("sessionEnded"));
		events.cardPlayed.AddListener(() => Debug.Log("cardPlayed"));
		events.cardApplied.AddListener(() => Debug.Log("cardApplied"));
	}

	// Singleton 
	public static LevelManager getInstance() { return instance; }
	void assertSingleton() { if (instance == null) { instance = this; } else { Destroy(gameObject); } }

	void selectGameType() {
		puzzle = FindObjectOfType<Puzzle>();
		match = FindObjectOfType<Match>();
		if (puzzle == null) {
			getGame = () => { return match; };
		} else {
			getGame = () => { return puzzle; };
		}
	}
}