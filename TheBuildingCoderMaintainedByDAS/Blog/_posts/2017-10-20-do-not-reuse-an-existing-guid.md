---
layout: "post"
title: "Do Not Reuse an Existing GUID"
date: "2017-10-20 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Getting Started"
  - "SDK Samples"
  - "Update"
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2017/10/do-not-reuse-an-existing-guid.html "
typepad_basename: "do-not-reuse-an-existing-guid"
typepad_status: "Publish"
---

<p>Several Revit API objects make use of
a <a href="https://en.wikipedia.org/wiki/Universally_unique_identifier">GUID</a> to
uniquely identify themselves.</p>

<p>When you copy and paste source code including any such GUID, you need to take care to replace the original GUID by your own one.</p>

<p>You can easily create a new GUID using the Visual Studio GUID generator tool <code>guidgen.exe</code> and by other means, cf. the explanation on creating
an <a href="http://thebuildingcoder.typepad.com/blog/2010/04/addin-manifest-and-guidize.html#4">add-in client id</a> for
a Revit add-in manifest.</p>

<p>Boost your BIM recently encountered and reported this issue in
its <a href="https://boostyourbim.wordpress.com/2017/10/13/quick-tip-change-those-guids">quick tip &ndash; change those GUIDs!</a></p>

<p>Here is another case illustrating the kind of problems that can occur if you simply copy and reuse an existing GUID:</p>

<p><strong>Question:</strong> I recently updated to Revit 2018.2.
As a result, a custom dockable panel now throws an error at load.
This error was not encountered until after the update:</p>

<pre>
Error Message:
Cannot register the same dockable pane ID more than once.
Parameter name: id
</pre>

<p></p>

<p>This is the code generating the error:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">DockablePaneId</span>&nbsp;id
&nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">DockablePaneId</span>(&nbsp;<span style="color:blue;">new</span>&nbsp;Guid(
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#a31515;">&quot;{D7C963CE-B7CA-426A-8D51-6E8254D21157}&quot;</span>&nbsp;)&nbsp;);

&nbsp;&nbsp;uiApp.RegisterDockablePane(&nbsp;id,&nbsp;<span style="color:#a31515;">&quot;Xyz&quot;</span>,
&nbsp;&nbsp;&nbsp;&nbsp;XyzPanelClass&nbsp;<span style="color:blue;">as</span>&nbsp;<span style="color:#2b91af;">IDockablePaneProvider</span>&nbsp;);
</pre>

<p>What happened?</p>

<p><strong>Answer:</strong> Several developers reported this issue.</p>

<p>I suspect people are copying code straight from
the <a href="http://thebuildingcoder.typepad.com/blog/2013/05/a-simpler-dockable-panel-sample.html">simpler dockable panel sample</a>,
not realizing they need to replace the GUID with their own one. </p>

<p>Create your own GUID to replace the original one and the problem will be resolved.</p>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b8d2b6803e970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b8d2b6803e970c img-responsive" style="width: 352px; " alt="GUID collision" title="GUID collision" src="/assets/image_d30fd9.jpg" /></a><br /></p>

<p></center></p>
