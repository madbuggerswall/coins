using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementPopupUI : MonoBehaviour {
	Queue<string> descriptions;
	new Animation animation;

	[SerializeField] Text description;

	void Awake() {
		descriptions = new Queue<string>();
		animation = GetComponent<Animation>();
	}

	public void displayAchievement(string description) {
		descriptions.Enqueue(description);
		if (!animation.isPlaying)
			StartCoroutine(playQueue());
	}

	IEnumerator playQueue() {
		while (descriptions.Count > 0 && !animation.isPlaying) {
			description.text = descriptions.Dequeue();
			animation.Play();
			yield return new WaitWhile(() => { return animation.isPlaying; });
		}
	}
}
