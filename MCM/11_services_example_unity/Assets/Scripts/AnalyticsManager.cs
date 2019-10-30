using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsManager : MonoBehaviour {
    
    public GoogleAnalyticsV4 googleAnalytics;

    void Start() {
        LogScreen("MainScreen");
    }

    private void LogScreen(string name) {
        var result = Analytics.CustomEvent("Screen", new Dictionary<string, object> {{"screenName", name}});

        googleAnalytics.LogScreen(name);
        Debug.Log("On screen: " + name + ", unity result = " + result);
    }

    public void SendCustomEvent(string name) {
        var result = Analytics.CustomEvent(name);

        googleAnalytics.LogEvent(new EventHitBuilder()
            .SetEventCategory("main")
            .SetEventAction(name));

        Debug.Log("Sent custom analytics event " + name + ", unity result = " + result);
    }
}