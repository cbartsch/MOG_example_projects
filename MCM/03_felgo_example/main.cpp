#include <QApplication>
#include <FelgoApplication>
#include <QQmlApplicationEngine>
#include <QDebug>
#include <QtQml>

#include "testitem.h"

int main(int argc, char *argv[])
{
  QApplication app(argc, argv);
  FelgoApplication felgo;
  QQmlApplicationEngine engine;

  qmlRegisterType<TestItem>("MyTypes", 1, 0, "TestItem");

  felgo.initialize(&engine);
  felgo.setMainQmlFileName(QStringLiteral("qml/Main.qml"));
  engine.load(QUrl(felgo.mainQmlFileName()));

  engine.rootObjects().first()->setProperty("testString", QString("C++ string"));

  return app.exec();
}

