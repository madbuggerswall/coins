using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reposition : Card {
	float radius = 1.5f;

	Transform[] repositionAreas = new Transform[3];

	void Awake() {
		initializeRepositionAreas();
	}

	public override void apply() {
		spawnRepositionAreas();
		relocateCoins();
		gameObject.AddComponent<DoubleTap>();
		spawnParticleFX(Particles.repositionFXPrefab);
		LevelManager.getInstance().events.cardApplied.AddListener(reset);
	}

	public override void reset() {
		disableRepositionAreas();
		Destroy(GetComponent<DoubleTap>());
		Coin[] coins = CoinSet.getInstance().getCoins();
		foreach (Coin coin in coins) {
			Destroy(coin.gameObject.GetComponent<Relocate>());
		}
	}

	void initializeRepositionAreas() {
		GameObject repositionAreasPrefab = Resources.Load<GameObject>("Prefabs/Reposition Areas");
		GameObject repositionAreasParent = Instantiate(repositionAreasPrefab);
		List<Transform> repositionAreas = new List<Transform>(repositionAreasParent.GetComponentsInChildren<Transform>());
		repositionAreas.RemoveAt(0);
		this.repositionAreas = repositionAreas.ToArray();
	}

	void relocateCoins() {
		Coin[] coins = CoinSet.getInstance().getCoins();
		foreach (Coin coin in coins) {
			coin.gameObject.AddComponent<Relocate>();
		}
	}

	void spawnRepositionAreas() {
		Coin[] coins = FindObjectOfType<CoinSet>().getCoins();
		for (int i = 0; i < coins.Length; i++) {
			float posX = coins[i].transform.position.x;
			float posY = repositionAreas[i].transform.position.y;
			float posZ = coins[i].transform.position.z;
			repositionAreas[i].transform.position = new Vector3(posX, posY, posZ);
			repositionAreas[i].GetComponent<Renderer>().enabled = true;
		}
	}

	void disableRepositionAreas() {
		foreach (Transform repositionArea in repositionAreas) {
			repositionArea.GetComponent<Renderer>().enabled = false;
		}
	}
}
