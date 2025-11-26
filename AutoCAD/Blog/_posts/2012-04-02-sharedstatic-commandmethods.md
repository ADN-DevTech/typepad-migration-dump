---
layout: "post"
title: "Shared/Static CommandMethods"
date: "2012-04-02 17:59:39"
author: "Madhukar Moogala"
categories:
  - ".NET"
  - "AutoCAD"
  - "Stephen Preston"
original_url: "https://adndevblog.typepad.com/autocad/2012/04/sharedstatic-commandmethods.html "
typepad_basename: "sharedstatic-commandmethods"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/stephen-preston.html" target="_self">Stephen Preston</a></p>
<p>If you set your CommandMethods as normal class instance members:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MyCommands</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &lt;</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">"TEST"</span><span style="line-height: 140%;">)&gt; _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> MyCommand()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">'My code</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Class</span></p>
<p style="margin: 0px;">&nbsp;</p>
</div>
<p>then AutoCAD will automatically instantiate your class when your command is called in a new document. However, if you make your CommandMethod a Shared (static in C#) method, then AutoCAD can call your method without instantiating your class:</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MyCommands</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &lt;</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">"TEST"</span><span style="line-height: 140%;">)&gt; _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;"><span style="background-color: #ffff00;"><strong>Shared</strong></span></span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> MyCommand()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">'My code</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Class</span></p>
<p style="margin: 0px;">&nbsp;</p>
</div>
<p>You can test this by writing a constructor for your class and watching when its called.</p>
