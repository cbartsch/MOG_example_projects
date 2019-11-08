import Felgo 3.0
import QtQuick 2.0

Scene {
  id: matchmakingScene

  property alias multiplayerView: multiplayerView

  width: 480
  height: 320
  visible: activeScene === matchmakingScene

  MultiplayerView {
    id: multiplayerView
    anchors.fill: parent
    onShowCalled: activeScene = matchmakingScene
    onBackClicked: activeScene = menuScene
  }
}
