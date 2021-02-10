using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : Card {
	float radius = 1;

	public override void apply() {
		gameObject.GetComponent<Renderer>().enabled = false;
		GetComponentInChildren<CardOutline>().gameObject.SetActive(false);
		relocateCoins();
		FindObjectOfType<Puzzle>().getEvents().playerShotEnded.AddListener(reset);
	}

	public override void reset() {

		FindObjectOfType<Puzzle>().getEvents().playerShotEnded.RemoveListener(reset);
	}

	void relocateCoins() {
		Coin[] coins = FindObjectOfType<CoinSet>().getCoins();
		foreach (Coin coin in coins) {
			coin.gameObject.AddComponent<Relocate>();
		}
	}

	void resetRelocating() {
		Coin[] coins = FindObjectOfType<CoinSet>().getCoins();
		foreach (Coin coin in coins) {
			Destroy(coin.gameObject.GetComponent<Relocate>());
		}
	}
}
