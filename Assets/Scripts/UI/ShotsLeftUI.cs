using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class ShotsLeftUI {
	// readonly Color red = new Color32(0xB5, 0x70, 0x70, 0xFF);
	readonly Color red = new Color32(0xD7, 0x28, 0x57, 0xFF);
	readonly Color green = new Color32(0x00, 0xB7, 0x75, 0xFF);
	List<Image> shots = new List<Image>();

	public ShotsLeftUI(GameObject shotsLeftPanel) {
		shotsLeftPanel.GetComponentsInChildren<Image>(shots);
	}

	public ShotsLeftUI(GameObject shotsLeftPanel, int shotsLeft) {
		shotsLeftPanel.GetComponentsInChildren<Image>(shots);
		initializeShotsLeft(shotsLeft, shotsLeftPanel.transform);
	}

	void initializeShotsLeft(int shotsLeft, Transform parent) {
		float offsetX = shots[shots.Count - 1].transform.localPosition.x - shots[shots.Count - 2].transform.localPosition.x;
		Vector3 offset = Vector3.right * offsetX;
		int shotCount = shots.Count;
		for (int i = 0; i < shotsLeft - shotCount; i++) {
			GameObject shot = GameObject.Instantiate(shots[shots.Count - 1].gameObject, parent);
			shot.transform.localPosition += (i + 1) * offset;
			shots.Add(shot.GetComponent<Image>());
		}
	}

	public void setShotsLeft(int shotsLeft) {
		for (int i = 0; i < shots.Count; i++) {
			if (i < shotsLeft)
				shots[i].color = green;
			else
				shots[i].color = red;
		}
	}
	public void resetShotsLeft() {
		foreach (Image image in shots) {
			image.color = green;
		}
	}
}