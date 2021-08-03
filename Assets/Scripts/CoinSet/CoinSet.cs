using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinSet : MonoBehaviour {
	SelectedCoinIndicator selectedCoinIndicator;
	SetMechanics setMechanics;
	GuideString guide;

	Coin[] coins;
	CoinSetState state;
	void Awake() {
		coins = GetComponentsInChildren<Coin>();
		guide = GetComponentInChildren<GuideString>();

		setMechanics = new SetMechanics(this);
		// Move this line to a more context-appropriate script/function.
		LevelManager.getInstance().events.playerContinuesTurn.AddListener(() => setState(new AimState(this)));
		LevelManager.getInstance().events.coinShot.AddListener(() => setState(new ShotState(this)));
		LevelManager.getInstance().events.cardPlayed.AddListener(() => setState(new StationaryState(this)));
		LevelManager.getInstance().events.cardApplied.AddListener(() => setState(new AimState(this)));
	}

	 void Start() {
		selectedCoinIndicator = new SelectedCoinIndicator();
	}

	void Update() {
		state.hasCoinsStopped();
	}

	void FixedUpdate() {
		state.checkPassThrough();
	}

	public bool hasCoinsStopped() {
		bool result = true;
		foreach (Coin coin in coins) {
			bool coinStopped = coin.GetComponent<Rigidbody>().IsSleeping();
			result = result && coinStopped;
		}
		return result;
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
			coin.GetComponent<Slingshot>().resetStatus();
			coin.gameObject.layer = Layers.coin;
		}
	}

	// Setters & Getters
	public void setState(CoinSetState state) { this.state = state; }
	public Coin[] getCoins() { return coins; }
	public GuideString getGuide() { return guide; }
	public SetMechanics getMechanics() { return setMechanics; }
}