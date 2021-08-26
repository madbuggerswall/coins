using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TrajectoryPrediction : MonoBehaviour {
	[SerializeField] int iterations = 40;
	[SerializeField] List<GameObject> obstacles;
	[SerializeField] List<GameObject> coins;

	PhysicsScene physicsScene;
	Scene trajectoryScene;

	LineRenderer lineRenderer;

	UnityAction simulatePhysics;
	void Awake() {
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.positionCount = iterations;

		// Physics.autoSimulation = false;
		CreateSceneParameters sceneParameters = new CreateSceneParameters(LocalPhysicsMode.Physics3D);
		trajectoryScene = SceneManager.CreateScene("Trajectory Scene", sceneParameters);
		physicsScene = trajectoryScene.GetPhysicsScene();


		initalizeElements();
		instantiateElements();
	}

	void initalizeElements() {
		// Initialize obstacles.
		GameObject[] levelElements = FindObjectsOfType<GameObject>();
		foreach (GameObject levelElement in levelElements) {
			if (levelElement.layer == Layers.obstacle && levelElement.GetComponent<Collider>() != null) {
				obstacles.Add(levelElement);
			} else if (levelElement.layer == Layers.wall && levelElement.GetComponent<Collider>() != null) {
				obstacles.Add(levelElement);
			} else if (levelElement.layer == Layers.booster && levelElement.GetComponent<Collider>() != null) {
				obstacles.Add(levelElement);
			} else if (levelElement.layer == Layers.trigger && levelElement.GetComponent<Collider>() != null) {
				obstacles.Add(levelElement);
			} else if (levelElement.layer == Layers.ground && levelElement.GetComponent<Collider>() != null) {
				obstacles.Add(levelElement);
			}
		}
	}

	void instantiateElements() {
		// Instantiate obstacles and walls & disable thier renderers.
		List<GameObject> instantiatedGameObjects = new List<GameObject>();
		foreach (GameObject obstacle in obstacles) {
			GameObject obstacleInstance = Instantiate(obstacle, obstacle.transform.position, obstacle.transform.rotation);
			foreach (Renderer renderer in obstacleInstance.GetComponentsInChildren<Renderer>()) { renderer.enabled = false; }
			instantiatedGameObjects.Add(obstacleInstance);
		}

		// Instantiate coins & disable thier renderers.
		foreach (Coin coin in CoinSet.getInstance().getCoins()) {
			GameObject coinInstance = Instantiate(coin.gameObject, coin.transform.position, coin.transform.rotation);
			Destroy(coinInstance.GetComponent<Coin>());
			Destroy(coinInstance.GetComponent<Slingshot>());
			Destroy(coinInstance.GetComponent<Cinemachine.CinemachineCollisionImpulseSource>());
			foreach (Renderer renderer in coinInstance.GetComponentsInChildren<Renderer>()) { renderer.enabled = false; }
			coins.Add(coinInstance);
			instantiatedGameObjects.Add(coinInstance);
		}

		// Move instantiated objects to physics scene.
		foreach (GameObject instantiatedGameObject in instantiatedGameObjects) {
			SceneManager.MoveGameObjectToScene(instantiatedGameObject, trajectoryScene);
		}
	}

	public void simulate(Transform realCoin, Vector3 force) {

		GameObject coin = coins[0];
		Rigidbody coinRigidbody = coin.GetComponent<Rigidbody>();
		coin.transform.position = realCoin.position;
		coin.transform.rotation = realCoin.rotation;
		coinRigidbody.velocity = Vector3.zero;
		lineRenderer.SetPosition(0, coin.transform.position);
		coinRigidbody.AddForce(force, ForceMode.Impulse);
		for (int i = 1; i < iterations; i++) {
			physicsScene.Simulate(2 * Time.fixedDeltaTime);
			lineRenderer.SetPosition(i, coin.transform.position);
		}
	}
}
