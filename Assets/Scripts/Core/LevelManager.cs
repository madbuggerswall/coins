using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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