using UnityEngine;
using System.Collections;

public class Undo : Card {
	public override void apply() {
		StartCoroutine(resetCoinSet());
	}

	public override void reset() { }

	IEnumerator resetCoinSet() {
		yield return FindObjectOfType<Puzzle>().getFormation().resetCoinSet();
		LevelManager.getInstance().events.playerContinuesTurn.Invoke();
	}
}