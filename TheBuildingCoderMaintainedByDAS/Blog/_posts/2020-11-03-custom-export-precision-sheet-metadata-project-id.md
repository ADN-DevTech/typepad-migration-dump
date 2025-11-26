---
layout: "post"
title: "Custom Export Precision, Sheet Metadata, Project Id"
date: "2020-11-03 05:00:00"
author: "Jeremy Tammik"
categories:
  - "360"
  - "AI"
  - "Algorithm"
  - "AU"
  - "BIM"
  - "Data Access"
  - "Forge"
  - "User Interface"
  - "Win32"
original_url: "https://thebuildingcoder.typepad.com/blog/2020/11/custom-export-precision-sheet-metadata-project-id.html "
typepad_basename: "custom-export-precision-sheet-metadata-project-id"
typepad_status: "Publish"
---

<p>I lit upon many interesting topics in the past few days, on pure Revit API, Forge, BIM360 and AI:</p>

<ul>
<li><a href="#2">Custom export precision</a></li>
<li><a href="#3">Dismissing a Windows dialogue with JtClicker</a></li>
<li><a href="#4">AU classes for construction customers</a></li>
<li><a href="#5">Retrieve sheet metadata in Forge viewer</a></li>
<li><a href="#6">Determining the BIM 360 project id</a></li>
<li><a href="#7">AI solves partial differential equations</a></li>
<li><a href="#8">AI-enhanced video editing</a></li>
</ul>

<h4><a name="2"></a> Custom Export Precision</h4>

<p><a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/3074901">Sunsflower</a> took
another look at improving the precision of a custom exporter in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/customexporter-export-very-jagged-mesh-for-curved-surfaces/td-p/9820131">CustomExporter Export Very Jagged Mesh for Curved Surfaces</a>:</p>

<p><strong>Question:</strong> As shown in the screenshots below, when I tried to export a curved surface, the <code>OnPolyMesh</code> method in <code>IExportContext</code> produces very jagged edges:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833026bdea153ec200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833026bdea153ec200c img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="Jagged edges" title="Jagged edges" src="/assets/image_e20747.jpg" /></a><br /></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833026be4203cd3200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833026be4203cd3200d img-responsive" style="width: 400px; display: block; margin-left: auto; margin-right: auto;" alt="Smooth edges" title="Smooth edges" src="/assets/image_2b4573.jpg" /></a><br /></p>

<p></center></p>

<p>Is there a way to improve this?</p>

<p><strong>Answer:</strong> Check out The Building Coder topic group on
the <a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.1">custom exporter</a>.</p>

<p>Especially, please read these two posts:</p>

<ul>
<li><a href="https://thebuildingcoder.typepad.com/blog/2015/06/angelhack-athens-sustainability-and-export-precision.html#4">Revit export precision and tolerance</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2016/02/reorg-fomt-devcon-ted-qr-custom-exporter-quality.html#8">Controlling the quality of the geometry on custom export</a></li>
</ul>

<p>I all else fails, you will have to access the real element geometry instead of using the custom exporter.</p>

<p>That is normally a lot more work, though.</p>

<p><strong>Response:</strong> My solution is to triangulate the face in the <code>OnFace</code> method.
This way, I can input a <code>LevelOfDetail</code> parameter.
However, I lose all <code>UV</code> data at the same time.
Currently, I use <code>Face.Project</code> to approximate a set of <code>UV</code>, which is quite unstable.</p>

<p>I also tried to set the <code>LevelOfDetail</code> property on <code>ViewNode</code>, and it also works.</p>

<h4><a name="3"></a> Dismissing a Windows Dialogue with JtClicker</h4>

<p>Another topic group is dedicated
to <a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.32">detecting and handling dialogues and failures</a>.</p>

<p>It started out before the <a href="https://www.revitapidocs.com/2020/cb46ea4c-2b80-0ec2-063f-dda6f662948a.htm">DialogBoxShowing event</a> and
<a href="https://www.revitapidocs.com/2020/c03bb2e5-f679-bf24-4e87-08b3c3a08385.htm">Failure handling APIs</a> were implemented, using a Windows hook to determine that a dialogue was being shown:</p>

<p><strong>Question:</strong> You explained how to use the native Windows API hook
to <a href="https://thebuildingcoder.typepad.com/blog/2009/10/dismiss-dialogue-using-windows-api.html">dismiss a dialogue</a>.
Is there a complete sample project and solution available to understand how to use it to dismiss the dialogue box in Revit?</p>

<p><strong>Answer:</strong> Whenever searching for such information, one of the first places to go
are <a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#5">The Building Coder topic groups</a>.
In this case, you can look at <a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.32">detecting and handling dialogues and failures</a>.
The Windows hook functionality is not really used 'in Revit', as far as I can remember.
It is independent functionality that can interact with a Revit add-in, if you like.
The complete project is available in
the <a href="https://github.com/jeremytammik/JtClicker">JtClicker repository on GitHub</a>.</p>

<h4><a name="4"></a> AU Classes for Construction Customers</h4>

