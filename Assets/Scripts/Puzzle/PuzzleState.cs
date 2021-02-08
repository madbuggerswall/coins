using UnityEngine;
namespace PuzzleState {
	public interface State { }

	public class PlayerTurn : State {
		Puzzle puzzle;

		public PlayerTurn(Puzzle puzzle) {
			this.puzzle = puzzle;
			puzzle.getCoinSet().setState(new AimState(puzzle.getCoinSet()));
		}
	}

	public class PlayerScored : State {
		Puzzle puzzle;

		public PlayerScored(Puzzle puzzle) {
			this.puzzle = puzzle;
		}
	}

	public class PuzzleEnded : State {
		Puzzle puzzle;
		public PuzzleEnded(Puzzle puzzle) {
			this.puzzle = puzzle;
		}
	}
}