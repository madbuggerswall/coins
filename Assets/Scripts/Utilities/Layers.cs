using UnityEngine;

static class Layers {
	public static int coin = LayerMask.NameToLayer("Coin");
	public static int thrownCoin = LayerMask.NameToLayer("Thrown Coin");
	public static int ground = LayerMask.NameToLayer("Ground");
	public static int wall = LayerMask.NameToLayer("Wall");
	public static int goalpost = LayerMask.NameToLayer("Goalpost");
	public static int obstacle = LayerMask.NameToLayer("Obstacle");
	public static int trigger = LayerMask.NameToLayer("Trigger");
}
