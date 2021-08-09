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


	void decrementShotsLeft() {
		shotsLeft--;
		if (shotsLeft <= 0) LevelManager.getInstance().events.playerHasNoShotsLeft.Invoke();
	}
	void incrementCollectibles() { collectibles++; }
	public void incrementScore() { score++; }
	public void setShotsLeft(int shotsLeft) { this.shotsLeft = shotsLeft; }
	public int getShotsLeft() { return shotsLeft; }
	public int getScore() { return score; }
}
