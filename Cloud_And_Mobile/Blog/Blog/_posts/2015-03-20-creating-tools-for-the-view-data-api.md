---
layout: "post"
title: "Creating \"Tools\" for the View & Data API"
date: "2015-03-20 10:53:02"
author: "Philippe Leefsma"
categories:
  - "Client"
  - "HTML5"
  - "Javascript"
  - "Philippe Leefsma"
  - "View and Data API"
original_url: "https://adndevblog.typepad.com/cloud_and_mobile/2015/03/creating-tools-for-the-view-data-api.html "
typepad_basename: "creating-tools-for-the-view-data-api"
typepad_status: "Publish"
---

<p style="text-align: left;">By&nbsp;<a href="http://adndevblog.typepad.com/cloud_and_mobile/philippe-leefsma.html">Philippe Leefsma</a></p>
<p style="text-align: left;">Here is just a quickie before the weekend showing you how to create a "tool" for the viewer. A tool is basically a custom "class" (in the JavaScript way) implementing the&nbsp;<a title="" href="http://developer.api.autodesk.com/documentation/v1/vs/viewers/Autodesk.Viewing.ToolInterface.html" target="_self">ToolInterface</a>&nbsp;that will give your code easy access to callbacks such as mouse events, keyboard events, touch events and so on...</p>
<p style="text-align: left;">It's pretty straightforward to put in place and use it. The below implementation does not much except dumping to the browser console the various tool callbacks and their parameters. I commented out the "update" and "handleMouseMove" so the console doesn't get too much stuff logged in:</p>




<pre style="line-height: 100%;font-family:monospace;background-color:#ffffff; border-width:0.01mm; border-color:#000000; border-style:solid;padding:4px;font-size:9pt;"><span style="color:#800000;background-color:#f0f0f0;">  1 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">  2 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// Simple Custom Tool viewer extension
</span><span style="color:#800000;background-color:#f0f0f0;">  3 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">// by Philippe Leefsma, March 2015
</span><span style="color:#800000;background-color:#f0f0f0;">  4 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//
</span><span style="color:#800000;background-color:#f0f0f0;">  5 </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">///////////////////////////////////////////////////////////////////
</span><span style="color:#800000;background-color:#f0f0f0;">  6 </span><span style="background-color:#ffffff;">AutodeskNamespace(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"Autodesk.ADN.Viewing.Extension"</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">  7 
  8 </span><span style="background-color:#ffffff;">Autodesk.ADN.Viewing.Extension.CustomTool = 
</span><span style="color:#800000;background-color:#f0f0f0;">  9 </span><span style="background-color:#ffffff;">    
</span><span style="color:#800000;background-color:#f0f0f0;"> 10 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> (viewer, options) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 11 
 12 </span><span style="background-color:#ffffff;">    Autodesk.Viewing.Extension.call(</span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">, viewer, options);
</span><span style="color:#800000;background-color:#f0f0f0;"> 13 
 14 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> _self = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 15 
 16 </span><span style="background-color:#ffffff;">    _self.tool = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">null</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 17 
 18 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> AdnTool(viewer, toolName) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 19 
 20 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.getNames = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">() {
</span><span style="color:#800000;background-color:#f0f0f0;"> 21 
 22 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> [toolName];
</span><span style="color:#800000;background-color:#f0f0f0;"> 23 </span><span style="background-color:#ffffff;">        };
</span><span style="color:#800000;background-color:#f0f0f0;"> 24 
 25 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.getName = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">() {
</span><span style="color:#800000;background-color:#f0f0f0;"> 26 
 27 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> toolName;
</span><span style="color:#800000;background-color:#f0f0f0;"> 28 </span><span style="background-color:#ffffff;">        };
</span><span style="color:#800000;background-color:#f0f0f0;"> 29 
 30 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.activate = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(name) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 31 
 32 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'-------------------'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 33 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Tool:activate(name)'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 34 </span><span style="background-color:#ffffff;">            console.log(name);
</span><span style="color:#800000;background-color:#f0f0f0;"> 35 </span><span style="background-color:#ffffff;">        };
</span><span style="color:#800000;background-color:#f0f0f0;"> 36 
 37 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.deactivate = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(name) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 38 
 39 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'-------------------'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 40 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Tool:deactivate(name)'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 41 </span><span style="background-color:#ffffff;">            console.log(name);
