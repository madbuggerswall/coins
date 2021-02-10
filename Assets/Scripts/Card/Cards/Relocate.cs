using UnityEngine;

public class Relocate : MonoBehaviour {
	Rigidbody rigidBody;
	Vector3 initialPosition;
	float radius = 1;

	void Awake() {
		rigidBody = GetComponent<Rigidbody>();
		initialPosition = rigidBody.position;
	}

	void OnMouseDown() {
		Debug.Log("OnMouseDown");
	}
	void OnMouseUpAsButton() {
		Debug.Log("MouseUp");
	}

	void OnMouseDrag() {
		Vector3 mousePosition = PlayerInput.getMousePosition(Camera.main.transform.position.y - transform.position.y);
		mousePosition.y = rigidBody.position.y;
		Vector3 coinPosition = Vector3.ClampMagnitude(mousePosition - initialPosition, radius);
		// coinPosition.y = rigidBody.position.y;
		rigidBody.MovePosition(initialPosition + coinPosition);
	}
}