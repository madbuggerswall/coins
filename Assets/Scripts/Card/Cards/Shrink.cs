﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shrink : Card {
	float scaleMul = 0.75f;
	float shrinkSpeed = 2f;
	Vector3 initialScale;
	Vector3 shrunkenScale;

	public override void apply() {
		StartCoroutine(shrink());
		spawnParticleFX(Particles.shrinkFXPrefab);
		LevelManager.getInstance().events.playerShotValid.AddListener(reset);
	}

	public override void reset() {
		StartCoroutine(resetShrink());
		LevelManager.getInstance().events.playerShotValid.RemoveListener(reset);
	}

	IEnumerator shrink() {
		Coin[] coins = FindObjectOfType<CoinSet>().getCoins();
		float interpolant = 0;

		initialScale = coins[0].transform.localScale;
		shrunkenScale = new Vector3(initialScale.x * scaleMul, initialScale.y, initialScale.z * scaleMul);
		while (true) {
			foreach (Coin coin in coins) {
				coin.transform.localScale = Vector3.Lerp(initialScale, shrunkenScale, interpolant);
			}
			if (interpolant > 1) break;
			interpolant += Time.deltaTime * shrinkSpeed;
			yield return new WaitForEndOfFrame();
		}
		LevelManager.getInstance().events.cardApplied.Invoke();
	}

	IEnumerator resetShrink() {
		Coin[] coins = FindObjectOfType<CoinSet>().getCoins();

		float interpolant = 0;
		while (true) {
			foreach (Coin coin in coins) {
				coin.transform.localScale = Vector3.Lerp(shrunkenScale, initialScale, interpolant);
			}

			if (interpolant > 1) break;
			interpolant += Time.deltaTime * shrinkSpeed;
			yield return new WaitForEndOfFrame();
		}
	}
}
