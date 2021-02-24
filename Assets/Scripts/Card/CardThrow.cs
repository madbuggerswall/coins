using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardThrow : MonoBehaviour {
	Rigidbody rigidBody;
	CardOutline outline;

	Vector3 initialPosition;
	Vector3 initialMousePos;
	Vector3 finalMousePos;

	bool throwCanceled;
	float offset;

	UnityAction onMouseEnter;
	UnityAction onMouseExit;
	UnityAction onMouseDown;
	UnityAction onMouseDrag;
	UnityAction onMouseUp;

	void Awake() {
		initialPosition = transform.localPosition;
		rigidBody = GetComponent<Rigidbody>();
		outline = GetComponentInChildren<CardOutline>();
		offset = Camera.main.transform.position.y - transform.position.y;
		enableControls();

		LevelManager.getInstance().events.coinShot.AddListener(disableControls);
		LevelManager.getInstance().events.playerFouled.AddListener(disableControls);
		LevelManager.getInstance().events.playerContinuesTurn.AddListener(enableControls);
	}

	void OnMouseEnter() { onMouseEnter(); }
	void OnMouseExit() { onMouseExit(); }
	void OnMouseDown() { onMouseDown(); }
	void OnMouseDrag() { onMouseDrag(); }
	void OnMouseUp() { onMouseUp(); }

	void enableControls() {
		onMouseEnter = () => { outline.enable(true); };
		onMouseExit = () => { outline.enable(false); };
		onMouseDown = () => { initialMousePos = PlayerInput.getPosition(offset); };
		onMouseDrag = () => {
			finalMousePos = PlayerInput.getPosition(offset);
			cancelThrow();
			transform.position = finalMousePos;
		};
		onMouseUp = () => {
			if (throwCanceled) {
				transform.localPosition = initialPosition;
			} else {
				gameObject.GetComponent<Renderer>().enabled = false;
				GetComponentInChildren<CardOutline>().gameObject.SetActive(false);
				LevelManager.getInstance().events.cardPlayed.Invoke();
				GetComponent<Card>().apply();
			}
			outline.resetColor();
			outline.enable(false);
		};
	}

	void disableControls() {
		onMouseEnter = () => { };
		onMouseExit = () => { };
		onMouseDown = () => { };
		onMouseDrag = () => { };
		onMouseUp = () => { };
	}

	void cancelThrow() {
		if ((finalMousePos - initialMousePos).magnitude > 5) {
			outline.changeColor(Color.green);
			throwCanceled = false;
		} else {
			throwCanceled = true;
			outline.changeColor(Color.red);
		}
	}
}
