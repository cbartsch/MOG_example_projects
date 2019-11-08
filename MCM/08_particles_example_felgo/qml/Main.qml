import Felgo 3.0
import QtQuick 2.0
import QtQuick.Particles 2.0

GameWindow {
  id: gameWindow

  activeScene: scene

  screenWidth: 960
  screenHeight: 640

  Scene {
    id: scene

    width: 480
    height: 320

    MouseArea {
      id: mouse
      hoverEnabled: true
      anchors.fill: parent

      onClicked: emitter.burst(100, emitter.x, emitter.y)
    }

    ParticleSystem {
      id: particleSystem

      Emitter {
        id: emitter

        property real angle: 0

        x: mouse.mouseX
        y: mouse.mouseY

        emitRate: 40
        lifeSpan: 2000
        lifeSpanVariation: 500
        maximumEmitted: 1000

        size: 10

        velocity: AngleDirection {
          angle: emitter.angle
          angleVariation: 20
          magnitude: 100
          magnitudeVariation: 40
        }

        PropertyAnimation on angle {
          from: 0
          to: 360
          running: true
          loops: Animation.Infinite
          duration: 10000
        }
      }

      ItemParticle {
        fade: false

        delegate: Rectangle {
          property real size: 0

          color: "red"

          width: size
          height: size
          radius: size / 2

          PropertyAnimation on size {
            from: 0
            to: emitter.size
            running: true
            duration: emitter.lifeSpan / 2
            onRunningChanged: if(!running) size = 0
          }

          PropertyAnimation on color {
            from: "red"
            to: "blue"
            running: true
            duration: emitter.lifeSpan / 2
            onRunningChanged: if(!running) color = "red"
          }
        }
      }
    }

  }
}
