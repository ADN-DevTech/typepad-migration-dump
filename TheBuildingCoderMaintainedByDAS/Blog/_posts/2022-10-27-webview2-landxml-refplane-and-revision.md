---
layout: "post"
title: "WebView2, LandXML, RefPlane and Revision"
date: "2022-10-27 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Data Access"
  - "Deletion"
  - "External"
original_url: "https://thebuildingcoder.typepad.com/blog/2022/10/webview2-landxml-refplane-and-revision.html "
typepad_basename: "webview2-landxml-refplane-and-revision"
typepad_status: "Publish"
---

<p>Questions on using WebView2 in an add-in cropped up now and then, so we share some clarification on that usage, plus a couple of other interesting little titbits from
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a>:</p>

<ul>
<li><a href="#2">WebView2 and CefSharp in Revit add-ins</a></li>
<li><a href="#2.1">WebView2 sample add-in project</a></li>
<li><a href="#3">Retrieve reference plane in element</a></li>
<li><a href="#4">Remove revisions on sheets</a></li>
<li><a href="#5">LandXML <code>P</code> tag</a></li>
</ul>

<h4><a name="2"></a> WebView2 and CefSharp in Revit Add-Ins</h4>

<p>Here are some thoughts and experiences from an internal discussion aimed at defining best practices for Revit add-ins using WebView2:</p>

<p><strong>Question:</strong> I would like to make use of WebView2 in my add-in.
What should I do?</p>

<ul>
<li>Rely on future versions of Revit installing the evergreen runtime?</li>
<li>Use the same User Data Folder created by Revit for instances of the web view in add-ins?</li>
</ul>

<p>For add-ins using WebView2 in Revit 2023 and earlier, before Revit itself includes it, is there a consensus on the best place to create a custom UDF?</p>

<p>My interpretation of the <code>CoreWebView2Environment.CreateAsync</code> docs are that all WebView2 invocations from the same process need to agree on the <code>options</code> object, but do not need to agree on the <code>userDataFolder</code>.</p>

<p>A sensible location for the UDF might be a subfolder of <em>%LOCALAPPDATA%\Autodesk\Revit\Autodesk Revit 2023</em>
&ndash; this is where Revit is currently creating the CEF cache folder &ndash;
but it's not clear if we should namespace that subfolder name with our add-in name or not. </p>

<p><strong>Answer:</strong> You only need to agree on the UDF if you want to use the same main WV2 process, i.e., act like a "tab" in the browser as opposed to starting a new browser "window". 
 The "tab" way &ndash; sharing the UDF and options &ndash; is more performant and uses less memory.</p>

<p>In that case, you need to use the same options as all the other instances.
If you use your own UDF, you are free to set any options you like.</p>

<p>However, we are currently finding WV2 unstable with Revit and may choose not to use it, in which case we will continue using CEF.</p>

<!--

We are seeing what looks like crashes of the rendering subprocesses in some situations like opening certain large files... memory issues? Blocking the main thread for too long?
We don't know yet.
It also crashes after leaving Revit idle overnight.

-->

<p>The worst part is that if one instance of WV2 crashes, all the other instances stop working, even newly created ones after that.
The only remedy is to restart Revit.
Other products' experience with WV2 seems to be fine and I haven't been able to reproduce these issues outside of Revit.
You might want to put your add-in through some tests.</p>

<!--

Actually, if it works for you without problems, we should probably compare notes how we are using and setting up WV2.

-->

<p>In Revit, we are working on a separate component that isolates the browser code from Revit and I would like all our uses to go through that.
It supports both CEF and WV2, so it's easy to switch which one we want to use.
Among other things, it takes care of setting up the UDF, so they are all in the same location.</p>

<p>The development builds of Revit include both WV2 and the evergreen runtime.
In fact, it was put into Revit even before we started the migration; looks like the SSO component uses it now.</p>

<p>Dynamo is currently fully committed to WebView2 from v2.17, so we are testing its stability there as well.</p>

<p><strong>Response:</strong> If there is a push to revert back to cefsharp, does this mean future releases won't have wv2?</p>

<p><strong>Answer:</strong> No, I think we will still be shipping both CefSharp and WV2 for some time, since our component needs both even if we're using just one of them.
 We will try to switch again once WV2 is more stable or once we have more time to figure out what's going on.
If anything, we might stop shipping CefSharp at some point. </p>

<p>Also, WV2 comes included in Win11 by default and can't be uninstalled (I think...), so that will be no issue hopefully soon.</p>

<h4><a name="2.1"></a> WebView2 Sample Add-In Project</h4>

<p><a href="https://www.linkedin.com/in/alexander-laktionov-a50474162/">Alexander Laktionov</a>, <a href="https://github.com/laksan1">@laksan1</a>,
shared a WebView2 sample add-in on <a href="https://www.linkedin.com/posts/alexander-laktionov-a50474162_bim-revit-autodesk-activity-6967953596038438913-R0xk?utm_source=share&amp;utm_medium=member_desktop">LinkedIn</a>:</p>

