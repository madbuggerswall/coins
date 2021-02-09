using UnityEngine.Events;

public class PuzzleEvents : GameEvents {
	public UnityEvent puzzleEnded;

	public PuzzleEvents(Puzzle puzzle) : base() {
		puzzleEnded = new UnityEvent();
	}
}