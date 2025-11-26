---
layout: "post"
title: "Integrating Ammo.js Physics with <br> Autodesk View & Data API"
date: "2015-01-01 23:00:00"
author: "Philippe Leefsma"
categories:
  - "Browser"
  - "Client"
  - "Cloud"
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "WebGL"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/01/integrating-ammojs-physics-with-autodesk-view-data-api.html "
typepad_basename: "integrating-ammojs-physics-with-autodesk-view-data-api"
typepad_status: "Publish"
---

<script src="http://code.jquery.com/jquery-2.1.3.min.js"></script>

<p>By <a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>

<p>Following my <a title="" href="http://adndevblog.typepad.com/cloud_and_mobile/2014/12/fun-with-the-physics.html" target="_blank">preliminary work</a> on <a href="https://github.com/kripken/ammo.js/" target="_blank">Ammo.js</a> - for those who do not click links right away, Ammo.js is an <a href="https://github.com/kripken/emscripten" target="_blank">emscripten</a> port of <a href="http://bulletphysics.org/wordpress/" target="_blank">Bullet</a>, a well-established C++ Physics library - this week post features my latest viewer extension, “Physics”.</p>

You can test the live sample from that <a href="http://viewer.autodesk.io/node/physics/#/viewer">link</a> and below here how it looks like:
<br/><br/>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01bb08744815970d-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b01bb08744815970d image-full img-responsive" alt="Screen Shot 2015-09-18 at 3.31.49 PM" title="Screen Shot 2015-09-18 at 3.31.49 PM" src="/assets/image_8c14d3.jpg" border="0" /></a><br />


