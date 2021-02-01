using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Layers {
	public static int coin = LayerMask.NameToLayer("Coin");
	public static int thrownCoin = LayerMask.NameToLayer("Thrown Coin");
	public static int ground = LayerMask.NameToLayer("Ground");
	public static int wall = LayerMask.NameToLayer("Wall");
	public static int goalpost = LayerMask.NameToLayer("Goalpost");
}

public class Coin : MonoBehaviour {

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}
}
