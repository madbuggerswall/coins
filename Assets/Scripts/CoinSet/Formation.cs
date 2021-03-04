using UnityEngine;
using System.Collections;

public class Formation {
	Coin[] coins;
	TransformDTO[] formation;
	TransformDTO[] currentFormation;

	public Formation(Coin[] coins) {
		this.coins = coins;
		formation = new TransformDTO[coins.Length];
		currentFormation = new TransformDTO[coins.Length];

		LevelManager.getInstance().events.coinShot.AddListener(setFormation);
	}

	void setFormation() {
		for (int i = 0; i < coins.Length; i++) {
			formation[i] = new TransformDTO(coins[i].transform);
		}
	}

	void setCurrentFormation() {
		for (int i = 0; i < coins.Length; i++) {
			currentFormation[i] = new TransformDTO(coins[i].transform);
		}
	}

	void lerpCoins(float interpolant) {
		for (int i = 0; i < coins.Length; i++) {
			Vector3 currentPosition = currentFormation[i].localPosition;
			Quaternion currentRotation = currentFormation[i].localRotation;
			coins[i].transform.position = Vector3.Lerp(currentPosition, formation[i].localPosition, interpolant);
			coins[i].transform.rotation = Quaternion.Lerp(currentRotation, formation[i].localRotation, interpolant);
		}
	}

	public IEnumerator resetCoinSet() {
		setCurrentFormation();

		float interpolant = 0;
		while (true) {
			lerpCoins(interpolant);
			if (interpolant > 1) break;
			interpolant += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}
}
