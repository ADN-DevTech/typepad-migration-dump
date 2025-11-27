---
layout: "post"
title: "Integrating the 3dRudder with AutoCAD"
date: "2017-06-22 10:23:14"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Virtual Reality"
original_url: "https://www.keanw.com/2017/06/integrating-the-3drudder-with-autocad.html "
typepad_basename: "integrating-the-3drudder-with-autocad"
typepad_status: "Publish"
---

<p>I mentioned this device, <a href="http://through-the-interface.typepad.com/through_the_interface/2016/11/google-earth-vr-3d-rudder.html" target="_blank">late last year</a>, and have been meaning to spend time integrating it into AutoCAD ever since. The <a href="https://www.3drudder.com/" target="_blank">3dRudder</a> is an interesting perpheral: while currently targeted at helping seated VR users navigate intuitively in 3D – effectively keeping their hands free – it was originally intended for CAD users. So it’s nice that it’s going back to its roots, as it were. Here’s an excerpt of <a href="https://youtu.be/vUr33Rov46Q" target="_blank">a video</a> of Christian Slater demoing the 3dRudder integration with Rhino (I’m kidding about it being Christian Slater – he just looks a bit like him :-).</p><p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201bb09a7e009970d-pi" target="_blank"><img width="500" height="281" title="3dRudder in action" style="margin: 30px auto; float: none; display: block;" alt="3dRudder in action" src="/assets/image_101076.jpg"></a></p><p>Over the last few days, I’ve finally managed to find time to create a skeleton .NET application that integrates the 3dRudder into AutoCAD. Unfortunately I haven’t managed to spend the time to make use of the navigation APIs to pan/zoom/orbit around AutoCAD’s 2D or 3D environments, but that shouldn’t be a huge task for anyone wanting to take this code further.</p><p>Here’s the C# code that makes use of the 3dRudder .NET SDK to bring data into AutoCAD:</p><p><div style="background: white; color: black; line-height: 140%; font-family: courier new; font-size: 8pt;">
<p style="margin: 0px;"><span style="color: blue;">using</span> System;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> System.Runtime.InteropServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.ApplicationServices;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.EditorInput;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.GraphicsInterface;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> Autodesk.AutoCAD.Runtime;</p>
<p style="margin: 0px;"><span style="color: blue;">using</span> ns3DRudder;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;"><span style="color: blue;">namespace</span> MyApplication</p>
<p style="margin: 0px;">{</p>
<p style="margin: 0px;">&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">partial</span> <span style="color: blue;">class</span> <span style="color: rgb(43, 145, 175);">Commands</span></p>
<p style="margin: 0px;">&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; [<span style="color: rgb(43, 145, 175);">CommandMethod</span>(<span style="color: rgb(163, 21, 21);">"3DRUDDER"</span>, <span style="color: rgb(43, 145, 175);">CommandFlags</span>.Modal)]</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">void</span> Rudder()</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> doc = <span style="color: rgb(43, 145, 175);">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> ed = doc.Editor;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Create and run our jig</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> rj = <span style="color: blue;">new</span> <span style="color: rgb(43, 145, 175);">RudderJig</span>();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> pr = ed.Drag(rj);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">class</span> <span style="color: rgb(43, 145, 175);">RudderJig</span> : <span style="color: rgb(43, 145, 175);">DrawJig</span></p>
<p style="margin: 0px;">&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">int</span> _numRuddersConnected;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: rgb(43, 145, 175);">CSdk</span> _rudderSdk;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: rgb(43, 145, 175);">Axis</span> axis;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: rgb(43, 145, 175);">ModeAxis</span> mode;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: rgb(43, 145, 175);">CurveArray</span> curves;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">int</span> _selectedIndex = -1;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; ns3DRudder.<span style="color: rgb(43, 145, 175);">Status</span> _prevStatus;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">class</span> <span style="color: rgb(43, 145, 175);">Item</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">string</span> Name;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">int</span> Value;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> Item(<span style="color: blue;">string</span> name, <span style="color: blue;">int</span> value)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Name = name; Value = value;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">override</span> <span style="color: blue;">string</span> ToString()</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// Generates the text shown in the combo box</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> Name;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">// To stop the running jig by sending a cancel request</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; [<span style="color: rgb(43, 145, 175);">DllImport</span>(<span style="color: rgb(163, 21, 21);">"accore.dll"</span>, CharSet = <span style="color: rgb(43, 145, 175);">CharSet</span>.Auto,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; CallingConvention = <span style="color: rgb(43, 145, 175);">CallingConvention</span>.Cdecl,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; EntryPoint = <span style="color: rgb(163, 21, 21);">"?acedPostCommand@@YAHPEB_W@Z"</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp; )]</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">extern</span> <span style="color: blue;">static</span> <span style="color: blue;">private</span> <span style="color: blue;">int</span> acedPostCommand(<span style="color: blue;">string</span> strExpr);</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> RudderJig()</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _rudderSdk = <span style="color: blue;">new</span> <span style="color: rgb(43, 145, 175);">CSdk</span>();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _rudderSdk.Init();</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; axis = <span style="color: blue;">new</span> <span style="color: rgb(43, 145, 175);">Axis</span>();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; mode = <span style="color: rgb(43, 145, 175);">ModeAxis</span>.NormalizedValue;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; curves = <span style="color: blue;">new</span> <span style="color: rgb(43, 145, 175);">CurveArray</span>();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">// Flag and property for when we want to exit</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">protected</span> <span style="color: blue;">bool</span> _finished;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">bool</span> Finished</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">get</span> { <span style="color: blue;">return</span> _finished; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">set</span> { _finished = <span style="color: blue;">value</span>; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">private</span> <span style="color: blue;">void</span> UpdateStatus()</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> doc = <span style="color: rgb(43, 145, 175);">Application</span>.DocumentManager.MdiActiveDocument;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> ed = doc.Editor;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (_numRuddersConnected != _rudderSdk.GetNumberOfConnectedDevice())</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _numRuddersConnected = _rudderSdk.GetNumberOfConnectedDevice();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">for</span> (<span style="color: blue;">uint</span> i = 0; i &lt; <span style="color: rgb(43, 145, 175);">i3DR</span>._3DRUDDER_SDK_MAX_DEVICE; i++)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (_rudderSdk.IsDeviceConnected(i))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ed.WriteMessage(<span style="color: rgb(163, 21, 21);">"3DRudder #{0}\n"</span>, i);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _selectedIndex = (<span style="color: blue;">int</span>)i;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (_selectedIndex != -1)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> selected = (<span style="color: blue;">uint</span>)_selectedIndex;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (_rudderSdk.IsDeviceConnected(selected))</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: green;">// ushort nVersion = _rudderSdk.GetVersion(selected);</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">string</span> Status = <span style="color: rgb(163, 21, 21);">""</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (_rudderSdk.GetAxis(selected, mode, axis, curves) == <span style="color: rgb(43, 145, 175);">ErrorCode</span>.Success)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> stat = _rudderSdk.GetStatus(selected);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">switch</span> (stat)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">case</span> ns3DRudder.<span style="color: rgb(43, 145, 175);">Status</span>.NoFootStayStill:</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Status = <span style="color: rgb(163, 21, 21);">"Don't put your feet!!! Stay still for 5s"</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">break</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">case</span> ns3DRudder.<span style="color: rgb(43, 145, 175);">Status</span>.Initialisation:</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Status = <span style="color: rgb(163, 21, 21);">"Initialisation"</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">break</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">case</span> ns3DRudder.<span style="color: rgb(43, 145, 175);">Status</span>.PutYourFeet:</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Status = <span style="color: rgb(163, 21, 21);">"Please put your feet"</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">break</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">case</span> ns3DRudder.<span style="color: rgb(43, 145, 175);">Status</span>.PutSecondFoot:</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Status = <span style="color: rgb(163, 21, 21);">"Put your second foot"</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">break</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">case</span> ns3DRudder.<span style="color: rgb(43, 145, 175);">Status</span>.StayStill:</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Status = <span style="color: rgb(163, 21, 21);">"Stay still"</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">break</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">case</span> ns3DRudder.<span style="color: rgb(43, 145, 175);">Status</span>.InUse:</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Status = <span style="color: rgb(163, 21, 21);">"3DRudder in use"</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">break</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">case</span> ns3DRudder.<span style="color: rgb(43, 145, 175);">Status</span>.ExtendedMode:</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Status = <span style="color: rgb(163, 21, 21);">"Extended functions activated"</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">break</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (stat != _prevStatus)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; doc.Editor.WriteMessage(<span style="color: rgb(163, 21, 21);">"{0}\n"</span>, Status);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; _prevStatus = stat;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (stat == ns3DRudder.<span style="color: rgb(43, 145, 175);">Status</span>.InUse || stat == ns3DRudder.<span style="color: rgb(43, 145, 175);">Status</span>.ExtendedMode)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> XAxis = axis.GetXAxis();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> YAxis = axis.GetYAxis();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> ZAxis = axis.GetZAxis();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> ZRotation = axis.GetZRotation();</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ed.WriteMessage(</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: rgb(163, 21, 21);">"X: {0:0.00}, Y: {1:0.00}, Z: {2:0.00}, Rotation: {3:0.00} ({4})\n"</span>,</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; XAxis, YAxis, ZAxis, ZRotation, Status</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; );</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">protected</span> <span style="color: blue;">virtual</span> <span style="color: rgb(43, 145, 175);">SamplerStatus</span> SamplerData()</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; UpdateStatus();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; ForceMessage();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: rgb(43, 145, 175);">SamplerStatus</span>.NoChange; <span style="color: green;">// Cancel;</span></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">protected</span> <span style="color: blue;">virtual</span> <span style="color: blue;">bool</span> WorldDrawData(<span style="color: rgb(43, 145, 175);">WorldDraw</span> draw)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: blue;">false</span>;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">protected</span> <span style="color: blue;">override</span> <span style="color: rgb(43, 145, 175);">SamplerStatus</span> Sampler(<span style="color: rgb(43, 145, 175);">JigPrompts</span> prompts)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> opts = <span style="color: blue;">new</span> <span style="color: rgb(43, 145, 175);">JigPromptPointOptions</span>(<span style="color: rgb(163, 21, 21);">"\nClick to finish: "</span>);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; opts.UserInputControls = <span style="color: rgb(43, 145, 175);">UserInputControls</span>.AnyBlankTerminatesInput;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; opts.Cursor = <span style="color: rgb(43, 145, 175);">CursorType</span>.Invisible;</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">var</span> ppr = prompts.AcquirePoint(opts);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (ppr.Status == <span style="color: rgb(43, 145, 175);">PromptStatus</span>.OK || ppr.Status == <span style="color: rgb(43, 145, 175);">PromptStatus</span>.Cancel)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">if</span> (_finished)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; CancelJig();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: rgb(43, 145, 175);">SamplerStatus</span>.Cancel;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> SamplerData();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> <span style="color: rgb(43, 145, 175);">SamplerStatus</span>.Cancel;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">protected</span> <span style="color: blue;">override</span> <span style="color: blue;">bool</span> WorldDraw(<span style="color: rgb(43, 145, 175);">WorldDraw</span> draw)</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span style="color: blue;">return</span> WorldDrawData(draw);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: green;">// Cancel the running jig</span></p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">internal</span> <span style="color: blue;">static</span> <span style="color: blue;">void</span> CancelJig()</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; acedPostCommand(<span style="color: rgb(163, 21, 21);">"CANCELCMD"</span>);</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;"><br></p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; <span style="color: blue;">public</span> <span style="color: blue;">void</span> ForceMessage()</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; {</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; System.Windows.Forms.<span style="color: rgb(43, 145, 175);">Cursor</span>.Position =</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; System.Windows.Forms.<span style="color: rgb(43, 145, 175);">Cursor</span>.Position;</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; System.Windows.Forms.<span style="color: rgb(43, 145, 175);">Application</span>.DoEvents();</p>
<p style="margin: 0px;">&nbsp;&nbsp;&nbsp; }</p>
<p style="margin: 0px;">&nbsp; }</p>
<p style="margin: 0px;">}</p><p style="margin: 0px;"><br></p>
</div>
<p>To get this code to work, you just need to create a standard AutoCAD .NET project with the usual assembly references (which can be added via NuGet) as well as an additional reference to 3DRudderSDK.net (which can found in the .NET sample on the <a href="https://www.3drudder.com/windows-sdk-virtual-reality-app-development/" target="_blank">3dRudder website</a>).</p><p>The above code was very much inspired by the work I did <a href="http://through-the-interface.typepad.com/through_the_interface/2011/04/previewing-and-capturing-kinect-sensor-data-directly-inside-autocad.html" target="_blank">way back when</a> to integrate point cloud input from Kinect into AutoCAD: in many ways it’s a similiar scenario, in that there’s an external device that sends data to AutoCAD and influences the active session in some way. It’s quite possible that there are other approaches that might not need a separate command to be called or the cursor to be on the drawing canvas, such as – for instance – <a href="http://through-the-interface.typepad.com/through_the_interface/2015/03/autocad-2016-calling-commands-from-external-events-using-net.html" target="_blank">using the DocumentCollection.ExecuteInCommandContextAsync() method</a>. But I’ve stuck to what I know best for this initial integration.</p><p>When we run the 3DRUDDER command – with a 3dRudder connected and initialized – we should see a stream of data coming from the device that indicates the way the person is using it: X, Y and Z axes as well as a Z rotation (I’m not fully sure what the Z axis measurement means, admittedly: for me X is roll, Y is pitch and the Z rotation is yaw… the Z axis data seems redundant, although it’s altogether possible that I’m just not understanding it properly).</p><p>Here’s another video of the Rhino integration, this time showing in more detail how 3dRudder gestures can be interpreted for 3D model navigation:</p><p align="center"><br></p><p align="center"><iframe width="500" height="281" src="https://www.youtube-nocookie.com/embed/KRTTLIy1TZY?rel=0&amp;showinfo=0?ecver=1" frameborder="0" allowfullscreen=""></iframe></p><p><br></p><p>I wish I had more time to spend taking this code further – I won’t have space during our round-the-world trip (which starts in just over a week!) to slip a 3dRudder into my backpack – but hopefully someone reading this blog will take up the baton.</p>
