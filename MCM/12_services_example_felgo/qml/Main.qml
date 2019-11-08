import Felgo 3.0
import QtQuick 2.0

import "logic"
import "scenes"

GameWindow {
  id: gameWindow

  //licenseKey: "<generate one from https://v-play.net/licenseKey>"

  activeScene: mainScene

  screenWidth: 640
  screenHeight: 960

  property bool rewarded: false
  onRewardedChanged: console.log("rewarded =", rewarded)

  Analytics {
    id: analytics
  }

  IapManager {
    id: iap
  }

  FelgoGameNetwork {
    id: gameNetwork
    // register your game at: https://gamenetwork.v-play.net
    gameId: 594
    secret: "mcm"
    gameNetworkView: highscoreScene.gnView
  }

  AdMobInterstitial {
    id: interstitial

    property bool loaded: false
    onInterstitialReceived: loaded = true
    onPluginLoaded: loadInterstitial()

    adUnitId: "ca-app-pub-3940256099942544/1033173712" // interstitial test ad by AdMob
    testDeviceIds: [ "53A1B79FF3CC323833E1CB49D8BC7BC2" ] // add your test device ID here

    onInterstitialOpened: analytics.logEvent("InterstitialOpened")
  }

  AdMobRewardedVideo {
    id: rewarded

    property bool loaded: false
    onRewardedVideoReceived: loaded = true
    onPluginLoaded: loadRewardedVideo()

    adUnitId: "ca-app-pub-3940256099942544/5224354917" // rewarded video test ad by AdMob
    testDeviceIds: [ "53A1B79FF3CC323833E1CB49D8BC7BC2" ] // add your test device ID here

    onRewardedVideoOpened: analytics.logEvent("RewardedVideoOpened")
    onRewardedVideoRewarded: gameWindow.rewarded = true
  }

  NotificationManager {
    id: notificationManager

    onNotificationFired: {
      console.log("Notification fired:", notificationId)
    }
  }

  MainScene {
    id: mainScene
    visible: gameWindow.activeScene === mainScene

    onVisibleChanged: if(visible) analytics.logScreen("MainScreen")

    onShowHighscores: gameWindow.activeScene = highscoreScene
    onShowIap: gameWindow.activeScene = iapScene
  }

  HighscoreScene {
    id: highscoreScene
    visible: gameWindow.activeScene === highscoreScene

    onVisibleChanged: if(visible) {
                        analytics.logScreen("HighscoresScreen")
                        gameNetwork.showLeaderboard()
                      }

    onBackClicked: gameWindow.activeScene = mainScene
  }

  IapScene {
    id: iapScene
    visible: gameWindow.activeScene === iapScene

    onVisibleChanged: if(visible) analytics.logScreen("IapScene")

    onBackClicked: gameWindow.activeScene = mainScene
  }

  AdMobBanner {
    id: banner

    visible: !iap.hasRemovedAds

    onAdReceived: console.log("RECEIVED AD")
    onAdFailedToReceive: console.log("FAILED AD")

    adUnitId: "ca-app-pub-3940256099942544/6300978111" // banner test ad by AdMob
    testDeviceIds: [ "53A1B79FF3CC323833E1CB49D8BC7BC2" ] // add your test device ID here
    banner: AdMobBanner.Smart

    anchors.bottom: parent.bottom
    anchors.horizontalCenter: parent.horizontalCenter
  }
}
