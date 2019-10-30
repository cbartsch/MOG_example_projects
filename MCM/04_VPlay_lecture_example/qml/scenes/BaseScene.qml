import VPlay 2.0
import QtQuick 2.0

Scene {
  id: baseScene

  // the "logical size" - the scene content is auto-scaled to match the GameWindow size
  width: 480
  height: 320

  opacity: gameWindow.activeScene === baseScene ? 1 : 0

  Behavior on opacity {
    PropertyAnimation {
      duration: 500
    }
  }
}
