---
layout: "post"
title: "Fun with the Physics!"
date: "2014-12-30 03:32:43"
author: "Philippe Leefsma"
categories:
  - "Browser"
  - "Client"
  - "Cloud"
  - "CSS"
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "WebGL"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2014/12/fun-with-the-physics.html "
typepad_basename: "fun-with-the-physics"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>

<p>As we were getting closer to Christmas, I thought it could be useful to write a little demo in order to simulate distribution of Santa’s presents along a conveyor :) … More seriously, I was looking for a good JavaScript physics library so I could integrate it with the Autodesk Viewer.<br /><br />I stumbled across one post in particular:&nbsp;<a title="" href="http://buildnewgames.com/physics-engines-comparison/" target="_self">JavaScript Physics Engines Comparison by Chandler Prall</a>, and I have to say, reused some of the big lines along which the code was written to create the sample below. <br /><br />From the four libraries presented in that review, two were 2d-only, so as I was looking for a fully 3d physic simulation, those were ruled out straight from the start. I gave a quick try at&nbsp;<a title="" href="https://github.com/schteppe/cannon.js" target="_self">Cannon.js</a> to realize that performances and capabilities were not yet satisfying enough, and finally focused on&nbsp;<a title="" href="https://github.com/kripken/ammo.js/" target="_self">Ammo.js</a> which is an&nbsp;<a title="" href="https://github.com/kripken/emscripten" target="_self">emscripten</a> port of <a title="" href="http://bulletphysics.org/wordpress" target="_self">Bullet</a>, a well-established C++ Physics library used in many commercial games already.<br /><br />The sample is producing some pseudo-randomized&nbsp;<a title="" href="http://threejs.org" target="_self">three.js</a> default shapes dropped above a conveyor, gravity is the only force applied to the solids and the physics is doing the rest… &nbsp;<br />

<div id='ammoImgId'>
<b><u><a href="http://adndevblog.typepad.com/cloud_and_mobile/2014/12/fun-with-the-physics.html">Click here to see the live demo</a></u></b>
<br/>
<br/>
<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d0b51cdf970c-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01b8d0b51cdf970c image-full img-responsive" alt="Ammo" title="Ammo.js demo" src="/assets/image_f69e88.jpg" border="0" /></a>
</div>

<div id='canvasContainer'></div>

<script src="http://code.jquery.com/jquery-2.1.3.min.js"></script>
<script src="http://threejs.org/build/three.min.js"></script>
<script src="https://rawgit.com/kripken/ammo.js/master/builds/ammo.js"></script>
<script src="https://rawgit.com/darsain/fpsmeter/master/dist/fpsmeter.min.js"></script>
<script src="https://rawgit.com/mrdoob/three.js/master/examples/js/controls/TrackballControls.js"></script>

