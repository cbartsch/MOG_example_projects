import Felgo 3.0
import QtQuick 2.0

Rectangle {
  id: myButton
  width: 200
  height: 40
  color: mouseArea.pressed ? "blue" : "red"

  property string text: "Hello world"

  onTextChanged: {
    console.log("Button text changed to:", myButton.text)
  }

  signal clicked

  Text {
    id: btnText
    text: myButton.text
    anchors.centerIn: parent
    font.pixelSize: 20
  }

  MouseArea {
    id: mouseArea
    width: parent.width
    height: parent.height

    onClicked: myButton.clicked()
  }
}

