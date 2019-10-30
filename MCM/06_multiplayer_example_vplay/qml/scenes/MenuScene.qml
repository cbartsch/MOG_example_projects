import VPlay 2.0
import QtQuick 2.0

Scene {
  id: menuScene

  width: 480
  height: 320
  visible: activeScene === menuScene

  Column {
    anchors.centerIn: parent
    spacing: 6

    ButtonVPlay {
      anchors.horizontalCenter: parent.horizontalCenter
      text: "Show matchmaking"
      onClicked: {
        multiplayer.showMatchmaking()
      }
    }
  }
}
