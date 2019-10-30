import VPlay 2.0
import QtQuick 2.0

EntityBase {
  id: myEntity

  entityType: "MyEntity"

  width: 50
  height: 50

  property color color: "green"

  Rectangle {
    anchors.fill: parent
    color: parent.color
  }
}
