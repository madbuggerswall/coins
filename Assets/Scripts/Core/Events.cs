using UnityEngine.Events;

public class Events {
	public UnityEvent coinStatusChanged;
	public UnityEvent coinShot;
	public UnityEvent coinShotInGoal;
	public UnityEvent coinShotEnded;

	public UnityEvent collectibleCollected;
	public UnityEvent obstacleHit;

	public UnityEvent playerFouled;
	public UnityEvent playerScored;
	public UnityEvent playerContinuesTurn;
	public UnityEvent playerTurnPassed;
	public UnityEvent playerHasNoShotsLeft;
	public UnityEvent sessionEnded;

	// Puzzle
	public UnityEvent cardPlayed;
	public UnityEvent cardApplied;

	public Events() {
		coinStatusChanged = new UnityEvent();
		coinShot = new UnityEvent();
		coinShotInGoal = new UnityEvent();
		coinShotEnded = new UnityEvent();

		collectibleCollected = new UnityEvent();
		obstacleHit = new UnityEvent();

		playerFouled = new UnityEvent();
		playerScored = new UnityEvent();
		playerContinuesTurn = new UnityEvent();
		playerTurnPassed = new UnityEvent();
		playerHasNoShotsLeft = new UnityEvent();
		sessionEnded = new UnityEvent();

		// Puzzle
		cardPlayed = new UnityEvent();
		cardApplied = new UnityEvent();
	}
}
