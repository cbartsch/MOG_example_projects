import Felgo 3.0
import QtQuick 2.0

Rectangle {
  id: myButton

  color: mouse.pressed ? "blue" : "red"

  Behavior on color {
    ColorAnimation {
      duration: 500
    }
  }

  width: 150
  height: 50

  property string text: "Hello World"

  signal clicked

  Text {
    id: topText
    text: myButton.text
    anchors.centerIn: parent
  }

  MouseArea {
    id: mouse
    width: parent.width
    height: parent.height

    onClicked: myButton.clicked()
  }
}
