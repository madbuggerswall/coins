using UnityEngine;
using System.Collections;

public class MultiplayerFormation {
	TransformDTO[] formationL;
	TransformDTO[] formationR;

	public MultiplayerFormation(Coin[] coins) {
		initializeFormations(coins);
	}

	void initializeFormations(Coin[] coins) {
		formationL = new TransformDTO[coins.Length];
		formationR = new TransformDTO[coins.Length];
		for (int i = 0; i < coins.Length; i++) {
			formationL[i] = new TransformDTO(coins[i].transform);
			formationR[i] = new TransformDTO(coins[i].transform);
			formationR[i].localPosition.x *= -1;
		}
	}

	public IEnumerator resetCoins(Coin[] coins, bool isLeft) {
		TransformDTO[] current = new MultiplayerFormation(coins).formationL;
		TransformDTO[] formation = isLeft ? formationL : formationR;
		float interpolant = 0;
		while (true) {
			for (int i = 0; i < coins.Length; i++) {
				coins[i].transform.localPosition = Vector3.Lerp(current[i].localPosition, formation[i].localPosition, interpolant);
				coins[i].transform.localRotation = Quaternion.Lerp(current[i].localRotation, formation[i].localRotation, interpolant);
			}

			if (interpolant > 1f)
				break;

			interpolant += Time.deltaTime;
			yield return new WaitForEndOfFrame();
		}
	}
}