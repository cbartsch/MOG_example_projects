import VPlay 2.0
import QtQuick 2.0

import "scenes"

GameWindow {
  id: gameWindow

  screenWidth: 960
  screenHeight: 640

  activeScene: menuScene

  EntityManager {
    id: entityManager
    entityContainer: gameScene
  }

  MenuScene { id: menuScene }
  GameScene { id: gameScene }

  Component.onCompleted: {
    console.log("game window completed, size: ", width, height)
  }
  Component.onDestruction: console.log("game window destroyed")
}
