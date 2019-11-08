#include <QApplication>
#include <FelgoApplication>

#include <QQmlApplicationEngine>

#include <FelgoLiveClient>

int main(int argc, char *argv[])
{

  QApplication app(argc, argv);

  FelgoApplication felgo;

  // QQmlApplicationEngine is the preferred way to start qml projects since Qt 5.2
  // if you have older projects using Qt App wizards from previous QtCreator versions than 3.1, please change them to QQmlApplicationEngine
  QQmlApplicationEngine engine;
  felgo.initialize(&engine);

  // use this during development
  // for PUBLISHING, use the entry point below
  felgo.setMainQmlFileName(QStringLiteral("qml/Main.qml"));
  //  vplay.setMainQmlFileName(QStringLiteral("qrc:/qml/Main.qml"));
  engine.load(QUrl(felgo.mainQmlFileName()));

  //FelgoLiveClient liveClient(&engine);

  return app.exec();
}
