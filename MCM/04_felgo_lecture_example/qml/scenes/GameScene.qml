import Felgo 3.0
import QtQuick 2.0
import QtGraphicalEffects 1.0

import "../items"

BaseScene {
  id: gameScene

  Image {
    anchors.centerIn: parent
    source: "../../assets/test.jpg"
    width: 192
    height: 120
    fillMode: Image.PreserveAspectFit

    GaussianBlur {
      anchors.fill: parent
      source: parent
    }
  }
}
