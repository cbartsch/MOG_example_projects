import Felgo 3.0
import QtQuick 2.0

EntityBase {

  id: player

  entityType: "player"

  width: 20
  height: 40

  visible: playerObject && playerObject.connected

  property MultiplayerUser playerObject

  readonly property real moveSpeed: 200
  readonly property real rotateSpeed: 1000

  //clients are update this many times / second
  readonly property int updateRate: 10

  //generate nice random colors and show the local player in white
  property color color: isLocalPlayer
                        ? "white"
                        : Qt.hsla(Math.random(),
                                  Math.random() * 0.5 + 0.5,
                                  Math.random() * 0.5 + 0.25,
                                  1)

  readonly property bool isLocalPlayer: multiplayer.localPlayer === player.playerObject

  property real controlX: 0
  property real controlY: 0

  readonly property int messageControlChanged: 17
  readonly property int messageTransformChanged: 18

  onControlXChanged: sendControlMessage()
  onControlYChanged: sendControlMessage()

  Timer {
    running: multiplayer.amLeader
    repeat: true
    interval: 1000 / updateRate
    onTriggered: sendTransformMessage()
  }

  Rectangle {
    anchors.fill: parent
    color: player.color
    border.width: 1
    border.color: "black"
  }

  BoxCollider {
    id: collider

    //physics control
    force: Qt.point(0, controlY * moveSpeed)
    torque: controlX * rotateSpeed

    //physics properties
    density: 1 / (player.width * player.height)
    linearDamping: 0.6
    angularDamping: 0.6
  }

  function sendControlMessage() {
    if(!multiplayer.amLeader) {
      multiplayer.sendMessage(messageControlChanged, {
                                userId: playerObject.userId,
                                controlX: player.controlX,
                                controlY: player.controlY
                              })
    }
  }

  function sendTransformMessage() {
    if(multiplayer.amLeader) {
      multiplayer.sendMessage(messageTransformChanged, {
                                userId: playerObject.userId,
                                posX: player.x,
                                posY: player.y,
                                rotation: player.rotation,
                                vx: collider.linearVelocity.x,
                                vy: collider.linearVelocity.y,
                                va: collider.angularVelocity
                              })
    }
  }

  Connections {
    target: multiplayer

    onMessageReceived: {
      if(message.userId === playerObject.userId) {
        switch(code) {
        case messageControlChanged:
          controlX = message.controlX;
          controlY = message.controlY;
          break;
        case messageTransformChanged:
          x = message.posX;
          y = message.posY;
          rotation = message.rotation;
          collider.linearVelocity = Qt.point(message.vx, message.vy);
          collider.angularVelocity = message.va;
          break;
        }
      }
    }
  }
}
