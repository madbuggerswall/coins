using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {
	Rigidbody rigidBody;
	[SerializeField] float rotationSpeed = 4;
	void Awake() {
		rigidBody = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		rigidBody.MoveRotation(Quaternion.Euler(rigidBody.rotation.eulerAngles + Vector3.up * rotationSpeed));
	}

	void OnTriggerEnter(Collider other) {
		Particles.getInstance().explodeAt(Particles.collectiblePrefab, transform.position);
		gameObject.SetActive(false);
		LevelManager.getInstance().getGame().getPlayer().incrementCollectibles();
	}
}