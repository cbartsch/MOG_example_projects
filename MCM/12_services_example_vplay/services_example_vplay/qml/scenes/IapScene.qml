import VPlay 2.0
import QtQuick 2.0

import "../logic"

Scene {

  signal backClicked

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
      text: "Back to menu"
      onClicked: backClicked()
    }

    ButtonVPlay {
      width: 160
      text: !iap.hasRemovedAds ? "Remove ads" : "Ads are removed"
      onClicked: !iap.hasRemovedAds ? iap.buyNoAds() : iap.restoreAds()
    }

    Text {
      text: "Token balance: " + iap.tokenBalance
      color: "black"
      font.pixelSize: 24
    }

    ButtonVPlay {
      width: 160
      text: "Buy token"
      onClicked: iap.buyTokens()
    }

    ButtonVPlay {
      width: 160
      enabled: iap.tokenBalance > 0
      text: enabled ? "Spend token" : "You have no tokens"
      onClicked: iap.spendTokens(1)
    }
  }
}
