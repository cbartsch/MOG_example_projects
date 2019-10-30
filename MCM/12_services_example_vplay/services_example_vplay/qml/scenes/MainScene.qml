import VPlay 2.0
import VPlayPlugins 1.0
import QtQuick 2.0

Scene {
  id: scene

  width: 320
  height: 480

  signal showHighscores
  signal showIap

  Notification {
    id: notification
    notificationId: "test_not"
    message: "PLAY THIS GAME NOW"
    timeInterval: 1
  }

  Rectangle {
    anchors.fill: parent
    color: "white"
  }

  Column {
    x: 12
    y: 12
    spacing: 12

    ButtonVPlay {
      width: 160
      text: "Show highscores"
      onClicked: showHighscores()
    }

    ButtonVPlay {
      width: 160
      text: "Show IAP store"
      onClicked: showIap()
    }

    ButtonVPlay {
      width: 160
      text: "Increase score"
      onClicked: gameNetwork.reportRelativeScore(1)
    }

    ButtonVPlay {
      width: 160
      text: "Show notification"
      onClicked: notificationManager.schedule(notification)
    }

    ButtonVPlay {
      width: 160
      text: "Show interstitial"
      enabled: interstitial.loaded
      onClicked: interstitial.showInterstitialIfLoaded()
    }

    ButtonVPlay {
      width: 160
      text: "Show rewarded video"
      enabled: rewarded.loaded
      onClicked: rewarded.showRewardedVideoIfLoaded()
    }

    ButtonVPlay {
      width: 160
      text: "Log custom event"
      enabled: rewarded.loaded
      onClicked: analytics.logEvent("CustomEvent")
    }

    Text {
      text: "You are awesome!"
      color: "black"
      font.pixelSize: 24
      visible: gameWindow.rewarded
    }
  }
}
