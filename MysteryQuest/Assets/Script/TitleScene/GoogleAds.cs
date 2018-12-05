using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
 
public class GoogleAds : MonoBehaviour {

	private bool testDebug = false;
 
	// Use this for initialization
	void Start () {
        // アプリID
        string appId;
		if(testDebug){
			appId = "ca-app-pub-3940256099942544~3347511713";
		}else{
			#if UNITY_IPHONE
			appId = "ca-app-pub-6653853739750121/9648288992";
			#elif UNITY_ANDROID
			appId = "ca-app-pub-6653853739750121~1001967624";
			#else
			appId = "unexpected_platform";
			#endif
		}
        // Initialize the Google Mobile Ads SDK.
        MobileAds.Initialize(appId);
 
        RequestBanner();
	}

    private void RequestBanner()
    {
 
        // 広告ユニットID これはテスト用
        string adUnitId;
		if(testDebug){
			adUnitId = "ca-app-pub-3940256099942544/6300978111";
		}else{
			#if UNITY_IPHONE
			adUnitId = "ca-app-pub-6653853739750121/1063970396";
			#elif UNITY_ANDROID
			adUnitId= "ca-app-pub-6653853739750121/5871150927";
			#else
			adUnitId = "unexpected_platform";
			#endif
		}
 
        // Create a 320x50 banner at the top of the screen.
        BannerView bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
 
        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
 
        // Load the banner with the request.
        bannerView.LoadAd(request);
 
    }
 
	
	// Update is called once per frame
	void Update () {
		
	}
}