<p>Are you specifically interested in construction?
Check out the overview
of <a href="https://forge.autodesk.com/blog/forge-au-classes-construction">AU classes for construction customers</a>.</p>

<h4><a name="5"></a> Retrieve Sheet Metadata in Forge Viewer</h4>

<p>Now, let's turn to Forge.
Here is a pretty illuminating exploration on accessing Revit sheet metadata in that environment:</p>

<!-- https://autodesk.slack.com/archives/C0LP63082/p1602524162022900 -->

<p><strong>Question:</strong> I am trying to retrieve the 'Identity Data' of a sheet in a Revit model using the Model Derivative API:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e168978833026bdea15404200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833026bdea15404200c image-full img-responsive" alt="Sheet identity data" title="Sheet identity data" src="/assets/image_a61961.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Unfortunately, I was unable to find this info anywhere in the properties.
Is it possible, or do I have to use Design Automation for this?
Thanks!</p>

<p><strong>Answer:</strong>  It should be possible to get sheet properties by navigating the hierarchy of the object tree.
The root node (<code>id</code> = <code>1</code>) is the document, and the sheets will be listed as children of that root.
One would need to iterate through the children of the root to get the properties of the sheets and views.</p>

<p><strong>Response:</strong> I do see the model as <code>id</code> = <code>1</code>, but I can't find sheets as children of that root.
I also do not see <code>Sheet</code> in the model browser in the Forge viewer in any example (which certainly contain Revit models having sheets).
Am I overlooking something?</p>

<p><strong>Answer:</strong> The model browser will not show sheets, because they do not have physical geometry associated with them.
However, there will be sheet objects as children of the root.</p>

<p>Maybe, in some RVTs, sheets do not appear in the property database, though.
It probably depends on which API you use to get properties.
I'm referring to the full property database available to the Forge viewer.</p>

<p><strong>Response:</strong> This is an export of the tree of <code>/metadata</code> for my Revit model:</p>

