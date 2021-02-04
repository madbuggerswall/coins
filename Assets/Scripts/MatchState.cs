using UnityEngine;

public interface MatchState { }

public class CoinToss : MatchState {
	Match match;
	public CoinToss(Match match) {
		Debug.Log("CoinToss");
		this.match = match;
		tossCoin();
		match.setState(new PlayerTurn(match));
	}

	void tossCoin() {
		if (Random.value < 0.5f)
			match.setActivePlayer(match.getPlayerLeft());
		else
			match.setActivePlayer(match.getPlayerRight());
	}
}

public class PlayerTurn : MatchState {
	Match match;

	CoinSet coinSet;
	bool isPlayerScored;
	public PlayerTurn(Match match) {
		Debug.Log("PlayerTurn");
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
			passTurnToOtherPlayer();
		} else if (isPlayerScored) {
			match.setState(new PlayerScored(match));
		} else if (playerHasShotsLeft()) {
			continueTurn();
		} else {
			passTurnToOtherPlayer();
		}
	}

	bool playerFouled() {
		if (coinSet.hasPassedThrough())
			return false;
		return true;
	}

	bool playerHasShotsLeft() {
		match.getActivePlayer().decrementShotsLeft();
		if (match.getActivePlayer().getShotsLeft() > 0)
			return true;
		return false;
	}

	void passTurnToOtherPlayer() {
		Debug.Log("passTurn");
		match.playerScored.RemoveListener(playerScored);
		match.playerShotEnded.RemoveListener(evaluateShot);

		match.getActivePlayer().restoreShotsLeft();
		match.passTurn();
		match.setState(new PlayerTurn(match));
	}

	void continueTurn() {
		coinSet.setState(new AimState(coinSet));
	}
}

public class PlayerScored : MatchState {
	Match match;
	CoinSet coinSet;

	public PlayerScored(Match match) {
		Debug.Log("PlayerScored");
		this.match = match;
		coinSet = match.getCoinSet();
		match.getActivePlayer().incrementScore();

		evaluateWin();
	}

	void evaluateWin() {
		if (playerWon()) {
			match.setState(new MatchEnded(match));
		} else {
			passTurnToOtherPlayer();
		}
	}

	void passTurnToOtherPlayer() {
		Debug.Log("passTurn");
		match.playerScored.RemoveAllListeners();
		match.playerShotEnded.RemoveAllListeners();

		match.getActivePlayer().restoreShotsLeft();
		match.passTurn();
		match.setState(new PlayerTurn(match));
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
	}
}