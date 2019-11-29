import Felgo 3.0
import QtQuick 2.0

// import Custom lib defined in C++:
import MyTypes 1.0

import "scenes"

GameWindow {
  id: gameWindow
  activeScene: menuScene
  screenWidth: 960
  screenHeight: 640

  property string testString: "initial value"

  EntityManager {
    id: entityManager
    entityContainer: gameScene
  }

  // instantiate custom type:
  TestItem {
    id: testItem
  }

  MenuScene {
    id: menuScene

    Text {
      anchors.centerIn: parent
      text: testString

      color: "white"
    }
  }
  GameScene { id: gameScene }
}

