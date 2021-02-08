using UnityEngine;
namespace MatchState {
	public interface State { }

	public class CoinToss : State {
		Match match;
		public CoinToss(Match match) {
			this.match = match;
			tossCoin();
			match.events.playerTurnPassed.Invoke();
			match.startResettingCoins();
		}

		void tossCoin() {
			if (Random.value < 0.5f) {
				match.setActivePlayer(match.getPlayerLeft());
				match.blockLeftPost(true);
			} else {
				match.setActivePlayer(match.getPlayerRight());
				match.blockLeftPost(false);
			}
		}
	}

	public class PlayerTurn : State {
		Match match;

		CoinSet coinSet;
		bool hasPlayerShotInGoal;
		public PlayerTurn(Match match) {
			this.match = match;
			coinSet = match.getCoinSet();
			hasPlayerShotInGoal = false;

			coinSet.setState(new AimState(coinSet));
			match.events.playerShotInGoal.AddListener(playerShotInGoal);
			match.events.playerShotEnded.AddListener(evaluateShot);
		}

		void playerShotInGoal() {
			hasPlayerShotInGoal = true;
		}

		// Move this function to Match
		void evaluateShot() {
			if (playerFouled()) {
				match.events.playerFouled.Invoke();
				removeListeners();
				match.passTurn();
			} else if (hasPlayerShotInGoal) {
				match.events.playerScored.Invoke();
				removeListeners();
				match.setState(new PlayerScored(match));
			} else if (playerHasShotsLeft()) {
				match.events.playerContinuesTurn.Invoke();
				continueTurn();
			} else {
				match.events.playerHasNoShotsLeft.Invoke();
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
			match.events.playerShotInGoal.RemoveListener(playerShotInGoal);
			match.events.playerShotEnded.RemoveListener(evaluateShot);
		}
	}

	public class PlayerScored : State {
		Match match;
		CoinSet coinSet;

		public PlayerScored(Match match) {
			this.match = match;
			coinSet = match.getCoinSet();
			match.getActivePlayer().incrementScore();
			match.events.playerScored.Invoke();
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

	public class MatchEnded : State {
		Match match;
		public MatchEnded(Match match) {
			this.match = match;
			match.events.matchEnded.Invoke();
		}
	}
}