using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
class Alternator : MonoBehaviour {
	[RangeAttribute(1, 24)]
	[SerializeField] float period = 16;
	[SerializeField] float radius = 8;

	float interpolant;
	Vector3[] axes;
	Vector3 center;

	Rigidbody rigidBody;
	void Awake() {
		axes = new Vector3[] { Vector3.right, Vector3.up, Vector3.forward };
		interpolant = 0;
		center = transform.position;
		rigidBody = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		rigidBody.MovePosition(center + sinAboutAxis(2));
	}

	public float sin() {
		return sin(period, radius);
	}

	public float sin(float period, float radius) {
		interpolant += Time.deltaTime;
		interpolant = interpolant % period;
		float radian = Mathf.InverseLerp(0, period, interpolant) * 2f * Mathf.PI;
		return Mathf.Sin(radian) * radius;
	}

	public Vector3 sinAboutAxis(int index) {
		return axes[index] * sin(period, radius);
	}

	public Vector3 getCenter() { return center; }
	public void setCenter(Vector3 center) { this.center = center; }
}