using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementPopupUI : MonoBehaviour {
	Queue<string> descriptions;
	Animation animPlayer;

	[SerializeField] Text description;

	void Awake() {
		descriptions = new Queue<string>();
		animPlayer = GetComponent<Animation>();
	}

	public void displayAchievement(string description) {
		descriptions.Enqueue(description);
		if (!animPlayer.isPlaying)
			StartCoroutine(playQueue());
	}

	IEnumerator playQueue() {
		while (descriptions.Count > 0 && !animPlayer.isPlaying) {
			description.text = descriptions.Dequeue();
			animPlayer.Play();
			yield return new WaitWhile(() => { return animPlayer.isPlaying; });
		}
	}
}
