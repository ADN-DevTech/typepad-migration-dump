---
layout: "post"
title: "Web Server with Navisworks ActiveX Control"
date: "2012-05-17 22:45:29"
author: "Xiaodong Liang"
categories:
  - "COM"
  - "Navisworks"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/aec/2012/05/web-server-with-navisworks-activex-control.html "
typepad_basename: "web-server-with-navisworks-activex-control"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p>To step into this topic, it is recommended you take a look at the <a href="http://adndevblog.typepad.com/aec/2012/05/navisworks-activex-controls.html" target="_self">previous blog</a> that introduces Navisworks ActiveX Controls.</p>
<p>1. Write the code of web page</p>
<p style="padding-left: 30px;">(1) To use the control with web, the web application (such as HTML) is similar to any other kind of ActiveX control. The core code is</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px; padding-left: 30px;"><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: #a31515;">OBJECT</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">ID</span><span style="line-height: 140%; color: blue;">=&quot;NWControl01&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">CLASSID</span><span style="line-height: 140%; color: blue;">=&quot;CLSID:xxxxxxxxxx&quot;</span></p>
<p style="margin: 0px; padding-left: 30px;"><span style="line-height: 140%; color: red;">CODEBASE</span><span style="line-height: 140%; color: blue;">=&quot;..\..\bin\Navisworks ActiveX Redistributable Setup.exe&quot;&gt;</span></p>
<p style="margin: 0px; padding-left: 30px;">&#0160;</p>
<p style="margin: 0px; padding-left: 30px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: #a31515;">PARAM</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">NAME</span><span style="line-height: 140%; color: blue;">=&quot;_cx&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">VALUE</span><span style="line-height: 140%; color: blue;">=&quot;14000&quot;&gt;</span></p>
<p style="margin: 0px; padding-left: 30px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: #a31515;">PARAM</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">NAME</span><span style="line-height: 140%; color: blue;">=&quot;_cy&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">VALUE</span><span style="line-height: 140%; color: blue;">=&quot;12000&quot;&gt;</span></p>
<p style="margin: 0px; padding-left: 30px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; </span><span style="line-height: 140%; color: blue;">&lt;</span><span style="line-height: 140%; color: #a31515;">PARAM</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">NAME</span><span style="line-height: 140%; color: blue;">=&quot;SRC&quot;</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: red;">VALUE</span><span style="line-height: 140%; color: blue;">=&quot;..\gatehouse.nwd&quot;&gt;</span></p>
<p style="margin: 0px; padding-left: 30px;">&#0160;</p>
<p style="margin: 0px; padding-left: 30px;"><span style="line-height: 140%; color: blue;">&lt;/</span><span style="line-height: 140%; color: #a31515;">OBJECT</span><span style="line-height: 140%; color: blue;">&gt;</span></p>
</div>
<p style="padding-left: 30px;">Where:</p>
<p style="padding-left: 30px;">- xxxxxxxx is the GUID mentioned in the post .<br />- CODEBASE is the installer for Lite, Full or Redistributable control.<br />- _cx, _cy, SRC are some properties of the control. SRC can also be specified dynamically. To know all properties please refer to \ &lt;Navisworks Install Path&gt; \api\COM \documentation\NavisworksCOM.chm<br />-&#0160; SRC can be absolute, UNC or relative path.</p>
<p style="padding-left: 30px;">(2) Every type of control can be used with the web page. You need to specify the correct GUID.</p>
<p style="padding-left: 30px;">(3)&#0160; For Built-in or Integrated control, CODEBASE is useless because the client should have installed Navisworks.</p>
<p style="padding-left: 30px;">(4) Before 2013, the &lt;Navisworks Intallation Path&gt;\api\COM\example\ACTX_01 is a good sample to get started with.</p>
<p>2. Setup the web server</p>
<p>In this blog, we take IIS as an example. It is very straightforward to setup the server like what you do normally.&#0160;</p>
<p>&#0160;&#0160;&#0160; (1)&#0160; put the model file in some location on your server. make sure the relative <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; path is correct</p>
<p>&#0160;&#0160;&#0160; (2)&#0160; put the HTML in the site that the client user can access</p>
<p>&#0160;&#0160;&#0160; (3) With Lite, Full or Redistributable control, make sure the installer is available <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; on the server and make sure&#0160;either 32bits or 64bits you want to provide.</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Lite:&#0160; nw_ax_lite.cab<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Full: NavisworksFullActiveXSetup.exe<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Redistributable : Navisworks ActiveX Redistributable Setup.exe&#0160;&#0160;&#0160;&#0160;</p>
<p>&#0160;&#0160;&#0160;&#0160; (4) Remember to add MIME type for nwd file. The format is:</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; .NWD&#0160;&#0160;&#0160; “application/octet-stream”</p>
<p>&#0160;&#0160;<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0163059fe074970d-pi" style="display: inline;"><img alt="Capture" border="0" class="asset  asset-image at-xid-6a0167607c2431970b0163059fe074970d image-full" height="305" src="/assets/image_554489.jpg" title="Capture" width="510" /></a></p>
<p>3. Client End</p>
<p>&#0160;&#0160;&#0160; (1)&#0160;&#0160; If the web page uses Built-in or Integrated control, make sure the client <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; machine has installed&#0160;&#0160;Navisworks (Simulate/Manage)</p>
<p>&#0160;&#0160;&#0160; (2)&#0160;&#0160; Currently only IE can open the page with Navisworks control</p>
<p>&#0160;&#0160;&#0160; (3)&#0160; If the web page uses 32bits control, you must open the web page in <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; IE-32bits.&#0160; If the web page uses 64bits control, you must open the web <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; page in IE-64bits.&#0160;This happens&#0160;&#0160;particularly when the client is 64bits OS.</p>
<p>&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; So, with Lite, Full or Redistributable control, this depends on which installer<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; &#0160;the server&#0160;provides.When you open the page in the first time, a warning <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; message will ask if installing&#0160; the ActiveX control.</p>
<p style="padding-left: 30px;">With Built-in or Integrated control, since they have only 32bits on 32bits OS, <br />&#0160;64bits on 64bits OS, you can only open the page with corresponding IE. <br />&#0160;No warning message for installing.</p>
<p>The screenshot below is a sample when I deploy the SDK sample ACTX_01 to my server.</p>
<p>&#0160; <a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0163059fd9cf970d-pi"><img alt="clip_image001[9]" border="0" height="554" src="/assets/image_236956.jpg" style="display: inline; border: 0px;" title="clip_image001[9]" width="908" /></a></p>
<p>&#0160;</p>
<p>4.&#0160; Trouble shooting</p>
<p style="padding-left: 30px;">- 64bits OS: On 64bits, Integrated Control and Redistributable Control (64bits) can only be opened by IE-64bits. IE-32bits can open Redistributable Control (32bits ).</p>
<p style="padding-left: 30px;">- The webpage is empty: check if the control has installed or you have enable to use the control when you access the webpage. Typically, a warning message will ask you if enable the control.</p>
<p style="padding-left: 30px;">- The control is black: check if the model is in the correct location. check if it is a supported format. check if NW product can open it.</p>
