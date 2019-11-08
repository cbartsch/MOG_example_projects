import Felgo 3.0
import QtQuick 2.0

Item {
  id: analytics

  // wait with start event until all SDKs are initialized
  readonly property bool initialized: ga.loaded && ampl.loaded
  onInitializedChanged: logScreen("MainScreen")

  GoogleAnalytics {
    id: ga
    propertyId: "UA-131763900-1"

    property bool loaded: false
    onPluginLoaded: loaded = true
  }

  Amplitude {
    id: ampl
    apiKey: "b81b75707a826fa3383af0326362443a"

    property bool loaded: false
    onPluginLoaded: loaded = true
  }

  // log app event with all analytics SDKs
  function logEvent(eventName) {
    console.log("Log event:", eventName)

    ga.logEvent("main", eventName)

    ampl.logEvent(eventName)
  }

  // log app screen with all analytics SDKs
  function logScreen(screenName) {
    console.log("Log screen:", screenName)

    ga.logScreen(screenName)

    // amplitude doesn't have screens, use an event instead
    ampl.logEvent("ScreenVisited", {
                    screenName: screenName
                  })
  }
}
