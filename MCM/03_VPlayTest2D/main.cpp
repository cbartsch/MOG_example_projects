#include <QApplication>
#include <VPApplication>
#include <QQmlApplicationEngine>
#include <QDebug>
#include <QtQml>

#include "testitem.h"

int main(int argc, char *argv[])
{
  QApplication app(argc, argv);
  VPApplication vplay;
  QQmlApplicationEngine engine;

  qmlRegisterType<TestItem>("MyTypes", 1, 0, "TestItem");

  vplay.initialize(&engine);
  vplay.setMainQmlFileName(QStringLiteral("qml/Main.qml"));
  engine.load(QUrl(vplay.mainQmlFileName()));

  return app.exec();
}

