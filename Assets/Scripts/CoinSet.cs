using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

enum CoinSelection {
	CoinA = (1 << 0),
	CoinB = (1 << 1),
	CoinC = (1 << 2)
}

public class CoinSet : MonoBehaviour {
	public CoinSetState state;

	Coin[] coins;
	Guide guide;
	bool passedThrough = false;

	public delegate void CheckFaulLineDelegate();
	public CheckFaulLineDelegate checkFaulLine;

	void Awake() {
		coins = GetComponentsInChildren<Coin>();
		guide = GetComponentInChildren<Guide>();

		Match.getInstance().coinStatusChanged.AddListener(drawGuide);
		Match.getInstance().coinShot.AddListener(selectFaulLine);
		Match.getInstance().coinShot.AddListener(() => { setState(new ShotState(this)); });

		state = new AimState(this);
	}

	// Update is called once per frame
	void Update() {
		state.checkPassThrough();
		state.hasCoinsStopped();
	}

	// Move to guide class
	void drawGuide() {
		guide.enable(true);
		int coinSelection = 0;
		for (int index = 0; index < coins.Length; index++) {
			bool coinSelected = coins[index].GetComponent<Slingshot>().getCoinStatus() > 0;

			if (coinSelected) {
				coinSelection = (1 << index);
				if (coinSelection == 1) {
					guide.setPoints(coins[1].transform.position, coins[2].transform.position);
				} else if (coinSelection == 2) {
					guide.setPoints(coins[0].transform.position, coins[2].transform.position);
				} else if (coinSelection == 4) {
					guide.setPoints(coins[0].transform.position, coins[1].transform.position);
				}
				return;
			}
		}
		guide.enable(false);
	}

	void selectFaulLine() {
		int coinSelection = 0;
		for (int index = 0; index < coins.Length; index++) {
			bool coinShot = (coins[index].GetComponent<Slingshot>().getCoinStatus() & CoinStatus.shot) > 0;
			if (coinShot) {
				coinSelection = (1 << index);
				if (coinSelection == 1) {
					checkFaulLine = () => { checkLineBetween(coins[1].transform.position, coins[2].transform.position); };
				} else if (coinSelection == 2) {
					checkFaulLine = () => { checkLineBetween(coins[0].transform.position, coins[2].transform.position); };
				} else if (coinSelection == 4) {
					checkFaulLine = () => { checkLineBetween(coins[0].transform.position, coins[1].transform.position); };
				}
			}
		}
	}

	void checkLineBetween(Vector3 startPos, Vector3 endPos) {
		int layerMask = 1 << Layers.thrownCoin;
		if (Physics.Linecast(startPos, endPos, layerMask)) {
			passedThrough = true;
			Debug.Log("In between");
		}
	}

	public bool hasCoinsStopped() {
		bool result = true;
		foreach (Coin coin in coins) {
			bool coinStopped = coin.GetComponent<Rigidbody>().IsSleeping();
			result = result && coinStopped;
		}
		return result;
	}

	// State functions
	public void enableControls() {
		foreach (Coin coin in coins) {
			coin.GetComponent<Slingshot>().enabled = true;
		}
	}

	public void disableControls() {
		foreach (Coin coin in coins) {
			coin.GetComponent<Slingshot>().enabled = false;
		}
	}

	public void clearAllFlags() {
		foreach (Coin coin in coins) {
			coin.GetComponent<Slingshot>().clearFlags();
			coin.gameObject.layer = Layers.coin;
		}
	}

	// Setters & Getters
	public void setState(CoinSetState state) { this.state = state; }
	public bool hasPassedThrough() { return passedThrough; }
}


public interface CoinSetState {
	void hasCoinsStopped();
	void checkPassThrough();
}

public class AimState : CoinSetState {
	CoinSet coinSet;

	public AimState(CoinSet coinSet) {
		this.coinSet = coinSet;
		coinSet.enableControls();
	}
	public void hasCoinsStopped() { }
	public void checkPassThrough() { }
}

public class ShotState : CoinSetState {
	CoinSet coinSet;

	public ShotState(CoinSet coinSet) {
		this.coinSet = coinSet;
		coinSet.disableControls();
	}

	public void hasCoinsStopped() {
		if (coinSet.hasCoinsStopped()) {
			coinSet.setState(new StationaryState(coinSet));
		}
	}

	public void checkPassThrough() {
		coinSet.checkFaulLine();
	}
}

public class StationaryState : CoinSetState {
	CoinSet coinSet;
	public StationaryState(CoinSet coinSet) {
		this.coinSet = coinSet;
		coinSet.clearAllFlags();
		if (!coinSet.hasPassedThrough()) {
			Debug.Log("Did not passed through");
		}else{
			Debug.Log("Passed through");
		}
	}
	public void hasCoinsStopped() { }
	public void checkPassThrough() { }
}
