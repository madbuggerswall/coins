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

	float cancelThreshold = 4;
	// Functions are to be overriden to disable controls.
	UnityAction onMouseEnter;
	UnityAction onMouseExit;
	UnityAction onMouseDown;
	UnityAction onMouseDrag;
	UnityAction onMouseUp;

	CoinStatus coinStatus;

	void Awake() {
		crosshair = GetComponentInChildren<Crosshair>();
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
		Events events = LevelManager.getInstance().events;

		// Select coin
		onMouseEnter = () => {
			coinStatus |= CoinStatus.selected;
			events.coinStatusChanged.Invoke();
		};

		onMouseExit = () => {
			coinStatus &= ~CoinStatus.selected;
			events.coinStatusChanged.Invoke();
		};

		// Draw
		onMouseDown = () => {
			initialPos = PlayerInput.getPosition();
			crosshair.setPoints(transform.position, transform.position);
			crosshair.enable(true);

			coinStatus |= CoinStatus.drawn;
			events.coinStatusChanged.Invoke();
		};

		// Aim
		onMouseDrag = () => {
			calculateThrowForce();
			crosshair.setPoints(transform.position, transform.position + throwForce * 0.4f);

			if (throwForce.magnitude <= cancelThreshold)
				crosshair.setColor(Color.red);
			else
				crosshair.setColor(Color.white);
		};


		// Release
		onMouseUp = () => {
			crosshair.enable(false);
			// Cancel shot
			if (throwForce.magnitude <= cancelThreshold) {
				coinStatus = coinStatus &= ~CoinStatus.drawn;
				return;
			}

			coinStatus = CoinStatus.shot;
			events.coinStatusChanged.Invoke();

			gameObject.layer = Layers.thrownCoin;
			rigidBody.AddForce(throwForce, ForceMode.Impulse);
			events.coinShot.Invoke();
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
		finalPos = PlayerInput.getPosition();
		throwForce = initialPos - finalPos;
		throwForce.y = 0f;
		
		#if UNITY_EDITOR
			throwForce *= 8;
		#elif UNITY_ANDROID
			throwForce *= 8;
		#endif
		throwForce = Vector3.ClampMagnitude(throwForce, maxThrowForceMag);
	}

	public void resetStatus() {
		coinStatus = 0;
	}

	public CoinStatus getCoinStatus() { return coinStatus; }
}
