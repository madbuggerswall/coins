using UnityEngine;

public class CameraPeek : MonoBehaviour {
	[SerializeField] Transform peekCrosshair;
	Coin[] coins;

	// Mouse
	Vector3 deltaPosition;
	Vector3 formerMousePosition;
	Vector3 deltaMousePosition;

	// Touch
	float initialDistance = 0;
	float distance = 0;

	Vector3 averageCoinPos;

	void Awake() {
		coins = CoinSet.getInstance().getCoins();
	}

	void Update() {
		averageCoinPos = (coins[0].transform.position + coins[1].transform.position + coins[2].transform.position) / 3f;

#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
		touchInput();
#endif

#if UNITY_EDITOR
		mouseInput();
#endif

		clampCrosshairPosition();
	}

	void touchInput() {
		Debug.Log("Touch input");
		if (Input.touchCount == 2 && initialDistance == 0) {
			initialDistance = screenToWorld(Input.GetTouch(0).position - Input.GetTouch(1).position, 0.1f).magnitude;
		} else if (Input.touchCount == 0 && initialDistance != 0)
			initialDistance = 0;

		if (Input.touchCount == 2) {
			distance = screenToWorld(Input.GetTouch(0).position - Input.GetTouch(1).position, 0.1f).magnitude;
			peekCrosshair.position = (initialDistance - distance + averageCoinPos.x) * Vector3.right;
		} else {
			peekCrosshair.position = averageCoinPos;
		}
	}

	void mouseInput() {
		if (Input.GetMouseButton(1)) {
			deltaMousePosition = Input.mousePosition - formerMousePosition;
			deltaMousePosition = screenToWorld(deltaMousePosition, 0.1f);
			deltaMousePosition.z = 0;
			peekCrosshair.position += deltaMousePosition;
		} else {
			peekCrosshair.position = averageCoinPos;
		}
		formerMousePosition = Input.mousePosition;
	}

	Vector3 screenToWorld(Vector3 position, float scale) {
		return new Vector3(-position.y, 0f, position.x) * scale;
	}

	void clampCrosshairPosition() {
		// TODO: Do the clamping according to ground plane. [collider.bounds.extends]
		// float posZ = Mathf.Clamp(peekCrosshair.position.z, -32, 32);
		float posX = Mathf.Clamp(peekCrosshair.position.x, 0, 80);
		peekCrosshair.position = new Vector3(posX, peekCrosshair.position.y, peekCrosshair.position.z);
	}
}