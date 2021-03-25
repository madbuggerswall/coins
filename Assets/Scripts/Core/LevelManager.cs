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

	Stats stats;
	Achievements achievements;

	void Awake() {
		assertSingleton();
		selectGameType();
		
		events = new Events();
		stats = new Stats();
		achievements = new Achievements(stats);
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