---
layout: "post"
title: "AU Classes on Python, UI, Server and Framework APIs"
date: "2012-11-30 10:00:00"
author: "Jeremy Tammik"
categories:
  - "2013"
  - "AU"
  - "Cloud"
  - "Events"
  - "Idling"
  - "Python"
  - "Server"
  - "Transaction"
  - "User Interface"
  - "Vasari"
  - "View"
  - "Win32"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/11/au-classes-on-python-ui-server-and-framework-apis.html "
typepad_basename: "au-classes-on-python-ui-server-and-framework-apis"
typepad_status: "Publish"
---

<p>Thursday was another very full and fruitful day at AU.
I gave my third and last class, the most exciting one in my eyes, on some UI and integration aspects.
I was unable to attend Iffat May's class introducing Python and using it in Revit and Vasari via RevitPythonShell, but I love her material so much I am including it anyway.
Adam Nagy presented a class addressing cloud and mobile development and the Revit Server API.
Finally, Arno&scaron;t L&ouml;bel provided a peek behind the scenes and into the depths of the fundamental frameworks underlying the Revit API.

<p>Here they are in chronological order:

<ol>
<li>
<a href="https://www.autodeskuniversity2012.com/connect/sessionDetail.ww?SESSION_ID=3837">CP3837-L</a>
Scripting with <a href="#1">RevitPythonShell in Vasari</a> by Iffat May.

<li>
<a href="https://www.autodeskuniversity2012.com/connect/sessionDetail.ww?SESSION_ID=4107">CP4107</a>
Let's Face It: New <a href="#2">Revit 2013 User Interface API</a> Functionality by Jeremy Tammik.

<li>
<a href="https://www.autodeskuniversity2012.com/connect/sessionDetail.ww?SESSION_ID=3093">CP3093</a>
My First Cloud/Mobile App with <a href="#3">Revit Server</a> by Adam Nagy.

<li>
<a href="https://www.autodeskuniversity2012.com/connect/sessionDetail.ww?SESSION_ID=3426">CP3426</a>
<a href="#4">Core Frameworks</a> in the Revit API by Arno&scaron;t L&ouml;bel.

</ol>

<p>I am including the class materials below, for your and my own convenience, and also to ensure that all this juicy stuff really is picked up and returned by Internet search engines.</p>



<a name="1"></a>

<h4>1.
<a href="https://www.autodeskuniversity2012.com/connect/sessionDetail.ww?SESSION_ID=3837">CP3837-L</a>
RevitPythonShell in Vasari</h4>

<p>Iffat provides an entire introductory tutorial to the Python programming language, and then shows how to apply it to solve a number of useful tasks in Revit and Vasari.
She makes use of Daren Thomas' 

<a href="http://code.google.com/p/revitpythonshell">Revit Python shell</a>,

originally implemented for Revit 2010, then for 

<a href="http://thebuildingcoder.typepad.com/blog/2011/07/python-shell-in-revit-and-vasari.html">
Vasari</a>,

<a href="http://thebuildingcoder.typepad.com/blog/2011/09/python-shell-for-revit-2012-and-vasari-21.html">
Revit 2012 and Vasari 2.1</a>,

and now available for Revit 2013.

<p>RevitPythonShell introduces interactive scripting ability to Revit and Project Vasari.
Designers now have the ability to interactively design and manipulate Revit elements using algorithms and computational logic.
The class explores the Python structures, variables, data types, and flow control, and shows how to use them to create scripts to control Revit elements dynamically.

<!--
/a/doc/revit/au/2012/doc2/CP3837
CP3837-L_Scripting_RevitPythonShell_handout.pdf
CP3837-L_Scripting_RevitPythonShell_presentation.pdf
CP3837-L_Datasets.zip
-->

<ul>
<li>

<span class="asset  asset-generic at-xid-6a00e553e168978833017c341d6a85970b"><a href="http://thebuildingcoder.typepad.com/files/cp3837-l_scripting_revitpythonshell_handout.pdf">Handout</a></span>
</li>
<li>
<span class="asset  asset-generic at-xid-6a00e553e168978833017d3e4c4643970c"><a href="http://thebuildingcoder.typepad.com/files/cp3837-l_scripting_revitpythonshell_presentation.pdf">Presentation</a></span>
</li>
<li>

<span class="asset  asset-generic at-xid-6a00e553e168978833017d3e4c476c970c"><a href="http://thebuildingcoder.typepad.com/files/cp3837-l_datasets.zip">Sample code and models</a></span>

</li>
</ul>



<a name="2"></a>

<h4>2. 
<a href="https://www.autodeskuniversity2012.com/connect/sessionDetail.ww?SESSION_ID=4107">CP4107</a>
Revit 2013 User Interface API</h4>

<p>This class takes a deeper look at the new user interface and add-in integration functionality provided by the Autodesk Revit 2013 API.
It covers 2013 features including Options dialogue custom extensions using WPF components, subscription to Revit progress bar notifications, embedding and controlling a Revit view inside an add-in dialogue for preview purposes, the new drag and drop API, and the UIView.
This class is complementary to and expands on CP3272, a Snapshot of the Revit UI API:

