import VPlay 2.0
import QtQuick 2.0

import "../items"

BaseScene {
  id: gameScene

  Component {
    id: entityComponent

    TestEntity { }
  }

  Column {
    anchors.centerIn: parent
    spacing: 10

    MyButton {
      text: "Create TestEntity"
      onClicked: {
        entityManager.createEntityFromComponentWithProperties(
              entityComponent, {
                x: Math.random() * gameScene.width,
                y: Math.random() * gameScene.height
              })
      }
    }

    MyButton {
      text: "Change all"
      onClicked: {
        var entities = entityManager.getEntityArrayByType("testEntity")
        for(var i = 0; i < entities.length; i++) {
          entities[i].color = "blue";
        }
      }
    }

    MyButton {
      text: "Remove all"
      onClicked: entityManager.removeEntitiesByFilter(["testEntity"])
    }
  }
}

