using UnityEngine;
using System.Collections;

public class Undo : Card {
	public override void apply() {
		StartCoroutine(resetCoinSet());
	}

	public override void reset() { }

	IEnumerator resetCoinSet() {
		yield return FindObjectOfType<CoinRecorder>().rewindLastValidShot();
	}
}