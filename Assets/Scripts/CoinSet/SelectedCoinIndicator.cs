using UnityEngine;

public class SelectedCoinIndicator {
	Coin[] coins;
	GameObject indicator;

	public SelectedCoinIndicator() {
		coins = CoinSet.getInstance().getCoins();
		LevelManager.getInstance().events.coinStatusChanged.AddListener(spawnIndicator);
	}

	void spawnIndicator() {
		foreach (Coin coin in coins) {
			if ((coin.GetComponent<Slingshot>().getCoinStatus() & CoinStatus.selected) > 0) {
				if (indicator != null) indicator.SetActive(false);
				indicator = Particles.getInstance().explodeAt(Particles.coinSelectedPrefab, coin.transform.position);
				return;
			}
		}
		if (indicator != null) indicator.SetActive(false);
	}
}