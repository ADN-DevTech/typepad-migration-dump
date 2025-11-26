---
layout: "post"
title: "No Revit API Class Diagram"
date: "2012-01-31 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "Debugging"
  - "Element Relationships"
  - "Getting Started"
  - "SDK Samples"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/01/no-revit-api-class-diagram.html "
typepad_basename: "no-revit-api-class-diagram"
typepad_status: "Publish"
---

<p>Quite a few people have asked for a Revit API class diagram in the past, and the request continues to pop up every now and then, so here is a post that I can point to in future when that happens.

<p><strong>Question:</strong> I am looking for a graphical diagram of the Revit API object model, because I cannot find it in the SDK. 
Where can I obtain that, please?

<p><strong>Answer:</strong> Unfortunately, I do not have any very satisfying answer for you.

<p>Once upon a time, the Revit SDK included a class diagram such as you are looking for. 
The last version was the 

<a href="http://thebuildingcoder.typepad.com/files/revit-api-class-diagram.png"><span class="asset  asset-image at-xid-6a00e553e16897883301630048cac3970d">Revit API Class Diagram.png</span></a><br /> provided 

in the Revit 2010 SDK:</p>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301630048d36a970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301630048d36a970d image-full" alt="Revit API Class Diagram" title="Revit API Class Diagram" src="/assets/image_d9a9c8.jpg" border="0" /></a><br />

<p>That is of course rather outdated by now.

<p>The class diagram has not been updated for the Revit 2011 or 2012 API, nor are there currently any plans to do so. 

<p>I personally find the interactive navigation in the Visual Studio object browser and the runtime code analysis and Intellisense more useful for my everyday needs, anyway.

<p>Here is what the development team responded regarding the current situation:

<p>Our tools for creating this diagram would have needed extensive updating to work with the new version and we did not think it was worth the effort. 
We also questioned how useful this diagram was to our customers. 
If you file a request for this to be reinstated, please include an explanation of you plan to use it, as there might be other ways for us to provide the needed information.

<p>The most reliable browsable information on the Revit object model is available by using the object browser built into Visual Studio: 

<p>In the solution explorer, open the 'References' node of your project and right click on 'RevitAPI', then select 'View in Object Browser'.

<p>You can also open the class view node Project References > RevitAPI, navigate to various classes, right click on them and select 'View Class Diagram'. Doing this step by step assembles a partial class diagram for you.

<p>It would theoretically also be possible to use .NET reflection to automatically generate a complete diagram of all classes defined by the Revit API. 

<p>An example of such a tool including complete source code is presented in the Code Project article on the <i>100% Reflective Class Diagram Creation Tool</i> 

<a href="http://www.codeproject.com/csharp/AutoDiagrammer.asp">
AutoDiagrammer</a> and 

its updated version

<a href="http://www.codeproject.com/KB/WPF/AutoDiagrammerII.aspx">
AutoDiagrammerII</a>.

<p>I tried to apply this tool to RevitAPI.dll years ago, and it did not work right out of the box back then.
On the other hand, it has been updated since then.
In any case, it might be possible to adapt the source code provided so that it works and completes. 
I am sure there are many other solutions out there to try, as well.

<p>Regarding the relationship of object types to one another, the derivation relationships of all classes are explicitly noted in the Revit API help file RevitAPI.chm, and the Visual Studio object browser can provide them to you as well.
The developer guide is a good source of background information including other dependencies between the different classes.

<p>What I myself use most of all is the F12 key in the Visual Studio IDE.
It takes me to the definition source code of any variable, method or class, and from there I can continue using F12 to navigate further upwards through the hierarchy graph to its parent and more distant ancestors.

<h4>Happy Birthday, Autodesk!</h4>

<p>Autodesk is 30 years old this month!

<p><a href="http://www.fourmilab.ch">Fourmilab</a> provides 

all the information from the horse's mouth, sorry, from the founder John Walker, and here is some 

<a href="http://through-the-interface.typepad.com/through_the_interface/2012/02/happy-nfy.html">
more detailed info on it from Kean</a>.
