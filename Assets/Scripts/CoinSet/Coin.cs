﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
	Rigidbody rigidBody;

	void Awake() {
		rigidBody = GetComponent<Rigidbody>();
	}

	void OnCollisionEnter(Collision other) {
		if (other.gameObject.layer != Layers.ground) {
			SoundManager.getInstance().playImpactSound(other.collider, other.relativeVelocity.magnitude / 48);
			Particles.getInstance().explodeAt(Particles.wallParticlePrefab, other);
		}
	}

	void OnTriggerEnter(Collider other) {
		SoundManager.getInstance().playImpactSound(other, .2f);
	}

	public void setDrag(float drag) { rigidBody.drag = drag; }
}
