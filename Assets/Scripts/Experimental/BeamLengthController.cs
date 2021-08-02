using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class BeamLengthController : MonoBehaviour {
	[SerializeField] ParticleSystem particles;
	[SerializeField] float length = 0;

	float initialLifetime = 1f;
	float initialSpeed = 10f;
	float initialLength = 10;
	float initialEmissionRate = 24;

	void Update() {
		adjustLength(length);
	}

	void adjustLength(float length) {
		float ratio = length / initialLength;
		var mainModule = particles.main;
		var emissionModule = particles.emission;

		mainModule.startLifetime = initialLifetime * Mathf.Sqrt(ratio);
		mainModule.startSpeed = initialSpeed * Mathf.Sqrt(ratio);
		emissionModule.rateOverTime = initialEmissionRate * Mathf.Sqrt(ratio);
	}
}
