import Felgo 3.0
import QtQuick 2.0

EntityBase {
  id: testEntity
  entityType: "testEntity"

  property color color: Qt.rgba(Math.random(),
                                Math.random(),
                                Math.random(), 1)

  Rectangle {
    width: 50
    height: 50
    color: testEntity.color
  }
}


