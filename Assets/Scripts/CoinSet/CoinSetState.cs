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
		coinSet.getMechanics().resetPassedThrough();
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

		// Maybe trigger this function via events.coinShot inside each Slingshot script.
		coinSet.disableControls();
	}
	public void hasCoinsStopped() {
		if (coinSet.hasCoinsStopped()) {
			coinSet.setState(new StationaryState(coinSet));
			LevelManager.getInstance().events.coinShotEnded.Invoke();
		}
	}
	public void checkPassThrough() {
		coinSet.getMechanics().checkFoulLine();
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
		coinSet.disableControls();

		// TODO: Trigger these operations via Events.
		coinSet.getMechanics().checkFoulLine = () => { };
		coinSet.getGuide().drawGuideLine = () => { };
	}
	public void hasCoinsStopped() { }
	public void checkPassThrough() { }
}
