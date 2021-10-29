using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GuideString : MonoBehaviour {
	Coin[] coins;
	[SerializeField] float lineThickness = .4f;

	MeshRenderer meshRenderer;

	public UnityAction drawGuideLine = () => { };

	void Start() {
		coins = GetComponentInParent<CoinSet>().getCoins();
		meshRenderer = GetComponent<MeshRenderer>();
		LevelManager.getInstance().events.coinStatusChanged.AddListener(selectGuide);
		LevelManager.getInstance().events.coinPassedThrough.AddListener(delegate { StartCoroutine(strum()); });
		meshRenderer.material.SetFloat("_Amplitude", 0);
	}

	void Update() {
		drawGuideLine();
	}

	void enable(bool value) {
		meshRenderer.enabled = value;
	}

	void setPoints(Vector3 startPoint, Vector3 endPoint) {
		setPosition(startPoint, endPoint);
	}

	void setPosition(Vector3 start, Vector3 end) {
		Vector3 direction = end - start;
		transform.position = (end + start) * 0.5f;
		transform.rotation = Quaternion.FromToRotation(Vector3.forward, direction);
		transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, 0);
		transform.localScale = new Vector3(lineThickness, direction.magnitude - 2, 1);
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

	void selectGuide() {
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
		}
	}

	public Coin getShotCoin() {
		foreach (Coin coin in coins) {
			if (coin.GetComponent<Slingshot>().getCoinStatus() == CoinStatus.shot) {
				return coin;
			}
		}
		return null;
	}

	IEnumerator strum() {
		// Animation Speed and Amplitude will be effected by coin speed.
		float speed = getShotCoin().getRigidbody().velocity.magnitude;

		float decay = 0.2f / lineThickness;
		float amplitude = 0.3f / lineThickness * Mathf.InverseLerp(0f, 24f, speed);
		float animationSpeed = 14 + 14 * Mathf.InverseLerp(0f, 24f, speed);
		meshRenderer.material.SetFloat("_AnimationSpeed", animationSpeed);
		while (amplitude >= 0) {
			meshRenderer.material.SetFloat("_Amplitude", amplitude);
			amplitude -= Time.deltaTime * decay;
			yield return new WaitForEndOfFrame();
		}
		meshRenderer.material.SetFloat("_Amplitude", 0);
	}
}
