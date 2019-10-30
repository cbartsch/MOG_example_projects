import VPlay 2.0
import QtQuick 2.0

import "../entities"

Scene {
  id: gameScene

  width: 480
  height: 320
  visible: activeScene === gameScene

  onVisibleChanged: if(visible) multiplayer.playersChanged()

  readonly property Player localPlayer: {
    for(var i = 0; i < multiplayer.playerCount; i++) {
      var player = multiplayer.players[i]
      if(player && player === multiplayer.localPlayer) {
        console.log("found localPlayer")
        return playersRepeater.itemAt(i)
      }
    }
    return null
  }

  ButtonVPlay {
    anchors.left: parent.left
    anchors.top: parent.top
    anchors.margins: 6
    text: "Exit game"
    onClicked: multiplayer.leaveGame()
  }

  PhysicsWorld {
    id: physicsWorld

    gravity: Qt.point(0, 0)

    //only the "leader" player runs the game logic
    //(and thus the physics simulation)
    //running: multiplayer.amLeader

    debugDrawVisible: false
  }

  Repeater {
    id: playersRepeater

    model: multiplayer.players

    Player {
      playerObject: modelData
      x: (gameScene.width - width) / 2
      y: (gameScene.height - height) / 2
    }
  }

  focus: true

  Keys.onPressed: {
    if(event.key === Qt.Key_Left)       localPlayer.controlX = -1
    else if(event.key === Qt.Key_Right) localPlayer.controlX = 1
    else if(event.key === Qt.Key_Up)    localPlayer.controlY = -1
    else if(event.key === Qt.Key_Down)  localPlayer.controlY = 1
  }

  Keys.onReleased: {
    if(event.key === Qt.Key_Left ||
        event.key === Qt.Key_Right)
      localPlayer.controlX = 0

    else if(event.key === Qt.Key_Up ||
            event.key === Qt.Key_Down)
      localPlayer.controlY = 0
  }
}