<script>

    ///////////////////////////////////////////////////////////////////////
    // Ammo.js sample, by Philippe Leefsma
    // December 2014
    //
    ///////////////////////////////////////////////////////////////////////
    var Autodesk = Autodesk || {};
    Autodesk.ADN = Autodesk.ADN || {};

    ///////////////////////////////////////////////////////////////////////
    // A stopwatch!
    //
    ///////////////////////////////////////////////////////////////////////
    Autodesk.ADN.Stopwatch = function() {

        var _startTime = new Date().getTime();

        this.start = function (){

            _startTime = new Date().getTime();
        };

        this.getElapsedMs = function(){

            var elapsedMs = new Date().getTime() - _startTime;

            _startTime = new Date().getTime();

            return elapsedMs;
        }
    }

    ///////////////////////////////////////////////////////////////////////
    // Simulation Manager
    //
    ///////////////////////////////////////////////////////////////////////
    Autodesk.ADN.AdnSimulationManager = function (canvasId) {

    var _world = null;

    var _scene = null;

    var _meter = null;

    var _camera = null;

    var _renderer = null;

    var _trackball = null;

    var _physicBodies = {};

    var _intervalId = null;

    var _animationId = null;

    var _stopWatch = new Autodesk.ADN.Stopwatch();

    ///////////////////////////////////////////////////////////
    // Random number between (min, max)
    //
    ///////////////////////////////////////////////////////////
    function _random(min, max) {

        return min +  (max - min) * Math.random();
    }

    function _randomInt(min, max) {

        return Math.floor(Math.random() * (max - min)) + min;
    }

    ///////////////////////////////////////////////////////////
    // Unique GUID
    //
    ///////////////////////////////////////////////////////////
    function _newGuid () {

        var d = new Date().getTime();

        var guid = 'xxxx-xxxx-xxxx-xxxx'.replace(
            /[xy]/g,
            function (c) {
                var r = (d + Math.random() * 16) % 16 | 0;
                d = Math.floor(d / 16);

                return (c == 'x' ? r : (r & 0x7 | 0x8)).
                     toString(16);
            });

        return guid;
    };

    ///////////////////////////////////////////////////////////
    // Initializes Physics
    //
    ///////////////////////////////////////////////////////////
    function _initializePhysics() {

        var collisionConfiguration =
            new Ammo.btDefaultCollisionConfiguration;

        _world = new Ammo.btDiscreteDynamicsWorld(
            new Ammo.btCollisionDispatcher(
                    collisionConfiguration),
            new Ammo.btDbvtBroadphase,
            new Ammo.btSequentialImpulseConstraintSolver,
            collisionConfiguration);

        _world.setGravity(new Ammo.btVector3(0, -9.8, 0));
    }

    ///////////////////////////////////////////////////////////
    // Creates collision shape from mesh
    //
    ///////////////////////////////////////////////////////////
    function _createCollisionShape(mesh) {

        var geometry = mesh.geometry;

        var hull = new Ammo.btConvexHullShape();

        geometry.vertices.forEach(function(vertex){

            hull.addPoint(new Ammo.btVector3(
                vertex.x,
                vertex.y,
                vertex.z));
        });

        return hull;
    }

    ///////////////////////////////////////////////////////////
    // Creates physic rigid body from mesh
    //
    ///////////////////////////////////////////////////////////
    function _addRigidBody(mesh, mass) {

        var localInertia = new Ammo.btVector3(0, 0, 0);

        var shape = _createCollisionShape(mesh);

        shape.calculateLocalInertia(mass, localInertia);

        var transform = new Ammo.btTransform;

        transform.setIdentity();

        transform.setOrigin(new Ammo.btVector3(
            mesh.position.x,
            mesh.position.y,
            mesh.position.z));

        transform.setRotation(new Ammo.btQuaternion(
            mesh.quaternion.x,
            mesh.quaternion.y,
            mesh.quaternion.z,
            mesh.quaternion.w
        ));

        var motionState =
            new Ammo.btDefaultMotionState(transform);

        var rbInfo = new Ammo.btRigidBodyConstructionInfo(
            mass,
            motionState,
            shape,
            localInertia);

        var body = new Ammo.btRigidBody(rbInfo);

        body.mesh = mesh;

        _world.addRigidBody(body);

        _physicBodies[_newGuid()] = body;
    }

    ///////////////////////////////////////////////////////////
    // Updates mesh transform from physic body
    //
    ///////////////////////////////////////////////////////////
    function _updateMeshTransform(body) {

        var mesh = body.mesh;

        var transform = body.getCenterOfMassTransform();

        var origin = transform.getOrigin();

        mesh.position.set(
            origin.x(),
            origin.y(),
            origin.z());

        var rotation = transform.getRotation();

        mesh.quaternion.set(
            rotation.x(),
            rotation.y(),
            rotation.z(),
            rotation.w());
    }

    ///////////////////////////////////////////////////////////
    // Creates ramp mesh
    //
    ///////////////////////////////////////////////////////////
    function _createRamp(position, rotations) {

        var material = new THREE.MeshLambertMaterial({
            color: 0xF74F4F
        })

        var mesh = new THREE.Mesh(
            new THREE.BoxGeometry(50, 2, 10),
            material);

        mesh.position.copy(position);

        rotations.forEach(function(rotation){

            var q = new THREE.Quaternion();

            q.setFromAxisAngle(
                rotation.axis,
                rotation.angle);

            mesh.quaternion.multiply(q);
        });

        return mesh;
    }

    ///////////////////////////////////////////////////////////
    // Initializes three.js scene
    //
    ///////////////////////////////////////////////////////////
    function _initializeScene(id) {

        var viewport = document.getElementById(id)

        _meter = new FPSMeter(
            viewport, {
            smoothing: 10,
            show: 'fps',
            toggleOn: 'click',
            decimals: 1,
            zIndex: 999,
            theme: 'transparent',
            heat: 1,
            graph: 1,
            history: 20});

        _renderer =
            new THREE.WebGLRenderer({canvas: viewport})

        _renderer.setSize(
            viewport.clientWidth,
            viewport.clientHeight);

        _scene = new THREE.Scene
        _camera = new THREE.PerspectiveCamera(
            35, 1, 1, 1000)

        _camera.position.set(-50, 90, -150);
        _camera.lookAt(_scene.position);

        _scene.add(_camera);

        _trackball = new THREE.TrackballControls(
            _camera, viewport);

        _trackball.noPan = false;
        _trackball.panSpeed = 0.5;
        _trackball.noZoom = false;
        _trackball.zoomSpeed = 2.0;
        _trackball.minDistance = 1;
        _trackball.maxDistance = 300;
        _trackball.rotateSpeed = 3.5;
        _trackball.staticMoving = true;
        _trackball.dynamicDampingFactor = 0.3;

        // [r:rotate, z:zoom, p:pan]
        _trackball.keys = [82, 90, 80];

        var ambientLight = new THREE.AmbientLight(0x555555);
        _scene.add(ambientLight);

        var directionalLight =
            new THREE.DirectionalLight(0xffffff);

        directionalLight.position.set(
            -.5, .5, -1.5 ).normalize();

        _scene.add(directionalLight);

        var material = new THREE.MeshLambertMaterial({
            color: 0xdd0000
        })

        var ramp11 = _createRamp(

            new THREE.Vector3(-20, 25, 0),
            [{
                axis: new THREE.Vector3(0, 0, 1),
                angle: - Math.PI / 10
            }, {
                axis: new THREE.Vector3(1, 0, 0),
                angle: - Math.PI / 10
            }]);

        var ramp12 = _createRamp(

            new THREE.Vector3(-20, 25, -10),
            [{
                axis: new THREE.Vector3(0, 0, 1),
                angle: - Math.PI / 10
            }, {
                axis: new THREE.Vector3(1, 0, 0),
                angle: Math.PI / 10
            }]);


        var ramp21 = _createRamp(

            new THREE.Vector3(25, 5, 0),
            [{
                axis: new THREE.Vector3(0, 0, 1),
                angle: Math.PI / 10
            }, {
                axis: new THREE.Vector3(1, 0, 0),
                angle: - Math.PI / 10
            }]);

        var ramp22 = _createRamp(

            new THREE.Vector3(25, 5, -10),
            [{
                axis: new THREE.Vector3(0, 0, 1),
                angle: Math.PI / 10
            }, {
                axis: new THREE.Vector3(1, 0, 0),
                angle: Math.PI / 10
            }]);


        var ramp31 = _createRamp(

            new THREE.Vector3(-20, -10, 0),
            [{
                axis: new THREE.Vector3(0, 0, 1),
                angle: -Math.PI / 10
            }, {
                axis: new THREE.Vector3(1, 0, 0),
                angle: - Math.PI / 10
            }]);

        var ramp32 = _createRamp(

            new THREE.Vector3(-20, -10, -10),
            [{
                axis: new THREE.Vector3(0, 0, 1),
                angle: -Math.PI / 10
            }, {
                axis: new THREE.Vector3(1, 0, 0),
                angle: Math.PI / 10
            }]);

        _scene.add(ramp11);
        _scene.add(ramp12);

        _scene.add(ramp21);
        _scene.add(ramp22);

        _scene.add(ramp31);
        _scene.add(ramp32);

        _addRigidBody(ramp11, 0);
        _addRigidBody(ramp12, 0);

        _addRigidBody(ramp21, 0);
        _addRigidBody(ramp22, 0);

        _addRigidBody(ramp31, 0);
        _addRigidBody(ramp32, 0);
    }

    ///////////////////////////////////////////////////////////
    // Creates random geometry
    //
    ///////////////////////////////////////////////////////////
    function _createRandomGeometry(size) {

        switch(_randomInt(1, 9)) {

            case 1:
                return THREE.BoxGeometry(
                    size, size, size);

            case 2:
                return THREE.SphereGeometry(
                    size, 32, 32);

            case 3:
                return new THREE.IcosahedronGeometry(
                    size, 0);

            case 4:
                return new THREE.OctahedronGeometry(
                    size, 0);

            case 5:
                return new THREE.TetrahedronGeometry(
                    size, 0);

            case 6:
                return new THREE.CylinderGeometry(
                    0, size, size, 20, 4);

            case 7:
                return new THREE.CylinderGeometry(
                    size, size, size, 20, 4);

            case 8:
                return new THREE.CylinderGeometry(
                    size * 0.5, size, size, 20, 4);

            case 9:
                return new THREE.CylinderGeometry(
                    size * 0.5, size * 0.5, size, 20, 4);

            default:
                return null;
        }
    }

    ///////////////////////////////////////////////////////////
    // Add new elements to the scene
    //
    ///////////////////////////////////////////////////////////
    function _newElements() {

        function newElement() {

            var size = _random(1, 4);

            var mass = size;

            var color = Math.floor(Math.random() * 16777215);

            var material = new THREE.MeshLambertMaterial({
                color: color
            });

            var mesh = new THREE.Mesh(
                    _createRandomGeometry(size),
                    material);

            mesh.position.x = _random(-40, -20);
            mesh.position.y = 50;

            _scene.add(mesh);

            _addRigidBody(mesh, mass);
        }

        // adds a bunch ...

        var nbElems = _random(1, 5);

        for(var i=0; i < nbElems; ++i) {

            newElement();
        }
    }

    ///////////////////////////////////////////////////////////
    // Update loop
    //
    ///////////////////////////////////////////////////////////
    function _update() {

        _animationId = requestAnimationFrame(_update);

        _world.stepSimulation(
            _stopWatch.getElapsedMs() * 0.002,
            10);

        for(var key in _physicBodies) {

            _updateMeshTransform(_physicBodies[key]);
        }

        _trackball.update();

        _renderer.render(_scene, _camera);

        _meter.tick();
    }

    ///////////////////////////////////////////////////////////
    // Starts simulation
    //
    ///////////////////////////////////////////////////////////
    this.start = function() {

        _stopWatch.getElapsedMs();

        _update();

        _intervalId = setInterval(
            _newElements,
            1500);
    }

    ///////////////////////////////////////////////////////////
    // Stops simulation
    //
    ///////////////////////////////////////////////////////////
    this.stop = function() {

        cancelAnimationFrame(_animationId);

        clearInterval(_intervalId);

        _animationId = null;

        _intervalId = null;
    }

    ///////////////////////////////////////////////////////////
    //
    //
    ///////////////////////////////////////////////////////////
    _initializePhysics();

    _initializeScene(canvasId);
}

    

    if(window.location.toString().indexOf('fun-with-the-physics.html') > 0) {

        $('#ammoImgId').remove();

        var html = '<canvas id="ammojs" width="500" height="500"></canvas>'

        $('#canvasContainer').append(html);

        var simulationManager = new Autodesk.ADN.AdnSimulationManager('ammojs');

        simulationManager.start();
    }
    
