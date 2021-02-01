using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour {
	LineRenderer lineRenderer;

	void Awake() {
		lineRenderer = GetComponent<LineRenderer>();
	}

	// Update is called once per frame
	void Update() {

	}


	public void enable(bool value) {
		lineRenderer.enabled = value;
	}

	public void setPoints(Vector3 startPoint, Vector3 endPoint) {
		lineRenderer.SetPosition(0, startPoint);
		lineRenderer.SetPosition(1, endPoint);
	}
}
