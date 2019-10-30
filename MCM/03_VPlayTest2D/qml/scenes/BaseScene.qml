import VPlay 2.0
import QtQuick 2.0

Scene {
  id: scene
  opacity: gameWindow.activeScene === scene ? 1 : 0

  Behavior on opacity {
    PropertyAnimation {
      duration: 500
    }
  }

  //"logical size" - scaled up to fit screen
  width: 480
  height: 320

  function show() {
    gameWindow.activeScene = gameScene;
  }
}

