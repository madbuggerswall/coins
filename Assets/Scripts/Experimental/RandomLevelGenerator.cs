using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class RandomLevelGenerator : MonoBehaviour {
	[SerializeField] GameObject ground;
	ObstacleArea obstacleArea;
	PrefabManager prefabManager;
	Transform parent;
	
	void OnEnable() {
		obstacleArea = new ObstacleArea(ground.GetComponent<Collider>());
		prefabManager = new PrefabManager(PrefabPath.obstacles);
		parent = new GameObject("Parent").transform;
		foreach (Vector3 position in obstacleArea.getHalfGrid()) {
			if (Random.value > .8f) {
				GameObject obstaclePrefab = prefabManager.getRandomPrefab();
				Vector3 spawnPosition = position;
				spawnPosition.y = obstaclePrefab.transform.position.y;
				GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
				obstacle.transform.SetParent(parent);
			}
		}
	}

	void OnDrawGizmos() {
		float sphereRadius = .2f;
		foreach (Vector3 position in obstacleArea.getQuarterGrid()) {
			Gizmos.color = Color.cyan;
			Gizmos.DrawSphere(position, sphereRadius);
		}

		foreach (Vector3 position in obstacleArea.getHalfGrid()) {
			Gizmos.color = Color.yellow;
			Gizmos.DrawSphere(position, sphereRadius);
		}
	}
}

struct ObstacleArea {
	List<Vector3> quarterGrid;
	List<Vector3> halfGrid;
	float quarterGridIncrement;
	float halfGridIncrement;
	float xMax, xMin, zMax, zMin;

	public ObstacleArea(Collider collider) {
		quarterGridIncrement = 2f;
		halfGridIncrement = 4f;
		quarterGrid = new List<Vector3>();
		halfGrid = new List<Vector3>();
		xMax = collider.bounds.center.x + collider.bounds.extents.x;
		xMin = collider.bounds.center.x - collider.bounds.extents.x;
		zMax = collider.bounds.center.z + collider.bounds.extents.z;
		zMin = collider.bounds.center.z - collider.bounds.extents.z;
		initializeQuarterGrid();
		initializeHalfGrid();
	}

	void initializeQuarterGrid() {
		for (float i = xMin + 1; i < xMax - 1; i += quarterGridIncrement) {
			for (float j = zMin + 1; j < zMax - 1; j += quarterGridIncrement) {
				quarterGrid.Add(new Vector3(i, 0, j));
			}
		}
	}

	void initializeHalfGrid() {
		for (float i = xMin + 1; i < xMax - 1; i += quarterGridIncrement) {
			for (float j = zMin + 2; j < zMax - 2; j += halfGridIncrement) {
				halfGrid.Add(new Vector3(i, 0, j));
			}
		}
	}

	public List<Vector3> getHalfGrid() { return halfGrid; }
	public List<Vector3> getQuarterGrid() { return quarterGrid; }
}
