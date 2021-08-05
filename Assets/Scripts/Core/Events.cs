using UnityEngine.Events;

public class Events {
	public UnityEvent coinStatusChanged;
	public UnityEvent coinShot;
	public UnityEvent coinPassedThrough;
	public UnityEvent coinShotInGoal;
	public UnityEvent coinShotEnded;

	public UnityEvent collectibleCollected;
	public UnityEvent obstacleHit;

	public UnityEvent playerReady;
	public UnityEvent playerFouled;
	public UnityEvent playerScored;
	public UnityEvent playerContinuesTurn;
	public UnityEvent playerHasNoShotsLeft;
	public UnityEvent sessionEnded;

	// Puzzle
	public UnityEvent cardPlayed;
	public UnityEvent cardApplied;

	public UnityEvent gamePaused;
	public UnityEvent gameUnpaused;
	public UnityEvent cardDeckRevealed;
	public UnityEvent cardDeckHidden;

	public Events() {
		coinStatusChanged = new UnityEvent();
		coinShot = new UnityEvent();
		coinPassedThrough = new UnityEvent();
		coinShotInGoal = new UnityEvent();
		coinShotEnded = new UnityEvent();

		collectibleCollected = new UnityEvent();
		obstacleHit = new UnityEvent();

		playerReady = new UnityEvent();
		playerFouled = new UnityEvent();
		playerScored = new UnityEvent();
		playerContinuesTurn = new UnityEvent();
		// playerTurnPassed = new UnityEvent();
		playerHasNoShotsLeft = new UnityEvent();
		sessionEnded = new UnityEvent();

		// Puzzle
		cardPlayed = new UnityEvent();
		cardApplied = new UnityEvent();

		gamePaused = new UnityEvent();
		gameUnpaused = new UnityEvent();
		cardDeckRevealed = new UnityEvent();
		cardDeckHidden = new UnityEvent();
	}
}
