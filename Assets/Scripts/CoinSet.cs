using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

enum CoinSelection {
	CoinA = (1 << 0),
	CoinB = (1 << 1),
	CoinC = (1 << 2)
}

public class CoinSetEvents {
	public UnityEvent coinStatusChanged;
	public UnityEvent coinShot;
	public CoinSetEvents() {
		coinStatusChanged = new UnityEvent();
		coinShot = new UnityEvent();
	}
}

public class CoinSet : MonoBehaviour {
	public CoinSetEvents events;

	public UnityAction checkFaulLine = () => { };
	public UnityAction drawGuideLine = () => { };

	Coin[] coins;
	Guide guide;
	bool passedThrough = false;
	Pair<Vector3> endpoints = new Pair<Vector3>();

	CoinSetState state;
	void Awake() {
		coins = GetComponentsInChildren<Coin>();
		guide = GetComponentInChildren<Guide>();

		events = new CoinSetEvents();
		events.coinStatusChanged.AddListener(selectGuide);
		events.coinShot.AddListener(selectFaulLine);
		events.coinShot.AddListener(() => setState(new ShotState(this)));

		state = new AimState(this);
	}

	void Update() {
		drawGuideLine();
		state.checkPassThrough();
		state.hasCoinsStopped();
	}

	void selectFaulLine() {
		int coinSelection = 0;
		for (int index = 0; index < coins.Length; index++) {
			bool coinShot = (coins[index].GetComponent<Slingshot>().getCoinStatus() & CoinStatus.shot) > 0;
			if (coinShot) {
				coinSelection = (1 << index);
				if (coinSelection == 1) {
					checkFaulLine = () => checkLineBetween(coins[1].transform.position, coins[2].transform.position);
				} else if (coinSelection == 2) {
					checkFaulLine = () => checkLineBetween(coins[0].transform.position, coins[2].transform.position);
				} else if (coinSelection == 4) {
					checkFaulLine = () => checkLineBetween(coins[0].transform.position, coins[1].transform.position);
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
	public void selectGuide() {
		Debug.Log("selectGuide");
		guide.enable(true);
		CoinStatus maxCoinStatus = 0;
		int maxCoinStatusIndex = 0;
		for (int index = 0; index < coins.Length; index++) {
			CoinStatus coinStatus = coins[index].GetComponent<Slingshot>().getCoinStatus();
			if (coinStatus > maxCoinStatus) {
				maxCoinStatus = coinStatus;
				maxCoinStatusIndex = index;
			}
		}
		if (maxCoinStatus > 0) {
			selectCoinPair(maxCoinStatusIndex);
		} else
			guide.enable(false);
	}

	void selectCoinPair(int index) {
		int coinSelection = (1 << index);
		if (coinSelection == 1) {
			drawGuideLine = () => guide.setPoints(coins[1].transform.position, coins[2].transform.position);
		} else if (coinSelection == 2) {
			drawGuideLine = () => guide.setPoints(coins[0].transform.position, coins[2].transform.position);
		} else if (coinSelection == 4) {
			drawGuideLine = () => guide.setPoints(coins[0].transform.position, coins[1].transform.position);
		}
	}

	public void disableGuide() {
		guide.enable(false);
	}

	public void enableControls() {
		foreach (Slingshot slingshot in GetComponentsInChildren<Slingshot>()) {
			slingshot.enableControls();
		}
	}

	public void disableControls() {
		foreach (Slingshot slingshot in GetComponentsInChildren<Slingshot>()) {
			slingshot.disableControls();
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