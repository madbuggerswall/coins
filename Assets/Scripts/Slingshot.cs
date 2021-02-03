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

	Guide guide;
	Rigidbody rigidBody;

	// Functions are to be overriden to disable controls.
	UnityAction onMouseEnter;
	UnityAction onMouseExit;
	UnityAction onMouseDown;
	UnityAction onMouseDrag;
	UnityAction onMouseUp;

	[SerializeField] CoinStatus coinStatus;

	void Awake() {
		guide = GetComponentInChildren<Guide>();
		rigidBody = GetComponent<Rigidbody>();
		rigidBody.sleepThreshold = rigidBody.mass * 1f * 0.5f;
	}

	// Select coin
	void OnMouseEnter() {
		onMouseEnter();
	}

	void OnMouseExit() {
		onMouseExit();
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

	public void enableControls() {
		// Select coin
		onMouseEnter = () => {
			coinStatus |= CoinStatus.selected;
			GetComponentInParent<CoinSet>().events.coinStatusChanged.Invoke();
		};

		onMouseExit = () => {
			coinStatus &= ~CoinStatus.selected;
			GetComponentInParent<CoinSet>().events.coinStatusChanged.Invoke();
		};

		// Draw
		onMouseDown = () => {
			initialPos = Input.mousePosition;
			guide.enable(true);
			guide.setPoints(transform.position, transform.position + throwForce);
		};
		// Aim
		onMouseDrag = () => {
			coinStatus |= CoinStatus.drawn;
			GetComponentInParent<CoinSet>().events.coinStatusChanged.Invoke();

			calculateThrowForce();
			guide.setPoints(transform.position, transform.position + throwForce);
		};
		// Release
		onMouseUp = () => {
			coinStatus = CoinStatus.shot;
			GetComponentInParent<CoinSet>().events.coinStatusChanged.Invoke();

			rigidBody.AddForce(throwForce, ForceMode.Impulse);
			gameObject.layer = Layers.thrownCoin;
			GetComponentInParent<CoinSet>().events.coinShot.Invoke();
			guide.enable(false);
		};
	}

	public void disableControls() {
		onMouseEnter = () => { };
		onMouseExit = () => { };
		onMouseDown = () => { };
		onMouseDrag = () => { };
		onMouseUp = () => { };
	}

	void calculateThrowForce() {
		finalPos = Input.mousePosition;
		throwForce = initialPos - finalPos;
		throwForce.z = throwForce.y;
		throwForce.y = 0f;
		throwForce *= Time.fixedDeltaTime * 4;
	}

	public void clearFlags() {
		coinStatus = 0;
	}

	public CoinStatus getCoinStatus() { return coinStatus; }
}
