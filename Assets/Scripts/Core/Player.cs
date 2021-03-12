using UnityEngine;
using System;

[Serializable]
public class Player {
	[SerializeField] int shotsLeft;
	[SerializeField] int score;
	[SerializeField] int collectibles;

	private Player() { }

	public void incrementScore() { score++; }
	public void incrementCollectibles() { collectibles++; }
	public void decrementShotsLeft() { shotsLeft--; }
	public void setShotsLeft(int shotsLeft) { this.shotsLeft = shotsLeft; }
	public int getShotsLeft() { return shotsLeft; }
	public int getScore() { return score; }
}
