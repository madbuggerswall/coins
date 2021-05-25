using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ToastUI : MonoBehaviour {
	static ToastUI instance;

	Queue<string> messages;
	Animation animPlayer;

	[SerializeField] Text message;

	void Awake() {
		assertSingleton();
		messages = new Queue<string>();
		animPlayer = GetComponent<Animation>();
		DontDestroyOnLoad(this);
	}

	// Singleton
	public static ToastUI getInstance() { return instance; }
	void assertSingleton() { if (instance == null) { instance = this; } else { Destroy(gameObject); } }

	public void displayToast(string message) {
		messages.Enqueue(message);
		if (!animPlayer.isPlaying)
			StartCoroutine(playQueue());
	}

	IEnumerator playQueue() {
		while (messages.Count > 0 && !animPlayer.isPlaying) {
			message.text = messages.Dequeue();
			animPlayer.Play();
			yield return new WaitWhile(() => { return animPlayer.isPlaying; });
		}
	}
}
