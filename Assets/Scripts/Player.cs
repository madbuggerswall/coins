using UnityEngine;
using System;

[Serializable]
public class Player {
	static readonly ushort maxShots = 3;
	[SerializeField] ushort shotsLeft;
	[SerializeField] ushort score;

	public Player() {
		restoreShotsLeft();
		score = 0;
	}

	public void incrementScore() { score++; }
	public void decrementShotsLeft() { shotsLeft--; }
	public void restoreShotsLeft() { shotsLeft = maxShots; }
	public ushort getShotsLeft() { return shotsLeft; }
}
