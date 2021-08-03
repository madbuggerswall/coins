using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GuideString : MonoBehaviour {
	Coin[] coins;
	float lineThickness = .2f;

	MeshRenderer meshRenderer;

	public UnityAction drawGuideLine = () => { };
	
	void Start() {
		coins = GetComponentInParent<CoinSet>().getCoins();
		meshRenderer = GetComponent<MeshRenderer>();
		LevelManager.getInstance().events.coinStatusChanged.AddListener(selectGuide);
		LevelManager.getInstance().events.coinPassedThrough.AddListener(delegate { StartCoroutine(strum(4)); });
		meshRenderer.material.SetFloat("_Amplitude", 0);
	}

	void Update() {
		drawGuideLine();

		if (Input.GetKeyDown(KeyCode.Alpha1)) {
			meshRenderer.material.SetFloat("_Amplitude", 1);
		}
	}


	public void enable(bool value) {
		meshRenderer.enabled = value;
	}

	public void setPoints(Vector3 startPoint, Vector3 endPoint) {
		setPosition(startPoint, endPoint);
	}

	void setPosition(Vector3 start, Vector3 end) {
		Vector3 direction = end - start;
		transform.position = (end + start) * 0.5f;
		transform.rotation = Quaternion.FromToRotation(Vector3.forward, direction);
		transform.eulerAngles = new Vector3(90, transform.eulerAngles.y * Mathf.Sign(direction.y), 0);
		transform.localScale = new Vector3(lineThickness, direction.magnitude, 1);
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

	IEnumerator strum(float speed) {
		// Animation Speed and Amplitude will be effected by coin speed.
		float decay = 0.2f / lineThickness;
		float amplitude = 0.3f / lineThickness;
		while (amplitude >= 0) {
			meshRenderer.material.SetFloat("_Amplitude", amplitude);
			amplitude -= Time.deltaTime * decay;
			yield return new WaitForEndOfFrame();
		}
		meshRenderer.material.SetFloat("_Amplitude", 0);

	}

}
