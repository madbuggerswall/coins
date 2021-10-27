using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainShots : MonoBehaviour {
	[SerializeField] int shots;
	bool coinEntered = false;

	ParticleSystem[] particleSystems;

	void Awake() {
		particleSystems = GetComponentsInChildren<ParticleSystem>();
	}

	void OnTriggerEnter(Collider other) {
		if (!coinEntered) {
			coinEntered = true;
			Player player = LevelManager.getInstance().getPlayer();
			player.setShotsLeft(player.getShotsLeft() + shots);
			stopEmitting();
		}
	}

	void stopEmitting() {
		foreach (ParticleSystem particle in particleSystems) {
			ParticleSystem.EmissionModule emission = particle.emission;
			emission.enabled = false;
		}
	}
}
