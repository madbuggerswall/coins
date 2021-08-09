using UnityEngine;
using System;

public class Player : MonoBehaviour {
	[SerializeField] int shotsLeft;
	[SerializeField] int score;
	[SerializeField] int collectibles;

	void Awake() {
		LevelManager.getInstance().events.playerShotValid.AddListener(decrementShotsLeft);
		LevelManager.getInstance().events.collectibleCollected.AddListener(incrementCollectibles);
	}


	void incrementCollectibles() { collectibles++; }
	void decrementShotsLeft() { shotsLeft--; }
	public void incrementScore() { score++; }
	public void setShotsLeft(int shotsLeft) { this.shotsLeft = shotsLeft; }
	public int getShotsLeft() { return shotsLeft; }
	public int getScore() { return score; }
}
