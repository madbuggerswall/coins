using UnityEngine;
using System;

[Serializable]
public class Player {
	[SerializeField] int shotsLeft;
	[SerializeField] int score;

	private Player() { }

	public void incrementScore() { score++; }
	public void decrementShotsLeft() { shotsLeft--; }
	public void setShotsLeft(int shotsLeft) { this.shotsLeft = shotsLeft; }
	public int getShotsLeft() { return shotsLeft; }
	public int getScore() { return score; }
}
