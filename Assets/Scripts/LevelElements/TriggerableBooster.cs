﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerableBooster : Booster, ITriggerable {
	[SerializeField] bool isInverse;

	ParticleSystem particles;
	BoxCollider boxCollider;

	void Awake() {
		particles = GetComponentInChildren<ParticleSystem>();
		boxCollider = GetComponent<BoxCollider>();
		particles.gameObject.SetActive(isInverse);
		boxCollider.enabled = isInverse;
	}

	public void trigger() {
		boxCollider.enabled = !isInverse;
		particles.gameObject.SetActive(!isInverse);
	}
	public void untrigger() {
		boxCollider.enabled = isInverse;
		particles.gameObject.SetActive(isInverse);
	}
}
