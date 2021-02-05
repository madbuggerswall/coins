using UnityEngine;

public interface MatchState { }

public class CoinToss : MatchState {
	Match match;
	public CoinToss(Match match) {
		this.match = match;
		tossCoin();
		match.startResettingCoins();
		GameObject.FindObjectOfType<UI>().resetShotsLeftL();
		GameObject.FindObjectOfType<UI>().resetShotsLeftR();
	}

	void tossCoin() {
		if (Random.value < 0.5f) {
			match.setActivePlayer(match.getPlayerLeft());
			match.blockLeftPost(true);
			GameObject.FindObjectOfType<UI>().passTurn(true);
		} else {
			match.setActivePlayer(match.getPlayerRight());
			match.blockLeftPost(false);
			GameObject.FindObjectOfType<UI>().passTurn(false);
		}
	}
}

public class PlayerTurn : MatchState {
	Match match;

	CoinSet coinSet;
	bool isPlayerScored;
	public PlayerTurn(Match match) {
		this.match = match;
		coinSet = match.getCoinSet();
		isPlayerScored = false;

		coinSet.setState(new AimState(coinSet));
		match.playerScored.AddListener(playerScored);
		match.playerShotEnded.AddListener(evaluateShot);
	}

	void playerScored() {
		isPlayerScored = true;
	}

	void evaluateShot() {
		if (playerFouled()) {
			match.showFaulPanel();            //	UI
			match.resetActivePlayerShotsUI(); //	UI
			removeListeners();
			match.passTurn();
		} else if (isPlayerScored) {
			// UI
			match.resetActivePlayerShotsUI();
			removeListeners();
			match.setState(new PlayerScored(match));
		} else if (playerHasShotsLeft()) {
			// UI
			match.setActivePlayerShotsLeft();
			continueTurn();
		} else {
			match.resetActivePlayerShotsUI();
			removeListeners();
			match.passTurn();
		}
	}



	bool playerFouled() {
		if (coinSet.getMechanics().hasPassedThrough())
			return false;
		return true;
	}

	bool playerHasShotsLeft() {
		match.getActivePlayer().decrementShotsLeft();
		if (match.getActivePlayer().getShotsLeft() > 0)
			return true;
		return false;
	}

	void continueTurn() {
		coinSet.setState(new AimState(coinSet));
	}

	void removeListeners() {
		match.playerScored.RemoveListener(playerScored);
		match.playerShotEnded.RemoveListener(evaluateShot);
	}
}

public class PlayerScored : MatchState {
	Match match;
	CoinSet coinSet;

	public PlayerScored(Match match) {
		this.match = match;
		coinSet = match.getCoinSet();
		match.showGoalPanel();
		match.getActivePlayer().incrementScore();
		match.setActivePlayerScore();
		evaluateWin();
	}

	void evaluateWin() {
		if (playerWon()) {
			match.setState(new MatchEnded(match));
		} else {
			match.passTurn();
		}
	}

	bool playerWon() {
		if (match.getActivePlayer().getScore() >= match.getWinningScore())
			return true;
		return false;
	}
}

public class MatchEnded : MatchState {
	Match match;
	public MatchEnded(Match match) {
		this.match = match;
		match.showWinPanel();
	}
}