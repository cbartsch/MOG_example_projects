import VPlay 2.0
import QtQuick 2.0

import "../items"

BaseScene {
  id: menuScene

  MyButton {
    id: btn
    x: 10
    y: 10
    text: "Show game scene"
    onClicked: gameScene.show()
  }
}
