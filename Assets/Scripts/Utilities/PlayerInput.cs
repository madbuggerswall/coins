using UnityEngine;

public static class PlayerInput {
	public static Vector3 getPosition() {
#if UNITY_EDITOR
		return getMousePosition();
#elif UNITY_ANDROID
			return getTouchPosition();
#endif
	}

	public static Vector3 getPosition(float planeDistance) {
#if UNITY_EDITOR
		return getMousePosition(planeDistance);
#elif UNITY_ANDROID
			return getTouchPosition(planeDistance);
#endif
	}

	public static Vector3 getMousePosition() {
		Vector3 mousePosition = Input.mousePosition;
		// mousePosition.z = Camera.main.nearClipPlane;
		mousePosition.z = Camera.main.transform.position.y;
		return Camera.main.ScreenToWorldPoint(mousePosition);
	}

	public static Vector3 getMousePosition(float planeDistance) {
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = planeDistance;
		return Camera.main.ScreenToWorldPoint(mousePosition);
	}

	public static Vector3 getTouchPosition() {
		if (Input.touchCount > 0) {
			Vector3 touchPosition = Input.GetTouch(0).position;
			// touchPosition.z = Camera.main.nearClipPlane;
			touchPosition.z = Camera.main.transform.position.y;
			return Camera.main.ScreenToWorldPoint(touchPosition);
		} else {
			return Vector3.zero;
		}
	}

	public static Vector3 getTouchPosition(float planeDistance) {
		if (Input.touchCount > 0) {
			Vector3 touchPosition = Input.GetTouch(0).position;
			touchPosition.z = planeDistance;
			return Camera.main.ScreenToWorldPoint(touchPosition);
		} else {
			return Vector3.zero;
		}
	}
}