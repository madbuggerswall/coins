// using UnityEngine;

// public interface State { }

// public class PlayerTurn : State {
// 	Puzzle puzzle;

// 	CoinSet coinSet;
// 	bool isPlayerScored;
	
// 	public PlayerTurn(Puzzle puzzle) {
// 		this.puzzle = puzzle;
// 		coinSet = puzzle.getCoinSet();
// 		isPlayerScored = false;

// 		coinSet.setState(new AimState(coinSet));
// 		puzzle.playerScored.AddListener(playerScored);
// 		puzzle.playerShotEnded.AddListener(evaluateShot);
// 	}

// 	void playerScored() {
// 		isPlayerScored = true;
// 	}

// 	void evaluateShot() {
// 		if (playerFouled()) {
// 			puzzle.showFaulPanel();            //	UI
// 			puzzle.resetActivePlayerShotsUI(); //	UI
// 			removeListeners();
// 			puzzle.passTurn();
// 		} else if (isPlayerScored) {
// 			// UI
// 			puzzle.resetActivePlayerShotsUI();
// 			removeListeners();
// 			puzzle.setState(new PlayerScored(puzzle));
// 		} else if (playerHasShotsLeft()) {
// 			// UI
// 			puzzle.setActivePlayerShotsLeft();
// 			continueTurn();
// 		} else {
// 			puzzle.resetActivePlayerShotsUI();
// 			removeListeners();
// 			puzzle.passTurn();
// 		}
// 	}



// 	bool playerFouled() {
// 		if (coinSet.getMechanics().hasPassedThrough())
// 			return false;
// 		return true;
// 	}

// 	bool playerHasShotsLeft() {
// 		puzzle.getActivePlayer().decrementShotsLeft();
// 		if (puzzle.getActivePlayer().getShotsLeft() > 0)
// 			return true;
// 		return false;
// 	}

// 	void continueTurn() {
// 		coinSet.setState(new AimState(coinSet));
// 	}

// 	void removeListeners() {
// 		puzzle.playerScored.RemoveListener(playerScored);
// 		puzzle.playerShotEnded.RemoveListener(evaluateShot);
// 	}
// }

// public class PlayerScored : State {
// 	Puzzle puzzle;
// 	CoinSet coinSet;

// 	public PlayerScored(Puzzle puzzle) {
// 		this.puzzle = puzzle;
// 		coinSet = puzzle.getCoinSet();
// 		puzzle.showGoalPanel();
// 		puzzle.getActivePlayer().incrementScore();
// 		puzzle.setActivePlayerScore();
// 		evaluateWin();
// 	}

// 	void evaluateWin() {
// 		if (playerWon()) {
// 			puzzle.setState(new PuzzleEnded(puzzle));
// 		} else {
// 			puzzle.passTurn();
// 		}
// 	}
// }

// public class PuzzleEnded : State {
// 	Puzzle puzzle;
// 	public PuzzleEnded(Puzzle puzzle) {
// 		this.puzzle = puzzle;
// 		puzzle.showWinPanel();
// 	}
// }