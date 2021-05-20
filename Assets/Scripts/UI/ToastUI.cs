using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToastUI : MonoBehaviour {
	static ToastUI instance;

	Queue<string> messages;
	new Animation animation;

	[SerializeField] Text message;

	void Awake() {
		assertSingleton();
		messages = new Queue<string>();
		animation = GetComponent<Animation>();
	}

	// Singleton
	public static ToastUI getInstance() { return instance; }
	void assertSingleton() { if (instance == null) { instance = this; } else { Destroy(gameObject); } }

	public void displayToast(string message) {
		messages.Enqueue(message);
		if (!animation.isPlaying)
			StartCoroutine(playQueue());
	}

	IEnumerator playQueue() {
		while (messages.Count > 0 && !animation.isPlaying) {
			message.text = messages.Dequeue();
			animation.Play();
			yield return new WaitWhile(() => { return animation.isPlaying; });
		}
	}
}
