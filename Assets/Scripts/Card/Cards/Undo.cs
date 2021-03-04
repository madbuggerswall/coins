using UnityEngine;
using System.Collections;

public class Undo : Card {
	public override void apply() {
		StartCoroutine(resetCoinSet());
	}

	public override void reset() { }

	IEnumerator resetCoinSet() {
		yield return ((Puzzle) LevelManager.getInstance().getGame()).getFormation().resetCoinSet();
		LevelManager.getInstance().events.playerContinuesTurn.Invoke();
	}
}