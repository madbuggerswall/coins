using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour {
	[SerializeField] Transform target;
	Vector3 offset;
	void Awake() {
		offset = transform.position - target.position;
	}
	void Update() {
		if (transform.position != target.position + offset)
			transform.position += (target.position + offset - transform.position) * Time.deltaTime;
	}
}
