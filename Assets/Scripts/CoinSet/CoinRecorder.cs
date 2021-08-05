using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRecorder : MonoBehaviour {
	Coin[] coins;
	List<TransformDTO>[] coinTrails;

	void Awake() {
		coins = CoinSet.getInstance().getCoins();
		coinTrails = new List<TransformDTO>[3];
		coinTrails[0] = new List<TransformDTO>();
		coinTrails[1] = new List<TransformDTO>();
		coinTrails[2] = new List<TransformDTO>();

		LevelManager.getInstance().events.coinShot.AddListener(clearTrailData);
		LevelManager.getInstance().events.coinShot.AddListener(delegate { StartCoroutine(record()); });
		LevelManager.getInstance().events.coinShotEnded.AddListener(delegate { StopAllCoroutines(); });
		LevelManager.getInstance().events.playerFouled.AddListener(delegate { StartCoroutine(play()); });
	}

	IEnumerator record() {
		while (true) {
			for (int i = 0; i < coins.Length; i++) {
				coinTrails[i].Add(new TransformDTO(coins[i].transform));
			}
			yield return null;
		}
	}

	public IEnumerator play() {
		setCoinsKinematic(true);

		for (int i = coinTrails[0].Count - 1; i >= 0; i--) {
			for (int j = 0; j < coins.Length; j++) {
				coinTrails[j][i].applyValuesTo(coins[j].transform);
			}
			yield return null;
		}

		setCoinsKinematic(false);
		clearTrailData();
		LevelManager.getInstance().events.playerContinuesTurn.Invoke();
	}

	void clearTrailData() {
		foreach (List<TransformDTO> transformDTOList in coinTrails) {
			transformDTOList.Clear();
		}
	}

	void setCoinsKinematic(bool isKinematic) {
		foreach (Coin coin in coins) {
			coin.getRigidbody().isKinematic = isKinematic;
		}
	}
}
