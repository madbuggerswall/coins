using UnityEngine;
using UnityEngine.Events;

// Checks pass-through line only when a coin is shot.
public class SetMechanics {
	CoinSet coinSet;
	bool passedThrough = false;
	bool hasPlayerShotInGoal;

	public UnityAction checkFoulLine = delegate { };
	public UnityAction hasCoinShotEnded = delegate { };

	public SetMechanics(CoinSet coinSet) {
		this.coinSet = coinSet;

		LevelManager.getInstance().events.coinShot.AddListener(delegate { hasCoinShotEnded = isCoinSetStationary; });

		LevelManager.getInstance().events.coinShotEnded.AddListener(delegate {
			hasCoinShotEnded = delegate { };
			checkFoulLine = delegate { };
		});

		LevelManager.getInstance().events.coinShot.AddListener(selectFoulLine);
		LevelManager.getInstance().events.playerContinuesTurn.AddListener(delegate { passedThrough = false; });

		LevelManager.getInstance().events.coinShotInGoal.AddListener(delegate { hasPlayerShotInGoal = true; });
		LevelManager.getInstance().events.coinShotEnded.AddListener(evaluateShot);
	}

	void evaluateShot() {
		Events events = LevelManager.getInstance().events;
		if (!passedThrough) {
			events.playerFouled.Invoke();
			hasPlayerShotInGoal = false;
		} else if (hasPlayerShotInGoal) {
			events.playerScored.Invoke();
		} else if (!playerHasShotsLeft()) {
			events.playerHasNoShotsLeft.Invoke();
		} else {
			events.playerShotValid.Invoke();
			events.playerContinuesTurn.Invoke();
		}
	}

	bool playerHasShotsLeft() {
		if (LevelManager.getInstance().getPlayer().getShotsLeft() > 0)
			return true;
		return false;
	}

	// Selects a coin pair to detect pass-through 
	void selectFoulLine() {
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

	// Casts a line between two points to detect pass-through.
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

	void isCoinSetStationary() {
		if (hasCoinsStopped()) {
			LevelManager.getInstance().events.coinShotEnded.Invoke();
		}
	}
}