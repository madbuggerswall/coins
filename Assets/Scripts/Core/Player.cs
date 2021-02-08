using UnityEngine;
using System;

[Serializable]
public class Player {
	static readonly int maxShots = 3;
	[SerializeField] int shotsLeft;
	[SerializeField] int score;

	public Player() {
		restoreShotsLeft();
		score = 0;
	}

	public void incrementScore() { score++; }
	public void decrementShotsLeft() { shotsLeft--; }
	public void restoreShotsLeft() { shotsLeft = maxShots; }
	public int getShotsLeft() { return shotsLeft; }
	public int getScore() { return score; }
}
