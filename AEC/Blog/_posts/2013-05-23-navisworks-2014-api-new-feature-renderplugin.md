---
layout: "post"
title: "Navisworks 2014 API new feature - RenderPlugin"
date: "2013-05-23 22:36:00"
author: "Xiaodong Liang"
categories:
  - ".NET"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2013/05/navisworks-2014-api-new-feature-renderplugin.html "
typepad_basename: "navisworks-2014-api-new-feature-renderplugin"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>I have frequently heard the voice that the developers want to draw some custom graphics in Naivsworks e.g. the custom red line. Now it came true. The new plugin type provides methods to render user-graphics. It can render in 3D model space or 2D screen space. The new class <span style="color: #ff0080;">Graphics</span> is used to create the 2D/3D primitives. </p>
<p>Firstly, create a class deriving from RenderPlugin. It is a kind of implicit plugin which has no interface. So the attribute DisplayName is useless.</p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">[</span><span style="line-height: 140%; color: #2b91af;">Plugin</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">&quot;RenderPluginTest&quot;</span><span style="line-height: 140%;">, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #a31515;">&quot;ADSK&quot;</span><span style="line-height: 140%;">)]</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">RenderPluginTest</span><span style="line-height: 140%;"> : </span><span style="line-height: 140%; color: #2b91af;">RenderPlugin</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span></p>
<p style="margin: 0px;">&#0160;</p>
</div>
<p>Next, override the methods you want to use. </p>
<ul>
<li>public virtual void OverlayRenderModel(<a href="T_Autodesk_Navisworks_Api_View.htm">View</a> view,<a href="T_Autodesk_Navisworks_Api_Graphics.htm">Graphics</a> graphics) </li>
</ul>
<blockquote>
<p>This method renders the graphics in the overlay buffer.By default it’s set up for you to call graphics methods that take 3D model space coordinates. You can call graphics methods that take 2D model space coordinates from inside a BeginWindowContext-EndWindowContext block. </p>
</blockquote>
<ul>
<li>public virtual void OverlayRenderWindow(<a href="T_Autodesk_Navisworks_Api_View.htm">View</a> view,<a href="T_Autodesk_Navisworks_Api_Graphics.htm">Graphics</a> graphics) </li>
</ul>
<blockquote>
<p>This method renders the graphics in the overlay buffer. By default it’s set up for you to call graphics methods that take 2D window space coordinates. You can also call graphics methods that take 3D model space coordinates from inside a BeginModelContext-EndModelContext block </p>
</blockquote>
<ul>
<li>public virtual void RenderModel(<a href="T_Autodesk_Navisworks_Api_View.htm">View</a> view,<a href="T_Autodesk_Navisworks_Api_Graphics.htm">Graphics</a> graphics) </li>
</ul>
<blockquote>
<p>Similar to OverlayRenderModel, but renders in the main buffer. </p>
</blockquote>
<ul>
<li>public virtual void RenderWindow(<a href="T_Autodesk_Navisworks_Api_View.htm">View</a> view,<a href="T_Autodesk_Navisworks_Api_Graphics.htm">Graphics</a> graphics) </li>
</ul>
<blockquote>
<p>Similar to OverlayRenderWindow but renders in the main buffer.</p>
</blockquote>
<p>Where possible, it is recommended that you should be using Overlay*** to do ‘overly graphics’ rather than using ‘Render***’. </p>
<p>The code below draws a triangle and two lines. </p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">override</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> OverlayRenderWindow(</span><span style="line-height: 140%; color: #2b91af;">View</span><span style="line-height: 140%;"> view,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Graphics</span><span style="line-height: 140%;"> graphics)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{&#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// the color for the graphics</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Color</span><span style="line-height: 140%;"> colorRed = </span><span style="line-height: 140%; color: #2b91af;">Color</span><span style="line-height: 140%;">.FromByteRGB(255, 0, 0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// color and alpha value </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; graphics.Color(colorRed, 0.7);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//set line width</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; graphics.LineWidth(5);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Point2D</span><span style="line-height: 140%;"> p1 = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point2D</span><span style="line-height: 140%;">(20, 20);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Point2D</span><span style="line-height: 140%;"> p2 = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point2D</span><span style="line-height: 140%;">(view.Width - 20, 20);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Point2D</span><span style="line-height: 140%;"> p3 = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point2D</span><span style="line-height: 140%;">(view.Width /2.0, view.Height - 20);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// draw triangle, fill in it</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; graphics.Triangle(p1,p2,p3,</span><span style="line-height: 140%; color: blue;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// the color for the graphics</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Color</span><span style="line-height: 140%;"> colorGreen = </span><span style="line-height: 140%; color: #2b91af;">Color</span><span style="line-height: 140%;">.FromByteRGB(0, 255, 0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// color and alpha value </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; graphics.Color(colorGreen, 0.7);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Point2D</span><span style="line-height: 140%;"> p4 = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point2D</span><span style="line-height: 140%;">(20, 20);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Point2D</span><span style="line-height: 140%;"> p5 = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point2D</span><span style="line-height: 140%;">(view.Width / 2.0, (view.Height - 20) / 2.0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Point2D</span><span style="line-height: 140%;"> p6 = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point2D</span><span style="line-height: 140%;">(view.Width - 20, 20);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">//draw two lines</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; graphics.Line(p4, p5);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; graphics.Line(p5, p6);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">} </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span></p>
</div>
Since the 2D is in window space coordinates, the graphics will update with the view size changing.
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;<a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0191026643cc970c-pi"><img alt="image" border="0" height="247" src="/assets/image_77083.jpg" style="display: inline; border-width: 0px;" title="image" width="294" /></a> </p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0192aa2eb25c970d-pi"><img alt="image" border="0" height="122" src="/assets/image_455346.jpg" style="display: inline; border-width: 0px;" title="image" width="295" /></a></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;">The code below draws a cylinder. </p>
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<div style="font-family: courier new; background: white; color: black; font-size: 9pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">override</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">void</span><span style="line-height: 140%;"> OverlayRenderModel(</span><span style="line-height: 140%; color: #2b91af;">View</span><span style="line-height: 140%;"> view, </span><span style="line-height: 140%; color: #2b91af;">Graphics</span><span style="line-height: 140%;"> graphics)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{ </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// the color for the graphics</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Color</span><span style="line-height: 140%;"> colorBlue = </span><span style="line-height: 140%; color: #2b91af;">Color</span><span style="line-height: 140%;">.FromByteRGB(0, 0, 255);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// color and alpha value </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; graphics.Color(colorBlue, 1); </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Point3D</span><span style="line-height: 140%;"> p1 = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point3D</span><span style="line-height: 140%;">(0, 0,0);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Point3D</span><span style="line-height: 140%;"> p2 = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point3D</span><span style="line-height: 140%;">(0, 0,10);&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// draw Cylinder </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; graphics.Cylinder(p1,p2,10);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// the color for the graphics</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Color</span><span style="line-height: 140%;"> colorGrey = </span><span style="line-height: 140%; color: #2b91af;">Color</span><span style="line-height: 140%;">.FromByteRGB(128, 128, 128);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// color and alpha value </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; graphics.Color(colorGrey, 1);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: #2b91af;">Point3D</span><span style="line-height: 140%;"> p3 = </span><span style="line-height: 140%; color: blue;">new</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">Point3D</span><span style="line-height: 140%;">(20, 20,0);&#0160;&#0160;&#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: green;">// draw sphere</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; graphics.Sphere(p3, 10);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">} </span></p>
</div>
<p style="margin: 0px;"><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01901c705ea4970b-pi"><img alt="image" border="0" height="215" src="/assets/image_646971.jpg" style="display: inline; border: 0px;" title="image" width="356" /></a> </p>
</div>
