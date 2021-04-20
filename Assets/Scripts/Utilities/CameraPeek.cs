using UnityEngine;

public class CameraPeek : MonoBehaviour {
	[SerializeField] Transform peekCrosshair;
	[SerializeField] Transform coinL;
	[SerializeField] Transform coinC;
	[SerializeField] Transform coinR;

	Vector3 deltaPosition;
	Vector3 formerMousePosition;
	Vector3 deltaMousePosition;
	Vector3 averageCoinPosition;

	void Update() {
		averageCoinPosition = (coinL.position + coinC.position + coinR.position) / 3f;

		if (Input.touchCount == 2) {
			deltaPosition = (Input.GetTouch(0).deltaPosition + Input.GetTouch(1).deltaPosition) / 2f;
			deltaPosition = screenToWorld(deltaPosition, 0.1f);
			deltaMousePosition.z = 0;
			peekCrosshair.position += deltaPosition;
		} else if (Input.GetMouseButton(1)) {
			deltaMousePosition = Input.mousePosition - formerMousePosition;
			deltaMousePosition = screenToWorld(deltaMousePosition, 0.1f);
			deltaMousePosition.z = 0;
			peekCrosshair.position += deltaMousePosition;
		} else {
			peekCrosshair.position = averageCoinPosition;
		}
		clampCrosshairPosition();
		formerMousePosition = Input.mousePosition;
	}

	Vector3 screenToWorld(Vector3 position, float scale) {
		return new Vector3(-position.y, 0f, position.x) * scale;
	}
	void clampCrosshairPosition() {
		// TODO: Do the clamping according to ground plane. [collider.bounds.extends]
		// float posZ = Mathf.Clamp(peekCrosshair.position.z, -32, 32);
		float posX = Mathf.Clamp(peekCrosshair.position.x, 0, 80);
		peekCrosshair.position = new Vector3(posX, peekCrosshair.position.y, peekCrosshair.position.z);
	}
}