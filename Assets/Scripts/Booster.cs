using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster : MonoBehaviour {
	float force = 36;
	Material material;
	Vector2 textureOffset = new Vector2(0,0);
	void Awake() {
		material = GetComponent<Renderer>().material;
	}

	void Update() {
		textureOffset -= Vector2.up * Time.deltaTime;
		textureOffset.y = textureOffset.y % -1;
		material.SetTextureOffset("_MainTex", textureOffset);
	}

	void OnTriggerEnter(Collider other) {
		StartCoroutine(changeColor(Color.gray, Color.white));
	}

	void OnTriggerStay(Collider other) {
		other.attachedRigidbody.AddForce(transform.up * force);
	}

	void OnTriggerExit(Collider other) {
		StartCoroutine(changeColor(Color.white, Color.gray));
	}

	IEnumerator changeColor(Color first, Color last) {
		float interpolant = 0;
		while (true) {
			material.color = Color.Lerp(first, last, interpolant);
			if (interpolant > 1) break;
			interpolant += Time.deltaTime * 6;
			yield return new WaitForEndOfFrame();
		}
	}
}
