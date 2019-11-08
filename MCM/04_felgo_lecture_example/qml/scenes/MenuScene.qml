import Felgo 3.0
import QtQuick 2.0

import "../items"

BaseScene {

  MyButton {
    id: btn
    x: 10
    y: 10
    height: 30
    width: 130
    text: "The Button"
    onClicked: gameWindow.activeScene = gameScene
  }
}
