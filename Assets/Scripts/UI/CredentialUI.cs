using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CredentialUI : MonoBehaviour {
	new Animation animation;

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

	void onSignUpPressed() {
		Authentication authentication = Authentication.getInstance();
		if (checkCredentials(emailField.text, passwordField.text, passwordCheckField.text)) {
			authentication.StartCoroutine(authentication.registerUser(emailField.text, passwordField.text));
		}
	}

	bool checkCredentials(string email, string password, string passwordCheck) {
		bool isEmailValid = email.Contains("@") && email.Substring(email.IndexOf('@')).Contains(".");
		bool isPasswordLengthValid = password.Length >= 8;
		bool isPasswordValid = password == passwordCheck;

		if (!isEmailValid) {
			ToastUI.getInstance().displayToast("Email address is not valid.");
			return false;
		} else if (!isPasswordLengthValid) {
			ToastUI.getInstance().displayToast("Password must be at least 8 characters long.");
			return false;
		} else if (!isPasswordValid) {
			ToastUI.getInstance().displayToast("Passwords do not match");
			return false;
		} else {
			return true;
		}
	}
}
