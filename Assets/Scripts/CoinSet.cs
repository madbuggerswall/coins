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
	Coin[] coins;
	Guide guide;

	int layerMask;

	delegate void CheckLine();
	CheckLine checkLine;

	delegate void CheckAllStopped();
	CheckAllStopped checkAllStopped;

	bool hasFauled = true;
	CoinSetState state;

	void Awake() {
		coins = GetComponentsInChildren<Coin>();
		guide = GetComponentInChildren<Guide>();
		layerMask = 1 << Layers.thrownCoin;

		Match.getInstance().coinSelected.AddListener(drawGuide);
		Match.getInstance().coinDeselected.AddListener(deleteGuide);
		Match.getInstance().coinShot.AddListener(drawFaulLine);

		state = new AimState(this);

		checkLine = () => { };
		checkAllStopped = () => { };
	}

	// Update is called once per frame
	void Update() {
		checkLine();
		checkAllStopped();
	}

	void drawGuide() {
		guide.enable(true);
		int coinSelection = 0;
		for (int index = 0; index < coins.Length; index++) {
			bool coinSelected = coins[index].GetComponent<Slingshot>().isSelected();
			bool coinShot = coins[index].GetComponent<Slingshot>().isShot();

			if (coinSelected || coinShot) {
				coinSelection = (1 << index);
				if (coinSelection == 1) {
					guide.setPoints(coins[1].transform.position, coins[2].transform.position);
				} else if (coinSelection == 2) {
					guide.setPoints(coins[0].transform.position, coins[2].transform.position);
				} else if (coinSelection == 4) {
					guide.setPoints(coins[0].transform.position, coins[1].transform.position);
				}
			}
		}
	}

	void drawFaulLine() {
		int coinSelection = 0;
		for (int index = 0; index < coins.Length; index++) {
			bool coinShot = coins[index].GetComponent<Slingshot>().isShot();
			if (coinShot) {
				coinSelection = (1 << index);
				if (coinSelection == 1) {
					checkLine = () => { checkLineBetween(coins[1].transform.position, coins[2].transform.position); };
				} else if (coinSelection == 2) {
					checkLine = () => { checkLineBetween(coins[0].transform.position, coins[2].transform.position); };
				} else if (coinSelection == 4) {
					checkLine = () => { checkLineBetween(coins[0].transform.position, coins[1].transform.position); };
				}
			}
		}
	}

	void deleteGuide() {
		guide.enable(false);
	}

	void checkLineBetween(Vector3 startPos, Vector3 endPos) {
		if (Physics.Linecast(startPos, endPos, layerMask)) {
			hasFauled = false;
			Debug.Log("In between");
		}
	}

	bool hasCoinsStopped() {
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
		}
	}
}

public class AimState : CoinSetState {
	CoinSet coinSet;
	public AimState(CoinSet coinSet) {
		coinSet.enableControls();
	}
}

public class ShotState : CoinSetState {
	CoinSet coinSet;
	public ShotState(CoinSet coinSet) {
		coinSet.disableControls();
	}
}

public class StationaryState : CoinSetState {
	CoinSet coinSet;
	public StationaryState(CoinSet coinSet) { }
}

public interface CoinSetState {

}
