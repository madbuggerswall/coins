using UnityEngine.Events;

public abstract class GameEvents {
	public UnityEvent playerShotInGoal;
	public UnityEvent playerShotEnded;
	public UnityEvent playerFouled;
	public UnityEvent playerScored;
	public UnityEvent playerContinuesTurn;
	public UnityEvent playerHasNoShotsLeft;

	protected GameEvents() {
		playerShotInGoal = new UnityEvent();
		playerShotEnded = new UnityEvent();
		playerFouled = new UnityEvent();
		playerScored = new UnityEvent();
		playerContinuesTurn = new UnityEvent();
		playerHasNoShotsLeft = new UnityEvent();
	}
}
