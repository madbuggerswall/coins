using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ObjectPool : MonoBehaviour {
	protected int maxPoolAmount;
	[SerializeField] protected List<GameObject> pool;

	// Creates an Object Pool in hierarchy and returns the ObjectPool component.
	public static ObjectPool createObjectPool(Transform parent) {
		ObjectPool objectPool = createInstance(parent).AddComponent<ObjectPool>();
		objectPool.pool = new List<GameObject>();
		return objectPool;
	}

	// Creates an Object Pool GO in hierarchy.
	protected static GameObject createInstance(Transform parent) {
		GameObject objectPool = new GameObject("Object Pool");
		objectPool.transform.position = parent.position;
		objectPool.transform.eulerAngles = Vector3.zero;
		objectPool.transform.SetParent(parent);
		return objectPool;
	}

	// Add an object to the pool.
	GameObject addObject(GameObject prefab) {
		GameObject pooledObject = GameObject.Instantiate(prefab);
		pooledObject.name = prefab.name;
		pooledObject.SetActive(false);
		pooledObject.transform.SetParent(transform);
		pool.Add(pooledObject);
		return pooledObject;
	}

	// Add a list of objects to the pool.
	void addObjects(List<GameObject> prefabs) {
		addObjects(prefabs.ToArray());
	}

	// Add an array of objects to the pool.
	void addObjects(Object[] prefabs) {
		foreach (GameObject prefab in prefabs) {
			addObject(prefab);
		}
	}

	// Returns a disabled pooled object. Creates one if there's none.
	public GameObject getObject(GameObject prefab) {
		foreach (GameObject pooledObject in pool) {
			if (prefab.name == pooledObject.name && !pooledObject.activeInHierarchy) {
				return pooledObject;
			}
		}
		return addObject(prefab);
	}
}
