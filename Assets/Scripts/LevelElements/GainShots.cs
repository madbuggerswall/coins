using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainShots : MonoBehaviour {
	[SerializeField] int shots;
	bool coinEntered = false;

	void OnTriggerEnter(Collider other) {
		if (!coinEntered) {
			coinEntered = true;
			Player player = LevelManager.getInstance().getPlayer();
			player.setShotsLeft(player.getShotsLeft() + shots);
		}
	}
}
