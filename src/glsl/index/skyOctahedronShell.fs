precision highp float;

uniform float time;

varying vec3 vPosition;
varying float vOpacity;

const float duration = 2.0;
const float delay = 3.0;

#pragma glslify: convertHsvToRgb = require(glsl-util/convertHsvToRgb);
#pragma glslify: cnoise3 = require(glsl-noise/classic/3d)

void main() {
  float now = clamp((time - delay) / duration, 0.0, 1.0);
  float noise1 = cnoise3(vec3((vPosition * vec3(0.4, 2.0, -0.6) * 2.0 + time))) * 8.0 - (1.0 + (1.0 - now) * 7.0);
  float noise2 = cnoise3(vec3((vPosition * 42.0 + time)));
  float noise3 = cnoise3(vec3((vPosition * 7.0 + time))) * 2.0;
  float color = smoothstep(-0.2, 1.0, (noise1 + noise2 + noise3) * now);
  vec3 v = normalize(vPosition);
  vec3 rgb = (1.0 - now) * vec3(1.0) + convertHsvToRgb(vec3(0.5 + (v.x + v.y + v.x) / 40.0 + time * 0.1, 0.4, 1.0));
  if (color < 0.4) discard;
  gl_FragColor = vec4(rgb * vec3(1.0 - color + 0.8), 0.4 + vOpacity * 0.5);
}