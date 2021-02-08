using UnityEngine;
using UnityEngine.Events;

public class CoinSetEvents {
	public UnityEvent coinStatusChanged;
	public UnityEvent coinShot;
	public UnityEvent shotEnded;

	public CoinSetEvents() {
		coinStatusChanged = new UnityEvent();
		coinShot = new UnityEvent();
		shotEnded = new UnityEvent();
	}
}