using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinRecorder : MonoBehaviour {
	Coin[] coins;
	List<TransformDTO>[] lastShot;
	List<TransformDTO>[] lastValidShot;

	void Awake() {
		coins = CoinSet.getInstance().getCoins();

		initializeTrailArrays();

		LevelManager.getInstance().events.coinShot.AddListener(clearTrailData);
		LevelManager.getInstance().events.coinShot.AddListener(delegate { StartCoroutine("record"); });
		LevelManager.getInstance().events.coinShotEnded.AddListener(delegate { StopCoroutine("record"); });
		LevelManager.getInstance().events.playerFouled.AddListener(delegate { StartCoroutine(rewindLastShot()); });
		LevelManager.getInstance().events.playerShotValid.AddListener(saveLastShot);
	}

	void initializeTrailArrays() {
		lastShot = new List<TransformDTO>[3];
		lastShot[0] = new List<TransformDTO>();
		lastShot[1] = new List<TransformDTO>();
		lastShot[2] = new List<TransformDTO>();

		lastValidShot = new List<TransformDTO>[3];
		lastValidShot[0] = new List<TransformDTO>();
		lastValidShot[1] = new List<TransformDTO>();
		lastValidShot[2] = new List<TransformDTO>();
	}

	IEnumerator record() {
		while (true) {
			for (int i = 0; i < coins.Length; i++) {
				lastShot[i].Add(new TransformDTO(coins[i].transform));
			}
			yield return null;
		}
	}

	IEnumerator rewindLastShot() {
		setCoinsKinematic(true);
		yield return rewind(lastShot);
		setCoinsKinematic(false);
		clearTrailData();
		LevelManager.getInstance().events.playerContinuesTurn.Invoke();
	}

	public IEnumerator rewindLastValidShot() {
		setCoinsKinematic(true);
		yield return rewind(lastValidShot);
		setCoinsKinematic(false);
		LevelManager.getInstance().events.playerContinuesTurn.Invoke();
	}

	IEnumerator rewind(List<TransformDTO>[] shotRecording) {
		for (int i = shotRecording[0].Count - 1; i >= 0; i--) {
			for (int j = 0; j < coins.Length; j++) {
				shotRecording[j][i].applyValuesTo(coins[j].transform);
			}
			yield return null;
		}
	}

	void saveLastShot() {
		clearLastValidShot();
		for (int i = 0; i < lastShot.Length; i++) {
			for (int j = 0; j < lastShot[0].Count; j++) {
				lastValidShot[i].Add(lastShot[i][j]);
			}
		}
	}

	void clearLastValidShot() {
		foreach (List<TransformDTO> transformDTOList in lastValidShot) {
			transformDTOList.Clear();
		}
	}

	void clearTrailData() {
		foreach (List<TransformDTO> transformDTOList in lastShot) {
			transformDTOList.Clear();
		}
	}

	void setCoinsKinematic(bool isKinematic) {
		foreach (Coin coin in coins) {
			coin.getRigidbody().isKinematic = isKinematic;
		}
	}
}
