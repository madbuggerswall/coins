using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-8)]
public class GameManager : MonoBehaviour {
	
	void Awake() {
		Application.targetFrameRate = 60;
	}
}
