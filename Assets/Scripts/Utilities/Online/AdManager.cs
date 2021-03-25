using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdManager : MonoBehaviour {
	InterstitialAd interstitialAd;
	string testAdUnitID = "ca-app-pub-3940256099942544/1033173712";

	void Start() {
		MobileAds.Initialize(initStatus => { Debug.Log("Mobile Ads initialized."); });
		requestInterstitialAd();
	}

	private void Update() {
		// if (Input.GetKeyDown(KeyCode.A) && interstitialAd.IsLoaded()) {
		// 	interstitialAd.Show();
		// }
	}

	void requestInterstitialAd() {
		interstitialAd = new InterstitialAd(testAdUnitID);

		// Called when an ad request has successfully loaded.
		interstitialAd.OnAdLoaded += handleOnAdLoaded;
		// Called when an ad request failed to load.
		interstitialAd.OnAdFailedToLoad += handleOnAdFailedToLoad;
		// Called when an ad is shown.
		interstitialAd.OnAdOpening += handleOnAdOpened;
		// Called when the ad is closed.
		interstitialAd.OnAdClosed += handleOnAdClosed;
		// Called when the ad click caused the user to leave the application.
		interstitialAd.OnAdLeavingApplication += handleOnAdLeavingApplication;

		AdRequest request = new AdRequest.Builder().Build();
		interstitialAd.LoadAd(request);
	}

	public void handleOnAdLoaded(object sender, EventArgs args) {
		MonoBehaviour.print("HandleAdLoaded event received");
	}

	public void handleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args) {
		MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
	}

	public void handleOnAdOpened(object sender, EventArgs args) {
		MonoBehaviour.print("HandleAdOpened event received");
	}

	public void handleOnAdClosed(object sender, EventArgs args) {
		MonoBehaviour.print("HandleAdClosed event received");
	}

	public void handleOnAdLeavingApplication(object sender, EventArgs args) {
		MonoBehaviour.print("HandleAdLeavingApplication event received");
	}
}
