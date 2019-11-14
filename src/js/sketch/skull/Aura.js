import * as THREE from 'three';
import MathEx from 'js-util/MathEx';

import vs from './glsl/Aura.vs';
import fs from './glsl/Aura.fs';

export default class Aura extends THREE.Mesh {
  constructor() {
    // Define Geometry
    const geometry = new THREE.PlaneBufferGeometry(20, 20);

    // Define Material
    const material = new THREE.RawShaderMaterial({
      uniforms: {
        time: {
          type: 'f',
          value: 0
        },
        outlineTex: {
          type: 't',
          value: null
        },
        noiseTex: {
          type: 't',
          value: null
        },
      },
      vertexShader: vs,
      fragmentShader: fs,
      transparent: true,
    });

    // Create Object3D
    super(geometry, material);
    this.name = 'Aura';
    this.isActive = false;
  }
  start(outlineTex, noiseTex) {
    this.isActive = true;
    this.material.uniforms.outlineTex.value = outlineTex;
    this.material.uniforms.noiseTex.value = noiseTex;
  }
  update(time, camera) {
    if (this.isActive === false) return;
    this.rotation.copy(camera.rotation);
    this.material.uniforms.time.value += time;
  }
}
