using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Events;

public class CardThrow : EventTrigger {
	GameObject cancelOverlay;
	Vector3 initialPosition;
	Vector3 initialMousePos;
	Vector3 finalMousePos;

	bool throwCanceled;

	UnityAction onMouseEnter;
	UnityAction onMouseExit;
	UnityAction onMouseDown;
	UnityAction onMouseDrag;
	UnityAction onMouseUp;

	void Awake() {
		initialPosition = transform.localPosition;
		cancelOverlay = transform.Find("Cancel Overlay").gameObject;
		enableControls();

		LevelManager.getInstance().events.coinShot.AddListener(disableControls);
		LevelManager.getInstance().events.playerFouled.AddListener(disableControls);
		LevelManager.getInstance().events.playerContinuesTurn.AddListener(enableControls);
	}

	public override void OnPointerEnter(PointerEventData pointerEventData) { onMouseEnter(); }
	public override void OnPointerExit(PointerEventData pointerEventData) { onMouseExit(); }
	public override void OnPointerDown(PointerEventData pointerEventData) { onMouseDown(); }
	public override void OnDrag(PointerEventData pointerEventData) { onMouseDrag(); }
	public override void OnPointerUp(PointerEventData pointerEventData) { onMouseUp(); }

	void enableControls() {
		onMouseEnter = () => { };
		onMouseExit = () => { };
		onMouseDown = () => { initialMousePos = Input.mousePosition; };
		onMouseDrag = () => {
			finalMousePos = Input.mousePosition;
			cancelThrow();
			transform.position = finalMousePos;
		};
		
		onMouseUp = () => {
			if (throwCanceled) {
				transform.localPosition = initialPosition;
				cancelOverlay.SetActive(false);
			} else {
				LevelManager.getInstance().events.cardPlayed.Invoke();
				GetComponent<Card>().apply();
				
				GetComponentInChildren<Text>().enabled = false;
				foreach (Image image in GetComponentsInChildren<Image>()) {
					image.enabled = false;
				}
			}
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
		if ((finalMousePos - initialMousePos).magnitude > 400) {
			throwCanceled = false;
			cancelOverlay.SetActive(false);
		} else {
			throwCanceled = true;
			cancelOverlay.SetActive(true);
		}
	}
}
