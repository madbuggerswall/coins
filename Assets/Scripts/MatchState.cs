using UnityEngine;

public interface MatchState {

}

public class CoinToss : MatchState {
	Match match;
	public CoinToss(Match match) {
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
	public PlayerTurn(Match match) {
		this.match = match;
		match.playerScored.AddListener(playerScored);
		match.playerShotEnded.AddListener(checkShotsLeft);
	}

	void playerScored() {
		match.setState(new PlayerScored(match));
	}

	void checkShotsLeft() {
		CoinSet coinSet = match.getCoinSet();
		match.getActivePlayer().decrementShotsLeft();
		if (match.getActivePlayer().getShotsLeft() > 0) {
			coinSet.setState(new AimState(coinSet));
		} else {
			match.passTurn();
			coinSet.setState(new AimState(coinSet));
		}
	}
}

public class PlayerScored : MatchState {
	Match match;
	public PlayerScored(Match match) {
		this.match = match;
		match.getActivePlayer().incrementScore();
	}
}

public class MatchEnded : MatchState {
	Match match;
	public MatchEnded(Match match) {
		this.match = match;
	}
}