using UnityEngine;

public class Crosshair : MonoBehaviour {
	LineRenderer lineRenderer;
	CoinSet coinSet;

	void Awake() {
		lineRenderer = GetComponent<LineRenderer>();
	}

	public void enable(bool value) {
		lineRenderer.enabled = value;
	}

	public void setPoints(Vector3 startPoint, Vector3 endPoint) {
		lineRenderer.SetPosition(0, startPoint);
		lineRenderer.SetPosition(1, endPoint);
	}
}
