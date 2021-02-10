using UnityEngine;

public static class PlayerInput {
	static Vector3 lastMousePosition = Vector3.zero;

	public static Vector3 getMousePosition() {
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = Camera.main.nearClipPlane;
		return Camera.main.ScreenToWorldPoint(mousePosition);
	}

	public static Vector3 getMousePosition(float planeDistance) {
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = planeDistance;
		return Camera.main.ScreenToWorldPoint(mousePosition);
	}

	public static Vector3 getDeltaMousePosition() {
		Vector3 mousePosition = getMousePosition();
		Vector3 deltaPosition = mousePosition - lastMousePosition;
		lastMousePosition = mousePosition;
		return deltaPosition;
	}

	public static Vector3 getDeltaMousePosition(float planeDistance) {
		Vector3 mousePosition = getMousePosition(planeDistance);
		Vector3 deltaPosition = mousePosition - lastMousePosition;
		lastMousePosition = mousePosition;
		return deltaPosition;
	}
}