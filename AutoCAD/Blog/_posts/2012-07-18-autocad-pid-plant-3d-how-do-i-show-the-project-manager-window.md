---
layout: "post"
title: "AutoCAD P&amp;ID / Plant 3D: How do I show the Project Manager window?"
date: "2012-07-18 03:50:47"
author: "Marat Mirgaleev"
categories:
  - ".NET"
  - "Marat Mirgaleev"
  - "Plant3D"
  - "PnID"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/autocad-pid-plant-3d-how-do-i-show-the-project-manager-window.html "
typepad_basename: "autocad-pid-plant-3d-how-do-i-show-the-project-manager-window"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/marat-mirgaleev.html" target="_self">Marat Mirgaleev</a></p>  <p><strong>Issue</strong></p>  <p><em>I would like to show the Project Manager palette programmatically, if it’s hidden. Can I do this?</em></p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017743712ade970d-pi"><img style="background-image: none; border-right-width: 0px; padding-left: 0px; padding-right: 0px; display: inline; border-top-width: 0px; border-bottom-width: 0px; border-left-width: 0px; padding-top: 0px" title="image" border="0" alt="image" src="/assets/image_946703.jpg" width="513" height="550" /></a></p>  <p><strong>Solution</strong></p>  <p>There is an undocumented command called &quot;_REFRESHPMESW&quot; that shows the Project Manager. So, we can simply call that command with these lines of code:</p>  <div style="font-family: courier new; background: white; color: black; font-size: 8pt">   <p style="margin: 0px"><span style="line-height: 140%">&#160; </span><span style="line-height: 140%; color: #2b91af">Document</span><span style="line-height: 140%"> doc = </span><span style="line-height: 140%; color: #2b91af">Application</span><span style="line-height: 140%">.DocumentManager.MdiActiveDocument;</span></p>    <p style="margin: 0px"><span style="line-height: 140%">&#160; doc.SendStringToExecute(</span><span style="line-height: 140%; color: #a31515">&quot;_REFRESHPMESW &quot;</span><span style="line-height: 140%">, </span><span style="line-height: 140%; color: blue">true</span><span style="line-height: 140%">, </span><span style="line-height: 140%; color: blue">false</span><span style="line-height: 140%">, </span><span style="line-height: 140%; color: blue">true</span><span style="line-height: 140%"> );&#160;&#160;&#160;&#160; </span></p>    <p style="margin: 0px"></p> </div>  <p>The &quot;_REFRESHPMESW&quot; command also refreshes the information in the window. It may be helpful when your program changes the project structure.</p>
