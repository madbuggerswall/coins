using UnityEngine.Events;

public class PuzzleEvents {

	public UnityEvent playerShotInGoal;
	public UnityEvent playerShotEnded;
	public UnityEvent playerFouled;
	public UnityEvent playerScored;
	public UnityEvent playerContinuesTurn;
	public UnityEvent playerHasNoShotsLeft;
	public UnityEvent puzzleEnded;

	public PuzzleEvents(Puzzle puzzle) {
		playerShotInGoal = new UnityEvent();
		playerShotEnded = new UnityEvent();
		playerFouled = new UnityEvent();
		playerScored = new UnityEvent();
		playerContinuesTurn = new UnityEvent();
		playerHasNoShotsLeft = new UnityEvent();
		puzzleEnded = new UnityEvent();
	}
}