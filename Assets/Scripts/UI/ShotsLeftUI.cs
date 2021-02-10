using UnityEngine;
using UnityEngine.UI;

public class ShotsLeftUI {
	Image[] shots;

	public ShotsLeftUI(GameObject shotsLeftPanel) {
		shots = shotsLeftPanel.GetComponentsInChildren<Image>();
	}

	public void setShotsLeft(int shotsLeft) {
		for (int i = 0; i < shots.Length - shotsLeft; i++) {
			shots[i].color = Color.red;
		}
	}
	public void resetShotsLeft() {
		foreach (Image image in shots) {
			image.color = Color.green;
		}
	}
}