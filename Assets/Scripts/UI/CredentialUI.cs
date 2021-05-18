using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CredentialUI : MonoBehaviour {
	Animation animation;

	[SerializeField] InputField emailField;
	[SerializeField] InputField passwordField;
	[SerializeField] InputField passwordCheckField;

	[SerializeField] Button cancel;
	[SerializeField] Button signUp;

	void Awake() {
		animation = GetComponent<Animation>();

		cancel.onClick.AddListener(() => {
			animation.Play("hideCredentialPanel");
			FindObjectOfType<MainMenuUI>().GetComponent<Animation>().Play("displayMainMenuPanel");
		});

		signUp.onClick.AddListener(() => onSignUpPressed());
	}

	void onSignUpPressed(){
		string email = emailField.text;
		if(passwordField.text != passwordCheckField.text){
			FindObjectOfType<ToastUI>().displayToast("Passwords do not match");
		}
	}
}
