using UnityEngine;
namespace MatchState {
	public interface State { }

	public class CoinToss : State {
		Match match;
		public CoinToss(Match match) {
			this.match = match;
			tossCoin();
			((MatchEvents) match.getEvents()).playerTurnPassed.Invoke();
			match.startResettingCoins();
		}

		void tossCoin() {
			if (Random.value < 0.5f) {
				match.setPlayer(match.getPlayerLeft());
				match.blockLeftPost(true);
			} else {
				match.setPlayer(match.getPlayerRight());
				match.blockLeftPost(false);
			}
		}
	}

	public class PlayerTurn : State {
		Match match;
		public PlayerTurn(Match match) {
			this.match = match;
			match.setPlayerShotInGoal(false);
			match.getCoinSet().setState(new AimState(match.getCoinSet()));
		}
	}

	public class PlayerScored : State {
		Match match;
		CoinSet coinSet;

		public PlayerScored(Match match) {
			this.match = match;
			coinSet = match.getCoinSet();
			match.getPlayer().incrementScore();
			match.getEvents().playerScored.Invoke();
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
			if (match.getPlayer().getScore() >= match.getWinningScore())
				return true;
			return false;
		}
	}

	public class MatchEnded : State {
		Match match;
		public MatchEnded(Match match) {
			this.match = match;
			((MatchEvents) match.getEvents()).matchEnded.Invoke();
		}
	}
}