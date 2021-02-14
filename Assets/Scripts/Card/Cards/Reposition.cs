﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reposition : Card {
	float radius = 1;

	public override void apply() {
		gameObject.GetComponent<Renderer>().enabled = false;
		GetComponentInChildren<CardOutline>().gameObject.SetActive(false);
		spawnRepositionAreas();
		relocateCoins();
		((PuzzleEvents) FindObjectOfType<Puzzle>().getEvents()).cardApplied.AddListener(resetRelocating);
	}

	public override void reset() { }

	void relocateCoins() {
		Coin[] coins = FindObjectOfType<CoinSet>().getCoins();
		foreach (Coin coin in coins) {
			coin.gameObject.AddComponent<Relocate>();
		}
	}

	void resetRelocating() {
		disableRepositionAreas();
		Coin[] coins = FindObjectOfType<CoinSet>().getCoins();
		foreach (Coin coin in coins) {
			Destroy(coin.gameObject.GetComponent<Relocate>());
		}
	}

	void spawnRepositionAreas() {
		GameObject[] repositionAreas = GameObject.FindGameObjectsWithTag("RepositionArea");
		Coin[] coins = FindObjectOfType<CoinSet>().getCoins();
		for (int i = 0; i < coins.Length; i++) {
			float posX = coins[i].transform.position.x;
			float posY = repositionAreas[i].transform.position.y;
			float posZ = coins[i].transform.position.z;
			repositionAreas[i].transform.position = new Vector3(posX, posY, posZ);
			repositionAreas[i].GetComponent<Renderer>().enabled = true;
		}
	}

	void disableRepositionAreas() {
		GameObject[] repositionAreas = GameObject.FindGameObjectsWithTag("RepositionArea");
		foreach (GameObject repositionArea in repositionAreas) {
			repositionArea.GetComponent<Renderer>().enabled = false;
		}
	}
}
