#include <QApplication>
#include <VPApplication>

#include <QQmlApplicationEngine>

#include <VPLiveClient>

int main(int argc, char *argv[])
{

  QApplication app(argc, argv);

  VPApplication vplay;

  // QQmlApplicationEngine is the preferred way to start qml projects since Qt 5.2
  // if you have older projects using Qt App wizards from previous QtCreator versions than 3.1, please change them to QQmlApplicationEngine
  QQmlApplicationEngine engine;
  vplay.initialize(&engine);

  // use this during development
  // for PUBLISHING, use the entry point below
  //vplay.setMainQmlFileName(QStringLiteral("qml/Main.qml"));
  //  vplay.setMainQmlFileName(QStringLiteral("qrc:/qml/Main.qml"));
  //engine.load(QUrl(vplay.mainQmlFileName()));

  VPlayLiveClient liveClient(&engine);

  return app.exec();
}
