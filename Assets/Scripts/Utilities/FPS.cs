using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour {
	Text fps;
	float deltaTime;
	void Awake() {
		fps = GetComponent<Text>();
	}
	void Update() {
		deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
		fps.text = Mathf.Ceil(1.0f / deltaTime).ToString();
	}
}