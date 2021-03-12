using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour {
	static Particles instance;	
	PrefabPool pool;

	// Impact
	public static GameObject wallParticlePrefab;

	// Coin 
	public static GameObject coinSelectedPrefab;
	public static GameObject collectiblePrefab;

	// Cards
	public static GameObject boostFXPrefab;
	public static GameObject shrinkFXPrefab;
	public static GameObject expandFXPrefab;
	public static GameObject repositionFXPrefab;
	public static GameObject ghostFXPrefab;


	void Awake() {
		assertSingleton();
		
		pool = PrefabPool.createPrefabPool(this.transform);
		wallParticlePrefab = Resources.Load(PrefabPath.explosions + "/Wall") as GameObject;

		coinSelectedPrefab = Resources.Load(PrefabPath.coin + "/Coin Selected") as GameObject;
		collectiblePrefab = Resources.Load(PrefabPath.coin + "/Collectible") as GameObject;

		boostFXPrefab = Resources.Load(PrefabPath.cards + "/Boost") as GameObject;
		shrinkFXPrefab = Resources.Load(PrefabPath.cards + "/Shrink") as GameObject;
		expandFXPrefab = Resources.Load(PrefabPath.cards + "/Expand") as GameObject;
		repositionFXPrefab = Resources.Load(PrefabPath.cards + "/Reposition") as GameObject;
		ghostFXPrefab = Resources.Load(PrefabPath.cards + "/Ghost") as GameObject;
	}

	// Singleton 
	public static Particles getInstance() { return instance; }
	void assertSingleton() { if (instance == null) { instance = this; } else { Destroy(gameObject); } }

	public GameObject explodeAt(GameObject particlePrefab, Vector3 position) {
		return pool.spawn(particlePrefab, position);
	}

	public void explodeAt(GameObject particlePrefab, Collision other) {
		Vector3 position = other.contacts[0].point;
		Quaternion rotation = Quaternion.LookRotation(other.contacts[0].normal, Vector3.up);
		pool.spawn(particlePrefab, position, rotation);
	}
}
