﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;


public class Authentication : MonoBehaviour {
	static Authentication instance;

	FirebaseUser user;
	FirebaseAuth authentication;

	void Awake() {
		assertSingleton();
		authentication = FirebaseAuth.DefaultInstance;
		authentication.StateChanged += authStateChanged;
	}

	// Singleton
	public static Authentication getInstance() { return instance; }
	void assertSingleton() { if (instance == null) { instance = this; } else { Destroy(gameObject); } }

	// Check for persistent login behaviour
	void authStateChanged(object sender, System.EventArgs eventArgs) {
		if (authentication.CurrentUser != user) {
			bool signedIn = user != authentication.CurrentUser && authentication.CurrentUser != null;
			if (!signedIn && user != null) {
				ToastUI.getInstance().displayToast("Signed out " + user.UserId);
			}
			user = authentication.CurrentUser;
			if (signedIn) {
				ToastUI.getInstance().displayToast("Signed in " + user.Email);
			}
		}
	}

	// TODO: Implement Google sing-in
	IEnumerator signInWithGoogle() {
		// TODO: Native extension for getting credential prerequisites.
		string googleIdToken = "", googleAccessToken = "";

		Credential credential = GoogleAuthProvider.GetCredential(googleIdToken, googleAccessToken);
		var signInTask = authentication.SignInWithCredentialAsync(credential);
		yield return new WaitUntil(() => signInTask.IsCompleted);

		if (signInTask.Exception != null) {
			ToastUI.getInstance().displayToast($"Failed to register task with{signInTask.Exception}");
		} else {
			user = signInTask.Result;
			ToastUI.getInstance().displayToast("Firebase user created successfully: " + user.DisplayName + " " + user.UserId);
		}
	}

	// Anonymous sign-in
	IEnumerator signInAnonymously() {
		var signInTask = authentication.SignInAnonymouslyAsync();
		yield return new WaitUntil(() => signInTask.IsCompleted);

		if (signInTask.Exception != null) {
			ToastUI.getInstance().displayToast($"Failed to register task with {signInTask.Exception}");
		} else {
			user = signInTask.Result;
			ToastUI.getInstance().displayToast("Firebase user created successfully: " + user.DisplayName + " " + user.UserId);
		}
	}

	// Register user
	public IEnumerator registerUser(string email, string password) {
		FirebaseAuth authentication = FirebaseAuth.DefaultInstance;
		var registrationTask = authentication.CreateUserWithEmailAndPasswordAsync(email, password);
		yield return new WaitUntil(() => registrationTask.IsCompleted);

		if (registrationTask.Exception != null) {
			ToastUI.getInstance().displayToast($"Failed to register task with{registrationTask.Exception}");
		} else {
			user = registrationTask.Result;
			ToastUI.getInstance().displayToast("Firebase user created successfully: " + user.DisplayName + " " + user.UserId);
			StartCoroutine(sendVerificationEmail());
		}
	}

	// Sign in with mail and password
	IEnumerator signInWithEmail(string email, string password) {
		var signInTask = authentication.SignInWithEmailAndPasswordAsync(email, password);
		yield return new WaitUntil(() => signInTask.IsCompleted);

		if (signInTask.Exception != null) {
			ToastUI.getInstance().displayToast($"Failed to sign in with {signInTask.Exception}");
		} else {
			user = signInTask.Result;
			ToastUI.getInstance().displayToast("Firebase user created successfully: " + user.DisplayName + " " + user.UserId);
		}
	}


	IEnumerator sendVerificationEmail() {
		if (user != null) {
			if (!user.IsEmailVerified) {
				var emailTask = user.SendEmailVerificationAsync();
				yield return new WaitUntil(() => emailTask.IsCompleted);

				if (emailTask.IsCanceled) {
					ToastUI.getInstance().displayToast("SendEmailVerificationAsync was canceled.");
				} else if (emailTask.IsFaulted) {
					ToastUI.getInstance().displayToast("SendEmailVerificationAsync encountered an error: " + emailTask.Exception);
				} else {
					ToastUI.getInstance().displayToast("Email sent successfully.");
				}
			} else {
				ToastUI.getInstance().displayToast("User is verified");
			}
		}
	}
}
