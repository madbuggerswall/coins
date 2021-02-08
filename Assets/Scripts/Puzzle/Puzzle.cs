using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Puzzle : MonoBehaviour {
	CoinSet coinSet;
	Player player;
	public UnityEvent playerShotEnded;
	
	void Awake() {
		playerShotEnded = new UnityEvent();
	}

	public CoinSet getCoinSet() { return coinSet; }
	public Player GetPlayer() { return player; }
}
