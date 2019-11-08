import Felgo 3.0
import QtQuick 2.0

// EMPTY SCENE

Scene {
  id: highscoreScene

  signal backClicked

  property alias gnView: gnView

  GameNetworkView {
    id: gnView

    onBackClicked: highscoreScene.backClicked()
  }
}
