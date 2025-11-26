---
layout: "post"
title: "Deleting a Global Parameter and RevitPythonShell"
date: "2017-11-13 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Data Access"
  - "Debugging"
  - "Deletion"
  - "Python"
original_url: "https://thebuildingcoder.typepad.com/blog/2017/11/deleting-a-global-parameter-and-revitpythonshell.html "
typepad_basename: "deleting-a-global-parameter-and-revitpythonshell"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>

<p>This is another entry in the list of my attempts at teaching Revit API developers how to fish instead of feeding them.
Mostly, it ends up a mixture between the two, of course:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2009/02/creating-a-group-and-how-to-fish.html">Creating a group and how to fish</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/07/teaching-a-man-how-to-fish-and-schedule-creation.html">Teaching a man how to fish and schedule creation</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/09/trusted-signature-motivation-and-fishing.html">Trusted signature motivation and fishing</a></li>
</ul>

<p>This time, we address the question on how to:</p>

<ul>
<li><a href="#2">Delete a global parameter</a> and </li>
<li><a href="#3">Test the result using RevitPythonShell</a></li>
</ul>

<h4><a name="2"></a>Deleting a Global Parameter</h4>

<p><strong>Question:</strong> I have list of all global parameters from the active Revit document. I want to delete a specific global parameter from the list programmatically. Kindly suggest a way to delete a global parameter from the active document.</p>

<p><strong>Answer:</strong> Thank you for your query.</p>

<p>You can easily answer this question yourself, you know.</p>

<p>I did not know either, on first reading your question.</p>

<p>Here is the path I took to search for an answer:</p>

<p>I initially <a href="https://duckduckgo.com/?q=revit+api+delete+global+parameter">searched the Internet for 'revit api delete global parameter'</a>.</p>

<p>This led to some non-API discussions, such as the one
by <a href="http://revitcat.blogspot.de">RevitCat</a> 
on <a href="http://revitcat.blogspot.de/2017/04/deleting-global-parameters-in-revit.html">Deleting Global Parameters in Revit</a>.</p>

<p>It also led to the official <a href="https://knowledge.autodesk.com/support/revit-products/getting-started/caas/CloudHelp/cloudhelp/2017/ENU/Revit-API/files/GUID-9FDC35A5-C054-46CA-B2DC-E20958FD197F-htm.html">developer guide discussion on managing global parameters</a>.</p>

<p>There, I learned that this is achieved programmatically using
the <a href="http://www.revitapidocs.com/2018.1/f3af05ec-1f0c-fe86-6708-0a211a40bcda.htm"><code>GlobalParametersManager</code> class</a>.</p>

<p>It does not provide any method to delete a global parameter.</p>

<p>However, the access to global parameters is provided by
the <a href="http://www.revitapidocs.com/2018.1/7c7a7bd3-18e8-d9be-d9a7-66cd9ecdccc7.htm"><code>FindByName</code> method</a>.</p>

<p>That method simply returns an element id.</p>

<p>This means that each global parameter is stored in the document database as a normal Revit element.</p>

<p>This means that it can be deleted using the <code>Document.Delete</code> method taking an <code>ElementId</code> or a collection, just like any other Revit element.</p>

<h4><a name="3"></a>Testing Using RevitPythonShell</h4>

<p>I decided to try this out on the fly
using <a href="https://github.com/architecture-building-systems/revitpythonshell">RevitPythonShell</a>.</p>

<p>I did not have it installed previously, but that can be achieved in seconds with a single click on
the <a href="https://github.com/architecture-building-systems/revitpythonshell#installation">RevitPythonShell installer</a>.</p>

<p>I then launched Revit and created a global parameter manually through the user interface:</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d2bdd4d0970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d2bdd4d0970c img-responsive" style="width: 475px; display: block; margin-left: auto; margin-right: auto;" alt="Global parameter" title="Global parameter" src="/assets/image_d7f9fa.jpg" /></a><br /></p>

<p></center></p>

<p>Next, I started the interactive Python shell and ran the following code:</p>

<pre class="prettyprint">
from Autodesk.Revit.DB import *
doc = __revit__.ActiveUIDocument.Document
id = GlobalParametersManager.FindByName(doc,'Test')
t = Transaction(doc)
t.Start('delete gp')
doc.Delete(id)
t.Commit()
</pre>

<p>Immediately after running this code, the global parameter is no longer listed in the user interface.</p>

<p>I trust this solves your issue.</p>
