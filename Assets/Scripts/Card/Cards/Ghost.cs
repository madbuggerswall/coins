using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Card {
	Color ghostColor = new Color32(0x43, 0x5B, 0x5E, 0xFF);
	Color coinColor = new Color32(0x80, 0x80, 0x80, 0xFF);

	public override void apply() {
		becomeGhost();
		spawnParticleFX(Particles.ghostFXPrefab);
		LevelManager.getInstance().events.coinShotEnded.AddListener(reset);
	}

	public override void reset() {
		resetGhost();
		LevelManager.getInstance().events.coinShotEnded.RemoveListener(reset);
	}

	void becomeGhost() {
		Coin[] coins = FindObjectOfType<CoinSet>().getCoins();
		foreach (Coin coin in coins) {
			coin.GetComponent<Renderer>().material.color = ghostColor;
		}
		Physics.IgnoreLayerCollision(Layers.coin, Layers.obstacle, true);
		Physics.IgnoreLayerCollision(Layers.thrownCoin, Layers.obstacle, true);

		LevelManager.getInstance().events.cardApplied.Invoke();
	}

	void resetGhost() {
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
