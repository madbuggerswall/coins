﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Card {

	public override void apply() {
		gameObject.GetComponent<Renderer>().enabled = false;
		GetComponentInChildren<CardOutline>().gameObject.SetActive(false);
		becomeGhost();
		FindObjectOfType<Puzzle>().getEvents().playerShotEnded.AddListener(reset);
	}

	public override void reset() {
		resetGhost();
		FindObjectOfType<Puzzle>().getEvents().playerShotEnded.RemoveListener(reset);
	}

	void becomeGhost() {
		Coin[] coins = FindObjectOfType<CoinSet>().getCoins();
		foreach (Coin coin in coins) {
			coin.GetComponent<Renderer>().material.color = Color.cyan;
		}
		Physics.IgnoreLayerCollision(Layers.coin, Layers.obstacle, true);
		Physics.IgnoreLayerCollision(Layers.thrownCoin, Layers.obstacle, true);

		((PuzzleEvents) FindObjectOfType<Puzzle>().getEvents()).cardApplied.Invoke();
	}

	void resetGhost() {
		Color coinColor = new Color(0xF6, 0xFF, 0x00);
		Coin[] coins = FindObjectOfType<CoinSet>().getCoins();
		foreach (Coin coin in coins) {
			coin.GetComponent<Renderer>().material.color = coinColor;
			moveOutsideObstacle(coin.transform);
		}
		Physics.IgnoreLayerCollision(Layers.coin, Layers.obstacle, false);
		Physics.IgnoreLayerCollision(Layers.thrownCoin, Layers.obstacle, false);
	}

	void moveOutsideObstacle(Transform transform) {
		if (isInsideObstacle(transform)) {
			while (isInsideObstacle(transform)) {
				transform.position -= Vector3.right;
			}
		}
	}

	bool isInsideObstacle(Transform transform) {
		return Physics.CheckBox(transform.position, transform.localScale / 2, transform.rotation, (1 << Layers.obstacle));
	}
}