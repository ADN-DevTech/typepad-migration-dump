---
layout: "post"
title: "WebGL VR Effects with three.js"
date: "2015-02-13 03:36:40"
author: "Philippe Leefsma"
categories:
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "WebGL"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/02/webgl-vr-effects-with-threejs.html "
typepad_basename: "webgl-vr-effects-with-threejs"
typepad_status: "Publish"
---

<p>By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p>This week post features some cool <a title="" href="http://threejs.org/" target="_self">three.js</a>&nbsp;effects that you can easily use to do VR. Two VR effects are now shipped out of the box with that great WebGL library:</p>
<p style="padding-left: 30px;">- An Occulust Rift effect</p>
<p style="padding-left: 30px;">- A Stereo Effect</p>
<p>They are pretty easy to apply to any standard three.js WebGL scene, all you need to do is passing your renderer to the effect and it will take care of splitting and distorting the view for you...</p>
<p>In order to properly test the effects, you need to download the attached sample and test it on your side, however if you use Chrome, you may hit some security restrictions as you open the page locally. This works fine in Firefox, which is more permissive regarding local html files. You would also need an <a title="Occulus Rift headset" href="https://www.oculus.com/dk2/" target="_self">Occulus headset</a>&nbsp;if you want to see the VR effect, but that should give you an idea...</p>
<p>Here is how it looks like when running fullscreen - I had to disable the fullscreen feature for the demo embedded at the bootom of this post, as fullscreen mode didn't play well when run from that blog:</p>
<p><a class="asset-img-link" style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07eff260970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb07eff260970d image-full img-responsive" title="Screen Shot 2015-02-13 at 1.02.11 PM" src="/assets/image_28616e.jpg" alt="Screen Shot 2015-02-13 at 1.02.11 PM" border="0" /></a></p>
<p>Here is the demo and the controls for it:</p>
<p style="padding-left: 30px;">- Rotating/Zooming the object using mouse</p>
<p style="padding-left: 30px;">- "r" Key to switch to "Occulus Rift" effect</p>
<p style="padding-left: 30px;">- "s" Key to switch to "Stereo" effect</p>
<p style="padding-left: 30px;">- "n" Key to switch back to normal mode&nbsp;</p>
<p style="padding-left: 30px;">- For mobile: double tap to cycle through the view mode: normal &gt; rift &gt; stereo</p>
<p>
<div id='adskImgDivId'>
<img class="asset  asset-image at-xid-6a0167607c2431970b01bb07eff023970d image-full img-responsive" title="Adsk.1500x1500" src="/assets/image_9a310d.jpg" alt="Adsk.1500x1500" border="0" /></p>
<pre>&nbsp;</pre>
</div>

		</style>

	<div id="webGLDiv">

		<canvas id="renderer">

		</canvas>
	</div>

		<script src="http://code.jquery.com/jquery-2.1.3.min.js"></script>
		<script src="http://threejs.org/build/three.min.js"></script>
		<script src="http://learningthreejs.com/data/THREEx/THREEx.FullScreen.js"></script>
		<script src="https://rawgit.com/mrdoob/three.js/master/examples/js/controls/TrackballControls.js"></script>
		<script src="https://rawgit.com/mrdoob/three.js/master/examples/js/effects/OculusRiftEffect.js"></script>
		<script src="https://rawgit.com/mrdoob/three.js/master/examples/js/effects/StereoEffect.js"></script>
		<script src="https://hammerjs.github.io/dist/hammer.js"></script>
		<script src="https://rawgit.com/hammerjs/touchemulator/master/touch-emulator.js"></script>

		<script>



			var Autodesk = {} || Autodesk;
			Autodesk.ADN = {} ||  Autodesk.ADN;

			/////////////////////////////////////////////////////////////////////////
			// A Three.js demo that illustrates use of
			// OculusRiftEffect & StereoEffect
			/////////////////////////////////////////////////////////////////////////
			Autodesk.ADN.EffectsDemo = function (canvasId) {

				var _self = this;

				var _canvasId = canvasId;

				var _camera, _scene, _controls, _mesh, _renderer, _glRenderer;

				/////////////////////////////////////////////////////////////////////
				// Resize handler
				//
				/////////////////////////////////////////////////////////////////////
				_self.resizeCanvas = function () {

					function getClientSize() {

						var w = window,
							d = document,
							e = d.documentElement,
							g = d.getElementsByTagName('body')[0],
							sx = w.innerWidth || e.clientWidth || g.clientWidth,
							sy = w.innerHeight || e.clientHeight || g.clientHeight;

						return {x: sx, y: sy};
					}

					//var size = getClientSize();

                                        var size = {x: 490, y: 490};

					_camera.aspect = size.x / size.y;
					_camera.updateProjectionMatrix();

					_renderer.setSize(size.x, size.y);

					var canvas = document.getElementById(_canvasId);

					_controls = new THREE.TrackballControls(
							_camera,
							canvas);

					_controls.rotateSpeed = 1.0;
					_controls.minDistance = 200;
					_controls.maxDistance = 6000;

					_controls.addEventListener('change', render);
				}

				/////////////////////////////////////////////////////////////////////
				//
				//
				/////////////////////////////////////////////////////////////////////
				function initializeScene() {

					var canvas = document.getElementById(
							_canvasId);

					_camera = new THREE.PerspectiveCamera(
							70, 1, 1, 10000);

					_camera.position.z = 400;

					_scene = new THREE.Scene();

					var geometry = new THREE.BoxGeometry(
							150, 150, 150);

					var texture = THREE.ImageUtils.loadTexture(
							'http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb07eff023970d-800wi');

					var material = new THREE.MeshPhongMaterial( {
						ambient: 0x030303,
						color: 0xdddddd,
						specular: 0x009900,
						shininess: 30,
						shading: THREE.FlatShading,
						map: texture
					});

					var l1 = new THREE.DirectionalLight(0xffffff);
					var l2 = new THREE.DirectionalLight(0xffffff);
					var l3 = new THREE.DirectionalLight(0xffffff);
					var l4 = new THREE.DirectionalLight(0xffffff);

					l1.position.set(5, 0, 0).normalize();
					l2.position.set(-5, 0, 0).normalize();
					l3.position.set(0, 10, 0).normalize();
					l4.position.set(10, 0, 10).normalize();

					_scene.add(l1);
					_scene.add(l2);
					_scene.add(l3);
					_scene.add(l4);

					_mesh = new THREE.Mesh(geometry, material);

					_scene.add(_mesh);

					_renderer = _glRenderer = new THREE.WebGLRenderer({
						canvas: canvas
					});

					_renderer.setPixelRatio(window.devicePixelRatio);

					_renderer.setClearColor(0x1771C0, 1);

					_self.resizeCanvas();
				}

				/////////////////////////////////////////////////////////////////////
				//
				//
				/////////////////////////////////////////////////////////////////////
				function update() {

					requestAnimationFrame(update);

					_mesh.rotation.x += 0.01;
					_mesh.rotation.y += 0.01;

					_controls.update();

					render();
				}

				/////////////////////////////////////////////////////////////////////
				//
				//
				/////////////////////////////////////////////////////////////////////
				function render() {

					_renderer.render(_scene, _camera);
				}

				/////////////////////////////////////////////////////////////////////
				//
				//
				/////////////////////////////////////////////////////////////////////
				_self.setGlRenderer = function () {

					initializeScene();

					_self.resizeCanvas();
				}

				/////////////////////////////////////////////////////////////////////
				//
				//
				/////////////////////////////////////////////////////////////////////
				_self.setOcculusRenderer = function () {

					_renderer = new THREE.OculusRiftEffect(
							_glRenderer,
							{worldScale: 100});

					_self.resizeCanvas();
				}

				/////////////////////////////////////////////////////////////////////
				//
				//
				/////////////////////////////////////////////////////////////////////
				_self.setStereoRenderer = function () {

					_renderer = new THREE.StereoEffect(_glRenderer);

					//_renderer.eyeSeparation = 5;

					_self.resizeCanvas();
				}

				/////////////////////////////////////////////////////////////////////
				//
				//
				/////////////////////////////////////////////////////////////////////

				initializeScene();
				update();
			}

			/////////////////////////////////////////////////////////////////////////
			// On document Ready
			//
			/////////////////////////////////////////////////////////////////////////
			$( document ).ready(function() {

                        $('#adskImgDivId').remove();

				var demo = new Autodesk.ADN.EffectsDemo('renderer');

				window.addEventListener('resize', function() {

					demo.resizeCanvas();

				} , false);

				function setGlMode() {

					THREEx.FullScreen.cancel();

					demo.setGlRenderer();
				}

				function setOcculusMode() {

					if (!THREEx.FullScreen.activated()) {

						//THREEx.FullScreen.request();
					}

					demo.setOcculusRenderer();
				}

				function setStereoMode() {

					if (!THREEx.FullScreen.activated()) {

						//THREEx.FullScreen.request();
					}

					demo.setStereoRenderer();
				}

				var modeIdx = 0;

				var modes = [
					setGlMode,
					setOcculusMode,
					setStereoMode
				];

				$(document).keypress(function (event) {

					switch (event.which) {

						case 0: //ESC key

							demo.setGlRenderer();
							break;

						case 102: //f key

							if (THREEx.FullScreen.activated()) {

								THREEx.FullScreen.cancel();
								demo.setGlRenderer();

							} else {

								//THREEx.FullScreen.request();
							}

							break;

						case 110: //n key
							setGlMode();
							break;

						case 114: //r key

							setOcculusMode();
							break;

						case 115: //s key

							setStereoMode();
							break;
					}
				});

				// Handle mobile touch events using Hammer lib
				
				var mc = new Hammer.Manager(
					document.getElementById("webGLDiv"));

				mc.add(new Hammer.Tap({
					event: 'doubletap',
					taps: 2
				}));

				mc.add(new Hammer.Tap({
					event: 'singletap'
				}));

				mc.get('doubletap').recognizeWith('singletap');
				mc.get('singletap').requireFailure('doubletap');

				mc.on("doubletap", function (ev) {

					modeIdx = (++modeIdx) % 3;

					modes[modeIdx]();
				});
			});

		</script>

<br>

<script src="https://gist.github.com/leefsmp/1883c8e608847a883521.js"></script>


<span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c74c40f2970b img-responsive"><a href="http://adndevblog.typepad.com/files/vr-effects.zip">Download Vr effects</a></span>
