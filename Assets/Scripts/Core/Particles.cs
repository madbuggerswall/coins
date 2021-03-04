using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour {
	PrefabPool pool;

	public static float force = 8;
	public static float radius = 4;

	public static GameObject wallParticlePrefab;

	void Awake() {
		pool = PrefabPool.createPrefabPool(this.transform);
		wallParticlePrefab = Resources.Load(PrefabPath.explosions + "/Wall") as GameObject;
	}

	public void explodeAt(GameObject particlePrefab, Vector3 position) {
		pool.spawn(particlePrefab, position);
	}

	public void explodeAt(GameObject particlePrefab, Collision other) {
		Vector3 position = other.contacts[0].point;
		Quaternion rotation = Quaternion.LookRotation(other.contacts[0].normal, Vector3.up);
		pool.spawn(particlePrefab, position, rotation);
	}
}
