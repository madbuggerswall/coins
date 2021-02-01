using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Bitwise check: |= uncheck: &= ~(value)
public enum CoinStatus {
	selected = (1 << 0),
	drawn = (1 << 1),
	shot = (1 << 2)
}

public class Slingshot : MonoBehaviour {

	Vector3 initialPos;
	Vector3 finalPos;
	Vector3 throwForce;

	Guide guide;
	Rigidbody rigidBody;

	[SerializeField] CoinStatus coinStatus;
	void Awake() {
		Debug.Log(coinStatus);
		guide = GetComponentInChildren<Guide>();
		rigidBody = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update() {

	}

	// Select coin
	void OnMouseEnter() {
		coinStatus |= CoinStatus.selected;
		Match.getInstance().coinStatusChanged.Invoke();
	}

	void OnMouseExit() {
		coinStatus &= ~CoinStatus.selected;
		Match.getInstance().coinStatusChanged.Invoke();
	}

	// Draw
	void OnMouseDown() {
		initialPos = Input.mousePosition;
		guide.enable(true);
		guide.setPoints(transform.position, transform.position + throwForce);
	}

	// Aim
	void OnMouseDrag() {
		coinStatus |= CoinStatus.drawn;
		Match.getInstance().coinStatusChanged.Invoke();

		calculateThrowForce();
		guide.setPoints(transform.position, transform.position + throwForce);
	}

	// Release
	void OnMouseUp() {
		coinStatus = CoinStatus.shot;
		Match.getInstance().coinStatusChanged.Invoke();

		rigidBody.AddForce(throwForce, ForceMode.Impulse);
		gameObject.layer = Layers.thrownCoin;
		Match.getInstance().coinShot.Invoke();
		guide.enable(false);
	}

	void calculateThrowForce() {
		finalPos = Input.mousePosition;
		throwForce = initialPos - finalPos;
		throwForce.z = throwForce.y;
		throwForce.y = 0f;
		throwForce *= Time.fixedDeltaTime;
	}

	public void clearFlags() {
		coinStatus = 0;
	}

	public CoinStatus getCoinStatus() { return coinStatus; }
}