<p>Have you ever wondered, "How can I link a plugin in Revit to an application created on a web platform?"
Usually, plug-ins in Revit are created on a ribbon, but that's not the only way.
You can create your own panel in several ways:</p>

<ul>
<li>RibbonPanel (built into Revit)</li>
<li>Dockable Panel (built into Revit)</li>
<li>Electron</li>
<li>Telegram (you can create commands and call your plugins)</li>
<li>Any web project and connect it to the Revit plugin via sockets (websockets or socket.io), specifically:</li>
<li>WebView2</li>
</ul>

<p>Let's talk about the last method.
What is WebView2?
The Microsoft Edge WebView2 control allows you to embed web technologies (HTML, CSS, and JavaScript) into native applications.
The WebView2 control uses Microsoft Edge as a rendering mechanism to display web content in native applications.
The <a href="https://github.com/laksan1/WebView2Example">WebView2Example GitHub repository</a> shows 
such an application.</p>

<p>The repository contains two projects.
The first is a frontend made in angular (you can use any other framework).
It uses a single <code>WebView2Service</code> file to interact with your plugin to send data to the Revit application.
The second project is a C# project that includes the nugget package <code>Microsoft.Web.WebView2</code>.
For introductory purposes, I made a simple project that updates floors in the Revit model when they are changed in the WPF window (WebView2).</p>

<p><center>
<a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302aed93eb084200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302aed93eb084200d img-responsive" alt="WebView2Example" title="WebView2Example" src="/assets/image_ef578a.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br />
</center></p>

<p>Many thanks to Alexander for sharing this nice sample!</p>

<h4><a name="3"></a> Retrieve Reference Plane in Element</h4>

<p>Some clarification on how symbol and instance geometry is generated from the family definiton:</p>

<p><strong>Question:</strong> How can I retrieve a reference plane that is inside an element?
I prefer to avoid opening and analysing the family file.
Is it possible to get the reference plane directly from the element via Revit API?</p>

<p><strong>Answer:</strong> You don't need to open the family.
If there is a placed instance, you can call <code>GetReferenceByName</code> on that instance, or get all the references from it.</p>

<p><strong>Response:</strong> I am able to get the reference using </p>

<pre class="code">
Reference reference=famInst.GetReferenceByName("Center (Left/Right)");
</pre>

<p>Now, how to get the reference plane from the reference?</p>

<p><strong>Answer:</strong> Use <code>GetGeometryObjectFromReference</code>.</p>

<p><strong>Response:</strong> That throws an exception saying that I cannot convert <code>GeometryObject</code> to <code>ReferencePlane</code>:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302ae7b08261d200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302ae7b08261d200b img-responsive" alt="Cast error" title="Cast error"  src="/assets/image_7d3df6.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p><strong>Answer:</strong> I think the problem is that you're actually getting back a surface instead of a plane, so your cast fails.
try casting to a surface, or, better yet, check out the underlying type in Visual Studio.</p>

<p>A reference in an element is not a ReferencePlane element.
In the family definition, a ReferencePlane element is added, but in the family symbol and instance, all geometry generated is consumed into the single element. <br />
Therefore, you are receiving a Surface or more specifically a Plane, which gives you the location of the plane.
If you want to attach a dimension to it, use the Reference you get originally.
You don't have access to a separate element from this.</p>

<h4><a name="4"></a> Remove Revisions on Sheets</h4>

<p>Two solutions
to <a href="https://forums.autodesk.com/t5/revit-api-forum/remove-revisions-on-sheets/m-p/11449618">remove revisions on sheets</a>:</p>

<p><strong>Question:</strong> I am writing an addin to edit revisions for many sheets simultaneously.
I see in the API that revisions can be added to a particular sheet using the <code>SetAdditionalRevisionIds</code> method.
However, I don't see an obvious way to remove revisions currently on a sheet.
(This is all assuming we are modifying revisions that were manually added and not controlled by any content on the sheet, e.g., revision clouds.)
Is there a way to remove revisions from a sheet in the API?</p>

<p><strong>Answer:</strong> Use <code>GetAdditionalRevisionIds</code> &ndash; these are the revisions not created by revision clouds on the sheet or in any of the placed views.</p>

<p>From the returned collection, remove the ids of the revisions that need removing.</p>

<p>Then call <code>SetAdditionalRevisionIds</code> with the edited collection from above.</p>

<p>P.S.: <code>GetAllRevisionIds</code> will return ALL revisions, from Revision clouds and "Revision on sheet" checkboxes.</p>

<p>Conclusion: <code>GetAllRevisionIds</code> - <code>GetAdditionalRevisionIds</code> = RevisionIds set by revision clouds.</p>

