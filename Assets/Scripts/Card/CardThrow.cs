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
		initialPosition = transform.position;
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
		initialMousePos = getMousePosition();
	}

	void OnMouseDrag() {
		finalMousePos = getMousePosition();
		cancelThrow();
		rigidBody.MovePosition(finalMousePos);
	}

	void OnMouseUp() {
		if (throwCanceled) {
			rigidBody.MovePosition(initialPosition);
		} else {
			((PuzzleEvents) FindObjectOfType<Puzzle>().getEvents()).cardPlayed.Invoke();
			GetComponent<Card>().apply();
		}
		outline.resetColor();
		outline.enable(false);
	}

	Vector3 getMousePosition() {
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = Camera.main.nearClipPlane;
		mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
		mousePosition.y = transform.position.y;
		return mousePosition;
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