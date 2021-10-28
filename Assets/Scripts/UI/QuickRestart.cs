using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuickRestart : Button {
	float windUpTime = 2f;

	Image restartImage;
	Button button;

	float pressTime = 0;

	UnityAction buttonAction = delegate { };

	protected override void Awake() {
		Debug.Log("Awake");
		restartImage = GetComponentsInChildren<Image>()[1];
		Debug.Log(restartImage.name);
		restartImage.fillAmount = 0f;
	}

	void Update() {
		buttonAction();
	}

	public override void OnPointerDown(PointerEventData eventData) {
		buttonAction = delegate {
			pressTime += Time.deltaTime;
			float restartProgress = Mathf.InverseLerp(0, windUpTime, pressTime);
			restartImage.fillAmount = restartProgress;
			if (restartProgress > 0.8f) GameManager.getInstance().levelLoader.restartLevel();
		};
	}

	public override void OnPointerUp(PointerEventData eventData) {
		pressTime = 0;
		restartImage.fillAmount = Mathf.InverseLerp(0, windUpTime, pressTime);
		buttonAction = delegate { };
	}

}
