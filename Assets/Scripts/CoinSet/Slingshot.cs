using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


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
	[SerializeField] float maxThrowForceMag = 32;

	Crosshair crosshair;
	Rigidbody rigidBody;

	float cancelThreshold = 8;
	// Functions are to be overriden to disable controls.
	UnityAction onMouseDown;
	UnityAction onMouseDrag;
	UnityAction onMouseUp;
	UnityAction aimAction;

	CoinStatus coinStatus;

	void Awake() {
		crosshair = GetComponentInChildren<Crosshair>();
		rigidBody = GetComponent<Rigidbody>();
		// rigidBody.sleepThreshold = rigidBody.mass * 1f * 0.5f;
		aimAction = () => { };
	}

	void Update() {
		aimAction();
	}

	// Draw
	void OnMouseDown() {
		onMouseDown();
	}

	// Aim
	void OnMouseDrag() {
		onMouseDrag();
	}

	// Release
	void OnMouseUp() {
		onMouseUp();
	}

	void draw() {
		initialPos = PlayerInput.getPosition();
		crosshair.setPoints(transform.position, transform.position);
		crosshair.enable(true);

		// Select coin
		coinStatus |= CoinStatus.selected;
		resetOtherCoinStatus();
		LevelManager.getInstance().events.coinStatusChanged.Invoke();
	}

	void aim() {
		calculateThrowForce();
		crosshair.setPoints(transform.position, transform.position + throwForce * 0.4f);

		if (throwForce.magnitude <= cancelThreshold)
			crosshair.setColor(Color.red);
		else
			crosshair.setColor(Color.white);
	}

	void release() {
		crosshair.enable(false);
		// Cancel shot
		if (throwForce.magnitude <= cancelThreshold) {
			aimAction = freeAim;
			return;
		}

		Events events = LevelManager.getInstance().events;
		coinStatus = CoinStatus.shot;
		events.coinStatusChanged.Invoke();
		aimAction = () => { };

		gameObject.layer = Layers.thrownCoin;
		rigidBody.AddForce(throwForce, ForceMode.Impulse);
		events.coinShot.Invoke();
	}

	void freeAim() {
		if ((coinStatus & CoinStatus.selected) > 0) {
			if (Input.GetMouseButtonDown(0)) {
				crosshair.enable(true);
				initialPos = PlayerInput.getPosition();
			} else if (Input.GetMouseButton(0)) {
				aim();
			} else if (Input.GetMouseButtonUp(0)) {
				release();
			}
		}
	}

	void calculateThrowForce() {
		finalPos = PlayerInput.getPosition();
		throwForce = initialPos - finalPos;
		throwForce.y = 0f;
		throwForce *= 8;
		throwForce = Vector3.ClampMagnitude(throwForce, maxThrowForceMag);
	}

	void resetOtherCoinStatus() {
		Coin[] coins = LevelManager.getInstance().getGame().getCoinSet().getCoins();
		foreach (Coin coin in coins) {
			if (coin != GetComponent<Coin>()) {
				coin.GetComponent<Slingshot>().resetStatus();
			}
		}
	}

	public void enableControls() {
		onMouseDown = draw;
		onMouseDrag = aim;
		onMouseUp = release;
		aimAction = freeAim;
	}

	public void disableControls() {
		onMouseDown = () => { };
		onMouseDrag = () => { };
		onMouseUp = () => { };
		aimAction = () => { };
	}

	public void resetStatus() {
		coinStatus = 0;
	}

	public CoinStatus getCoinStatus() { return coinStatus; }
	public float getMaxForceMag() { return maxThrowForceMag; }
	public void setMaxForceMag(float maxThrowForceMag) { this.maxThrowForceMag = maxThrowForceMag; }
}