<pre class="code">
Untitled 
{
"data": {
  "type": "objects",
  "objects": [
      {
      // ... (2,059 lines)
</pre>

<p>Here are the <code>/properties</code>:</p>

<pre class="code">
Untitled 
{
"data": {
  "type": "properties",
  "collection": [
      {
      // ... (20,859 lines)
</pre>

<p>These come from a sheet called "A102 - Plans".</p>

<p><strong>Answer:</strong> I don't know what subset of element properties are returned by the Forge properties API.
I do know that the Forge viewer will show sheet properties in some cases, e.g., in BIM360 Docs when you open the sheet.</p>

<p><strong>Response:</strong> Yes indeed, BIM 360 Docs does show properties of sheets.
I checked and confirmed.
Now I wonder how it gets those properties.</p>

<p><strong>Answer:</strong> Just like I said &ndash; it loops through the children of the root node and finds the sheet element with the matching name.
However, it's not using the Forge properties API.
It uses the raw property data, available to the Forge Viewer</p>

<p><strong>Response:</strong> I kind of understand what you say.
I understand that the properties are being retrieved by the raw property data.
However, to first select the element id, the hierarchy (from the <code>/metadata</code> endpoint) should retrieve sheets, right?
I don't see sheets in that response; or is it that there's also other raw data which is different from the Forge metadata API?</p>

<p><strong>Answer:</strong> The Forge metadata endpoint is not raw, it's processed data.
From the above, it looks like it's missing the child properties that will let you easily find the sheets from the root element.</p>

<p><strong>Response:</strong> Thanks, this is very helpful.
Final question: can this raw data be accessed by a customer?</p>

<p><strong>Answer:</strong> Yes, using Forge Viewer.
It may be possible to get this information via metadata somehow that I am not aware of.</p>

<p><strong>Response:</strong> Hmm... so, if I want to query and fetch attribute values, that won't be possible using Forge viewer, right?</p>

<p><strong>Answer:</strong> The MD service does let you perform queries to get the metadata you want, with two choices of data format.
If you run into data that MD does not collect, and Revit Design Automation would be your fallback.</p>

<p>Here is an example accessing additional metadata,
to <a href="https://github.com/augustogoncalves/forge-customproperty-revit">extract compound structure layer from RVT files using Design Automation for Revit</a>.</p>

<p>The resources listed for the <a href="https://forge.autodesk.com/blog/forge-au-2020-pre-event-online-bootcamp">Forge at AU 2020 pre-event online bootcamp</a> will probably also be useful for you.</p>

<h4><a name="6"></a> Determining the BIM 360 Project Id</h4>

<p>Kevin Augustino very kindly shared his current approach
to <a href="https://forums.autodesk.com/t5/revit-api-forum/bim-360-document-management-project-id-of-revit-cloud-model/m-p/9830419">retrieve the BIM 360 Document Management Project Id of the active Revit cloud model</a>:</p>

<p><strong>Question:</strong> How can I retrieve the BIM 360 Document Management Project Id of the active Revit model?
I'm aware of <em>Document.GetCloudModelPath().GetProjectGUID()</em>, but this seems to be a C4R Project Id.
I need the Document Management Id to interface with
the <a href="https://forge.autodesk.com/en/docs/bim360/v1/reference/http/">Forge BIM 360 and Data Management APIs</a>.</p>

<p>So far, I've found that the Document Management file has an attribute that matches the C4R Project Guid: <em>attributes.extension.data.projectGuid</em>.</p>

<p>So, I need to find the Docs project that contains a file such that:</p>

<pre>
  attributes.extension.data.projectGuid
    = &lt;ActiveRevitDocument&gt;.GetCloudModelPath().GetProjectGUID().
</pre>

<p>But surely there's a better approach than doing a <a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/projects-project_id-folders-folder_id-search-GET/">folder search</a> using a filter matching <em>filter[attributes.extension.data.projectGuid]</em> with <code>ValueFromCloudModelPath</code> on every Docs Project that my Forge App has access to?</p>

<p><strong>Answer:</strong> I asked the development team for you whether they can suggest a better way.
They are currently discussing the implementation of a direct method to retrieve the BIM 360 project id of the document via a property such as <code>Document.ProjectId</code>, now as we speak. It will hopefully be available in a future release of Revit.</p>

<p>Meanwhile, the convoluted approach you describe sounds significantly better than nothing at all to me, so well done finding a way through the maze.</p>

<p><strong>Response:</strong> For anyone else who runs into this same need, here are some of my other findings:</p>

<p><code>Document.PathName</code> seems to be a string in this form when opening a cloud model:</p>

<pre>
  BIM 360://&lt;DocsProjectName&gt;/&lt;ModelName&gt;.rvt
</pre>

<p>So, another option is to try parsing <code>Document.PathName</code> to get the Document Management Project name:</p>

<pre class="code">
  string regexPattern =
    @"^BIM 360:\/\/(?&lt;ProjectName&gt;.*)\/(?&lt;ModelName&gt;.*)$";

  if (Regex.IsMatch(doc.PathName, regexPattern))
  {
    Match match = Regex.Match( doc.PathName, regexPattern );
    string projectName = match.Groups["ProjectName"].Value;
  }
</pre>

<p>Then look for a project with that name by iterating each hub returned
from <em>https://forge.autodesk.com/en/docs/data/v2/reference/http/hubs-GET/</em>,
and, on each one, try
to <a href="https://forge.autodesk.com/en/docs/data/v2/reference/http/hubs-hub_id-projects-GET/">get a project using a name filter</a>, 
using a filter such as</p>

<pre>
  string.format( "?filter[attributes.name]={0}",
    HttpUtility.UrlEncode(projectName))
</pre>

<p></p>

<p>If this project name isn't unique, then this approach might not get the correct one.
But additional processing can be applied to use a folder search looking for</p>

<pre>
  attributes.extension.data.projectGuid
    = &lt;ActiveRevitDocument&gt;.GetCloudModelPath().GetProjectGUID()
</pre>

<p>So at least this way, the folder search is only done on potential matches, rather than every single project.</p>

<p>If the Document Management project name changes, then <code>Document.PathName</code> won't refresh to the new project name until you re-save the model.
So, as a fallback, if I still haven't found the project Id, I resort to the folder search on every project regardless of name.</p>

<p>Not ideal, but hopefully a direct method will be added to the Revit API in the future!</p>

<p>Many thanks to Kevin for all his research and documentation work on this!</p>

<h4><a name="7"></a> AI Solves Partial Differential Equations</h4>

<p><a href="https://www.technologyreview.com/2020/10/30/1011435/ai-fourier-neural-network-cracks-navier-stokes-and-partial-differential-equations">AI has cracked a key mathematical puzzle for understanding our world</a>:</p>

<blockquote>
  <p>Partial differential equations can describe everything from planetary motion to plate tectonics, but they’re notoriously hard to solve...</p>
  
  <p>They can be used to model everything from planetary orbits to plate tectonics to the air turbulence that disturbs a flight, which in turn allows us to do practical things like predict seismic activity and design safe planes...</p>
  
  <p>PDEs are notoriously hard to solve...</p>
  
  <p>Researchers at Caltech have introduced a new deep-learning technique for solving PDEs,
  a <a href="https://arxiv.org/pdf/2010.08895.pdf">Fourier Neural Operator for Parametric
  Partial Differential Equations</a>
  ... dramatically more accurate... much more generalizable ... 1'000 times faster ...</p>
</blockquote>

<h4><a name="8"></a> AI-Enhanced Video Editing</h4>

<p>Here is another example of AI usage that may come in handier to you right away than solving differential equations:</p>

<p>AU is coming up. Are you possibly thinking about recording a video?
Check out <a href="https://www.descript.com">Descript</a> before you do.
It is a collaborative audio and video editor that includes transcription, a screen recorder, publishing, full multitrack editing, and some mind-bendingly useful AI tools:</p>

<ul>
<li><a href="https://medium.com/descript/introducing-descript-fa37eb193819">Blog post</a></li>
<li><a href="https://youtu.be/Bl9wqNe5J8U">Video</a>:</li>
</ul>

<p><center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/Bl9wqNe5J8U" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>
</center></p>
