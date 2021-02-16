using UnityEngine;

// Base class for Match and Puzzle levels
public abstract class CoinGame : MonoBehaviour {
	protected CoinSet coinSet;
	[SerializeField] protected Player player;
	protected bool hasPlayerShotInGoal;

	protected bool playerFouled() {
		if (coinSet.getMechanics().hasPassedThrough())
			return false;
		return true;
	}
	protected bool playerHasShotsLeft() {
		player.decrementShotsLeft();
		if (player.getShotsLeft() > 0)
			return true;
		return false;
	}
	protected void continueTurn() {
		coinSet.setState(new AimState(coinSet));
	}
	protected abstract void evaluateShot();

	public void setPlayerShotInGoal(bool value) { hasPlayerShotInGoal = value; }
	public CoinSet getCoinSet() { return coinSet; }
	public Player getPlayer() { return player; }
}