using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-4)]
public class LevelManager : MonoBehaviour {
	static LevelManager instance;
	public Events events;

	// Gametype utils
	public Func<CoinGame> getGame;
	Puzzle puzzle;
	Match match;

	Level level;
	[SerializeField] Stats stats;
	[SerializeField] Achievements achievements;

	void Awake() {
		assertSingleton();
		selectGameType();

		events = new Events();
		stats.initialize();
		achievements.initialize(stats);

	}

	void Start() {
		SceneTransitionUI.getInstance().lighten();
	}
	
	void OnApplicationQuit() {
		Debug.Log("Application has quit");
		SaveManager.save<Stats>(stats, FilePath.stats);
		SaveManager.save<Achievements>(achievements, FilePath.achievements);
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