<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:8pt;"><span style="color:#800000;background-color:#f0f0f0;">  1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">  2 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Ammo.js Physics viewer extension
</span><span style="color:#800000;background-color:#f0f0f0;">  3 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// by Philippe Leefsma, December 2014
</span><span style="color:#800000;background-color:#f0f0f0;">  4 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">  5 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Dependencies:
</span><span style="color:#800000;background-color:#f0f0f0;">  6 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">  7 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// https://rawgit.com/kripken/ammo.js/master/builds/ammo.js
</span><span style="color:#800000;background-color:#f0f0f0;">  8 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// https://rawgit.com/darsain/fpsmeter/master/dist/fpsmeter.min.js
</span><span style="color:#800000;background-color:#f0f0f0;">  9 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// https://rawgit.com/vitalets/angular-xeditable/master/dist/js/xeditable.min.js
</span><span style="color:#800000;background-color:#f0f0f0;"> 10 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 11 
 12 </span><span style="background-color:#ffffff;">AutodeskNamespace(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"Autodesk.ADN.Viewing.Extension"</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 13 
 14 </span><span style="background-color:#ffffff;">Autodesk.ADN.Viewing.Extension.Physics = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (viewer, options) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 15 
 16 </span><span style="background-color:#ffffff;">    Autodesk.Viewing.Extension.call(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">, viewer, options);
</span><span style="color:#800000;background-color:#f0f0f0;"> 17 
 18 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _fps = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">null</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 19 
 20 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _self = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 21 
 22 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _panel = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">null</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 23 
 24 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _world = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">null</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 25 
 26 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _meshMap = {};
</span><span style="color:#800000;background-color:#f0f0f0;"> 27 
 28 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _viewer = viewer;
</span><span style="color:#800000;background-color:#f0f0f0;"> 29 
 30 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _started = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 31 
 32 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _running = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 33 
 34 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _animationId = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">null</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 35 
 36 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _selectedEntry = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">null</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 37 
 38 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 39 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// A stopwatch!
</span><span style="color:#800000;background-color:#f0f0f0;"> 40 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 41 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 42 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> Stopwatch = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">() {
</span><span style="color:#800000;background-color:#f0f0f0;"> 43 
 44 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _startTime = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Date().getTime();
</span><span style="color:#800000;background-color:#f0f0f0;"> 45 
 46 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.start = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (){
</span><span style="color:#800000;background-color:#f0f0f0;"> 47 
 48 </span><span style="background-color:#ffffff;">            _startTime = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Date().getTime();
</span><span style="color:#800000;background-color:#f0f0f0;"> 49 </span><span style="background-color:#ffffff;">        };
</span><span style="color:#800000;background-color:#f0f0f0;"> 50 
 51 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.getElapsedMs = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(){
</span><span style="color:#800000;background-color:#f0f0f0;"> 52 
 53 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> elapsedMs = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Date().getTime() - _startTime;
</span><span style="color:#800000;background-color:#f0f0f0;"> 54 
 55 </span><span style="background-color:#ffffff;">            _startTime = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Date().getTime();
</span><span style="color:#800000;background-color:#f0f0f0;"> 56 
 57 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> elapsedMs;
</span><span style="color:#800000;background-color:#f0f0f0;"> 58 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;"> 59 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;"> 60 
 61 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _stopWatch = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Stopwatch();
</span><span style="color:#800000;background-color:#f0f0f0;"> 62 
 63 </span><span style="background-color:#ffffff;">    String.prototype.replaceAll = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (find, replace) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 64 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.replace(
</span><span style="color:#800000;background-color:#f0f0f0;"> 65 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> RegExp(find.replace(</span><span style="color:#0000ff;background-color:#ffffff;">/[-\/\\^$*+?.()|[\]{}]/g</span><span style="background-color:#ffffff;">, </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">\\</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">$&'</span><span style="background-color:#ffffff;">), </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'g'</span><span style="background-color:#ffffff;">),
</span><span style="color:#800000;background-color:#f0f0f0;"> 66 </span><span style="background-color:#ffffff;">            replace);
</span><span style="color:#800000;background-color:#f0f0f0;"> 67 </span><span style="background-color:#ffffff;">    };
</span><span style="color:#800000;background-color:#f0f0f0;"> 68 
 69 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 70 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Extension load callback
</span><span style="color:#800000;background-color:#f0f0f0;"> 71 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;"> 72 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;"> 73 </span><span style="background-color:#ffffff;">    _self.load = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> () {
</span><span style="color:#800000;background-color:#f0f0f0;"> 74 
 75 </span><span style="background-color:#ffffff;">        console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Autodesk.ADN.Viewing.Extension.Physics loading ...'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 76 
 77 </span><span style="background-color:#ffffff;">        $(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'&lt;link/&gt;'</span><span style="background-color:#ffffff;">, {
</span><span style="color:#800000;background-color:#f0f0f0;"> 78 </span><span style="background-color:#ffffff;">            rel: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'stylesheet'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;"> 79 </span><span style="background-color:#ffffff;">            type: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'text/css'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;"> 80 </span><span style="background-color:#ffffff;">            href: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'https://rawgit.com/vitalets/angular-xeditable/master/dist/css/xeditable.css'
</span><span style="color:#800000;background-color:#f0f0f0;"> 81 </span><span style="background-color:#ffffff;">        }).appendTo(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'head'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 82 
 83 </span><span style="background-color:#ffffff;">        require([
</span><span style="color:#800000;background-color:#f0f0f0;"> 84 </span><span style="background-color:#ffffff;">            </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'https://rawgit.com/kripken/ammo.js/master/builds/ammo.js'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;"> 85 </span><span style="background-color:#ffffff;">            </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'https://rawgit.com/darsain/fpsmeter/master/dist/fpsmeter.min.js'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;"> 86 </span><span style="background-color:#ffffff;">            </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'https://rawgit.com/vitalets/angular-xeditable/master/dist/js/xeditable.min.js'
</span><span style="color:#800000;background-color:#f0f0f0;"> 87 </span><span style="background-color:#ffffff;">        ], </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">() {
</span><span style="color:#800000;background-color:#f0f0f0;"> 88 
 89 </span><span style="background-color:#ffffff;">            _self.initialize(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">() {
</span><span style="color:#800000;background-color:#f0f0f0;"> 90 
 91 </span><span style="background-color:#ffffff;">                _panel = _self.loadPanel();
</span><span style="color:#800000;background-color:#f0f0f0;"> 92 
 93 </span><span style="background-color:#ffffff;">                _viewer.addEventListener(
</span><span style="color:#800000;background-color:#f0f0f0;"> 94 </span><span style="background-color:#ffffff;">                    Autodesk.Viewing.SELECTION_CHANGED_EVENT,
</span><span style="color:#800000;background-color:#f0f0f0;"> 95 </span><span style="background-color:#ffffff;">                    _self.onItemSelected);
</span><span style="color:#800000;background-color:#f0f0f0;"> 96 
 97 </span><span style="background-color:#ffffff;">                console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Autodesk.ADN.Viewing.Extension.Physics loaded'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 98 </span><span style="background-color:#ffffff;">            });
</span><span style="color:#800000;background-color:#f0f0f0;"> 99 </span><span style="background-color:#ffffff;">        });
</span><span style="color:#800000;background-color:#f0f0f0;">100 
101 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">102 </span><span style="background-color:#ffffff;">    };
</span><span style="color:#800000;background-color:#f0f0f0;">103 
104 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">105 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Extension unload callback
</span><span style="color:#800000;background-color:#f0f0f0;">106 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">107 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">108 </span><span style="background-color:#ffffff;">    _self.unload = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> () {
</span><span style="color:#800000;background-color:#f0f0f0;">109 
110 </span><span style="background-color:#ffffff;">        $(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'#physicsDivId'</span><span style="background-color:#ffffff;">).remove();
</span><span style="color:#800000;background-color:#f0f0f0;">111 
112 </span><span style="background-color:#ffffff;">        _panel.setVisible(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">, </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">113 
114 </span><span style="background-color:#ffffff;">        _panel = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">null</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">115 
116 </span><span style="background-color:#ffffff;">        _self.stop();
</span><span style="color:#800000;background-color:#f0f0f0;">117 
118 </span><span style="background-color:#ffffff;">        console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Autodesk.ADN.Viewing.Extension.Physics unloaded'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">119 
120 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">121 </span><span style="background-color:#ffffff;">    };
</span><span style="color:#800000;background-color:#f0f0f0;">122 
123 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">124 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Initializes meshes and grab initial properties
</span><span style="color:#800000;background-color:#f0f0f0;">125 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">126 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">127 </span><span style="background-color:#ffffff;">    _self.initialize = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(callback) {
</span><span style="color:#800000;background-color:#f0f0f0;">128 
129 </span><span style="background-color:#ffffff;">        _viewer.getObjectTree(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (rootComponent) {
</span><span style="color:#800000;background-color:#f0f0f0;">130 
131 </span><span style="background-color:#ffffff;">            rootComponent.children.forEach(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(component) {
</span><span style="color:#800000;background-color:#f0f0f0;">132 
133 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> fragIdsArray = (Array.isArray(component.fragIds) ?
</span><span style="color:#800000;background-color:#f0f0f0;">134 </span><span style="background-color:#ffffff;">                    component.fragIds :
</span><span style="color:#800000;background-color:#f0f0f0;">135 </span><span style="background-color:#ffffff;">                    [component.fragIds]);
</span><span style="color:#800000;background-color:#f0f0f0;">136 
137 </span><span style="background-color:#ffffff;">                fragIdsArray.forEach(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(subFragId) {
</span><span style="color:#800000;background-color:#f0f0f0;">138 
139 </span><span style="background-color:#ffffff;">                    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> mesh = _viewer.impl.getRenderProxy(
</span><span style="color:#800000;background-color:#f0f0f0;">140 </span><span style="background-color:#ffffff;">                        _viewer,
</span><span style="color:#800000;background-color:#f0f0f0;">141 </span><span style="background-color:#ffffff;">                        subFragId);
</span><span style="color:#800000;background-color:#f0f0f0;">142 
143 </span><span style="background-color:#ffffff;">                    _viewer.getPropertyValue(
</span><span style="color:#800000;background-color:#f0f0f0;">144 </span><span style="background-color:#ffffff;">                        component.dbId,
</span><span style="color:#800000;background-color:#f0f0f0;">145 </span><span style="background-color:#ffffff;">                        </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"Mass"</span><span style="background-color:#ffffff;">, </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(mass) {
</span><span style="color:#800000;background-color:#f0f0f0;">146 
147 </span><span style="background-color:#ffffff;">                        mass = (mass !== </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'undefined'</span><span style="background-color:#ffffff;"> ? mass : </span><span style="color:#0000ff;background-color:#ffffff;">1.0</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">148 
149 </span><span style="background-color:#ffffff;">                        _viewer.getPropertyValue(
</span><span style="color:#800000;background-color:#f0f0f0;">150 </span><span style="background-color:#ffffff;">                            component.dbId,
</span><span style="color:#800000;background-color:#f0f0f0;">151 </span><span style="background-color:#ffffff;">                            </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"vInit"</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">152 </span><span style="background-color:#ffffff;">                            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (vInit) {
</span><span style="color:#800000;background-color:#f0f0f0;">153 
154 </span><span style="background-color:#ffffff;">                                vInit =
</span><span style="color:#800000;background-color:#f0f0f0;">155 </span><span style="background-color:#ffffff;">                                (vInit !== </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'undefined'</span><span style="background-color:#ffffff;"> ? vInit : </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"0;0;0"</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">156 
157 </span><span style="background-color:#ffffff;">                                vInit = parseArray(vInit, </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">';'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">158 
159 </span><span style="background-color:#ffffff;">                                _meshMap[subFragId] = {
</span><span style="color:#800000;background-color:#f0f0f0;">160 </span><span style="background-color:#ffffff;">                                   transform: mesh.matrixWorld.clone(),
</span><span style="color:#800000;background-color:#f0f0f0;">161 </span><span style="background-color:#ffffff;">                                   component: component,
</span><span style="color:#800000;background-color:#f0f0f0;">162 
163 </span><span style="background-color:#ffffff;">                                   vAngularInit: [</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">,</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">,</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">],
</span><span style="color:#800000;background-color:#f0f0f0;">164 </span><span style="background-color:#ffffff;">                                   vAngular: [</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">,</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">,</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">],
</span><span style="color:#800000;background-color:#f0f0f0;">165 
166 </span><span style="background-color:#ffffff;">                                   vLinearInit: vInit,
</span><span style="color:#800000;background-color:#f0f0f0;">167 </span><span style="background-color:#ffffff;">                                   vLinear: vInit,
</span><span style="color:#800000;background-color:#f0f0f0;">168 
169 </span><span style="background-color:#ffffff;">                                   mass: mass,
</span><span style="color:#800000;background-color:#f0f0f0;">170 </span><span style="background-color:#ffffff;">                                   mesh: mesh,
</span><span style="color:#800000;background-color:#f0f0f0;">171 </span><span style="background-color:#ffffff;">                                   body: </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">null
</span><span style="color:#800000;background-color:#f0f0f0;">172 </span><span style="background-color:#ffffff;">                                }
</span><span style="color:#800000;background-color:#f0f0f0;">173 </span><span style="background-color:#ffffff;">                            });
</span><span style="color:#800000;background-color:#f0f0f0;">174 </span><span style="background-color:#ffffff;">                    });
</span><span style="color:#800000;background-color:#f0f0f0;">175 </span><span style="background-color:#ffffff;">                });
</span><span style="color:#800000;background-color:#f0f0f0;">176 </span><span style="background-color:#ffffff;">            });
</span><span style="color:#800000;background-color:#f0f0f0;">177 
178 </span><span style="background-color:#ffffff;">            </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//done
</span><span style="color:#800000;background-color:#f0f0f0;">179 </span><span style="background-color:#ffffff;">            callback();
</span><span style="color:#800000;background-color:#f0f0f0;">180 </span><span style="background-color:#ffffff;">        });
</span><span style="color:#800000;background-color:#f0f0f0;">181 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">182 
183 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">184 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">185 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">186 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">187 </span><span style="background-color:#ffffff;">    _self.displayVelocity = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(vLinear, vAngular) {
</span><span style="color:#800000;background-color:#f0f0f0;">188 
189 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> editable = angular.element($(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"#editableDivId"</span><span style="background-color:#ffffff;">)).scope();
</span><span style="color:#800000;background-color:#f0f0f0;">190 
191 </span><span style="background-color:#ffffff;">        editable.editables.vx = parseFloat(vLinear[</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">].toFixed(</span><span style="color:#0000ff;background-color:#ffffff;">3</span><span style="background-color:#ffffff;">));
</span><span style="color:#800000;background-color:#f0f0f0;">192 </span><span style="background-color:#ffffff;">        editable.editables.vy = parseFloat(vLinear[</span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">].toFixed(</span><span style="color:#0000ff;background-color:#ffffff;">3</span><span style="background-color:#ffffff;">));
</span><span style="color:#800000;background-color:#f0f0f0;">193 </span><span style="background-color:#ffffff;">        editable.editables.vz = parseFloat(vLinear[</span><span style="color:#0000ff;background-color:#ffffff;">2</span><span style="background-color:#ffffff;">].toFixed(</span><span style="color:#0000ff;background-color:#ffffff;">3</span><span style="background-color:#ffffff;">));
</span><span style="color:#800000;background-color:#f0f0f0;">194 
195 </span><span style="background-color:#ffffff;">        editable.editables.ax = parseFloat(vAngular[</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">].toFixed(</span><span style="color:#0000ff;background-color:#ffffff;">3</span><span style="background-color:#ffffff;">));
</span><span style="color:#800000;background-color:#f0f0f0;">196 </span><span style="background-color:#ffffff;">        editable.editables.ay = parseFloat(vAngular[</span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">].toFixed(</span><span style="color:#0000ff;background-color:#ffffff;">3</span><span style="background-color:#ffffff;">));
</span><span style="color:#800000;background-color:#f0f0f0;">197 </span><span style="background-color:#ffffff;">        editable.editables.az = parseFloat(vAngular[</span><span style="color:#0000ff;background-color:#ffffff;">2</span><span style="background-color:#ffffff;">].toFixed(</span><span style="color:#0000ff;background-color:#ffffff;">3</span><span style="background-color:#ffffff;">));
</span><span style="color:#800000;background-color:#f0f0f0;">198 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">199 
200 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">201 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// item selected callback
</span><span style="color:#800000;background-color:#f0f0f0;">202 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">203 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">204 </span><span style="background-color:#ffffff;">    _self.onItemSelected = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (event) {
</span><span style="color:#800000;background-color:#f0f0f0;">205 
206 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> dbId = event.dbIdArray[</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">];
</span><span style="color:#800000;background-color:#f0f0f0;">207 
208 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;">(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">typeof</span><span style="background-color:#ffffff;"> dbId === </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'undefined'</span><span style="background-color:#ffffff;">) {
</span><span style="color:#800000;background-color:#f0f0f0;">209 </span><span style="background-color:#ffffff;">            $(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'#editableDivId'</span><span style="background-color:#ffffff;">).css(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'visibility'</span><span style="background-color:#ffffff;">,</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'collapse'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">210 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">211 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;">212 
213 </span><span style="background-color:#ffffff;">        $(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'#editableDivId'</span><span style="background-color:#ffffff;">).css(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'visibility'</span><span style="background-color:#ffffff;">,</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'visible'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">214 
215 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> fragId = event.fragIdsArray[</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">]
</span><span style="color:#800000;background-color:#f0f0f0;">216 
217 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> fragIdsArray = (Array.isArray(fragId) ?
</span><span style="color:#800000;background-color:#f0f0f0;">218 </span><span style="background-color:#ffffff;">            fragId :
</span><span style="color:#800000;background-color:#f0f0f0;">219 </span><span style="background-color:#ffffff;">            [fragId]);
</span><span style="color:#800000;background-color:#f0f0f0;">220 
221 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> subFragId = fragIdsArray[</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">];
</span><span style="color:#800000;background-color:#f0f0f0;">222 
223 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> vLinear = _meshMap[subFragId].vLinear;
</span><span style="color:#800000;background-color:#f0f0f0;">224 
225 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> vAngular = _meshMap[subFragId].vAngular;
</span><span style="color:#800000;background-color:#f0f0f0;">226 
227 </span><span style="background-color:#ffffff;">        _self.displayVelocity(vLinear, vAngular);
</span><span style="color:#800000;background-color:#f0f0f0;">228 
229 </span><span style="background-color:#ffffff;">        _selectedEntry = _meshMap[subFragId];
</span><span style="color:#800000;background-color:#f0f0f0;">230 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">231 
232 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">233 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Creates control panel
</span><span style="color:#800000;background-color:#f0f0f0;">234 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">235 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">236 </span><span style="background-color:#ffffff;">    _self.loadPanel = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">() {
</span><span style="color:#800000;background-color:#f0f0f0;">237 
238 </span><span style="background-color:#ffffff;">        Autodesk.ADN.Viewing.Extension.Physics.ControlPanel = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(
</span><span style="color:#800000;background-color:#f0f0f0;">239 </span><span style="background-color:#ffffff;">            parentContainer,
</span><span style="color:#800000;background-color:#f0f0f0;">240 </span><span style="background-color:#ffffff;">            id,
</span><span style="color:#800000;background-color:#f0f0f0;">241 </span><span style="background-color:#ffffff;">            title,
</span><span style="color:#800000;background-color:#f0f0f0;">242 </span><span style="background-color:#ffffff;">            content,
</span><span style="color:#800000;background-color:#f0f0f0;">243 </span><span style="background-color:#ffffff;">            x, y)
</span><span style="color:#800000;background-color:#f0f0f0;">244 </span><span style="background-color:#ffffff;">        {
</span><span style="color:#800000;background-color:#f0f0f0;">245 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.content = content;
</span><span style="color:#800000;background-color:#f0f0f0;">246 
247 </span><span style="background-color:#ffffff;">            Autodesk.Viewing.UI.DockingPanel.call(
</span><span style="color:#800000;background-color:#f0f0f0;">248 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">249 </span><span style="background-color:#ffffff;">                parentContainer,
</span><span style="color:#800000;background-color:#f0f0f0;">250 </span><span style="background-color:#ffffff;">                id, </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">''</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">251 </span><span style="background-color:#ffffff;">                {shadow:</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true</span><span style="background-color:#ffffff;">});
</span><span style="color:#800000;background-color:#f0f0f0;">252 
253 </span><span style="background-color:#ffffff;">            </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Auto-fit to the content and don't allow resize.
</span><span style="color:#800000;background-color:#f0f0f0;">254 </span><span style="background-color:#ffffff;">            </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Position at the given coordinates
</span><span style="color:#800000;background-color:#f0f0f0;">255 
256 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.container.style.top = y + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"px"</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">257 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.container.style.left = x + </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"px"</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">258 
259 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.container.style.width = </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"auto"</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">260 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.container.style.height = </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"auto"</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">261 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.container.style.resize = </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"none"</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">262 </span><span style="background-color:#ffffff;">        };
</span><span style="color:#800000;background-color:#f0f0f0;">263 
264 </span><span style="background-color:#ffffff;">        Autodesk.ADN.Viewing.Extension.Physics.
</span><span style="color:#800000;background-color:#f0f0f0;">265 </span><span style="background-color:#ffffff;">            ControlPanel.prototype = Object.create(
</span><span style="color:#800000;background-color:#f0f0f0;">266 </span><span style="background-color:#ffffff;">                Autodesk.Viewing.UI.DockingPanel.prototype);
</span><span style="color:#800000;background-color:#f0f0f0;">267 
268 </span><span style="background-color:#ffffff;">        Autodesk.ADN.Viewing.Extension.Physics.
</span><span style="color:#800000;background-color:#f0f0f0;">269 </span><span style="background-color:#ffffff;">            ControlPanel.prototype.constructor =
</span><span style="color:#800000;background-color:#f0f0f0;">270 </span><span style="background-color:#ffffff;">                Autodesk.ADN.Viewing.Extension.Physics.ControlPanel;
</span><span style="color:#800000;background-color:#f0f0f0;">271 
272 </span><span style="background-color:#ffffff;">        Autodesk.ADN.Viewing.Extension.Physics.
</span><span style="color:#800000;background-color:#f0f0f0;">273 </span><span style="background-color:#ffffff;">            ControlPanel.prototype.initialize = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">() {
</span><span style="color:#800000;background-color:#f0f0f0;">274 
275 </span><span style="background-color:#ffffff;">            </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Override DockingPanel initialize() to:
</span><span style="color:#800000;background-color:#f0f0f0;">276 </span><span style="background-color:#ffffff;">            </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// - create a standard title bar
</span><span style="color:#800000;background-color:#f0f0f0;">277 </span><span style="background-color:#ffffff;">            </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// - click anywhere on the panel to move
</span><span style="color:#800000;background-color:#f0f0f0;">278 </span><span style="background-color:#ffffff;">            </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// - create a close element at the bottom right
</span><span style="color:#800000;background-color:#f0f0f0;">279 </span><span style="background-color:#ffffff;">            </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">280 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.title = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.createTitleBar(
</span><span style="color:#800000;background-color:#f0f0f0;">281 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.titleLabel ||
</span><span style="color:#800000;background-color:#f0f0f0;">282 </span><span style="background-color:#ffffff;">                </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.container.id);
</span><span style="color:#800000;background-color:#f0f0f0;">283 
284 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.container.appendChild(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.title);
</span><span style="color:#800000;background-color:#f0f0f0;">285 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.container.appendChild(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.content);
</span><span style="color:#800000;background-color:#f0f0f0;">286 
287 </span><span style="background-color:#ffffff;">            </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//this.initializeMoveHandlers(this.container);
</span><span style="color:#800000;background-color:#f0f0f0;">288 
289 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.closer = document.createElement(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"div"</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">290 
291 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.closer.className = </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"AdnPanelClose"</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">292 </span><span style="background-color:#ffffff;">            </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//this.closer.textContent = "Close";
</span><span style="color:#800000;background-color:#f0f0f0;">293 
294 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.initializeCloseHandler(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.closer);
</span><span style="color:#800000;background-color:#f0f0f0;">295 
296 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.container.appendChild(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.closer);
</span><span style="color:#800000;background-color:#f0f0f0;">297 </span><span style="background-color:#ffffff;">        };
</span><span style="color:#800000;background-color:#f0f0f0;">298 
299 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> content = document.createElement(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'div'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">300 
301 </span><span style="background-color:#ffffff;">        content.id = </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'physicsDivId'</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">302 
303 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> panel = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Autodesk.ADN.Viewing.Extension.Physics.
</span><span style="color:#800000;background-color:#f0f0f0;">304 </span><span style="background-color:#ffffff;">            ControlPanel(
</span><span style="color:#800000;background-color:#f0f0f0;">305 </span><span style="background-color:#ffffff;">                _viewer.clientContainer,
</span><span style="color:#800000;background-color:#f0f0f0;">306 </span><span style="background-color:#ffffff;">                </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Physics'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">307 </span><span style="background-color:#ffffff;">                </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Physics'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">308 </span><span style="background-color:#ffffff;">                content,
</span><span style="color:#800000;background-color:#f0f0f0;">309 </span><span style="background-color:#ffffff;">                </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">310 
311 </span><span style="background-color:#ffffff;">        $(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'#physicsDivId'</span><span style="background-color:#ffffff;">).css(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'color'</span><span style="background-color:#ffffff;">, </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'white'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">312 
313 </span><span style="background-color:#ffffff;">        panel.setVisible(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">314 
315 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> appScope = angular.element($(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"#appBodyId"</span><span style="background-color:#ffffff;">)).scope();
</span><span style="color:#800000;background-color:#f0f0f0;">316 
317 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> format = </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'&lt;a href="#" editable-number="editables.%1" '</span><span style="background-color:#ffffff;"> +
</span><span style="color:#800000;background-color:#f0f0f0;">318 </span><span style="background-color:#ffffff;">            </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'e-step="any" e-style="width:100px" '</span><span style="background-color:#ffffff;"> +
</span><span style="color:#800000;background-color:#f0f0f0;">319 </span><span style="background-color:#ffffff;">            </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'onaftersave="afterSave()"&gt;{{editables.%1}}&lt;/a&gt;'
</span><span style="color:#800000;background-color:#f0f0f0;">320 
321 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> html =
</span><span style="color:#800000;background-color:#f0f0f0;">322 </span><span style="background-color:#ffffff;">            </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'&lt;button id="startBtnId" type="button" style="color:#000000;width:100px"&gt;Start&lt;/button&gt;'</span><span style="background-color:#ffffff;"> +
</span><span style="color:#800000;background-color:#f0f0f0;">323 </span><span style="background-color:#ffffff;">            </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'&lt;button id="resetBtnId" type="button" style="color:#000000;width:100px"&gt;Reset&lt;/button&gt;'</span><span style="background-color:#ffffff;"> +
</span><span style="color:#800000;background-color:#f0f0f0;">324 </span><span style="background-color:#ffffff;">            </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'&lt;div id="editableDivId" ng-controller="editableController" style="visibility: collapse"&gt;'</span><span style="background-color:#ffffff;"> +
</span><span style="color:#800000;background-color:#f0f0f0;">325 </span><span style="background-color:#ffffff;">            </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'&lt;br&gt;'</span><span style="background-color:#ffffff;"> +
</span><span style="color:#800000;background-color:#f0f0f0;">326 </span><span style="background-color:#ffffff;">            </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'&lt;br&gt;&nbsp Linear Velocity: '</span><span style="background-color:#ffffff;"> +
</span><span style="color:#800000;background-color:#f0f0f0;">327 </span><span style="background-color:#ffffff;">            </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'&lt;br&gt; &nbsp Vx = '</span><span style="background-color:#ffffff;"> + format.replaceAll(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'%1'</span><span style="background-color:#ffffff;">, </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'vx'</span><span style="background-color:#ffffff;">) +
</span><span style="color:#800000;background-color:#f0f0f0;">328 </span><span style="background-color:#ffffff;">            </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'&lt;br&gt; &nbsp Vy = '</span><span style="background-color:#ffffff;"> + format.replaceAll(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'%1'</span><span style="background-color:#ffffff;">, </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'vy'</span><span style="background-color:#ffffff;">) +
</span><span style="color:#800000;background-color:#f0f0f0;">329 </span><span style="background-color:#ffffff;">            </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'&lt;br&gt; &nbsp Vz = '</span><span style="background-color:#ffffff;"> + format.replaceAll(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'%1'</span><span style="background-color:#ffffff;">, </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'vz'</span><span style="background-color:#ffffff;">) +
</span><span style="color:#800000;background-color:#f0f0f0;">330 </span><span style="background-color:#ffffff;">            </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'&lt;br&gt;&lt;br&gt;&nbsp Angular Velocity: '</span><span style="background-color:#ffffff;"> +
</span><span style="color:#800000;background-color:#f0f0f0;">331 </span><span style="background-color:#ffffff;">            </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'&lt;br&gt; &nbsp Ax = '</span><span style="background-color:#ffffff;"> + format.replaceAll(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'%1'</span><span style="background-color:#ffffff;">, </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'ax'</span><span style="background-color:#ffffff;">) +
</span><span style="color:#800000;background-color:#f0f0f0;">332 </span><span style="background-color:#ffffff;">            </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'&lt;br&gt; &nbsp Ay = '</span><span style="background-color:#ffffff;"> + format.replaceAll(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'%1'</span><span style="background-color:#ffffff;">, </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'ay'</span><span style="background-color:#ffffff;">) +
</span><span style="color:#800000;background-color:#f0f0f0;">333 </span><span style="background-color:#ffffff;">            </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'&lt;br&gt; &nbsp Az = '</span><span style="background-color:#ffffff;"> + format.replaceAll(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'%1'</span><span style="background-color:#ffffff;">, </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'az'</span><span style="background-color:#ffffff;">) +
</span><span style="color:#800000;background-color:#f0f0f0;">334 </span><span style="background-color:#ffffff;">            </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'&lt;/div&gt;'
</span><span style="color:#800000;background-color:#f0f0f0;">335 
336 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> element = appScope.compile(html);
</span><span style="color:#800000;background-color:#f0f0f0;">337 
338 </span><span style="background-color:#ffffff;">        $(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'#physicsDivId'</span><span style="background-color:#ffffff;">).append(element);
</span><span style="color:#800000;background-color:#f0f0f0;">339 
340 </span><span style="background-color:#ffffff;">        _self.displayVelocity([</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">,</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">,</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">], [</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">,</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">,</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">]);
</span><span style="color:#800000;background-color:#f0f0f0;">341 
342 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> editable = angular.element($(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"#editableDivId"</span><span style="background-color:#ffffff;">)).scope();
</span><span style="color:#800000;background-color:#f0f0f0;">343 
344 </span><span style="background-color:#ffffff;">        editable.onAfterSave = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> () {
</span><span style="color:#800000;background-color:#f0f0f0;">345 
346 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> editables = editable.editables;
</span><span style="color:#800000;background-color:#f0f0f0;">347 
348 </span><span style="background-color:#ffffff;">            _selectedEntry.vAngular = [
</span><span style="color:#800000;background-color:#f0f0f0;">349 </span><span style="background-color:#ffffff;">                editables.ax,
</span><span style="color:#800000;background-color:#f0f0f0;">350 </span><span style="background-color:#ffffff;">                editables.ay,
</span><span style="color:#800000;background-color:#f0f0f0;">351 </span><span style="background-color:#ffffff;">                editables.az
</span><span style="color:#800000;background-color:#f0f0f0;">352 </span><span style="background-color:#ffffff;">            ];
</span><span style="color:#800000;background-color:#f0f0f0;">353 
354 </span><span style="background-color:#ffffff;">            _selectedEntry.vLinear = [
</span><span style="color:#800000;background-color:#f0f0f0;">355 </span><span style="background-color:#ffffff;">                editables.vx,
</span><span style="color:#800000;background-color:#f0f0f0;">356 </span><span style="background-color:#ffffff;">                editables.vy,
</span><span style="color:#800000;background-color:#f0f0f0;">357 </span><span style="background-color:#ffffff;">                editables.vz
</span><span style="color:#800000;background-color:#f0f0f0;">358 </span><span style="background-color:#ffffff;">            ];
</span><span style="color:#800000;background-color:#f0f0f0;">359 
360 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;">(!_started) {
</span><span style="color:#800000;background-color:#f0f0f0;">361 
362 </span><span style="background-color:#ffffff;">                _selectedEntry.vAngularInit =
</span><span style="color:#800000;background-color:#f0f0f0;">363 </span><span style="background-color:#ffffff;">                    _selectedEntry.vAngular;
</span><span style="color:#800000;background-color:#f0f0f0;">364 
365 </span><span style="background-color:#ffffff;">                _selectedEntry.vLinearInit =
</span><span style="color:#800000;background-color:#f0f0f0;">366 </span><span style="background-color:#ffffff;">                    _selectedEntry.vLinear;
</span><span style="color:#800000;background-color:#f0f0f0;">367 </span><span style="background-color:#ffffff;">            }
</span><span style="color:#800000;background-color:#f0f0f0;">368 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;">369 
370 </span><span style="background-color:#ffffff;">        _fps = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> FPSMeter(content, {
</span><span style="color:#800000;background-color:#f0f0f0;">371 </span><span style="background-color:#ffffff;">            smoothing: </span><span style="color:#0000ff;background-color:#ffffff;">10</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">372 </span><span style="background-color:#ffffff;">            show: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'fps'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">373 </span><span style="background-color:#ffffff;">            toggleOn: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'click'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">374 </span><span style="background-color:#ffffff;">            decimals: </span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">375 </span><span style="background-color:#ffffff;">            zIndex: </span><span style="color:#0000ff;background-color:#ffffff;">999</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">376 </span><span style="background-color:#ffffff;">            left: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'5px'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">377 </span><span style="background-color:#ffffff;">            top: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'60px'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">378 </span><span style="background-color:#ffffff;">            theme: </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'transparent'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">379 </span><span style="background-color:#ffffff;">            heat: </span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">380 </span><span style="background-color:#ffffff;">            graph: </span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">381 </span><span style="background-color:#ffffff;">            history: </span><span style="color:#0000ff;background-color:#ffffff;">32</span><span style="background-color:#ffffff;">});
</span><span style="color:#800000;background-color:#f0f0f0;">382 
383 </span><span style="background-color:#ffffff;">        $(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'#startBtnId'</span><span style="background-color:#ffffff;">).click(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> () {
</span><span style="color:#800000;background-color:#f0f0f0;">384 
385 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;"> (_animationId) {
</span><span style="color:#800000;background-color:#f0f0f0;">386 
387 </span><span style="background-color:#ffffff;">                $(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"#startBtnId"</span><span style="background-color:#ffffff;">).text(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Start'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">388 
389 </span><span style="background-color:#ffffff;">                _self.stop();
</span><span style="color:#800000;background-color:#f0f0f0;">390 </span><span style="background-color:#ffffff;">            }
</span><span style="color:#800000;background-color:#f0f0f0;">391 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">else</span><span style="background-color:#ffffff;"> {
</span><span style="color:#800000;background-color:#f0f0f0;">392 
393 </span><span style="background-color:#ffffff;">                $(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"#startBtnId"</span><span style="background-color:#ffffff;">).text(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Stop'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">394 
395 </span><span style="background-color:#ffffff;">                _self.start();
</span><span style="color:#800000;background-color:#f0f0f0;">396 </span><span style="background-color:#ffffff;">            }
</span><span style="color:#800000;background-color:#f0f0f0;">397 </span><span style="background-color:#ffffff;">        })
</span><span style="color:#800000;background-color:#f0f0f0;">398 
399 </span><span style="background-color:#ffffff;">        $(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'#resetBtnId'</span><span style="background-color:#ffffff;">).click(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> () {
</span><span style="color:#800000;background-color:#f0f0f0;">400 
401 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;">(_running) {
</span><span style="color:#800000;background-color:#f0f0f0;">402 
403 </span><span style="background-color:#ffffff;">                $(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"#startBtnId"</span><span style="background-color:#ffffff;">).text(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Start'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">404 
405 </span><span style="background-color:#ffffff;">                _self.stop();
</span><span style="color:#800000;background-color:#f0f0f0;">406 </span><span style="background-color:#ffffff;">            }
</span><span style="color:#800000;background-color:#f0f0f0;">407 
408 </span><span style="background-color:#ffffff;">            _self.reset();
</span><span style="color:#800000;background-color:#f0f0f0;">409 </span><span style="background-color:#ffffff;">        })
</span><span style="color:#800000;background-color:#f0f0f0;">410 
411 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> panel;
</span><span style="color:#800000;background-color:#f0f0f0;">412 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">413 
414 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">415 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Creates physics world
</span><span style="color:#800000;background-color:#f0f0f0;">416 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">417 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">418 </span><span style="background-color:#ffffff;">    _self.createWorld = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">() {
</span><span style="color:#800000;background-color:#f0f0f0;">419 
420 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> collisionConfiguration =
</span><span style="color:#800000;background-color:#f0f0f0;">421 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btDefaultCollisionConfiguration;
</span><span style="color:#800000;background-color:#f0f0f0;">422 
423 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> world = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btDiscreteDynamicsWorld(
</span><span style="color:#800000;background-color:#f0f0f0;">424 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btCollisionDispatcher(collisionConfiguration),
</span><span style="color:#800000;background-color:#f0f0f0;">425 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btDbvtBroadphase,
</span><span style="color:#800000;background-color:#f0f0f0;">426 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btSequentialImpulseConstraintSolver,
</span><span style="color:#800000;background-color:#f0f0f0;">427 </span><span style="background-color:#ffffff;">            collisionConfiguration);
</span><span style="color:#800000;background-color:#f0f0f0;">428 
429 </span><span style="background-color:#ffffff;">        world.setGravity(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btVector3(</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, -</span><span style="color:#0000ff;background-color:#ffffff;">9.8</span><span style="background-color:#ffffff;">));
</span><span style="color:#800000;background-color:#f0f0f0;">430 
431 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> world;
</span><span style="color:#800000;background-color:#f0f0f0;">432 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">433 
434 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">435 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Starts simulation
</span><span style="color:#800000;background-color:#f0f0f0;">436 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">437 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">438 </span><span style="background-color:#ffffff;">    _self.start = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">() {
</span><span style="color:#800000;background-color:#f0f0f0;">439 
440 </span><span style="background-color:#ffffff;">        _viewer.select([]);
</span><span style="color:#800000;background-color:#f0f0f0;">441 
442 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// force update
</span><span style="color:#800000;background-color:#f0f0f0;">443 </span><span style="background-color:#ffffff;">        _viewer.setView(_viewer.getCurrentView());
</span><span style="color:#800000;background-color:#f0f0f0;">444 
445 </span><span style="background-color:#ffffff;">        _world = _self.createWorld();
</span><span style="color:#800000;background-color:#f0f0f0;">446 
447 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">for</span><span style="background-color:#ffffff;">(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> key </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">in</span><span style="background-color:#ffffff;"> _meshMap){
</span><span style="color:#800000;background-color:#f0f0f0;">448 
449 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> entry = _meshMap[key];
</span><span style="color:#800000;background-color:#f0f0f0;">450 
451 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> body = createRigidBody(
</span><span style="color:#800000;background-color:#f0f0f0;">452 </span><span style="background-color:#ffffff;">                entry);
</span><span style="color:#800000;background-color:#f0f0f0;">453 
454 </span><span style="background-color:#ffffff;">            _world.addRigidBody(body);
</span><span style="color:#800000;background-color:#f0f0f0;">455 
456 </span><span style="background-color:#ffffff;">            entry.body = body;
</span><span style="color:#800000;background-color:#f0f0f0;">457 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;">458 
459 </span><span style="background-color:#ffffff;">        _running = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">460 
461 </span><span style="background-color:#ffffff;">        _started = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">462 
463 </span><span style="background-color:#ffffff;">        _stopWatch.getElapsedMs();
</span><span style="color:#800000;background-color:#f0f0f0;">464 
465 </span><span style="background-color:#ffffff;">        _self.update();
</span><span style="color:#800000;background-color:#f0f0f0;">466 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">467 
468 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">469 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Stops simulation
</span><span style="color:#800000;background-color:#f0f0f0;">470 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">471 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">472 </span><span style="background-color:#ffffff;">    _self.stop = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">() {
</span><span style="color:#800000;background-color:#f0f0f0;">473 
474 </span><span style="background-color:#ffffff;">        </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// save current velocities
</span><span style="color:#800000;background-color:#f0f0f0;">475 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">for</span><span style="background-color:#ffffff;">(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> key </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">in</span><span style="background-color:#ffffff;"> _meshMap){
</span><span style="color:#800000;background-color:#f0f0f0;">476 
477 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> entry = _meshMap[key];
</span><span style="color:#800000;background-color:#f0f0f0;">478 
479 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> va = entry.body.getAngularVelocity();
</span><span style="color:#800000;background-color:#f0f0f0;">480 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> vl = entry.body.getLinearVelocity();
</span><span style="color:#800000;background-color:#f0f0f0;">481 
482 </span><span style="background-color:#ffffff;">            entry.vAngular = [va.x(), va.y(), va.z()]
</span><span style="color:#800000;background-color:#f0f0f0;">483 </span><span style="background-color:#ffffff;">            entry.vLinear = [vl.x(), vl.y(), vl.z()]
</span><span style="color:#800000;background-color:#f0f0f0;">484 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;">485 
486 </span><span style="background-color:#ffffff;">        _running = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">487 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">488 
489 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">490 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Update loop
</span><span style="color:#800000;background-color:#f0f0f0;">491 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">492 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">493 </span><span style="background-color:#ffffff;">    _self.update = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">() {
</span><span style="color:#800000;background-color:#f0f0f0;">494 
495 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">if</span><span style="background-color:#ffffff;">(!_running) {
</span><span style="color:#800000;background-color:#f0f0f0;">496 
497 </span><span style="background-color:#ffffff;">            cancelAnimationFrame(_animationId);
</span><span style="color:#800000;background-color:#f0f0f0;">498 
499 </span><span style="background-color:#ffffff;">            _animationId = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">null</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">500 
501 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">502 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;">503 
504 </span><span style="background-color:#ffffff;">        _animationId = requestAnimationFrame(
</span><span style="color:#800000;background-color:#f0f0f0;">505 </span><span style="background-color:#ffffff;">            _self.update);
</span><span style="color:#800000;background-color:#f0f0f0;">506 
507 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> dt = _stopWatch.getElapsedMs() * </span><span style="color:#0000ff;background-color:#ffffff;">0.002</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">508 
509 </span><span style="background-color:#ffffff;">        dt = (dt &gt; </span><span style="color:#0000ff;background-color:#ffffff;">0.5</span><span style="background-color:#ffffff;"> ? </span><span style="color:#0000ff;background-color:#ffffff;">0.5</span><span style="background-color:#ffffff;"> : dt);
</span><span style="color:#800000;background-color:#f0f0f0;">510 
511 </span><span style="background-color:#ffffff;">        _world.stepSimulation(
</span><span style="color:#800000;background-color:#f0f0f0;">512 </span><span style="background-color:#ffffff;">           dt, </span><span style="color:#0000ff;background-color:#ffffff;">10</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">513 
514 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">for</span><span style="background-color:#ffffff;">(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> key </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">in</span><span style="background-color:#ffffff;"> _meshMap) {
</span><span style="color:#800000;background-color:#f0f0f0;">515 
516 </span><span style="background-color:#ffffff;">            updateMeshTransform(_meshMap[key].body);
</span><span style="color:#800000;background-color:#f0f0f0;">517 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;">518 
519 </span><span style="background-color:#ffffff;">        _viewer.impl.invalidate(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">520 
521 </span><span style="background-color:#ffffff;">        _fps.tick();
</span><span style="color:#800000;background-color:#f0f0f0;">522 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">523 
524 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">525 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Reset simulation
</span><span style="color:#800000;background-color:#f0f0f0;">526 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">527 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">528 </span><span style="background-color:#ffffff;">    _self.reset = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">() {
</span><span style="color:#800000;background-color:#f0f0f0;">529 
530 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">for</span><span style="background-color:#ffffff;">(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> key </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">in</span><span style="background-color:#ffffff;"> _meshMap) {
</span><span style="color:#800000;background-color:#f0f0f0;">531 
532 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> entry = _meshMap[key];
</span><span style="color:#800000;background-color:#f0f0f0;">533 
534 </span><span style="background-color:#ffffff;">            entry.mesh.matrixWorld =
</span><span style="color:#800000;background-color:#f0f0f0;">535 </span><span style="background-color:#ffffff;">                entry.transform.clone();
</span><span style="color:#800000;background-color:#f0f0f0;">536 
537 </span><span style="background-color:#ffffff;">            entry.vAngular = entry.vAngularInit;
</span><span style="color:#800000;background-color:#f0f0f0;">538 
539 </span><span style="background-color:#ffffff;">            entry.vLinear = entry.vLinearInit;
</span><span style="color:#800000;background-color:#f0f0f0;">540 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;">541 
542 </span><span style="background-color:#ffffff;">        _viewer.impl.invalidate(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">543 
544 </span><span style="background-color:#ffffff;">        _started = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">545 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">546 
547 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">548 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Parses string to array: a1;a2;a3 -&gt; [a1, a2, a3]
</span><span style="color:#800000;background-color:#f0f0f0;">549 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">550 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">551 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> parseArray(str, separator) {
</span><span style="color:#800000;background-color:#f0f0f0;">552 
553 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> array = str.split(separator);
</span><span style="color:#800000;background-color:#f0f0f0;">554 
555 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> result = [];
</span><span style="color:#800000;background-color:#f0f0f0;">556 
557 </span><span style="background-color:#ffffff;">        array.forEach(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(element){
</span><span style="color:#800000;background-color:#f0f0f0;">558 
559 </span><span style="background-color:#ffffff;">            result.push(parseFloat(element));
</span><span style="color:#800000;background-color:#f0f0f0;">560 </span><span style="background-color:#ffffff;">        });
</span><span style="color:#800000;background-color:#f0f0f0;">561 
562 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> result;
</span><span style="color:#800000;background-color:#f0f0f0;">563 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">564 
565 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">566 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Updates mesh transform according to physic body
</span><span style="color:#800000;background-color:#f0f0f0;">567 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">568 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">569 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> updateMeshTransform(body) {
</span><span style="color:#800000;background-color:#f0f0f0;">570 
571 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> mesh = body.mesh;
</span><span style="color:#800000;background-color:#f0f0f0;">572 
573 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> transform = body.getCenterOfMassTransform();
</span><span style="color:#800000;background-color:#f0f0f0;">574 
575 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> origin = transform.getOrigin();
</span><span style="color:#800000;background-color:#f0f0f0;">576 
577 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> q = transform.getRotation();
</span><span style="color:#800000;background-color:#f0f0f0;">578 
579 </span><span style="background-color:#ffffff;">        mesh.matrixWorld.makeRotationFromQuaternion({
</span><span style="color:#800000;background-color:#f0f0f0;">580 </span><span style="background-color:#ffffff;">            x: q.x(),
</span><span style="color:#800000;background-color:#f0f0f0;">581 </span><span style="background-color:#ffffff;">            y: q.y(),
</span><span style="color:#800000;background-color:#f0f0f0;">582 </span><span style="background-color:#ffffff;">            z: q.z(),
</span><span style="color:#800000;background-color:#f0f0f0;">583 </span><span style="background-color:#ffffff;">            w: q.w()
</span><span style="color:#800000;background-color:#f0f0f0;">584 </span><span style="background-color:#ffffff;">        });
</span><span style="color:#800000;background-color:#f0f0f0;">585 
586 </span><span style="background-color:#ffffff;">        mesh.matrixWorld.setPosition(
</span><span style="color:#800000;background-color:#f0f0f0;">587 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Vector3(
</span><span style="color:#800000;background-color:#f0f0f0;">588 </span><span style="background-color:#ffffff;">                origin.x(),
</span><span style="color:#800000;background-color:#f0f0f0;">589 </span><span style="background-color:#ffffff;">                origin.y(),
</span><span style="color:#800000;background-color:#f0f0f0;">590 </span><span style="background-color:#ffffff;">                origin.z()));
</span><span style="color:#800000;background-color:#f0f0f0;">591 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">592 
593 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">594 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Returns mesh position
</span><span style="color:#800000;background-color:#f0f0f0;">595 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">596 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">597 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> getMeshPosition(mesh) {
</span><span style="color:#800000;background-color:#f0f0f0;">598 
599 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> pos = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Vector3();
</span><span style="color:#800000;background-color:#f0f0f0;">600 
601 </span><span style="background-color:#ffffff;">        pos.setFromMatrixPosition(mesh.matrixWorld);
</span><span style="color:#800000;background-color:#f0f0f0;">602 
603 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> pos;
</span><span style="color:#800000;background-color:#f0f0f0;">604 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">605 
606 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">607 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Creates collision shape based on mesh vertices
</span><span style="color:#800000;background-color:#f0f0f0;">608 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">609 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">610 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> createCollisionShape(mesh) {
</span><span style="color:#800000;background-color:#f0f0f0;">611 
612 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> geometry = mesh.geometry;
</span><span style="color:#800000;background-color:#f0f0f0;">613 
614 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> hull = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btConvexHullShape();
</span><span style="color:#800000;background-color:#f0f0f0;">615 
616 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> vertexBuffer = geometry.vb;
</span><span style="color:#800000;background-color:#f0f0f0;">617 
618 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">for</span><span style="background-color:#ffffff;">(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> i=</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">; i &lt; vertexBuffer.length; i += geometry.vbstride) {
</span><span style="color:#800000;background-color:#f0f0f0;">619 
620 </span><span style="background-color:#ffffff;">            hull.addPoint(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btVector3(
</span><span style="color:#800000;background-color:#f0f0f0;">621 </span><span style="background-color:#ffffff;">                vertexBuffer[i],
</span><span style="color:#800000;background-color:#f0f0f0;">622 </span><span style="background-color:#ffffff;">                vertexBuffer[i+</span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">],
</span><span style="color:#800000;background-color:#f0f0f0;">623 </span><span style="background-color:#ffffff;">                vertexBuffer[i+</span><span style="color:#0000ff;background-color:#ffffff;">2</span><span style="background-color:#ffffff;">]));
</span><span style="color:#800000;background-color:#f0f0f0;">624 </span><span style="background-color:#ffffff;">        }
</span><span style="color:#800000;background-color:#f0f0f0;">625 
626 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> hull;
</span><span style="color:#800000;background-color:#f0f0f0;">627 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">628 
629 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">630 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Creates physic rigid body from mesh
</span><span style="color:#800000;background-color:#f0f0f0;">631 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">632 </span><span style="background-color:#ffffff;">    </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">633 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> createRigidBody(entry) {
</span><span style="color:#800000;background-color:#f0f0f0;">634 
635 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> localInertia = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btVector3(</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">, </span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">636 
637 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> shape = createCollisionShape(entry.mesh);
</span><span style="color:#800000;background-color:#f0f0f0;">638 
639 </span><span style="background-color:#ffffff;">        shape.calculateLocalInertia(entry.mass, localInertia);
</span><span style="color:#800000;background-color:#f0f0f0;">640 
641 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> transform = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btTransform;
</span><span style="color:#800000;background-color:#f0f0f0;">642 
643 </span><span style="background-color:#ffffff;">        transform.setIdentity();
</span><span style="color:#800000;background-color:#f0f0f0;">644 
645 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> position = getMeshPosition(entry.mesh);
</span><span style="color:#800000;background-color:#f0f0f0;">646 
647 </span><span style="background-color:#ffffff;">        transform.setOrigin(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btVector3(
</span><span style="color:#800000;background-color:#f0f0f0;">648 </span><span style="background-color:#ffffff;">            position.x,
</span><span style="color:#800000;background-color:#f0f0f0;">649 </span><span style="background-color:#ffffff;">            position.y,
</span><span style="color:#800000;background-color:#f0f0f0;">650 </span><span style="background-color:#ffffff;">            position.z));
</span><span style="color:#800000;background-color:#f0f0f0;">651 
652 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> q = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> THREE.Quaternion();
</span><span style="color:#800000;background-color:#f0f0f0;">653 
654 </span><span style="background-color:#ffffff;">        q.setFromRotationMatrix(entry.mesh.matrixWorld);
</span><span style="color:#800000;background-color:#f0f0f0;">655 
656 </span><span style="background-color:#ffffff;">        transform.setRotation(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btQuaternion(
</span><span style="color:#800000;background-color:#f0f0f0;">657 </span><span style="background-color:#ffffff;">            q.x, q.y, q.z, q.w
</span><span style="color:#800000;background-color:#f0f0f0;">658 </span><span style="background-color:#ffffff;">        ));
</span><span style="color:#800000;background-color:#f0f0f0;">659 
660 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> motionState = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btDefaultMotionState(transform);
</span><span style="color:#800000;background-color:#f0f0f0;">661 
662 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> rbInfo = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btRigidBodyConstructionInfo(
</span><span style="color:#800000;background-color:#f0f0f0;">663 </span><span style="background-color:#ffffff;">            entry.mass,
</span><span style="color:#800000;background-color:#f0f0f0;">664 </span><span style="background-color:#ffffff;">            motionState,
</span><span style="color:#800000;background-color:#f0f0f0;">665 </span><span style="background-color:#ffffff;">            shape,
</span><span style="color:#800000;background-color:#f0f0f0;">666 </span><span style="background-color:#ffffff;">            localInertia);
</span><span style="color:#800000;background-color:#f0f0f0;">667 
668 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> body = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btRigidBody(rbInfo);
</span><span style="color:#800000;background-color:#f0f0f0;">669 
670 </span><span style="background-color:#ffffff;">        body.setLinearVelocity(
</span><span style="color:#800000;background-color:#f0f0f0;">671 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btVector3(
</span><span style="color:#800000;background-color:#f0f0f0;">672 </span><span style="background-color:#ffffff;">                entry.vLinear[</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">],
</span><span style="color:#800000;background-color:#f0f0f0;">673 </span><span style="background-color:#ffffff;">                entry.vLinear[</span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">],
</span><span style="color:#800000;background-color:#f0f0f0;">674 </span><span style="background-color:#ffffff;">                entry.vLinear[</span><span style="color:#0000ff;background-color:#ffffff;">2</span><span style="background-color:#ffffff;">]));
</span><span style="color:#800000;background-color:#f0f0f0;">675 
676 </span><span style="background-color:#ffffff;">        body.setAngularVelocity(
</span><span style="color:#800000;background-color:#f0f0f0;">677 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> Ammo.btVector3(
</span><span style="color:#800000;background-color:#f0f0f0;">678 </span><span style="background-color:#ffffff;">                entry.vAngular[</span><span style="color:#0000ff;background-color:#ffffff;">0</span><span style="background-color:#ffffff;">],
</span><span style="color:#800000;background-color:#f0f0f0;">679 </span><span style="background-color:#ffffff;">                entry.vAngular[</span><span style="color:#0000ff;background-color:#ffffff;">1</span><span style="background-color:#ffffff;">],
</span><span style="color:#800000;background-color:#f0f0f0;">680 </span><span style="background-color:#ffffff;">                entry.vAngular[</span><span style="color:#0000ff;background-color:#ffffff;">2</span><span style="background-color:#ffffff;">]));
</span><span style="color:#800000;background-color:#f0f0f0;">681 
682 </span><span style="background-color:#ffffff;">        body.mesh = entry.mesh;
</span><span style="color:#800000;background-color:#f0f0f0;">683 
684 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> body;
</span><span style="color:#800000;background-color:#f0f0f0;">685 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">686 </span><span style="background-color:#ffffff;">};
</span><span style="color:#800000;background-color:#f0f0f0;">687 
688 </span><span style="background-color:#ffffff;">Autodesk.ADN.Viewing.Extension.Physics.prototype =
</span><span style="color:#800000;background-color:#f0f0f0;">689 </span><span style="background-color:#ffffff;">    Object.create(Autodesk.Viewing.Extension.prototype);
</span><span style="color:#800000;background-color:#f0f0f0;">690 
691 </span><span style="background-color:#ffffff;">Autodesk.ADN.Viewing.Extension.Physics.prototype.constructor =
</span><span style="color:#800000;background-color:#f0f0f0;">692 </span><span style="background-color:#ffffff;">    Autodesk.ADN.Viewing.Extension.Physics;
</span><span style="color:#800000;background-color:#f0f0f0;">693 
694 </span><span style="background-color:#ffffff;">Autodesk.Viewing.theExtensionManager.registerExtension(
</span><span style="color:#800000;background-color:#f0f0f0;">695 </span><span style="background-color:#ffffff;">    </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Autodesk.ADN.Viewing.Extension.Physics'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">696 </span><span style="background-color:#ffffff;">    Autodesk.ADN.Viewing.Extension.Physics);
</span><span style="color:#800000;background-color:#f0f0f0;">697 </span></pre>
