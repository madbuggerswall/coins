using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardOutline : MonoBehaviour {
	Renderer renderer;
	Material material;
	Color initialColor;

	void Awake() {
		renderer = GetComponent<Renderer>();
		material = renderer.material;
		initialColor = material.color;
	}

	public void changeColor(Color color) {
		material.color = color;
	}

	public void resetColor() {
		material.color = initialColor;
	}

	public void enable(bool enable) { renderer.enabled = enable; }
}
