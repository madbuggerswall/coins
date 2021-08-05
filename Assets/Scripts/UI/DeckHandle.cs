using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckHandle : EventTrigger {
	Vector3 initialPosition;
	Vector3 finalPosition;
	RectTransform parent;
	bool open = false;

	void Awake() {
		parent = transform.parent.GetComponent<RectTransform>();
		initialPosition = parent.anchoredPosition;
		finalPosition = initialPosition + Vector3.up * parent.sizeDelta.y;
		
		LevelManager.getInstance().events.cardPlayed.AddListener(() => {
			StartCoroutine(collapse(finalPosition, initialPosition));
		});
	}

	public override void OnPointerEnter(PointerEventData pointerEventData) { }
	public override void OnPointerExit(PointerEventData pointerEventData) { }
	public override void OnPointerDown(PointerEventData pointerEventData) {
	}

	public override void OnDrag(PointerEventData pointerEventData) {
		parent.anchoredPosition += Vector2.up * pointerEventData.delta.y;
		float clampedY = Mathf.Clamp(parent.anchoredPosition.y, initialPosition.y, finalPosition.y);
		parent.anchoredPosition = Vector2.up * clampedY;
	}

	public override void OnPointerUp(PointerEventData pointerEventData) {
		if (open) {
			LevelManager.getInstance().events.cardDeckHidden.Invoke();
			StartCoroutine(collapse(finalPosition, initialPosition));
		} else {
			LevelManager.getInstance().events.cardDeckRevealed.Invoke();
			StartCoroutine(collapse(initialPosition, finalPosition));
		}
	}

	IEnumerator collapse(Vector3 initialPosition, Vector3 finalPosition) {
		float interpolant = Mathf.InverseLerp(initialPosition.y, finalPosition.y, parent.anchoredPosition.y);
		while (true) {
			parent.anchoredPosition = Vector3.Lerp(initialPosition, finalPosition, interpolant);
			if (interpolant > 1) break;
			interpolant += Time.deltaTime * 2;
			yield return new WaitForEndOfFrame();
		}
		open = !open;
	}
}
