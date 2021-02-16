﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardThrow : MonoBehaviour {
	Rigidbody rigidBody;
	CardOutline outline;

	Vector3 initialPosition;

	Vector3 initialMousePos;
	Vector3 finalMousePos;

	bool throwCanceled;
	void Awake() {
		initialPosition = transform.localPosition;
		rigidBody = GetComponent<Rigidbody>();
		outline = GetComponentInChildren<CardOutline>();
	}

	void OnMouseEnter() {
		outline.enable(true);
	}

	void OnMouseExit() {
		outline.enable(false);
	}

	void OnMouseDown() {
		initialMousePos = PlayerInput.getMousePosition(Camera.main.nearClipPlane + 1);
	}

	void OnMouseDrag() {
		finalMousePos = PlayerInput.getMousePosition(Camera.main.nearClipPlane + 1);
		cancelThrow();
		// rigidBody.MovePosition(finalMousePos);
		transform.position = finalMousePos;
	}

	void OnMouseUp() {
		if (throwCanceled) {
			transform.localPosition = initialPosition;
		} else {
			LevelManager.getInstance().events.cardPlayed.Invoke();
			GetComponent<Card>().apply();
		}
		outline.resetColor();
		outline.enable(false);
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
