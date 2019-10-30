import VPlay 2.0
import QtQuick 2.0

// EMPTY SCENE

Scene {
  id: highscoreScene

  signal backClicked

  property alias gnView: gnView

  VPlayGameNetworkView {
    id: gnView

    onBackClicked: highscoreScene.backClicked()
  }
}
