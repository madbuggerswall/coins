using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrefabPath {
	public static readonly string explosions = "Prefabs/Particles/Impacts";
	public static readonly string cards = "Prefabs/Particles/Cards";
	public static readonly string coin = "Prefabs/Particles/Coin";
	public static readonly string obstacles = "Prefabs/Reference Obstacles";
}

// Random prefab generator. 
public class PrefabManager {
	List<GameObject> prefabs;

	public PrefabManager() {
		prefabs = new List<GameObject>();
	}

	public PrefabManager(string prefabPath) : this() {
		addPrefabs(prefabPath);
	}

	public List<GameObject> getPrefabs() {
		return prefabs;
	}

	public GameObject getRandomPrefab() {
		GameObject randomPrefab = prefabs[Random.Range(0, prefabs.Count)];
		return randomPrefab;
	}

	public void addPrefab(string prefabPath) {
		prefabs.Add(Resources.Load(prefabPath) as GameObject);
	}

	void addPrefabs(string prefabPath) {
		foreach (GameObject prefab in Resources.LoadAll(prefabPath)) {
			prefabs.Add(prefab);
		}
	}
}

