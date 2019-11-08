import Felgo 3.0
import QtQuick 2.0
import QtGraphicalEffects 1.0

GameWindow {
  id: gameWindow

  activeScene: scene

  screenWidth: 960
  screenHeight: 640

  readonly property real imgSize: 180

  Scene {
    id: scene

    width: 960
    height: 640

    Grid {
      anchors.centerIn: parent

      columns: 4
      columnSpacing: 24
      rowSpacing: 24

      Image {
        id: testImg

        width: imgSize
        height: imgSize

        source: "../assets/mario.png"
        fillMode: Image.PreserveAspectFit
      }

      GaussianBlur {
        id: blurredImg

        source: testImg
        radius: imgSize / 10
        samples: 96
        deviation: 100

        width: imgSize
        height: imgSize
      }

      DirectionalBlur {
        source: testImg
        samples: 96
        angle: 45
        length: 20

        width: imgSize
        height: imgSize
      }

      RadialBlur {
        source: testImg
        samples: 96
        angle: 30

        width: imgSize
        height: imgSize
      }

      ZoomBlur {
        source: testImg
        samples: 96
        length: 20

        width: imgSize
        height: imgSize
      }

      ColorOverlay {
        source: testImg
        color: "#80ff0000"

        width: imgSize
        height: imgSize
      }

      Blend {
        source: blurredImg
        foregroundSource: testImg

        width: imgSize
        height: imgSize
      }

      Glow {
        source: testImg
        color: "black"
        samples: 16
        radius: 5
        spread: 2

        width: imgSize
        height: imgSize
      }

      DropShadow {
        source: testImg

        horizontalOffset: 20
        verticalOffset: 20

        width: imgSize
        height: imgSize
      }

      InnerShadow {
        source: testImg

        horizontalOffset: -5
        verticalOffset: -5
        radius: 16
        samples: 32

        width: imgSize
        height: imgSize
      }

      OpacityMask {
        source: testImg
        maskSource: Image {
          width: imgSize
          height: imgSize

          source: "../assets/mario.png"
          fillMode: Image.PreserveAspectFit
          mirror: true
        }

        width: imgSize
        height: imgSize
      }

      CustomShader {
        source: testImg

        width: imgSize
        height: imgSize
      }
    }
  }
}