</span><span style="color:#800000;background-color:#f0f0f0;"> 42 </span><span style="background-color:#ffffff;">        };
</span><span style="color:#800000;background-color:#f0f0f0;"> 43 
 44 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.update = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(t) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 45 
 46 </span><span style="background-color:#ffffff;">            </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//console.log('-------------------');
</span><span style="color:#800000;background-color:#f0f0f0;"> 47 </span><span style="background-color:#ffffff;">            </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//console.log('Tool:update(t)');
</span><span style="color:#800000;background-color:#f0f0f0;"> 48 </span><span style="background-color:#ffffff;">            </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//console.log(t);
</span><span style="color:#800000;background-color:#f0f0f0;"> 49 
 50 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 51 </span><span style="background-color:#ffffff;">        };
</span><span style="color:#800000;background-color:#f0f0f0;"> 52 
 53 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.handleSingleClick = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(event, button) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 54 
 55 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'-------------------'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 56 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Tool:handleSingleClick(event, button)'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 57 </span><span style="background-color:#ffffff;">            console.log(event);
</span><span style="color:#800000;background-color:#f0f0f0;"> 58 </span><span style="background-color:#ffffff;">            console.log(button);
</span><span style="color:#800000;background-color:#f0f0f0;"> 59 
 60 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 61 </span><span style="background-color:#ffffff;">        };
</span><span style="color:#800000;background-color:#f0f0f0;"> 62 
 63 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.handleDoubleClick = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(event, button) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 64 
 65 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'-------------------'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 66 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Tool:handleDoubleClick(event, button)'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 67 </span><span style="background-color:#ffffff;">            console.log(event);
</span><span style="color:#800000;background-color:#f0f0f0;"> 68 </span><span style="background-color:#ffffff;">            console.log(button);
</span><span style="color:#800000;background-color:#f0f0f0;"> 69 
 70 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 71 </span><span style="background-color:#ffffff;">        };
</span><span style="color:#800000;background-color:#f0f0f0;"> 72 
 73 
 74 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.handleSingleTap = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(event) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 75 
 76 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'-------------------'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 77 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Tool:handleSingleTap(event)'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 78 </span><span style="background-color:#ffffff;">            console.log(event);
</span><span style="color:#800000;background-color:#f0f0f0;"> 79 
 80 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 81 </span><span style="background-color:#ffffff;">        };
</span><span style="color:#800000;background-color:#f0f0f0;"> 82 
 83 
 84 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.handleDoubleTap = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(event) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 85 
 86 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'-------------------'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 87 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Tool:handleDoubleTap(event)'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 88 </span><span style="background-color:#ffffff;">            console.log(event);
</span><span style="color:#800000;background-color:#f0f0f0;"> 89 
 90 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;"> 91 </span><span style="background-color:#ffffff;">        };
