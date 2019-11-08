
import Felgo 3.0
import QtQuick 2.0

Item {
  id: item

  property alias source: effect.src

  ShaderEffect {
    id: effect

    anchors.fill: parent

    property Item src

    vertexShader: "
                  uniform highp mat4 qt_Matrix;
                  attribute highp vec4 qt_Vertex;
                  attribute highp vec2 qt_MultiTexCoord0;
                  varying highp vec2 coord;
                  void main() {
                      coord = qt_MultiTexCoord0;
                      gl_Position = qt_Matrix * qt_Vertex;
                  }"

    fragmentShader: "
                  varying highp vec2 coord;
                  uniform sampler2D src;
                  uniform lowp float qt_Opacity;
                  void main() {
                      lowp vec4 tex = texture2D(src, coord);
                      gl_FragColor.rgb = vec3(1) - tex.rgb;
                      gl_FragColor.a = tex.a;
                  }"

    Component.onCompleted: console.log("status", status)
    onStatusChanged: console.log("status", status)
  }
}
