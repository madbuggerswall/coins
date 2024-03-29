using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expand : Card {
	float scaleMul = 1.25f;
	float expandSpeed = 2f;
	Vector3 initialScale;
	Vector3 expandedScale;

	public override void apply() {
		StartCoroutine(expand());
		spawnParticleFX(Particles.expandFXPrefab);
		LevelManager.getInstance().events.playerShotValid.AddListener(reset);
	}

	public override void reset() {
		StartCoroutine(resetExpand());
		LevelManager.getInstance().events.playerShotValid.RemoveListener(reset);
	}

	IEnumerator expand() {
		Coin[] coins = FindObjectOfType<CoinSet>().getCoins();
		float interpolant = 0;

		initialScale = coins[0].transform.localScale;
		expandedScale = new Vector3(initialScale.x * scaleMul, initialScale.y, initialScale.z * scaleMul);
		while (true) {
			foreach (Coin coin in coins) {
				coin.transform.localScale = Vector3.Lerp(initialScale, expandedScale, interpolant);
			}
			if (interpolant > 1) break;
			interpolant += Time.deltaTime * expandSpeed;
			yield return new WaitForEndOfFrame();
		}
		LevelManager.getInstance().events.cardApplied.Invoke();
	}

	IEnumerator resetExpand() {
		Coin[] coins = FindObjectOfType<CoinSet>().getCoins();

		float interpolant = 0;
		while (true) {
			foreach (Coin coin in coins) {
				coin.transform.localScale = Vector3.Lerp(expandedScale, initialScale, interpolant);
			}

			if (interpolant > 1) break;
			interpolant += Time.deltaTime * expandSpeed;
			yield return new WaitForEndOfFrame();
		}
	}
}