</span><span style="color:#800000;background-color:#f0f0f0;"> 92 
 93 
 94 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.handleKeyDown = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(event, keyCode) {
</span><span style="color:#800000;background-color:#f0f0f0;"> 95 
 96 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'-------------------'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 97 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Tool:handleKeyDown(event, keyCode)'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;"> 98 </span><span style="background-color:#ffffff;">            console.log(event);
</span><span style="color:#800000;background-color:#f0f0f0;"> 99 </span><span style="background-color:#ffffff;">            console.log(keyCode);
</span><span style="color:#800000;background-color:#f0f0f0;">100 
101 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">102 </span><span style="background-color:#ffffff;">        };
</span><span style="color:#800000;background-color:#f0f0f0;">103 
104 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.handleKeyUp = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(event, keyCode) {
</span><span style="color:#800000;background-color:#f0f0f0;">105 
106 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'-------------------'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">107 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Tool:handleKeyUp(event, keyCode)'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">108 </span><span style="background-color:#ffffff;">            console.log(event);
</span><span style="color:#800000;background-color:#f0f0f0;">109 </span><span style="background-color:#ffffff;">            console.log(keyCode);
</span><span style="color:#800000;background-color:#f0f0f0;">110 
111 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">112 </span><span style="background-color:#ffffff;">        };
</span><span style="color:#800000;background-color:#f0f0f0;">113 
114 
115 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.handleWheelInput = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(delta) {
</span><span style="color:#800000;background-color:#f0f0f0;">116 
117 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'-------------------'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">118 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Tool:handleWheelInput(delta)'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">119 </span><span style="background-color:#ffffff;">            console.log(delta);
</span><span style="color:#800000;background-color:#f0f0f0;">120 
121 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">122 </span><span style="background-color:#ffffff;">        };
</span><span style="color:#800000;background-color:#f0f0f0;">123 
124 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.handleButtonDown = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(event, button) {
</span><span style="color:#800000;background-color:#f0f0f0;">125 
126 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'-------------------'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">127 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Tool:handleButtonDown(event, button)'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">128 </span><span style="background-color:#ffffff;">            console.log(event);
</span><span style="color:#800000;background-color:#f0f0f0;">129 </span><span style="background-color:#ffffff;">            console.log(button);
</span><span style="color:#800000;background-color:#f0f0f0;">130 
131 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">132 </span><span style="background-color:#ffffff;">        };
</span><span style="color:#800000;background-color:#f0f0f0;">133 
134 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.handleButtonUp = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(event, button) {
</span><span style="color:#800000;background-color:#f0f0f0;">135 
136 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'-------------------'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">137 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Tool:handleButtonUp(event, button)'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">138 </span><span style="background-color:#ffffff;">            console.log(event);
</span><span style="color:#800000;background-color:#f0f0f0;">139 </span><span style="background-color:#ffffff;">            console.log(button);
</span><span style="color:#800000;background-color:#f0f0f0;">140 
141 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">142 </span><span style="background-color:#ffffff;">        };
</span><span style="color:#800000;background-color:#f0f0f0;">143 
144 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.handleMouseMove = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(event) {
</span><span style="color:#800000;background-color:#f0f0f0;">145 
146 </span><span style="background-color:#ffffff;">            </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//console.log('-------------------');
</span><span style="color:#800000;background-color:#f0f0f0;">147 </span><span style="background-color:#ffffff;">            </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//console.log('Tool:handleMouseMove(event)');
</span><span style="color:#800000;background-color:#f0f0f0;">148 </span><span style="background-color:#ffffff;">            </span><span style="color:#808080;background-color:#ffffff;font-style:italic;">//console.log(event);
</span><span style="color:#800000;background-color:#f0f0f0;">149 
150 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">151 </span><span style="background-color:#ffffff;">        };
</span><span style="color:#800000;background-color:#f0f0f0;">152 
153 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.handleGesture = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(event) {
</span><span style="color:#800000;background-color:#f0f0f0;">154 
155 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'-------------------'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">156 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Tool:handleGesture(event)'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">157 </span><span style="background-color:#ffffff;">            console.log(event);
</span><span style="color:#800000;background-color:#f0f0f0;">158 
159 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">160 </span><span style="background-color:#ffffff;">        };
</span><span style="color:#800000;background-color:#f0f0f0;">161 
162 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.handleBlur = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">(event) {
</span><span style="color:#800000;background-color:#f0f0f0;">163 
164 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'-------------------'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">165 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Tool:handleBlur(event)'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">166 </span><span style="background-color:#ffffff;">            console.log(event);
</span><span style="color:#800000;background-color:#f0f0f0;">167 
168 </span><span style="background-color:#ffffff;">            </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">false</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">169 </span><span style="background-color:#ffffff;">        };
</span><span style="color:#800000;background-color:#f0f0f0;">170 
171 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">this</span><span style="background-color:#ffffff;">.handleResize = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;">() {
</span><span style="color:#800000;background-color:#f0f0f0;">172 
173 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'-------------------'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">174 </span><span style="background-color:#ffffff;">            console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Tool:handleResize()'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">175 </span><span style="background-color:#ffffff;">        };
</span><span style="color:#800000;background-color:#f0f0f0;">176 </span><span style="background-color:#ffffff;">    }
</span><span style="color:#800000;background-color:#f0f0f0;">177 
178 </span><span style="background-color:#ffffff;">    </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">var</span><span style="background-color:#ffffff;"> toolName = </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">"Autodesk.ADN.Viewing.Tool.CustomTool"</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">179 
180 </span><span style="background-color:#ffffff;">    _self.load = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> () {
</span><span style="color:#800000;background-color:#f0f0f0;">181 
182 </span><span style="background-color:#ffffff;">        _self.tool = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">new</span><span style="background-color:#ffffff;"> AdnTool(viewer, toolName);
</span><span style="color:#800000;background-color:#f0f0f0;">183 
184 </span><span style="background-color:#ffffff;">        viewer.toolController.registerTool(_self.tool);
</span><span style="color:#800000;background-color:#f0f0f0;">185 
186 </span><span style="background-color:#ffffff;">        viewer.toolController.activateTool(toolName);
</span><span style="color:#800000;background-color:#f0f0f0;">187 
188 </span><span style="background-color:#ffffff;">        console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Autodesk.ADN.Viewing.Extension.CustomTool loaded'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">189 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">190 </span><span style="background-color:#ffffff;">    };
</span><span style="color:#800000;background-color:#f0f0f0;">191 
192 </span><span style="background-color:#ffffff;">    _self.unload = </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">function</span><span style="background-color:#ffffff;"> () {
</span><span style="color:#800000;background-color:#f0f0f0;">193 
194 </span><span style="background-color:#ffffff;">        viewer.toolController.deactivateTool(toolName);
</span><span style="color:#800000;background-color:#f0f0f0;">195 
196 </span><span style="background-color:#ffffff;">        console.log(</span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Autodesk.ADN.Viewing.Extension.CustomTool unloaded'</span><span style="background-color:#ffffff;">);
</span><span style="color:#800000;background-color:#f0f0f0;">197 </span><span style="background-color:#ffffff;">        </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">return</span><span style="background-color:#ffffff;"> </span><span style="color:#000080;background-color:#ffffff;font-weight:bold;">true</span><span style="background-color:#ffffff;">;
</span><span style="color:#800000;background-color:#f0f0f0;">198 </span><span style="background-color:#ffffff;">    };
</span><span style="color:#800000;background-color:#f0f0f0;">199 </span><span style="background-color:#ffffff;">};
</span><span style="color:#800000;background-color:#f0f0f0;">200 
201 </span><span style="background-color:#ffffff;">Autodesk.ADN.Viewing.Extension.CustomTool.prototype =
</span><span style="color:#800000;background-color:#f0f0f0;">202 </span><span style="background-color:#ffffff;">    Object.create(Autodesk.Viewing.Extension.prototype);
</span><span style="color:#800000;background-color:#f0f0f0;">203 
204 </span><span style="background-color:#ffffff;">Autodesk.ADN.Viewing.Extension.CustomTool.prototype.constructor =
</span><span style="color:#800000;background-color:#f0f0f0;">205 </span><span style="background-color:#ffffff;">    Autodesk.ADN.Viewing.Extension.CustomTool;
</span><span style="color:#800000;background-color:#f0f0f0;">206 
207 </span><span style="background-color:#ffffff;">Autodesk.Viewing.theExtensionManager.registerExtension(
</span><span style="color:#800000;background-color:#f0f0f0;">208 </span><span style="background-color:#ffffff;">    </span><span style="color:#008000;background-color:#ffffff;font-weight:bold;">'Autodesk.ADN.Viewing.Extension.CustomTool'</span><span style="background-color:#ffffff;">,
</span><span style="color:#800000;background-color:#f0f0f0;">209 </span><span style="background-color:#ffffff;">    Autodesk.ADN.Viewing.Extension.CustomTool);
</span><span style="color:#800000;background-color:#f0f0f0;">210 </span></pre>
<p style="text-align: left;">You can find the complete code as part of our viewer extension samples:</p>
<p style="text-align: left;"><a title="" href="https://github.com/Developer-Autodesk/library-javascript-viewer-extensions/blob/master/Autodesk.ADN.Viewing.Extension.CustomTool/Autodesk.ADN.Viewing.Extension.CustomTool.js" target="_self">Autodesk.ADN.Viewing.Extension.CustomTool.js</a></p>
