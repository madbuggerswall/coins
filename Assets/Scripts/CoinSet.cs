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

		events.coinShot.AddListener(() => setState(new ShotState(this)));
	}

	void Update() {
		state.checkPassThrough();
		state.hasCoinsStopped();
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

public class CoinSetEvents {
	public UnityEvent coinStatusChanged;
	public UnityEvent coinShot;
	public CoinSetEvents() {
		coinStatusChanged = new UnityEvent();
		coinShot = new UnityEvent();
	}
}

public class CoinFormation {
	TransformDTO[] formationL;
	TransformDTO[] formationR;

	public CoinFormation(Coin[] coins) {
		initializeFormations(coins);
		Debug.Log(formationL[0].localPosition);
		Debug.Log(formationR[0].localPosition);
	}

	void initializeFormations(Coin[] coins) {
		formationL = new TransformDTO[coins.Length];
		formationR = new TransformDTO[coins.Length];
		for (int i = 0; i < coins.Length; i++) {
			formationL[i] = new TransformDTO(coins[i].transform);
			formationR[i] = new TransformDTO(coins[i].transform);
			formationR[i].localPosition.x *= -1;
		}
	}

	public IEnumerator resetCoins(Coin[] coins) {
		TransformDTO[] current = new CoinFormation(coins).formationL;

		float interpolant = 0;
		while (true) {
			for (int i = 0; i < coins.Length; i++) {
				coins[i].transform.localPosition = Vector3.Lerp(current[i].localPosition, formationL[i].localPosition, interpolant);
				coins[i].transform.localRotation = Quaternion.Lerp(current[i].localRotation, formationL[i].localRotation, interpolant);
			}
			
			if (interpolant > 1f)
				break;

			interpolant += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}
}