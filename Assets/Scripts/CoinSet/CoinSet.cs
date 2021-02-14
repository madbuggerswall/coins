﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinSet : MonoBehaviour {
	public CoinSetEvents events;

	SetMechanics setMechanics;
	Guide guide;
	CoinFormation formation;

	Coin[] coins;
	CoinSetState state;
	void Awake() {
		coins = GetComponentsInChildren<Coin>();
		guide = GetComponentInChildren<Guide>();

		events = new CoinSetEvents();
		setMechanics = new SetMechanics(this);
		formation = new CoinFormation(coins);
		state = new StationaryState(this);

		events.coinShot.AddListener(() => setState(new ShotState(this)));
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
			coin.GetComponent<Slingshot>().clearFlags();
			coin.gameObject.layer = Layers.coin;
		}
	}

	// Setters & Getters
	public void setState(CoinSetState state) { this.state = state; }
	public Coin[] getCoins() { return coins; }
	public Guide getGuide() { return guide; }
	public SetMechanics getMechanics() { return setMechanics; }
	public CoinFormation getFormation() { return formation; }
}