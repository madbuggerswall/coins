using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;


public class Authentication : MonoBehaviour {
	static Authentication instance;

	FirebaseUser user;

	void Awake() {
		assertSingleton();
	}

	// Singleton
	public static Authentication getInstance() { return instance; }
	void assertSingleton() { if (instance == null) { instance = this; } else { Destroy(gameObject); } }

	// TODO: Implement Google sing-in
	IEnumerator signInWithGoogle() {
		// TODO: Native extension for getting credential prerequisites.
		string googleIdToken = "", googleAccessToken = "";
		
		FirebaseAuth authentication = FirebaseAuth.DefaultInstance;
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
		FirebaseAuth authentication = FirebaseAuth.DefaultInstance;

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
		FirebaseAuth authentication = FirebaseAuth.DefaultInstance;
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
