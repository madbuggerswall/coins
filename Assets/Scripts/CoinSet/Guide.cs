using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Guide : MonoBehaviour {
	LineRenderer lineRenderer;
	public Coin[] coins;

	public UnityAction drawGuideLine = () => { };

	void Awake() {
		lineRenderer = GetComponent<LineRenderer>();
	}

	void Start() {
		coins = GetComponentInParent<CoinSet>().getCoins();
		LevelManager.getInstance().events.coinStatusChanged.AddListener(selectGuide);
	}

	// Update is called once per frame
	void Update() {
		drawGuideLine();
	}


	public void enable(bool value) {
		lineRenderer.enabled = value;
	}

	public void setPoints(Vector3 startPoint, Vector3 endPoint) {
		lineRenderer.SetPosition(0, startPoint);
		lineRenderer.SetPosition(1, endPoint);
	}

	// State functions
	public void selectGuide() {
		enable(true);
		CoinStatus maxCoinStatus = 0;
		int maxCoinStatusIndex = 0;
		for (int index = 0; index < coins.Length; index++) {
			CoinStatus coinStatus = coins[index].GetComponent<Slingshot>().getCoinStatus();
			if (coinStatus > maxCoinStatus) {
				maxCoinStatus = coinStatus;
				maxCoinStatusIndex = index;
			}
		}
		if (maxCoinStatus > 0) {
			selectCoinPair(maxCoinStatusIndex);
		} else
			enable(false);
	}

	void selectCoinPair(int index) {
		int coinSelection = (1 << index);
		if (coinSelection == 1) {
			drawGuideLine = () => setPoints(coins[1].transform.position, coins[2].transform.position);
		} else if (coinSelection == 2) {
			drawGuideLine = () => setPoints(coins[0].transform.position, coins[2].transform.position);
		} else if (coinSelection == 4) {
			drawGuideLine = () => setPoints(coins[0].transform.position, coins[1].transform.position);
		}
	}
}
