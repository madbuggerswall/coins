using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finesse : Card {
	[SerializeField] bool isLeft;
	ApplyFinesse finesseHelper;
	
	public override void apply() {
		LevelManager.getInstance().events.coinShot.AddListener(applyFinesse);
		LevelManager.getInstance().events.cardApplied.Invoke();
		LevelManager.getInstance().events.coinShotEnded.AddListener(reset);

	}

	public override void reset() {
		LevelManager.getInstance().events.coinShot.RemoveListener(applyFinesse);
		LevelManager.getInstance().events.coinShotEnded.RemoveListener(reset);
		Destroy(finesseHelper);
	}

	void applyFinesse() {
		Coin[] coins = CoinSet.getInstance().getCoins();
		foreach (Coin coin in coins) {
			if (coin.gameObject.layer == Layers.thrownCoin) {
				finesseHelper = coin.gameObject.AddComponent<ApplyFinesse>();
				finesseHelper.setDirection(isLeft);
			}
		}
	}
}
