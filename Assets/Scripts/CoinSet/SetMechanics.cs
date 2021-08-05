using UnityEngine;
using UnityEngine.Events;

// Checks pass-through line only when a coin is shot.
public class SetMechanics {
	CoinSet coinSet;
	bool passedThrough = false;
	public UnityAction checkFoulLine = () => { };

	public SetMechanics(CoinSet coinSet) {
		this.coinSet = coinSet;
		LevelManager.getInstance().events.coinShot.AddListener(selectFoulLine);
		LevelManager.getInstance().events.playerContinuesTurn.AddListener(resetPassedThrough);
	}

	public void selectFoulLine() {
		int coinSelection = 0;
		Coin[] coins = coinSet.getCoins();
		for (int index = 0; index < coins.Length; index++) {
			bool coinShot = (coins[index].GetComponent<Slingshot>().getCoinStatus() & CoinStatus.shot) > 0;
			if (coinShot) {
				coinSelection = (1 << index);
				if (coinSelection == 1) {
					checkFoulLine = () => checkLineBetween(coins[1].transform.position, coins[2].transform.position);
				} else if (coinSelection == 2) {
					checkFoulLine = () => checkLineBetween(coins[0].transform.position, coins[2].transform.position);
				} else if (coinSelection == 4) {
					checkFoulLine = () => checkLineBetween(coins[0].transform.position, coins[1].transform.position);
				}
			}
		}
	}

	// Checks a line cast between two points to detect pass-through.
	void checkLineBetween(Vector3 startPos, Vector3 endPos) {
		int layerMask = 1 << Layers.thrownCoin;
		if (Physics.Linecast(startPos, endPos, layerMask)) {
			passedThrough = true;
			LevelManager.getInstance().events.coinPassedThrough.Invoke();
		}
	}

	// Checks if all coins are stationary.
	bool hasCoinsStopped() {
		bool result = true;
		foreach (Coin coin in coinSet.getCoins()) {
			bool coinStopped = coin.getRigidbody().IsSleeping();
			result = result && coinStopped;
		}
		return result;
	}

	// Invokes coin shot ended event if coin shots are stationry
	public void checkCoinSetStationary() {
		if (hasCoinsStopped()) {
			LevelManager.getInstance().events.coinShotEnded.Invoke();
		}
	}

	public bool hasPassedThrough() { return passedThrough; }
	public void resetPassedThrough() { passedThrough = false; }
}