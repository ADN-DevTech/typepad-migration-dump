---
layout: "post"
title: "Hitting breakpoints in .NET Class Libraries while debugging with Visual Studio 2010"
date: "2010-04-28 10:11:40"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Debugging"
  - "Visual Studio"
original_url: "https://www.keanw.com/2010/04/hitting-breakpoints-in-net-class-libraries-while-debugging-with-visual-studio-2010.html "
typepad_basename: "hitting-breakpoints-in-net-class-libraries-while-debugging-with-visual-studio-2010"
typepad_status: "Publish"
---

<p>This question has popped up a few times since <a href="http://www.microsoft.com/visualstudio/en-us/" target="_blank">Visual Studio 2010</a>’s <a href="http://www.microsoft.com/visualstudio/en-us/visual-studio-events" target="_blank">recent launch</a>. I received it by email overnight from Roland Cox, which is interesting as I’d already planned this post for today.</p>
<blockquote>
<p><em>Do you know how to debug AutoCAD and a custom add in using Visual Studio 2010?</em></p>
<p><em>What settings do I need to make?&#0160; I know the add is getting loaded (the code runs in AutoCAD) but I can&#39;t set a breakpoint (it shows a empty circle saying that VS doesn&#39;t have the assembly loaded).</em></p></blockquote>
<p>I first saw this issue raised a few times on threads with Microsoft, both with respect to <a href="http://social.msdn.microsoft.com/Forums/en/vsdebug/thread/22d4c3d3-bf77-40ed-8b1b-64223019f3f1" target="_blank">AutoCAD</a> and <a href="https://connect.microsoft.com/VisualStudio/feedback/details/487949/debugging-external-application" target="_blank">Revit</a>. Jeremy has also <a href="http://thebuildingcoder.typepad.com/blog/2010/04/debugging-with-visual-studio-2010-and-rvtsamples.html" target="_blank">posted something on his blog about it</a>.</p>
<p>Adam Nagy, from our DevTech team in Europe, has created <a href="http://adn.autodesk.com/adn/servlet/devnote?siteID=4814862&amp;id=15032210&amp;linkID=4900509" target="_blank">this excellent DevNote on the ADN website</a> covering the various options for dealing with this problem. I’ve used this as the basis for the details in today’s post. (Thanks, Adam! :-)</p>
<p>The problem is not at all specific to Autodesk products, although – if the above threads are an indicator – it does seem that as commonly used .NET plugin hosts our products are among the more common environments in which people are hitting this issue.</p>
<p>The cause of the problem boils down to the fact that VS 2010 does not choose the right version of the debugger for Class Library projects targeting prior versions of the .NET Framework: it always uses the default version, the debugger targeting v4 of the .NET Framework. This debugger doesn’t see breakpoints in projects targeting older versions of .NET.</p>
<p>The three workarounds Adam has highlighted show different ways to make VS 2010 use the correct debugger (one which actually hits breakpoints) on these projects. Which solution works best for you will depend on your specific scenario (I would tend to use option 2 or 3, myself).</p>
<h3>Solution 1</h3>
<p>Start the exe that loads your AddIn (acad.exe, revit.exe, etc) and then attach to it from Visual Studio (<strong>Debug -&gt; Attach to Process...</strong>)</p>
<h3>Solution 2</h3>
<p>Modify the config file of the exe that loads your AddIn (acad.exe.config, revit.exe.config, etc) so that it contains the following just before the &lt;/configuration&gt; part:</p>
<div style="FONT-FAMILY: courier new; BACKGROUND: white; COLOR: black; FONT-SIZE: 8pt">
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">&lt;</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">startup</span><span style="LINE-HEIGHT: 140%; COLOR: blue">&gt; </span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">&#0160; &lt;</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">supportedRuntime</span><span style="LINE-HEIGHT: 140%; COLOR: blue"> </span><span style="LINE-HEIGHT: 140%; COLOR: red">version</span><span style="LINE-HEIGHT: 140%; COLOR: blue">=</span><span style="LINE-HEIGHT: 140%">&quot;</span><span style="LINE-HEIGHT: 140%; COLOR: blue">v2.0.50727</span><span style="LINE-HEIGHT: 140%">&quot;</span><span style="LINE-HEIGHT: 140%; COLOR: blue"> /&gt; </span></p>
<p style="MARGIN: 0px"><span style="LINE-HEIGHT: 140%; COLOR: blue">&lt;/</span><span style="LINE-HEIGHT: 140%; COLOR: #a31515">startup</span><span style="LINE-HEIGHT: 140%; COLOR: blue">&gt;</span></p></div>
<h3>Solution 3</h3>
<p>Add the exe that loads your AddIn as an existing project to your solution and set the debugger version for it to v2.0.</p>
<ol>
<li>Right-click the Solution in the Solution Explorer and select <strong>Add -&gt; Existing Project...</strong> and locate the exe that loads your AddIn </li>
</ol>
<p>&#0160;<a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e20133ed0322ec970b-pi"><img alt="Add an existing project to our VS 2010 solution" border="0" height="239" src="/assets/image_741532.jpg" style="BORDER-RIGHT-WIDTH: 0px; MARGIN: 0px auto 30px; DISPLAY: block; FLOAT: none; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="Add an existing project to our VS 2010 solution" width="485" /></a> </p>
<ol start="2">
<li>Right-click on the Project that has just been added and select <strong>Set as StartUp Project</strong> </li>
</ol>
<p>&#0160;<a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201348032ca6c970c-pi"><img alt="Make our new project the startup project" border="0" height="126" src="/assets/image_506625.jpg" style="BORDER-RIGHT-WIDTH: 0px; MARGIN: 0px auto 30px; DISPLAY: block; FLOAT: none; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="Make our new project the startup project" width="327" /></a> </p>
<ol start="3">
<li>Right-click on the Project and select <strong>Properties</strong> </li>
</ol>
<p>&#0160;<a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201348032ca7d970c-pi"><img alt="Edit our project&#39;s properties" border="0" height="127" src="/assets/image_820678.jpg" style="BORDER-RIGHT-WIDTH: 0px; MARGIN: 0px auto 30px; DISPLAY: block; FLOAT: none; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="Edit our project&#39;s properties" width="328" /></a> </p>
<ol start="4">
<li>Set the <strong>Debugger Type</strong> to <strong>Managed (v2.0, v1.1, v1.0)</strong> </li>
</ol>
<p></p>
<p></p>
<p></p>
<p><a href="http://through-the-interface.typepad.com/.a/6a00d83452464869e201348032ca95970c-pi"><img alt="Change the debugger type in our executable project&#39;s properties" border="0" height="280" src="/assets/image_380093.jpg" style="BORDER-RIGHT-WIDTH: 0px; MARGIN: 30px auto; DISPLAY: block; FLOAT: none; BORDER-TOP-WIDTH: 0px; BORDER-BOTTOM-WIDTH: 0px; BORDER-LEFT-WIDTH: 0px" title="Change the debugger type in our executable project&#39;s properties" width="452" /></a></p>
