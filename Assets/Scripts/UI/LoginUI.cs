using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour {
	[SerializeField] Button googleSignIn;
	[SerializeField] Button facebookSignIn;
	[SerializeField] Button mailSignIn;

	void Awake() {
		mailSignIn.onClick.AddListener(() => {
			FindObjectOfType<CredentialUI>().GetComponent<Animation>().Play("displayCredentialPanel");
			FindObjectOfType<MainMenuUI>().GetComponent<Animation>().Play("hideMainMenuPanel");
		});
	}
}
