using UnityEngine;
using UnityEngine.Events;

public class CoinSetEvents {
	public UnityEvent coinStatusChanged;
	public UnityEvent coinShot;
	public UnityEvent shotEnded;

	public CoinSetEvents() {
		coinStatusChanged = new UnityEvent();
		coinShot = new UnityEvent();
		shotEnded = new UnityEvent();
		determineGameType();
	}

	// Assign shot ended event according to level type.
	void determineGameType() {
		Match match = GameObject.FindObjectOfType<Match>();
		Puzzle puzzle = GameObject.FindObjectOfType<Puzzle>();
		if (match != null) {
			Debug.Log("Match");
			shotEnded.AddListener(() => match.playerShotEnded.Invoke());
		} else if (puzzle != null) {
			Debug.Log("Puzzle");
			shotEnded.AddListener(() => puzzle.playerShotEnded.Invoke());
		}
	}
}