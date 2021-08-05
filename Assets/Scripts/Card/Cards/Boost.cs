using UnityEngine;
using System.Collections;

public class Boost : Card {

	float boostRatio = 1.5f;
	float initialForceMag;

	public override void apply() {
		boostCoins();
		spawnParticleFX(Particles.boostFXPrefab);
		LevelManager.getInstance().events.cardApplied.Invoke();
		LevelManager.getInstance().events.coinShotEnded.AddListener(reset);
	}

	public override void reset() {
		resetBoost();
		LevelManager.getInstance().events.coinShotEnded.AddListener(reset);
	}

	void boostCoins() {
		Coin[] coins = CoinSet.getInstance().getCoins();
		foreach (Coin coin in coins) {
			Slingshot slingshot = coin.GetComponent<Slingshot>();
			initialForceMag = slingshot.getMaxForceMag();
			slingshot.setMaxForceMag(initialForceMag * boostRatio);
		}
	}

	void resetBoost() {
		Coin[] coins = CoinSet.getInstance().getCoins();
		foreach (Coin coin in coins) {
			coin.GetComponent<Slingshot>().setMaxForceMag(initialForceMag);
		}
	}
}