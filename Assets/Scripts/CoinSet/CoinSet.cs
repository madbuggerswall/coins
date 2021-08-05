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

	UnityAction checkCoinSetStationary;
	UnityAction checkPassThrough;

	void Awake() {
		assertSingleton();

		coins = GetComponentsInChildren<Coin>();

		setMechanics = new SetMechanics(this);

		checkCoinSetStationary = delegate { };
		checkPassThrough = delegate { };

		LevelManager.getInstance().events.coinShot.AddListener(delegate {
			checkCoinSetStationary = setMechanics.checkCoinSetStationary;
			checkPassThrough = setMechanics.checkFoulLine;
		});

		LevelManager.getInstance().events.coinShotEnded.AddListener(delegate {
			checkCoinSetStationary = delegate { };
			checkPassThrough = delegate { };
		});
	}

	void Start() {
		selectedCoinIndicator = new SelectedCoinIndicator();
	}

	void Update() {
		checkCoinSetStationary();
	}

	void FixedUpdate() {
		checkPassThrough();
	}

	// Singleton 
	public static CoinSet getInstance() { return instance; }
	void assertSingleton() { if (instance == null) { instance = this; } else { Destroy(gameObject); } }

	// Setters & Getters
	public Coin[] getCoins() { return coins; }
	public SetMechanics getMechanics() { return setMechanics; }
}