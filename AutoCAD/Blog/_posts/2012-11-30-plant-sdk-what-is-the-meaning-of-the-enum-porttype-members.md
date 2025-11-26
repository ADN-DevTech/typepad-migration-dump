---
layout: "post"
title: "Plant SDK: What is the meaning of the enum PortType members?"
date: "2012-11-30 01:48:41"
author: "Marat Mirgaleev"
categories:
  - ".NET"
  - "Marat Mirgaleev"
  - "Plant3D"
original_url: "https://adndevblog.typepad.com/autocad/2012/11/plant-sdk-what-is-the-meaning-of-the-enum-porttype-members.html "
typepad_basename: "plant-sdk-what-is-the-meaning-of-the-enum-porttype-members"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/marat-mirgaleev.html" target="_self">Marat Mirgaleev</a></p>  <p><strong>Issue</strong></p>  <p><em>Several methods in the Plant SDK accept a parameter, which type is </em><font face="Courier New">enum PortType</font><em>. What do the members of this enumeration mean?</em></p>  <p><strong>Solution</strong></p>  <p>This enum has the following members:</p>  <div style="font-family: courier new; background: white; color: black; font-size: 8pt">   <p style="margin: 0px"><span style="line-height: 140%">&#160; </span><span style="line-height: 140%; color: blue">public</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: blue">enum</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">PortType</span></p>    <p style="margin: 0px"><span style="line-height: 140%">&#160; {</span></p>    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; Static&#160;&#160; = 0x01,</span></p>    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; Dynamic&#160; = 0x02,</span></p>    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; Both&#160;&#160;&#160;&#160; = 0x03,</span></p>    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; Symbolic = 0x04,</span></p>    <p style="margin: 0px"><span style="line-height: 140%">&#160;&#160;&#160; All&#160;&#160;&#160;&#160;&#160; = 0x07,</span></p>    <p style="margin: 0px"><span style="line-height: 140%">&#160; }</span></p> </div>  <p><font face="Courier New">Static</font> ports are defined by the object, i.e. an object has predefined ports that do not change.</p>  <p><font face="Courier New">Dynamic</font> ports are defined by a user at run-time. These ports may move around, be deleted, etc. For example, if you drill a hole in a pipe and run a new pipe into hole with a weld.</p>  <p><font face="Courier New">Both</font> means to fetch static and dynamic ports.</p>  <p><font face="Courier New">Symbolic </font>are special ports used only by Isometric symbols. The main difference here is that no connector entity is used between the pipe fitting and the isometric object. It’s a symbolic port to get desired connection behavior but not truly a port as the model is concerned. </p>  <p><font face="Courier New">All</font> means static, dynamic, and symbolic ports.</p>