<ul>
<li>Document management and View API
<li>Revit progress bar notifications
<li>Options dialogue WPF custom extensions
<li>Embedding and controlling a Revit view
<li>UIView and Windows coordinates
<li>Drag and drop
</ul>

<p>From my point of view, one of the most exciting new features provided by the Revit 2013 UI API is the possibility to correlate between Windows screen device coordinates and Revit model coordinates provided by the UIView class, which I used to examine and implement my

<a href="http://thebuildingcoder.typepad.com/blog/2012/10/uiview-windows-coordinates-referenceintersector-and-my-own-tooltip.html">
own Revit tooltip</a>,

which also makes use of the new ReferenceIntersector class and Idling event.
This has never previously been possible.
Now you can do anything you like using the Windows API and connect that intelligently with the underlying Revit model.

</p>
<!--
/a/doc/revit/au/2012/doc
cp4107_revit_2013_ui_api.pdf
cp4107_revit_2013_ui_api_ppt.pdf
cp4107_revit_2013_ui_api.zip
-->

<ul>
<li>

<span class="asset  asset-generic at-xid-6a00e553e168978833017c341d6e20970b"><a href="http://thebuildingcoder.typepad.com/files/cp4107_revit_2013_ui_api-2.pdf">Handout</a></span>
</li>
<li>

<span class="asset  asset-generic at-xid-6a00e553e168978833017ee5c1186e970d"><a href="http://thebuildingcoder.typepad.com/files/cp4107_revit_2013_ui_api_ppt-2.pdf">Presentation</a></span>

</li>
<li>

<span class="asset  asset-generic at-xid-6a00e553e168978833017d3e4c4b13970c"><a href="http://thebuildingcoder.typepad.com/files/cp4107_revit_2013_ui_api-1.zip">Sample code</a></span>

</li>
</ul>



<a name="3"></a>

<h4>3.
<a href="https://www.autodeskuniversity2012.com/connect/sessionDetail.ww?SESSION_ID=3093">CP3093</a>
Cloud/Mobile App with Revit Server</h4>

<p>This class has two objectives: to introduce you to the Revit Server REST API and to introduce you to cloud and mobile programming.

<p>Autodesk Revit Server is a server-based file storage system that we can use to store Revit files.
It has a public API that uses simple HTTP known as REST (or Representational State Transfer).
REST is a "style" for designing network applications, and it is used to communicate between the server and a client machine.
REST is simple, yet powerful enough. You can use it from anywhere where HTTP programming is available: from a desktop, a mobile device or a server-side application.
In this class, we discuss what REST is, using the context of Revit Server.

<p>We then go one step further and show you how to create your own REST service in .NET.
We implement it as a notification system in the cloud and demonstrate with an Apple iPad/iPhone application in Apple iOS.
At the end of this class, you will be able to:

<ul>
<li>Use HTTP requests to interact with a REST API
<li>Programmatically access and modify data available on Revit Server
<li>Create basic iOS applications in Xcode
<li>Create a basic REST service in .NET
<li>Use Apple Push Notification
</ul>

<!--
/a/doc/revit/au/2012/doc2/CP3093
CP3093_My_First_Cloud-Mobile_App_with_Autodesk_Revit_Server.pdf
-->

<p>Here is the detailed handout document:</p>

<ul>
<li>

<span class="asset  asset-generic at-xid-6a00e553e168978833017c341d70f9970b"><a href="http://thebuildingcoder.typepad.com/files/cp3093_my_first_cloud-mobile_app_with_autodesk_revit_server.pdf">Handout</a></span>

</li>
</ul>



<a name="4"></a>

<h4>4.
<a href="https://www.autodeskuniversity2012.com/connect/sessionDetail.ww?SESSION_ID=3426">CP3426</a>
Core Frameworks in the Revit API</h4>

<p>A good understanding of core frameworks in the Revit API is a prerequisite for developing well behaved Revit add-ins.
Among the most important ones, the following frameworks play key roles in most applications:

<ul>
<li>External applications, commands, and events
<li>Transactions phases
<li>Regeneration and transaction modes
<li>Updaters and other call-backs
<li>Scoped objects and element validity
</ul>

<p>These concepts have been around for many releases, yet there are still facts about them that may not be completely understood.
This class summarizes the necessary basics, and also sheds some light on the behaviour that is normally hidden 'under the hood'.
Knowledge acquired during this class will help Revit developers to build more efficient, safer, and robust applications.

<!--
/a/doc/revit/au/2012/doc2/CP3426
CP3426_Core_Revit_API_handout.pdf
CP3426_Core_Revit_API_paper.pdf
CP3426_Core_Revit_API_presentation.pdf
-->

<ul>
<li>
<span class="asset  asset-generic at-xid-6a00e553e168978833017ee5c11ef2970d"><a href="http://thebuildingcoder.typepad.com/files/cp3426_core_revit_api_paper.pdf">Paper</a></span>
</li>
<li>
<span class="asset  asset-generic at-xid-6a00e553e168978833017d3e4c4d78970c"><a href="http://thebuildingcoder.typepad.com/files/cp3426_core_revit_api_presentation.pdf">Presentation</a></span>
</li>
</ul>

<p>By the time you read this, I will be sitting in the plane heading back to Europe.

<p>Take care, and walk in beauty.</p>
