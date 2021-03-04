using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour {
	public abstract void apply();
	public abstract void reset();

	protected void spawnParticleFX(GameObject particlePrefab) {
		Particles particles = FindObjectOfType<Particles>();
		foreach (Coin coin in LevelManager.getInstance().getGame().getCoinSet().getCoins()) {
			particles.explodeAt(particlePrefab, coin.transform.position);
		}
	}
}
