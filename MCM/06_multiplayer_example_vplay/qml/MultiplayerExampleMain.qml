import VPlay 2.0
import QtQuick 2.0
import Qt.labs.settings 1.0

import "scenes"

GameWindow {
  id: gameWindow

  activeScene: menuScene

  screenWidth: 960
  screenHeight: 640

  title: "Multiplayer example - " +
         gameNetwork.user.name +
         " (" + gameNetwork.user.deviceId + ")"

  readonly property bool gameRunning: multiplayer.isInState(!multiplayer.stateEnum.game)

  Settings {
    id: gameSettings

    property int counterAppInstances: 0
  }

  EntityManager {
    id: entityManager
  }

  VPlayGameNetwork {
    id: gameNetwork

    multiplayerItem: multiplayer

    //create these at http://gamenetwork.v-play.net/
    gameId: 328
    secret: "mySecret"

    //generate unique ID for each app instance (to test with multiple windows on desktop)
    user {
      deviceId: system.UDID + "_" + (gameSettings.counterAppInstances % 4)
    }

    //clear data because deviceId might have changed
    clearAllUserDataAtStartup: true

    //increase the counter when starting the app to have unique IDs for each instance
    Component.onCompleted: gameSettings.counterAppInstances++
  }

  VPlayMultiplayer {
    id: multiplayer

    gameNetworkItem: gameNetwork
    multiplayerView: matchmakingScene.multiplayerView

    playerCount: 2

    onGameStarted: activeScene = gameScene
    onGameEnded: activeScene = menuScene
    onGameLeft: activeScene = menuScene
  }

  MenuScene { id: menuScene }

  MatchmakingScene { id: matchmakingScene }

  GameScene { id: gameScene }
}
