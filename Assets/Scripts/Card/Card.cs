using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour {
	public abstract void apply();
	public abstract void reset();

	protected void spawnParticleFX(GameObject particlePrefab) {
		Particles particles = Particles.getInstance();
		foreach (Coin coin in CoinSet.getInstance().getCoins()) {
			particles.explodeAt(particlePrefab, coin.transform.position);
		}
	}
}
