using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CoinSetState {
	void hasCoinsStopped();
	void checkPassThrough();
	void drawGuide();
}

public class AimState : CoinSetState {
	CoinSet coinSet;

	public AimState(CoinSet coinSet) {
		this.coinSet = coinSet;
		coinSet.enableControls();
	}
	public void hasCoinsStopped() { }
	public void checkPassThrough() { }
	public void drawGuide() {
		coinSet.drawGuideLine();
	}
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

	public void drawGuide() {
		coinSet.drawGuideLine();
	}
}

// Check if fauled, player has turns left to play or scored.
public class StationaryState : CoinSetState {
	CoinSet coinSet;
	public StationaryState(CoinSet coinSet) {
		this.coinSet = coinSet;
		coinSet.clearAllFlags();
		coinSet.disableGuide();
		coinSet.checkFaulLine = () => { };
		coinSet.drawGuideLine = () => { };

		if (coinSet.hasPassedThrough()) {
			Match.getInstance().playerShotEnded.Invoke();
		} else {
			Match.getInstance().playerFouled.Invoke();
		}
	}
	public void hasCoinsStopped() { }
	public void checkPassThrough() { }
	public void drawGuide() { }
}
