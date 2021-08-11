using UnityEngine;

// Reposition helper class.
public class Relocate : MonoBehaviour {
	Rigidbody rigidBody;
	Vector3 initialPosition;
	float radius = 1;

	void Awake() {
		rigidBody = GetComponent<Rigidbody>();
		initialPosition = rigidBody.position;
	}

	void OnMouseDrag() {
		Vector3 mousePosition = PlayerInput.getPosition(Vector3.Distance(Camera.main.transform.position, transform.position));
		mousePosition.y = rigidBody.position.y;
		Vector3 coinPosition = Vector3.ClampMagnitude(mousePosition - initialPosition, radius);
		rigidBody.MovePosition(initialPosition + coinPosition);
	}
}