<p>Note that deleting (and adding) revision clouds is only possible if the Issued state of the revision is off.</p>

<p>This is no needed for <code>SetAdditionalRevisionId</code> (like in UI).</p>

<p><strong>Response:</strong> I thought I tried this method, and it made no changes (the revisions were set to 'Issued').
However, I must have done something wrong the first time, because when I reimplemented, it works great!</p>

<p><strong>Answer 2:</strong> This week I had to solve a similar problem.
Another way to approach this matter is by using the FilteredElementCollector.
Assuming everything related to all the revisions on a sheet will be deleted, you can break it down in steps.
First, delete all revision cloud tags, secondly delete the revision clouds and finally the revisions.
Below you can see an example.
To complete the routine, I also added a bit of code to avoid conflicts with revisions that are already issued.
Hopefully this will help somebody for future use.</p>

<pre class="code">
Dim revs As New SortedList(Of String, Boolean)
Dim colRev As New FilteredElementCollector(doc)

For Each r As Revision In colRev.OfCategory(BuiltInCategory.OST_Revisions)
   revs.Add(r.UniqueId, r.Issued)

   r.Issued = False
Next

Dim colRevClTags As New FilteredElementCollector(doc, sheet.Id)
doc.Delete(colRevClTags.OfCategory(BuiltInCategory.OST_RevisionCloudTags).ToElementIds)

Dim colRevCl As New FilteredElementCollector(doc, sheet.Id)
doc.Delete(colRevCl.OfCategory(BuiltInCategory.OST_RevisionClouds).ToElementIds)

Dim colRevs As New FilteredElementCollector(doc, sheet.Id)
doc.Delete(colRevs.OfCategory(BuiltInCategory.OST_Revisions).ToElementIds)

For Each r In revs
   Dim rev As Revision = doc.GetElement(r.Key)

   rev.Issued = r.Value
Next
</pre>

<p>Later: Little correction.
I just edited the code fragment I posted yesterday.
Recovering the issued state of a revision doesn't seem to work by using the element id.
Using the stable unique id instead fixes the problem.</p>

<p><strong>Response:</strong> Wow! Thanks for the solution!
Yours goes above and beyond my initial question.
I'll convert it to C# and try it out when I have some time available.</p>

<h4><a name="5"></a> LandXML P Tag</h4>

<p><a href="https://forums.autodesk.com/t5/revit-api-forum/a-question-about-exporting-and-reading-landxml/m-p/11405400">A question about exporting and reading LandXML</a>,
led to an explanation of the coordinate order in the <code>P</code> tag:</p>

<p><strong>Question:</strong> This may be a bit off-topic, but I wonder about <a href="https://thebuildingcoder.typepad.com/blog/2010/01/import-landxml-surface.html">exporting toposurface to LandXML</a>.</p>

<p>In LandXML, the <code>P</code> tag takes the coordinates in an unexpected order:</p>

<pre>
  &lt;P id="1"&gt;Y X Z&lt;/P&gt;
</pre>

<p>Why is the order of collocation (Y X Z) and not (X Y Z)?</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302ae749e19b7200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302ae749e19b7200d img-responsive" alt="LandXML P tag" title="LandXML P tag" src="/assets/image_2b2c01.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302ae749e19c3200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302ae749e19c3200d img-responsive" alt="LandXML P tag" title="LandXML P tag" src="/assets/image_21724f.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a></p>

<!--

<a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302ae6fb2bd7b200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"></a><br />

<a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302ae6fb2bd7f200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"></a><br />

-->

<p></center></p>

<p><strong>Answer:</strong> Yes.
That is sort of off-topic.
Not just here, but almost everywhere in the universe.
There are probably reasons for that definition, but who cares?
Are you planning to change the LandXML definition?
If so, then you might want to discuss this with the people responsible for it
at <a href="http://landxml.org">LandXML.org</a>.</p>

<p>In general, when I am programming something that connects with something else, I have to accept the given conditions and adapt to them.
It may help to know the underlying reasons, but only in theory, for my acceptance and motivation.
If I can accept the facts and motivate myself regardless, there is no need to understand the underlying reasons.
Actually, that applies to every aspect of life (and death):
"Ours is not to question why; ours is but to do or die."</p>

<p>Later: I discovered an answer after all, in
the <a href="http://www.landxml.org/schema/LandXML-1.2/documentation/LandXML-1.2Doc_P.html#Link07F5D020">LandXML specification for the <code>P</code> tag</a>:</p>

<blockquote>
  <p>A surface point. it contains an id attribute and a space delimited "northing easting elevation" text value.</p>
</blockquote>

<p>The order of northing, easting, elevation makes perfect sense in the LandXML domain, and translates directly to the Y, X, Z that you observe in the file format receiving its coordinates from an instance of the Revit <code>XYZ</code> class.</p>
