using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PrefabPool : ObjectPool {
	protected PrefabManager prefabManager;

	public static PrefabPool createPrefabPool(Transform parent) {
		PrefabPool prefabPool = createInstance(parent).AddComponent<PrefabPool>();
		prefabPool.pool = new List<GameObject>();
		prefabPool.prefabManager = new PrefabManager();
		return prefabPool;
	}

	public static PrefabPool createPrefabPool(Transform parent, string prefabPath) {
		PrefabPool prefabPool = createInstance(parent).AddComponent<PrefabPool>();
		prefabPool.pool = new List<GameObject>();
		prefabPool.prefabManager = new PrefabManager(prefabPath);
		return prefabPool;
	}

	// Retrieve prefab from object pool.
	public GameObject spawn(GameObject prefab, Vector3 position) {
		GameObject instance = getObject(prefab);
		instance.transform.position = position;
		instance.SetActive(true);
		return instance;
	}

	// Retrieve prefab from object pool.
	public GameObject spawn(GameObject prefab, Vector3 position, Quaternion rotation) {
		GameObject instance = getObject(prefab);
		instance.transform.position = position;
		instance.transform.rotation = rotation;
		instance.SetActive(true);
		return instance;
	}

	// Retrieve prefab from object pool.
	public GameObject spawn(Vector3 position) {
		GameObject instance = getObject(prefabManager.getRandomPrefab());
		instance.transform.position = position;
		instance.SetActive(true);
		return instance;
	}

	// Return prefab to object pool.
	protected void destroy(GameObject instance) {
		instance.SetActive(false);
		instance.transform.SetParent(transform);
	}
}