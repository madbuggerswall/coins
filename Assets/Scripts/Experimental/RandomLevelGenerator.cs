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
		if (!Application.isPlaying) {
			obstacleArea = new ObstacleArea(ground.GetComponent<Collider>());
			prefabManager = new PrefabManager(PrefabPath.obstacles);
			parent = new GameObject("Parent").transform;
			spawnRandomObstacles();
		}
	}

	void OnDisable() {
		DestroyImmediate(parent.gameObject);
	}

	void OnDrawGizmos() {
		float sphereRadius = .2f;
		if (!Application.isPlaying) {
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

	void spawnRandomObstacles() {
		for (int i = 6; i < obstacleArea.getRowCount(); i++) {
			GameObject obstaclePrefab = prefabManager.getRandomPrefab();
			Vector3 position;
			if (obstaclePrefab.name == "Obstacle") {
				int randomIndex = Random.Range(0, obstacleArea.getHalfRowIncrement());
				position = obstacleArea.getHalfGrid()[i * obstacleArea.getHalfRowIncrement() + randomIndex];
				position.y = obstaclePrefab.transform.position.y;
			} else {
				int randomIndex = Random.Range(0, obstacleArea.getQuarterRowIncrement());
				position = obstacleArea.getQuarterGrid()[i * obstacleArea.getQuarterRowIncrement() + randomIndex];
				position.y = obstaclePrefab.transform.position.y;
			}

			if (Random.value < 0.5f) {
				GameObject obstacle = Instantiate(obstaclePrefab, position, Quaternion.identity);
				obstacle.transform.SetParent(parent);
			}
		}
	}
}

class ObstacleArea {
	List<Vector3> quarterGrid;
	List<Vector3> halfGrid;

	float quarterGridIncrement = 2f;
	float halfGridIncrement = 4f;

	int halfRowIncrement = 6;
	int quarterRowIncrement = 12;
	int rowCount;

	float xMax, xMin, zMax, zMin;

	public ObstacleArea(Collider collider) {
		quarterGrid = new List<Vector3>();
		halfGrid = new List<Vector3>();
		initializeBounds(collider);
		initializeQuarterGrid();
		initializeHalfGrid();
		rowCount = quarterGrid.Count / quarterRowIncrement;
		Debug.Log("Row Count: " + rowCount);
	}

	void initializeBounds(Collider collider) {
		xMax = collider.bounds.center.x + collider.bounds.extents.x;
		xMin = collider.bounds.center.x - collider.bounds.extents.x;
		zMax = collider.bounds.center.z + collider.bounds.extents.z;
		zMin = collider.bounds.center.z - collider.bounds.extents.z;
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

	public int getQuarterRowIncrement() { return quarterRowIncrement; }
	public int getHalfRowIncrement() { return halfRowIncrement; }
	public int getRowCount() { return rowCount; }
	public List<Vector3> getHalfGrid() { return halfGrid; }
	public List<Vector3> getQuarterGrid() { return quarterGrid; }
}
