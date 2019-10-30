import VPlay 2.0
import QtQuick 2.0

// import Custom lib defined in C++:
//import MyTypes 1.0


import "scenes"

GameWindow {
  id: gameWindow
  activeScene: menuScene
  screenWidth: 960
  screenHeight: 640

  EntityManager {
    id: entityManager
    entityContainer: gameScene
  }

  // instantiate custom type:
  //TestItem { }

  MenuScene { id: menuScene }
  GameScene { id: gameScene }
}

