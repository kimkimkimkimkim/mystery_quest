using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
 
public class GoogleAds : MonoBehaviour {

	private bool testDebug = true;
 
	// Use this for initialization
	void Start () {
        // アプリID
        string appId;
		if(testDebug){
			appId = "ca-app-pub-3940256099942544~3347511713";
		}else{
			appId = "ca-app-pub-6653853739750121~8204093814";
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
			adUnitId = "ca-app-pub-6653853739750121/1063970396";
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