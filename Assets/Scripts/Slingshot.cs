using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour {

	Vector3 initialPos;
	Vector3 finalPos;
	Vector3 throwForce;

	Guide guide;
	Rigidbody rigidBody;

	bool selected;
	bool shot;

	void Awake() {
		guide = GetComponentInChildren<Guide>();
		rigidBody = GetComponent<Rigidbody>();

		selected = false;
		shot = false;
	}

	// Update is called once per frame
	void Update() {

	}

	// Select coin
	void OnMouseEnter() {
		selected = true;
		Match.getInstance().coinSelected.Invoke();
	}

	void OnMouseExit() {
		selected = false;
		Match.getInstance().coinDeselected.Invoke();
	}

	// Draw
	void OnMouseDown() {
		initialPos = Input.mousePosition;
		guide.enable(true);
		guide.setPoints(transform.position, transform.position + throwForce);
	}

	// Aim
	void OnMouseDrag() {
		calculateThrowForce();
		guide.setPoints(transform.position, transform.position + throwForce);
	}

	// Release
	void OnMouseUp() {
		rigidBody.AddForce(throwForce, ForceMode.Impulse);
		shot = true;
		gameObject.layer = Layers.thrownCoin;
		Match.getInstance().coinShot.Invoke();
		guide.enable(false);
	}

	void calculateThrowForce() {
		finalPos = Input.mousePosition;
		throwForce = initialPos - finalPos;
		throwForce.z = throwForce.y;
		throwForce.y = 0f;
		throwForce *= Time.fixedDeltaTime;
	}

	public void clearFlags() {
		selected = false;
		shot = false;
	}
	
	public bool isSelected() { return selected; }
	public bool isShot() { return shot; }
}
