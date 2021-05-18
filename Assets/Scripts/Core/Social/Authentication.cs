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
		string googleIdToken = "", googleAccessToken = "";
		FirebaseAuth authentication = FirebaseAuth.DefaultInstance;
		Credential credential = GoogleAuthProvider.GetCredential(googleIdToken, googleAccessToken);
		var signInTask = authentication.SignInWithCredentialAsync(credential);
		yield return new WaitUntil(() => signInTask.IsCompleted);

		if (signInTask.Exception != null) {
			Debug.LogWarning($"Failed to register task with{signInTask.Exception}");
		} else {
			Firebase.Auth.FirebaseUser newUser = signInTask.Result;
			Debug.LogFormat("Firebase user created successfully: {0} ({1})", newUser.DisplayName, newUser.UserId);
		}
	}

	// Anonymous sign-in
	IEnumerator signInAnonymously() {
		FirebaseAuth authentication = FirebaseAuth.DefaultInstance;

		var signInTask = authentication.SignInAnonymouslyAsync();
		yield return new WaitUntil(() => signInTask.IsCompleted);

		if (signInTask.Exception != null) {
			Debug.LogWarning($"Failed to register task with{signInTask.Exception}");
		} else {
			user = signInTask.Result;
			Debug.LogFormat("Firebase user created successfully: {0} ({1})", user.DisplayName, user.UserId);
		}
	}

	// Register user
	IEnumerator registerUser(string email, string password) {
		FirebaseAuth authentication = FirebaseAuth.DefaultInstance;
		var registrationTask = authentication.CreateUserWithEmailAndPasswordAsync(email, password);
		yield return new WaitUntil(() => registrationTask.IsCompleted);

		if (registrationTask.Exception != null) {
			Debug.LogWarning($"Failed to register task with{registrationTask.Exception}");
		} else {
			user = registrationTask.Result;
			Debug.LogFormat("Firebase user created successfully: {0} ({1})", user.DisplayName, user.UserId);
			StartCoroutine(sendVerificationEmail());
		}
	}

	IEnumerator sendVerificationEmail() {
		if (user != null) {
			if (!user.IsEmailVerified) {
				var emailTask = user.SendEmailVerificationAsync();
				yield return new WaitUntil(() => emailTask.IsCompleted);

				if (emailTask.IsCanceled) {
					Debug.LogError("SendEmailVerificationAsync was canceled.");
				} else if (emailTask.IsFaulted) {
					Debug.LogError("SendEmailVerificationAsync encountered an error: " + emailTask.Exception);
				} else {
					Debug.Log("Email sent successfully.");
				}
			} else {
				Debug.Log("User is verified");
			}
		}
	}
}
