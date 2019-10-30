using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Monetization;
using UnityEngine.UI;
using ShowResult = UnityEngine.Monetization.ShowResult;

public class AdManager : MonoBehaviour {
    public string gameId;
    public bool testMode;
    public string placementId, bannerId;

    public static bool isRewarded = false;
    
    // Start is called before the first frame update
    void Start() {
        Monetization.Initialize(gameId, testMode);
        Advertisement.Initialize(gameId, testMode);

        ShowBanner();
    }

    // Update is called once per frame
    void Update() {
    }

    public void ShowAd() {
        StartCoroutine(ShowAdWhenReady());
    }

    private void ShowBanner() {
        StartCoroutine(ShowBannerWhenReady());
    }

    private IEnumerator ShowAdWhenReady() {
        while (!Monetization.IsReady(placementId)) {
            yield return new WaitForSeconds(0.5f);
        }

        if (Monetization.GetPlacementContent(placementId) is ShowAdPlacementContent ad) {
            ad.Show(AdFinished);
        }
        else {
            Debug.Log("Placement " + placementId + " is null.");
        }
    }

    IEnumerator ShowBannerWhenReady() {
        while (!Advertisement.IsReady(bannerId)) {
            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("Showing ad banner.");
        Advertisement.Banner.Show(bannerId);
    }

    // ReSharper disable once MemberCanBeMadeStatic.Local
    private void AdFinished(ShowResult finishState) {
        Debug.Log("finished ad:" + finishState);

        if (finishState == ShowResult.Finished) {
            // user watched rewarded video ad
            isRewarded = true;
        }
    }
}