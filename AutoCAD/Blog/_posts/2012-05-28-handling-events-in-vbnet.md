---
layout: "post"
title: "Handling events in VB.NET"
date: "2012-05-28 10:44:09"
author: "Adam Nagy"
categories:
  - ".NET"
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/05/handling-events-in-vbnet.html "
typepad_basename: "handling-events-in-vbnet"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I found a C# sample project called EventsWatcher, but I'm not sure how to do similar event handling in VB.NET</p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>In VB.NET you can handle events in two ways:</p>
<p>1) The VB6 way:</p>
<ol>
<li>declare a variable with WithEvents keyword </li>
<li>set it to the object which provides the events </li>
<li>select the variable in the combo box on the left side (A) above the coding area, then select the event you want to handle in the combo box on the right side (B) </li>
</ol>

<a class="asset-img-link"  style="display: inline;" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b016766e02b8f970b-pi"><img class="asset  asset-image at-xid-6a0167607c2431970b016766e02b8f970b image-full" alt="Vbevents" title="Vbevents" src="/assets/image_103173.jpg" border="0" /></a><br />
<p>2) The .NET way / handling a specific event</p>
<ol>
<li>define a function you want to handle the event with, e.g. OnDocumentDestroyed </li>
<li>create a delegate for the function, e.g. New DocumentDestroyedEventHandler(AddressOf OnDocumentDestroyed) </li>
<li>use AddHandler to add the delegate as one of the handlers of the specific event </li>
</ol>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Imports</span><span style="line-height: 140%;"> Autodesk.AutoCAD.Runtime</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Imports</span><span style="line-height: 140%;"> Autodesk.AutoCAD.DatabaseServices</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Imports</span><span style="line-height: 140%;"> Autodesk.AutoCAD.ApplicationServices</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Class</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">MyClass1</span><span style="line-height: 140%;">&nbsp; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: green;">' VB style&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">Private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Shared</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">WithEvents</span><span style="line-height: 140%;"> docEvents </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">DocumentCollection</span><span style="line-height: 140%;">&nbsp; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">Private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Shared</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> docEvents_DocumentCreated(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">ByVal</span><span style="line-height: 140%;"> sender </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Object</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: blue;">ByVal</span><span style="line-height: 140%;"> e </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">DocumentCollectionEventArgs</span><span style="line-height: 140%;">) _</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Handles</span><span style="line-height: 140%;"> docEvents.DocumentCreated&nbsp;&nbsp;&nbsp; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; MsgBox(e.Document.Name &amp; </span><span style="line-height: 140%; color: #a31515;">" opened"</span><span style="line-height: 140%;">)&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;">&nbsp; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: green;">' .NET style&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">Private</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Shared</span><span style="line-height: 140%;"> docDestroyedEvent </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">DocumentDestroyedEventHandler</span><span style="line-height: 140%;">&nbsp; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Shared</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> OnDocumentDestroyed(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">ByVal</span><span style="line-height: 140%;"> sender </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Object</span><span style="line-height: 140%;">, </span><span style="line-height: 140%; color: blue;">ByVal</span><span style="line-height: 140%;"> e </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">DocumentDestroyedEventArgs</span><span style="line-height: 140%;">) </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; MsgBox(e.FileName &amp; </span><span style="line-height: 140%; color: #a31515;">" closed"</span><span style="line-height: 140%;">)&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;">&nbsp; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &lt;</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">"StartEventHandling"</span><span style="line-height: 140%;">)&gt; _&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> Asdkcmd1()&nbsp;&nbsp;&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">' Start VB Style handling – all docEvents related event </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">' handling is started&nbsp;&nbsp;&nbsp;&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; docEvents = </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager&nbsp;&nbsp;&nbsp; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">' Start .NET style handling – only this specific event </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">' handling is started&nbsp;&nbsp;&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> docs </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">DocumentCollection</span><span style="line-height: 140%;"> = </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager&nbsp;&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> docDestroyedEvent </span><span style="line-height: 140%; color: blue;">Is</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Nothing</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Then</span><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; docDestroyedEvent = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">New</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">DocumentDestroyedEventHandler</span><span style="line-height: 140%;">(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">AddressOf</span><span style="line-height: 140%;"> OnDocumentDestroyed)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">AddHandler</span><span style="line-height: 140%;"> docs.DocumentDestroyed, docDestroyedEvent&nbsp;&nbsp;&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;">&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;">&nbsp; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; &lt;</span><span style="line-height: 140%; color: #2b91af;">CommandMethod</span><span style="line-height: 140%;">(</span><span style="line-height: 140%; color: #a31515;">"StopEventHandling"</span><span style="line-height: 140%;">)&gt; _&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">Public</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span><span style="line-height: 140%;"> Asdkcmd2()&nbsp;&nbsp;&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">' Stop VB style handling – all docEvents related event handling </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">' is stopped&nbsp;&nbsp;&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; docEvents = </span><span style="line-height: 140%; color: blue;">Nothing</span><span style="line-height: 140%;">&nbsp;&nbsp; </span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">' Stop .NET style handling – only this specific event handling </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: green;">' is stopped&nbsp;&nbsp;&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">Dim</span><span style="line-height: 140%;"> docs </span><span style="line-height: 140%; color: blue;">As</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">DocumentCollection</span><span style="line-height: 140%;"> = </span><span style="line-height: 140%; color: #2b91af;">Application</span><span style="line-height: 140%;">.DocumentManager&nbsp;&nbsp;&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Not</span><span style="line-height: 140%;"> docDestroyedEvent </span><span style="line-height: 140%; color: blue;">is</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Nothing</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Then</span><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">RemoveHandler</span><span style="line-height: 140%;"> docs.DocumentDestroyed, docDestroyedEvent&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; docDestroyedEvent = </span><span style="line-height: 140%; color: blue;">Nothing</span><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">If</span><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Sub</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">End</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: blue;">Class</span></p>
</div>
<p>In VB.NET “Shared” is the C#/C++ equivalent of “static”, which means that the same instance of the variable will be available for all instances of the container class. <br />You find this being used in the EventsWatcher sample as well. It’s needed, because otherwise a new instance of MyClass1 would be created for each document. <br />So without this “Shared” keyword, each document in which you called “StartEventHandling” would start running its own event handler.</p>