</script>

<p><b>Controls:</b></p>
<p>. P + Mouse: Pan</p>
<p>. R + Mouse: Rotate</p>
<p>. Z + Mouse: Zoom</p>

<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:9pt;"><span style="color:#800000;background-color:#f0f0f0;">  1 
  2 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">  3 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Ammo.js sample, by Philippe Leefsma
</span><span style="color:#800000;background-color:#f0f0f0;">  4 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// December 2014
</span><span style="color:#800000;background-color:#f0f0f0;">  5 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">  6 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">  7 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> Autodesk = Autodesk || {};
</span><span style="color:#800000;background-color:#f0f0f0;">  8 </span><span style="background-color:#ffffff;">Autodesk.ADN = Autodesk.ADN || {};
</span><span style="color:#800000;background-color:#f0f0f0;">  9 
 10 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 11 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// A stopwatch!
</span><span style="color:#800000;background-color:#f0f0f0;"> 12 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 13 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 14 </span><span style="background-color:#ffffff;">Autodesk.ADN.Stopwatch = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">() {
</span><span style="color:#800000;background-color:#f0f0f0;"> 15 
 16 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _startTime = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Date().getTime();
</span><span style="color:#800000;background-color:#f0f0f0;"> 17 
 18 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.start = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (){
</span><span style="color:#800000;background-color:#f0f0f0;"> 19 
 20 </span><span style="background-color:#ffffff;">        _startTime = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Date().getTime();
</span><span style="color:#800000;background-color:#f0f0f0;"> 21 </span><span style="background-color:#ffffff;">    };
</span><span style="color:#800000;background-color:#f0f0f0;"> 22 
 23 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.getElapsedMs = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(){
</span><span style="color:#800000;background-color:#f0f0f0;"> 24 
 25 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> elapsedMs = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Date().getTime() - _startTime;
</span><span style="color:#800000;background-color:#f0f0f0;"> 26 
 27 </span><span style="background-color:#ffffff;">        _startTime = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Date().getTime();
</span><span style="color:#800000;background-color:#f0f0f0;"> 28 
 29 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> elapsedMs;
</span><span style="color:#800000;background-color:#f0f0f0;"> 30 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;"> 31 </span><span style="background-color:#ffffff;">}
</span><span style="color:#800000;background-color:#f0f0f0;"> 32 
 33 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 34 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Simulation Manager
</span><span style="color:#800000;background-color:#f0f0f0;"> 35 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 36 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 37 </span><span style="background-color:#ffffff;">Autodesk.ADN.AdnSimulationManager = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (canvasId) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 38 
 39 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _world = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">null</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 40 
 41 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _scene = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">null</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 42 
 43 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _camera = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">null</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 44 
 45 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _renderer = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">null</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 46 
 47 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _trackball = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">null</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 48 
 49 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _physicBodies = {};
</span><span style="color:#800000;background-color:#f0f0f0;"> 50 
 51 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _intervalId = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">null</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 52 
 53 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _animationId = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">null</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 54 
 55 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _stopWatch = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Autodesk.ADN.Stopwatch();
</span><span style="color:#800000;background-color:#f0f0f0;"> 56 
 57 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 58 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Random number between (min, max)
</span><span style="color:#800000;background-color:#f0f0f0;"> 59 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 60 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 61 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> _random(min, max) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 62 
 63 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> min +  (max - min) * Math.random();
</span><span style="color:#800000;background-color:#f0f0f0;"> 64 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;"> 65 
 66 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> _randomInt(min, max) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 67 
 68 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> Math.floor(Math.random() * (max - min)) + min;
</span><span style="color:#800000;background-color:#f0f0f0;"> 69 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;"> 70 
 71 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 72 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Unique GUID
</span><span style="color:#800000;background-color:#f0f0f0;"> 73 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 74 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 75 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> _newGuid () {
</span><span style="color:#800000;background-color:#f0f0f0;"> 76 
 77 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> d = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Date().getTime();
</span><span style="color:#800000;background-color:#f0f0f0;"> 78 
 79 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> guid = </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'xxxx-xxxx-xxxx-xxxx'</span><span style="background-color:#ffffff;">.replace(
</span><span style="color:#800000;background-color:#f0f0f0;"> 80 </span><span style="background-color:#ffffff;">            </span><span style="color:#0000ff;background-color:#ffffff;">/[xy]/g</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;"> 81 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (c) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 82 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> r = (d + Math.random() * </span><span style="color:#0000ff;background-color:#ffffff;">16</span><span style="background-color:#ffffff;">) % </span><span style="color:#0000ff;background-color:#ffffff;">16</span><span style="background-color:#ffffff;"> | </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 83 </span><span style="background-color:#ffffff;">                d = Math.floor(d / </span><span style="color:#0000ff;background-color:#ffffff;">16</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 84 
 85 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> (c == </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'x'</span><span style="background-color:#ffffff;"> ? r : (r & </span><span style="color:#0000ff;background-color:#ffffff;">0x7</span><span style="background-color:#ffffff;"> | </span><span style="color:#0000ff;background-color:#ffffff;">0x8</span><span style="background-color:#ffffff;">)).
</span><span style="color:#800000;background-color:#f0f0f0;"> 86 </span><span style="background-color:#ffffff;">                     toString(</span><span style="color:#0000ff;background-color:#ffffff;">16</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 87 </span><span style="background-color:#ffffff;">            });
</span><span style="color:#800000;background-color:#f0f0f0;"> 88 
 89 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> guid;
</span><span style="color:#800000;background-color:#f0f0f0;"> 90 </span><span style="background-color:#ffffff;">    };
</span><span style="color:#800000;background-color:#f0f0f0;"> 91 
 92 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 93 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Initializes Physics
</span><span style="color:#800000;background-color:#f0f0f0;"> 94 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 95 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 96 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> _initializePhysics() {
</span><span style="color:#800000;background-color:#f0f0f0;"> 97 
 98 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> collisionConfiguration =
</span><span style="color:#800000;background-color:#f0f0f0;"> 99 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btDefaultCollisionConfiguration;
</span><span style="color:#800000;background-color:#f0f0f0;">100 
101 </span><span style="background-color:#ffffff;">        _world = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btDiscreteDynamicsWorld(
</span><span style="color:#800000;background-color:#f0f0f0;">102 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btCollisionDispatcher(
</span><span style="color:#800000;background-color:#f0f0f0;">103 </span><span style="background-color:#ffffff;">                    collisionConfiguration),
</span><span style="color:#800000;background-color:#f0f0f0;">104 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btDbvtBroadphase,
</span><span style="color:#800000;background-color:#f0f0f0;">105 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btSequentialImpulseConstraintSolver,
</span><span style="color:#800000;background-color:#f0f0f0;">106 </span><span style="background-color:#ffffff;">            collisionConfiguration);
</span><span style="color:#800000;background-color:#f0f0f0;">107 
108 </span><span style="background-color:#ffffff;">        _world.setGravity(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btVector3(</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, -</span><span style="color:#0000ff;background-color:#ffffff;">9.8</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">));
</span><span style="color:#800000;background-color:#f0f0f0;">109 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">110 
111 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">112 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Creates collision shape from mesh
</span><span style="color:#800000;background-color:#f0f0f0;">113 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">114 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">115 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> _createCollisionShape(mesh) {
</span><span style="color:#800000;background-color:#f0f0f0;">116 
117 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> geometry = mesh.geometry;
</span><span style="color:#800000;background-color:#f0f0f0;">118 
119 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> hull = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btConvexHullShape();
</span><span style="color:#800000;background-color:#f0f0f0;">120 
121 </span><span style="background-color:#ffffff;">        geometry.vertices.forEach(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(vertex){
</span><span style="color:#800000;background-color:#f0f0f0;">122 
123 </span><span style="background-color:#ffffff;">            hull.addPoint(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btVector3(
</span><span style="color:#800000;background-color:#f0f0f0;">124 </span><span style="background-color:#ffffff;">                vertex.x,
</span><span style="color:#800000;background-color:#f0f0f0;">125 </span><span style="background-color:#ffffff;">                vertex.y,
</span><span style="color:#800000;background-color:#f0f0f0;">126 </span><span style="background-color:#ffffff;">                vertex.z));
</span><span style="color:#800000;background-color:#f0f0f0;">127 </span><span style="background-color:#ffffff;">        });
</span><span style="color:#800000;background-color:#f0f0f0;">128 
129 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> hull;
</span><span style="color:#800000;background-color:#f0f0f0;">130 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">131 
132 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">133 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Creates physic rigid body from mesh
</span><span style="color:#800000;background-color:#f0f0f0;">134 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">135 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">136 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> _addRigidBody(mesh, mass) {
</span><span style="color:#800000;background-color:#f0f0f0;">137 
138 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> localInertia = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btVector3(</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">139 
140 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> shape = _createCollisionShape(mesh);
</span><span style="color:#800000;background-color:#f0f0f0;">141 
142 </span><span style="background-color:#ffffff;">        shape.calculateLocalInertia(mass, localInertia);
</span><span style="color:#800000;background-color:#f0f0f0;">143 
144 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> transform = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btTransform;
</span><span style="color:#800000;background-color:#f0f0f0;">145 
146 </span><span style="background-color:#ffffff;">        transform.setIdentity();
</span><span style="color:#800000;background-color:#f0f0f0;">147 
148 </span><span style="background-color:#ffffff;">        transform.setOrigin(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btVector3(
</span><span style="color:#800000;background-color:#f0f0f0;">149 </span><span style="background-color:#ffffff;">            mesh.position.x,
</span><span style="color:#800000;background-color:#f0f0f0;">150 </span><span style="background-color:#ffffff;">            mesh.position.y,
</span><span style="color:#800000;background-color:#f0f0f0;">151 </span><span style="background-color:#ffffff;">            mesh.position.z));
</span><span style="color:#800000;background-color:#f0f0f0;">152 
153 </span><span style="background-color:#ffffff;">        transform.setRotation(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btQuaternion(
</span><span style="color:#800000;background-color:#f0f0f0;">154 </span><span style="background-color:#ffffff;">            mesh.quaternion.x,
</span><span style="color:#800000;background-color:#f0f0f0;">155 </span><span style="background-color:#ffffff;">            mesh.quaternion.y,
</span><span style="color:#800000;background-color:#f0f0f0;">156 </span><span style="background-color:#ffffff;">            mesh.quaternion.z,
</span><span style="color:#800000;background-color:#f0f0f0;">157 </span><span style="background-color:#ffffff;">            mesh.quaternion.w
</span><span style="color:#800000;background-color:#f0f0f0;">158 </span><span style="background-color:#ffffff;">        ));
</span><span style="color:#800000;background-color:#f0f0f0;">159 
160 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> motionState =
</span><span style="color:#800000;background-color:#f0f0f0;">161 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btDefaultMotionState(transform);
</span><span style="color:#800000;background-color:#f0f0f0;">162 
163 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> rbInfo = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btRigidBodyConstructionInfo(
</span><span style="color:#800000;background-color:#f0f0f0;">164 </span><span style="background-color:#ffffff;">            mass,
</span><span style="color:#800000;background-color:#f0f0f0;">165 </span><span style="background-color:#ffffff;">            motionState,
</span><span style="color:#800000;background-color:#f0f0f0;">166 </span><span style="background-color:#ffffff;">            shape,
</span><span style="color:#800000;background-color:#f0f0f0;">167 </span><span style="background-color:#ffffff;">            localInertia);
</span><span style="color:#800000;background-color:#f0f0f0;">168 
169 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> body = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btRigidBody(rbInfo);
</span><span style="color:#800000;background-color:#f0f0f0;">170 
171 </span><span style="background-color:#ffffff;">        body.mesh = mesh;
</span><span style="color:#800000;background-color:#f0f0f0;">172 
173 </span><span style="background-color:#ffffff;">        _world.addRigidBody(body);
</span><span style="color:#800000;background-color:#f0f0f0;">174 
175 </span><span style="background-color:#ffffff;">        _physicBodies[_newGuid()] = body;
</span><span style="color:#800000;background-color:#f0f0f0;">176 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">177 
178 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">179 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Updates mesh transform from physic body
</span><span style="color:#800000;background-color:#f0f0f0;">180 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">181 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">182 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> _updateMeshTransform(body) {
</span><span style="color:#800000;background-color:#f0f0f0;">183 
184 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> mesh = body.mesh;
</span><span style="color:#800000;background-color:#f0f0f0;">185 
186 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> transform = body.getCenterOfMassTransform();
</span><span style="color:#800000;background-color:#f0f0f0;">187 
188 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> origin = transform.getOrigin();
</span><span style="color:#800000;background-color:#f0f0f0;">189 
190 </span><span style="background-color:#ffffff;">        mesh.position.set(
</span><span style="color:#800000;background-color:#f0f0f0;">191 </span><span style="background-color:#ffffff;">            origin.x(),
</span><span style="color:#800000;background-color:#f0f0f0;">192 </span><span style="background-color:#ffffff;">            origin.y(),
</span><span style="color:#800000;background-color:#f0f0f0;">193 </span><span style="background-color:#ffffff;">            origin.z());
</span><span style="color:#800000;background-color:#f0f0f0;">194 
195 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> rotation = transform.getRotation();
</span><span style="color:#800000;background-color:#f0f0f0;">196 
197 </span><span style="background-color:#ffffff;">        mesh.quaternion.set(
</span><span style="color:#800000;background-color:#f0f0f0;">198 </span><span style="background-color:#ffffff;">            rotation.x(),
</span><span style="color:#800000;background-color:#f0f0f0;">199 </span><span style="background-color:#ffffff;">            rotation.y(),
</span><span style="color:#800000;background-color:#f0f0f0;">200 </span><span style="background-color:#ffffff;">            rotation.z(),
</span><span style="color:#800000;background-color:#f0f0f0;">201 </span><span style="background-color:#ffffff;">            rotation.w());
</span><span style="color:#800000;background-color:#f0f0f0;">202 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">203 
204 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">205 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Creates ramp mesh
</span><span style="color:#800000;background-color:#f0f0f0;">206 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">207 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">208 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> _createRamp(position, rotations) {
</span><span style="color:#800000;background-color:#f0f0f0;">209 
210 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> material = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.MeshLambertMaterial({
</span><span style="color:#800000;background-color:#f0f0f0;">211 </span><span style="background-color:#ffffff;">            color: </span><span style="color:#0000ff;background-color:#ffffff;">0xF74F4F
</span><span style="color:#800000;background-color:#f0f0f0;">212 </span><span style="background-color:#ffffff;">        })
</span><span style="color:#800000;background-color:#f0f0f0;">213 
214 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> mesh = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Mesh(
</span><span style="color:#800000;background-color:#f0f0f0;">215 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.BoxGeometry(</span><span style="color:#0000ff;background-color:#ffffff;">50</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">2</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">10</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;">216 </span><span style="background-color:#ffffff;">            material);
</span><span style="color:#800000;background-color:#f0f0f0;">217 
218 </span><span style="background-color:#ffffff;">        mesh.position.copy(position);
</span><span style="color:#800000;background-color:#f0f0f0;">219 
220 </span><span style="background-color:#ffffff;">        rotations.forEach(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(rotation){
</span><span style="color:#800000;background-color:#f0f0f0;">221 
222 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> q = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Quaternion();
</span><span style="color:#800000;background-color:#f0f0f0;">223 
224 </span><span style="background-color:#ffffff;">            q.setFromAxisAngle(
</span><span style="color:#800000;background-color:#f0f0f0;">225 </span><span style="background-color:#ffffff;">                rotation.axis,
</span><span style="color:#800000;background-color:#f0f0f0;">226 </span><span style="background-color:#ffffff;">                rotation.angle);
</span><span style="color:#800000;background-color:#f0f0f0;">227 
228 </span><span style="background-color:#ffffff;">            mesh.quaternion.multiply(q);
</span><span style="color:#800000;background-color:#f0f0f0;">229 </span><span style="background-color:#ffffff;">        });
</span><span style="color:#800000;background-color:#f0f0f0;">230 
231 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> mesh;
</span><span style="color:#800000;background-color:#f0f0f0;">232 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">233 
234 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">235 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Initializes three.js scene
</span><span style="color:#800000;background-color:#f0f0f0;">236 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">237 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">238 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> _initializeScene(id) {
</span><span style="color:#800000;background-color:#f0f0f0;">239 
240 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> viewport = document.getElementById(id)
</span><span style="color:#800000;background-color:#f0f0f0;">241 
242 </span><span style="background-color:#ffffff;">        _renderer =
</span><span style="color:#800000;background-color:#f0f0f0;">243 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.WebGLRenderer({canvas: viewport})
</span><span style="color:#800000;background-color:#f0f0f0;">244 
245 </span><span style="background-color:#ffffff;">        _renderer.setSize(
</span><span style="color:#800000;background-color:#f0f0f0;">246 </span><span style="background-color:#ffffff;">            viewport.clientWidth,
</span><span style="color:#800000;background-color:#f0f0f0;">247 </span><span style="background-color:#ffffff;">            viewport.clientHeight);
</span><span style="color:#800000;background-color:#f0f0f0;">248 
249 </span><span style="background-color:#ffffff;">        _scene = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Scene
</span><span style="color:#800000;background-color:#f0f0f0;">250 </span><span style="background-color:#ffffff;">        _camera = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.PerspectiveCamera(
</span><span style="color:#800000;background-color:#f0f0f0;">251 </span><span style="background-color:#ffffff;">            </span><span style="color:#0000ff;background-color:#ffffff;">35</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">1000</span><span style="background-color:#ffffff;">)
</span><span style="color:#800000;background-color:#f0f0f0;">252 
253 </span><span style="background-color:#ffffff;">        _camera.position.set(-</span><span style="color:#0000ff;background-color:#ffffff;">50</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">90</span><span style="background-color:#ffffff;">, -</span><span style="color:#0000ff;background-color:#ffffff;">150</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">254 </span><span style="background-color:#ffffff;">        _camera.lookAt(_scene.position);
</span><span style="color:#800000;background-color:#f0f0f0;">255 
256 </span><span style="background-color:#ffffff;">        _scene.add(_camera);
</span><span style="color:#800000;background-color:#f0f0f0;">257 
258 </span><span style="background-color:#ffffff;">        _trackball = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.TrackballControls(
</span><span style="color:#800000;background-color:#f0f0f0;">259 </span><span style="background-color:#ffffff;">            _camera, viewport);
</span><span style="color:#800000;background-color:#f0f0f0;">260 
261 </span><span style="background-color:#ffffff;">        _trackball.noPan = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">262 </span><span style="background-color:#ffffff;">        _trackball.panSpeed = </span><span style="color:#0000ff;background-color:#ffffff;">0.5</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">263 </span><span style="background-color:#ffffff;">        _trackball.noZoom = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">264 </span><span style="background-color:#ffffff;">        _trackball.zoomSpeed = </span><span style="color:#0000ff;background-color:#ffffff;">2.0</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">265 </span><span style="background-color:#ffffff;">        _trackball.minDistance = </span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">266 </span><span style="background-color:#ffffff;">        _trackball.maxDistance = </span><span style="color:#0000ff;background-color:#ffffff;">300</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">267 </span><span style="background-color:#ffffff;">        _trackball.rotateSpeed = </span><span style="color:#0000ff;background-color:#ffffff;">3.5</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">268 </span><span style="background-color:#ffffff;">        _trackball.staticMoving = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">269 </span><span style="background-color:#ffffff;">        _trackball.dynamicDampingFactor = </span><span style="color:#0000ff;background-color:#ffffff;">0.3</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">270 
271 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// [r:rotate, z:zoom, p:pan]
</span><span style="color:#800000;background-color:#f0f0f0;">272 </span><span style="background-color:#ffffff;">        _trackball.keys = [</span><span style="color:#0000ff;background-color:#ffffff;">82</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">90</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">80</span><span style="background-color:#ffffff;">];
</span><span style="color:#800000;background-color:#f0f0f0;">273 
274 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> ambientLight = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.AmbientLight(</span><span style="color:#0000ff;background-color:#ffffff;">0x555555</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">275 </span><span style="background-color:#ffffff;">        _scene.add(ambientLight);
</span><span style="color:#800000;background-color:#f0f0f0;">276 
277 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> directionalLight = 
</span><span style="color:#800000;background-color:#f0f0f0;">278 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.DirectionalLight(</span><span style="color:#0000ff;background-color:#ffffff;">0xffffff</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">279 </span><span style="background-color:#ffffff;">        
</span><span style="color:#800000;background-color:#f0f0f0;">280 </span><span style="background-color:#ffffff;">        directionalLight.position.set(
</span><span style="color:#800000;background-color:#f0f0f0;">281 </span><span style="background-color:#ffffff;">            -</span><span style="color:#0000ff;background-color:#ffffff;">.5</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">.5</span><span style="background-color:#ffffff;">, -</span><span style="color:#0000ff;background-color:#ffffff;">1.5</span><span style="background-color:#ffffff;"> ).normalize();
</span><span style="color:#800000;background-color:#f0f0f0;">282 </span><span style="background-color:#ffffff;">        
</span><span style="color:#800000;background-color:#f0f0f0;">283 </span><span style="background-color:#ffffff;">        _scene.add(directionalLight);
</span><span style="color:#800000;background-color:#f0f0f0;">284 
285 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> material = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.MeshLambertMaterial({
</span><span style="color:#800000;background-color:#f0f0f0;">286 </span><span style="background-color:#ffffff;">            color: </span><span style="color:#0000ff;background-color:#ffffff;">0xdd0000
</span><span style="color:#800000;background-color:#f0f0f0;">287 </span><span style="background-color:#ffffff;">        })
</span><span style="color:#800000;background-color:#f0f0f0;">288 
289 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> ramp11 = _createRamp(
</span><span style="color:#800000;background-color:#f0f0f0;">290 
291 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Vector3(-</span><span style="color:#0000ff;background-color:#ffffff;">20</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">25</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;">292 </span><span style="background-color:#ffffff;">            [{
</span><span style="color:#800000;background-color:#f0f0f0;">293 </span><span style="background-color:#ffffff;">                axis: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Vector3(</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;">294 </span><span style="background-color:#ffffff;">                angle: - Math.PI / </span><span style="color:#0000ff;background-color:#ffffff;">10
</span><span style="color:#800000;background-color:#f0f0f0;">295 </span><span style="background-color:#ffffff;">            }, {
</span><span style="color:#800000;background-color:#f0f0f0;">296 </span><span style="background-color:#ffffff;">                axis: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Vector3(</span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;">297 </span><span style="background-color:#ffffff;">                angle: - Math.PI / </span><span style="color:#0000ff;background-color:#ffffff;">10
</span><span style="color:#800000;background-color:#f0f0f0;">298 </span><span style="background-color:#ffffff;">            }]);
</span><span style="color:#800000;background-color:#f0f0f0;">299 
300 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> ramp12 = _createRamp(
</span><span style="color:#800000;background-color:#f0f0f0;">301 
302 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Vector3(-</span><span style="color:#0000ff;background-color:#ffffff;">20</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">25</span><span style="background-color:#ffffff;">, -</span><span style="color:#0000ff;background-color:#ffffff;">10</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;">303 </span><span style="background-color:#ffffff;">            [{
</span><span style="color:#800000;background-color:#f0f0f0;">304 </span><span style="background-color:#ffffff;">                axis: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Vector3(</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;">305 </span><span style="background-color:#ffffff;">                angle: - Math.PI / </span><span style="color:#0000ff;background-color:#ffffff;">10
</span><span style="color:#800000;background-color:#f0f0f0;">306 </span><span style="background-color:#ffffff;">            }, {
</span><span style="color:#800000;background-color:#f0f0f0;">307 </span><span style="background-color:#ffffff;">                axis: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Vector3(</span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;">308 </span><span style="background-color:#ffffff;">                angle: Math.PI / </span><span style="color:#0000ff;background-color:#ffffff;">10
</span><span style="color:#800000;background-color:#f0f0f0;">309 </span><span style="background-color:#ffffff;">            }]);
</span><span style="color:#800000;background-color:#f0f0f0;">310 
311 
312 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> ramp21 = _createRamp(
</span><span style="color:#800000;background-color:#f0f0f0;">313 
314 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Vector3(</span><span style="color:#0000ff;background-color:#ffffff;">25</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">5</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;">315 </span><span style="background-color:#ffffff;">            [{
</span><span style="color:#800000;background-color:#f0f0f0;">316 </span><span style="background-color:#ffffff;">                axis: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Vector3(</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;">317 </span><span style="background-color:#ffffff;">                angle: Math.PI / </span><span style="color:#0000ff;background-color:#ffffff;">10
</span><span style="color:#800000;background-color:#f0f0f0;">318 </span><span style="background-color:#ffffff;">            }, {
</span><span style="color:#800000;background-color:#f0f0f0;">319 </span><span style="background-color:#ffffff;">                axis: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Vector3(</span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;">320 </span><span style="background-color:#ffffff;">                angle: - Math.PI / </span><span style="color:#0000ff;background-color:#ffffff;">10
</span><span style="color:#800000;background-color:#f0f0f0;">321 </span><span style="background-color:#ffffff;">            }]);
</span><span style="color:#800000;background-color:#f0f0f0;">322 
323 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> ramp22 = _createRamp(
</span><span style="color:#800000;background-color:#f0f0f0;">324 
325 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Vector3(</span><span style="color:#0000ff;background-color:#ffffff;">25</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">5</span><span style="background-color:#ffffff;">, -</span><span style="color:#0000ff;background-color:#ffffff;">10</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;">326 </span><span style="background-color:#ffffff;">            [{
</span><span style="color:#800000;background-color:#f0f0f0;">327 </span><span style="background-color:#ffffff;">                axis: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Vector3(</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;">328 </span><span style="background-color:#ffffff;">                angle: Math.PI / </span><span style="color:#0000ff;background-color:#ffffff;">10
</span><span style="color:#800000;background-color:#f0f0f0;">329 </span><span style="background-color:#ffffff;">            }, {
</span><span style="color:#800000;background-color:#f0f0f0;">330 </span><span style="background-color:#ffffff;">                axis: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Vector3(</span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;">331 </span><span style="background-color:#ffffff;">                angle: Math.PI / </span><span style="color:#0000ff;background-color:#ffffff;">10
</span><span style="color:#800000;background-color:#f0f0f0;">332 </span><span style="background-color:#ffffff;">            }]);
</span><span style="color:#800000;background-color:#f0f0f0;">333 
334 
335 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> ramp31 = _createRamp(
</span><span style="color:#800000;background-color:#f0f0f0;">336 
337 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Vector3(-</span><span style="color:#0000ff;background-color:#ffffff;">20</span><span style="background-color:#ffffff;">, -</span><span style="color:#0000ff;background-color:#ffffff;">10</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;">338 </span><span style="background-color:#ffffff;">            [{
</span><span style="color:#800000;background-color:#f0f0f0;">339 </span><span style="background-color:#ffffff;">                axis: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Vector3(</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;">340 </span><span style="background-color:#ffffff;">                angle: -Math.PI / </span><span style="color:#0000ff;background-color:#ffffff;">10
</span><span style="color:#800000;background-color:#f0f0f0;">341 </span><span style="background-color:#ffffff;">            }, {
</span><span style="color:#800000;background-color:#f0f0f0;">342 </span><span style="background-color:#ffffff;">                axis: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Vector3(</span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;">343 </span><span style="background-color:#ffffff;">                angle: - Math.PI / </span><span style="color:#0000ff;background-color:#ffffff;">10
</span><span style="color:#800000;background-color:#f0f0f0;">344 </span><span style="background-color:#ffffff;">            }]);
</span><span style="color:#800000;background-color:#f0f0f0;">345 
346 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> ramp32 = _createRamp(
</span><span style="color:#800000;background-color:#f0f0f0;">347 
348 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Vector3(-</span><span style="color:#0000ff;background-color:#ffffff;">20</span><span style="background-color:#ffffff;">, -</span><span style="color:#0000ff;background-color:#ffffff;">10</span><span style="background-color:#ffffff;">, -</span><span style="color:#0000ff;background-color:#ffffff;">10</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;">349 </span><span style="background-color:#ffffff;">            [{
</span><span style="color:#800000;background-color:#f0f0f0;">350 </span><span style="background-color:#ffffff;">                axis: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Vector3(</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;">351 </span><span style="background-color:#ffffff;">                angle: -Math.PI / </span><span style="color:#0000ff;background-color:#ffffff;">10
</span><span style="color:#800000;background-color:#f0f0f0;">352 </span><span style="background-color:#ffffff;">            }, {
</span><span style="color:#800000;background-color:#f0f0f0;">353 </span><span style="background-color:#ffffff;">                axis: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Vector3(</span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;">354 </span><span style="background-color:#ffffff;">                angle: Math.PI / </span><span style="color:#0000ff;background-color:#ffffff;">10
</span><span style="color:#800000;background-color:#f0f0f0;">355 </span><span style="background-color:#ffffff;">            }]);
</span><span style="color:#800000;background-color:#f0f0f0;">356 
357 </span><span style="background-color:#ffffff;">        _scene.add(ramp11);
</span><span style="color:#800000;background-color:#f0f0f0;">358 </span><span style="background-color:#ffffff;">        _scene.add(ramp12);
</span><span style="color:#800000;background-color:#f0f0f0;">359 
360 </span><span style="background-color:#ffffff;">        _scene.add(ramp21);
</span><span style="color:#800000;background-color:#f0f0f0;">361 </span><span style="background-color:#ffffff;">        _scene.add(ramp22);
</span><span style="color:#800000;background-color:#f0f0f0;">362 
363 </span><span style="background-color:#ffffff;">        _scene.add(ramp31);
</span><span style="color:#800000;background-color:#f0f0f0;">364 </span><span style="background-color:#ffffff;">        _scene.add(ramp32);
</span><span style="color:#800000;background-color:#f0f0f0;">365 
366 </span><span style="background-color:#ffffff;">        _addRigidBody(ramp11, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">367 </span><span style="background-color:#ffffff;">        _addRigidBody(ramp12, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">368 
369 </span><span style="background-color:#ffffff;">        _addRigidBody(ramp21, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">370 </span><span style="background-color:#ffffff;">        _addRigidBody(ramp22, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">371 
372 </span><span style="background-color:#ffffff;">        _addRigidBody(ramp31, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">373 </span><span style="background-color:#ffffff;">        _addRigidBody(ramp32, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">374 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">375 
376 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">377 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Creates random geometry
</span><span style="color:#800000;background-color:#f0f0f0;">378 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">379 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">380 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> _createRandomGeometry(size) {
</span><span style="color:#800000;background-color:#f0f0f0;">381 
382 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">switch</span><span style="background-color:#ffffff;">(_randomInt(</span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">9</span><span style="background-color:#ffffff;">)) {
</span><span style="color:#800000;background-color:#f0f0f0;">383 
384 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">case</span><span style="background-color:#ffffff;"> </span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">:
</span><span style="color:#800000;background-color:#f0f0f0;">385 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> THREE.BoxGeometry(
</span><span style="color:#800000;background-color:#f0f0f0;">386 </span><span style="background-color:#ffffff;">                    size, size, size);
</span><span style="color:#800000;background-color:#f0f0f0;">387 
388 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">case</span><span style="background-color:#ffffff;"> </span><span style="color:#0000ff;background-color:#ffffff;">2</span><span style="background-color:#ffffff;">:
</span><span style="color:#800000;background-color:#f0f0f0;">389 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> THREE.SphereGeometry(
</span><span style="color:#800000;background-color:#f0f0f0;">390 </span><span style="background-color:#ffffff;">                    size, </span><span style="color:#0000ff;background-color:#ffffff;">32</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">32</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">391 
392 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">case</span><span style="background-color:#ffffff;"> </span><span style="color:#0000ff;background-color:#ffffff;">3</span><span style="background-color:#ffffff;">:
</span><span style="color:#800000;background-color:#f0f0f0;">393 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.IcosahedronGeometry(
</span><span style="color:#800000;background-color:#f0f0f0;">394 </span><span style="background-color:#ffffff;">                    size, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">395 
396 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">case</span><span style="background-color:#ffffff;"> </span><span style="color:#0000ff;background-color:#ffffff;">4</span><span style="background-color:#ffffff;">:
</span><span style="color:#800000;background-color:#f0f0f0;">397 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.OctahedronGeometry(
</span><span style="color:#800000;background-color:#f0f0f0;">398 </span><span style="background-color:#ffffff;">                    size, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">399 
400 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">case</span><span style="background-color:#ffffff;"> </span><span style="color:#0000ff;background-color:#ffffff;">5</span><span style="background-color:#ffffff;">:
</span><span style="color:#800000;background-color:#f0f0f0;">401 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.TetrahedronGeometry(
</span><span style="color:#800000;background-color:#f0f0f0;">402 </span><span style="background-color:#ffffff;">                    size, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">403 
404 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">case</span><span style="background-color:#ffffff;"> </span><span style="color:#0000ff;background-color:#ffffff;">6</span><span style="background-color:#ffffff;">:
</span><span style="color:#800000;background-color:#f0f0f0;">405 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.CylinderGeometry(
</span><span style="color:#800000;background-color:#f0f0f0;">406 </span><span style="background-color:#ffffff;">                    </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, size, size, </span><span style="color:#0000ff;background-color:#ffffff;">20</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">4</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">407 
408 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">case</span><span style="background-color:#ffffff;"> </span><span style="color:#0000ff;background-color:#ffffff;">7</span><span style="background-color:#ffffff;">:
</span><span style="color:#800000;background-color:#f0f0f0;">409 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.CylinderGeometry(
</span><span style="color:#800000;background-color:#f0f0f0;">410 </span><span style="background-color:#ffffff;">                    size, size, size, </span><span style="color:#0000ff;background-color:#ffffff;">20</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">4</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">411 
412 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">case</span><span style="background-color:#ffffff;"> </span><span style="color:#0000ff;background-color:#ffffff;">8</span><span style="background-color:#ffffff;">:
</span><span style="color:#800000;background-color:#f0f0f0;">413 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.CylinderGeometry(
</span><span style="color:#800000;background-color:#f0f0f0;">414 </span><span style="background-color:#ffffff;">                    size * </span><span style="color:#0000ff;background-color:#ffffff;">0.5</span><span style="background-color:#ffffff;">, size, size, </span><span style="color:#0000ff;background-color:#ffffff;">20</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">4</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">415 
416 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">case</span><span style="background-color:#ffffff;"> </span><span style="color:#0000ff;background-color:#ffffff;">9</span><span style="background-color:#ffffff;">:
</span><span style="color:#800000;background-color:#f0f0f0;">417 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.CylinderGeometry(
</span><span style="color:#800000;background-color:#f0f0f0;">418 </span><span style="background-color:#ffffff;">                    size * </span><span style="color:#0000ff;background-color:#ffffff;">0.5</span><span style="background-color:#ffffff;">, size * </span><span style="color:#0000ff;background-color:#ffffff;">0.5</span><span style="background-color:#ffffff;">, size, </span><span style="color:#0000ff;background-color:#ffffff;">20</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">4</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">419 
420 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">default</span><span style="background-color:#ffffff;">:
</span><span style="color:#800000;background-color:#f0f0f0;">421 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">null</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">422 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;">423 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">424 
425 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">426 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Add new elements to the scene
</span><span style="color:#800000;background-color:#f0f0f0;">427 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">428 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">429 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> _newElements() {
</span><span style="color:#800000;background-color:#f0f0f0;">430 
431 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> newElement() {
</span><span style="color:#800000;background-color:#f0f0f0;">432 
433 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> size = _random(</span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">4</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">434 
435 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> mass = size;
</span><span style="color:#800000;background-color:#f0f0f0;">436 
437 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> color = Math.floor(Math.random() * </span><span style="color:#0000ff;background-color:#ffffff;">16777215</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">438 
439 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> material = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.MeshLambertMaterial({
</span><span style="color:#800000;background-color:#f0f0f0;">440 </span><span style="background-color:#ffffff;">                color: color
</span><span style="color:#800000;background-color:#f0f0f0;">441 </span><span style="background-color:#ffffff;">            });
</span><span style="color:#800000;background-color:#f0f0f0;">442 
443 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> mesh = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Mesh(
</span><span style="color:#800000;background-color:#f0f0f0;">444 </span><span style="background-color:#ffffff;">                    _createRandomGeometry(size),
</span><span style="color:#800000;background-color:#f0f0f0;">445 </span><span style="background-color:#ffffff;">                    material);
</span><span style="color:#800000;background-color:#f0f0f0;">446 
447 </span><span style="background-color:#ffffff;">            mesh.position.x = _random(-</span><span style="color:#0000ff;background-color:#ffffff;">40</span><span style="background-color:#ffffff;">, -</span><span style="color:#0000ff;background-color:#ffffff;">20</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">448 </span><span style="background-color:#ffffff;">            mesh.position.y = </span><span style="color:#0000ff;background-color:#ffffff;">50</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">449 
450 </span><span style="background-color:#ffffff;">            _scene.add(mesh);
</span><span style="color:#800000;background-color:#f0f0f0;">451 
452 </span><span style="background-color:#ffffff;">            _addRigidBody(mesh, mass);
</span><span style="color:#800000;background-color:#f0f0f0;">453 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;">454 
455 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// adds a bunch ...
</span><span style="color:#800000;background-color:#f0f0f0;">456 
457 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> nbElems = _random(</span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">5</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">458 
459 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">for</span><span style="background-color:#ffffff;">(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> i=</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">; i &lt; nbElems; ++i) {
</span><span style="color:#800000;background-color:#f0f0f0;">460 
461 </span><span style="background-color:#ffffff;">            newElement();
</span><span style="color:#800000;background-color:#f0f0f0;">462 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;">463 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">464 
465 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">466 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Update loop
</span><span style="color:#800000;background-color:#f0f0f0;">467 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">468 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">469 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> _update() {
</span><span style="color:#800000;background-color:#f0f0f0;">470 
471 </span><span style="background-color:#ffffff;">        _animationId = requestAnimationFrame(_update);
</span><span style="color:#800000;background-color:#f0f0f0;">472 
473 </span><span style="background-color:#ffffff;">        _world.stepSimulation(
</span><span style="color:#800000;background-color:#f0f0f0;">474 </span><span style="background-color:#ffffff;">            _stopWatch.getElapsedMs() * </span><span style="color:#0000ff;background-color:#ffffff;">0.002</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">475 </span><span style="background-color:#ffffff;">            </span><span style="color:#0000ff;background-color:#ffffff;">10</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">476 
477 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">for</span><span style="background-color:#ffffff;">(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> key </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">in</span><span style="background-color:#ffffff;"> _physicBodies) {
</span><span style="color:#800000;background-color:#f0f0f0;">478 
479 </span><span style="background-color:#ffffff;">            _updateMeshTransform(_physicBodies[key]);
</span><span style="color:#800000;background-color:#f0f0f0;">480 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;">481 
482 </span><span style="background-color:#ffffff;">        _trackball.update();
</span><span style="color:#800000;background-color:#f0f0f0;">483 
484 </span><span style="background-color:#ffffff;">        _renderer.render(_scene, _camera);
</span><span style="color:#800000;background-color:#f0f0f0;">485 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">486 
487 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">488 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Starts simulation
</span><span style="color:#800000;background-color:#f0f0f0;">489 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">490 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">491 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.start = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">() {
</span><span style="color:#800000;background-color:#f0f0f0;">492 
493 </span><span style="background-color:#ffffff;">        _stopWatch.getElapsedMs();
</span><span style="color:#800000;background-color:#f0f0f0;">494 
495 </span><span style="background-color:#ffffff;">        _update();
</span><span style="color:#800000;background-color:#f0f0f0;">496 
497 </span><span style="background-color:#ffffff;">        _intervalId = setInterval(
</span><span style="color:#800000;background-color:#f0f0f0;">498 </span><span style="background-color:#ffffff;">            _newElements,
</span><span style="color:#800000;background-color:#f0f0f0;">499 </span><span style="background-color:#ffffff;">            </span><span style="color:#0000ff;background-color:#ffffff;">1500</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">500 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">501 
502 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">503 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Stops simulation
</span><span style="color:#800000;background-color:#f0f0f0;">504 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">505 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">506 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.stop = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">() {
</span><span style="color:#800000;background-color:#f0f0f0;">507 
508 </span><span style="background-color:#ffffff;">        cancelAnimationFrame(_animationId);
</span><span style="color:#800000;background-color:#f0f0f0;">509 
510 </span><span style="background-color:#ffffff;">        clearInterval(_intervalId);
</span><span style="color:#800000;background-color:#f0f0f0;">511 
512 </span><span style="background-color:#ffffff;">        _animationId = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">null</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">513 
514 </span><span style="background-color:#ffffff;">        _intervalId = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">null</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">515 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">516 
517 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">518 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">519 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">520 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">521 </span><span style="background-color:#ffffff;">    _initializePhysics();
</span><span style="color:#800000;background-color:#f0f0f0;">522 
523 </span><span style="background-color:#ffffff;">    _initializeScene(canvasId);
</span><span style="color:#800000;background-color:#f0f0f0;">524 </span><span style="background-color:#ffffff;">}
</span><span style="color:#800000;background-color:#f0f0f0;">525 
526 </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> simulationManager =
</span><span style="color:#800000;background-color:#f0f0f0;">527 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Autodesk.ADN.AdnSimulationManager(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'ammojs'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">528 
529 </span><span style="background-color:#ffffff;">simulationManager.start();</span></pre>



<p><span class="asset  asset-generic at-xid-6a0167607c2431970b01b7c72a57df970b img-responsive"><a href="http://adndevblog.typepad.com/files/ammo.zip">Download Ammo Sample</a></span></p>
