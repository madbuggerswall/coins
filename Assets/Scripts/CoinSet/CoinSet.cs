using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[DefaultExecutionOrder(-2)]
public class CoinSet : MonoBehaviour {
	static CoinSet instance;

	Coin[] coins;

	SelectedCoinIndicator selectedCoinIndicator;
	SetMechanics setMechanics;

	void Awake() {
		assertSingleton();
	
		coins = GetComponentsInChildren<Coin>();
		setMechanics = new SetMechanics(this);
	}

	void Start() {
		selectedCoinIndicator = new SelectedCoinIndicator();
	}

	void Update() {
		setMechanics.hasCoinShotEnded();
	}

	void FixedUpdate() {
		setMechanics.checkFoulLine();
	}

	// Singleton 
	public static CoinSet getInstance() { return instance; }
	void assertSingleton() { if (instance == null) { instance = this; } else { Destroy(gameObject); } }

	// Setters & Getters
	
	public Coin[] getCoins() { return coins; }
	public SetMechanics getMechanics() { return setMechanics; }
}