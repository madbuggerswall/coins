using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CoinEffects : MonoBehaviour {
	Rigidbody rigidBody;
	PostProcessVolume postProcessVolume;
	ChromaticAberration chromaticAberration;
	Bloom bloom;

	void Awake() {
		rigidBody = GetComponent<Rigidbody>();
		postProcessVolume = FindObjectOfType<PostProcessVolume>();
		chromaticAberration = postProcessVolume.profile.GetSetting<ChromaticAberration>();
		bloom = postProcessVolume.profile.GetSetting<Bloom>();
	}

	void Update() {
		// speedRelatedChromaticAbberation();
		// speedRelatedBloomIntensity();
	}

	void speedRelatedChromaticAbberation() {
		float target = Mathf.InverseLerp(0, 40, rigidBody.velocity.magnitude) - chromaticAberration.intensity.value;
		chromaticAberration.intensity.value += target * Time.deltaTime * 4;
	}

	void speedRelatedBloomIntensity() {
		float target = Mathf.InverseLerp(0, 28, rigidBody.velocity.magnitude) * 4 - bloom.intensity.value;
		bloom.intensity.value += target * Time.deltaTime * 4;
	}
}
