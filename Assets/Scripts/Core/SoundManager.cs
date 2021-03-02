using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {
	static SoundManager instance;

	AudioSource audioSource;
	[SerializeField] AudioClip wall;
	[SerializeField] AudioClip coin;
	[SerializeField] AudioClip obstacle;
	[SerializeField] AudioClip trigger;
	[SerializeField] AudioClip goal;
	[SerializeField] AudioClip faul;


	void Awake() {
		assertSingleton();
		audioSource = GetComponent<AudioSource>();
	}

	public void playImpactSound(Collider other, float volumeScale) {
		if (other.gameObject.layer == Layers.wall) {
			audioSource.clip = wall;
			audioSource.PlayOneShot(audioSource.clip, volumeScale);
		}

		if (other.gameObject.layer == Layers.coin) {
			audioSource.clip = coin;
			audioSource.PlayOneShot(audioSource.clip, volumeScale);
		}
		
		if (other.gameObject.layer == Layers.obstacle) {
			audioSource.clip = obstacle;
			audioSource.PlayOneShot(audioSource.clip, volumeScale);
		}
		
		if (other.gameObject.layer == Layers.trigger) {
			audioSource.clip = trigger;
			audioSource.PlayOneShot(audioSource.clip, volumeScale);
		}
	}

	public static SoundManager getInstance() { return instance; }
	void assertSingleton() { if (instance == null) { instance = this; } else { Destroy(gameObject); } }
}
