using UnityEngine;

public class DoubleTap {

	float doubleTapThreshold = 0.5f;
	float tapTime;
	bool tappedOnce;

	public DoubleTap() { }

	public bool doubleTapped() {
		if (!tappedOnce) {
			tapTime = Time.time;
			tappedOnce = true;
		} else {
			tappedOnce = false;
			float doubleTapTime = Time.time - tapTime;
			if (doubleTapTime <= doubleTapThreshold)
				return true;
		}
		return false;
	}
}