#ifndef TESTITEM_H
#define TESTITEM_H

#include <QQuickItem>
#include <QtDebug>

class TestItem : public QQuickItem
{
  Q_OBJECT
public:
  TestItem();

  Q_INVOKABLE void testMethod() {
    qDebug() << "log from c++";
  }

signals:

public slots:
};

#endif // TESTITEM_H

