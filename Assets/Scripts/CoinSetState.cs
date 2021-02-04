using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CoinSetState {
	void hasCoinsStopped();
	void checkPassThrough();
}

public class AimState : CoinSetState {
	CoinSet coinSet;

	public AimState(CoinSet coinSet) {
		Debug.Log("AimState");
		this.coinSet = coinSet;
		coinSet.resetPassedThrough();
		coinSet.enableControls();
	}
	public void hasCoinsStopped() { }
	public void checkPassThrough() { }
}

public class ShotState : CoinSetState {
	CoinSet coinSet;

	public ShotState(CoinSet coinSet) {
		Debug.Log("ShotState");
		this.coinSet = coinSet;
		coinSet.disableControls();
	}
	public void hasCoinsStopped() {
		if (coinSet.hasCoinsStopped()) {
			coinSet.setState(new StationaryState(coinSet));
		}
	}
	public void checkPassThrough() {
		coinSet.checkFoulLine();
	}
}

// Check if fouled, player has turns left to play or scored.
public class StationaryState : CoinSetState {
	CoinSet coinSet;
	public StationaryState(CoinSet coinSet) {
		Debug.Log("StationaryState");
		this.coinSet = coinSet;

		coinSet.clearAllFlags();
		coinSet.disableGuide();
		coinSet.checkFoulLine = () => { };
		coinSet.drawGuideLine = () => { };

		GameObject.FindObjectOfType<Match>().playerShotEnded.Invoke();
	}
	public void hasCoinsStopped() { }
	public void checkPassThrough() { }
}
