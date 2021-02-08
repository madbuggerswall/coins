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
	Rigidbody rigidBody;

	void Awake() {
		rigidBody = GetComponent<Rigidbody>();
	}

	public void setDrag(float drag) { rigidBody.drag = drag; }
}
