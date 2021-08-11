using UnityEngine;

public class DoubleTap : MonoBehaviour {
	float doubleTapThreshold = 0.5f;
	float tapTime;
	bool tappedOnce;

	void Update() {
		if (doubleTapped()) {
			LevelManager.getInstance().events.cardApplied.Invoke();
		}
	}

	public bool doubleTapped() {
		if (!tappedOnce && Input.GetMouseButtonDown(0)) {
			tapTime = Time.time;
			tappedOnce = true;
		} else if (Input.GetMouseButtonDown(0)) {
			tappedOnce = false;
			float doubleTapTime = Time.time - tapTime;
			if (doubleTapTime <= doubleTapThreshold)
				return true;
		}
		return false;
	}
}