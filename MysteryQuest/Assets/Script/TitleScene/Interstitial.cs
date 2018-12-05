using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class Interstitial : MonoBehaviour {

	private InterstitialAd interstitial;
	private bool isTest = false;
	private string adUnitId;

	private void Start(){
		RequestInterstitial();
		GameOver();
	}

	private void RequestInterstitial()
	{
		if(isTest){
			adUnitId = "ca-app-pub-3940256099942544/6300978111";
		}else{
			#if UNITY_ANDROID
				adUnitId = "ca-app-pub-3940256099942544/1033173712";
			#elif UNITY_IPHONE
				adUnitId = "ca-app-pub-6653853739750121/9648288992";
			#else
				adUnitId = "unexpected_platform";
			#endif
		}

		// Initialize an InterstitialAd.
		this.interstitial = new InterstitialAd(adUnitId);

		
		// Called when an ad request has successfully loaded.
		this.interstitial.OnAdLoaded += HandleOnAdLoaded;
		// Called when an ad request failed to load.
		this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
		// Called when an ad is shown.
		this.interstitial.OnAdOpening += HandleOnAdOpened;
		// Called when the ad is closed.
		this.interstitial.OnAdClosed += HandleOnAdClosed;
		// Called when the ad click caused the user to leave the application.
		this.interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;
		

		// Create an empty ad request.
		AdRequest request = new AdRequest.Builder().Build();
		// Load the interstitial with the request.
		this.interstitial.LoadAd(request);
	}

	private void GameOver()
	{
		Debug.Log("--------------------------------------");
		Debug.Log("IsLoaded : " + this.interstitial.IsLoaded());
		Debug.Log("addUnitId : " + adUnitId);
		Debug.Log("--------------------------------------");
		if (this.interstitial.IsLoaded()) {
			this.interstitial.Show();
		}
	}

	public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }


}
