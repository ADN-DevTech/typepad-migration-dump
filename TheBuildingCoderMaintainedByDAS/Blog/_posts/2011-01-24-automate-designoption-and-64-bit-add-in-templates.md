---
layout: "post"
title: "Automate DesignOption and 64 Bit Add-In Templates"
date: "2011-01-24 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2011"
  - "Automation"
  - "External"
  - "Settings"
  - "User Interface"
  - "Utilities"
  - "VB"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/01/automate-designoption-and-64-bit-add-in-templates.html "
typepad_basename: "automate-designoption-and-64-bit-add-in-templates"
typepad_status: "Publish"
---

<p>Let's start off the week with another idea from Rudolf Honke of

<a href="http://www.acadgraph.de">
acadGraph CADstudio GmbH</a>.

He recently presented his results of exploring the Revit ribbon internals using UISpy,

<a href="http://thebuildingcoder.typepad.com/blog/2011/01/ribbon-spying-and-ui-automation.html">
driving Revit using UI Automation</a>,

<a href="http://thebuildingcoder.typepad.com/blog/2011/01/subscribing-to-ui-automation-events.html">
subscribing to UI Automation events</a>, and 

<a href="http://thebuildingcoder.typepad.com/blog/2011/01/further-ideas-for-using-ui-automation.html">
switching between different projects and views</a>.

<p>Here is another of his ideas of making use of UI Automation:

<p>This screen snapshot shows the design options dropdown list:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330148c7f01a49970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330148c7f01a49970c" alt="Design options dropdown list" title="Design options dropdown list" src="/assets/image_0c35ee.jpg" border="0" /></a> <br />

</center>

<p>When showing elements programmatically using the app.ActiveUIDocument.ShowElements( elementSet ) method, it might occur that some elements cannot be shown because their design option is currently invisible.

<p>The current design option visibility is defined per document and affects all views.

<p>UI Automation could be used to set the current design option to the selected element's one, so it can be made visible.

<p>So, if you want to show a given element, you can compare its design option with the current one before showing it.

<p>Of course this is only possible if all elements in the element set passed in the ShowElements have the same design option.

<p>If you just want to show a single element, this will never be a problem.

<p>Yet again, many thanks to Rudolf for his many ideas and these valuable pointers!

<a name="2"></a>

<h4>64-bit Visual Studio DevTV Revit Add-in Template</h4>

Stephen LeCompte posted a 

<a href="http://thebuildingcoder.typepad.com/blog/2010/07/devtv-addin-templates.html?cid=6a00e553e1689788330148c784cbbc970c#comment-6a00e553e1689788330148c784cbbc970c">
comment</a> on the

<a href="http://thebuildingcoder.typepad.com/blog/2010/07/devtv-addin-templates.html">
DevTV add-in templates</a> which 

integrate with the Visual Studio new project wizard to automatically set up a new Visual Studio project for a Revit add-in complete with skeleton external application, external command, and add-in manifest code. 
By the way, there is also a streamlined and

<a href="http://thebuildingcoder.typepad.com/blog/2010/10/revit-2011-devtv.html#2">
updated C# version</a> for

my personal use with comments removed and other tweaks.

<p>Stephen worked on making use of the templates on a 64 bit system and ran 

<a href="http://thebuildingcoder.typepad.com/blog/2010/07/devtv-addin-templates.html?cid=6a00e553e1689788330148c77b8fd9970c#comment-6a00e553e1689788330148c77b8fd9970c">
into</a>

<a href="http://thebuildingcoder.typepad.com/blog/2010/07/devtv-addin-templates.html?cid=6a00e553e1689788330147e171f013970b#comment-6a00e553e1689788330147e171f013970b">
some</a>

<a href="http://thebuildingcoder.typepad.com/blog/2010/07/devtv-addin-templates.html?cid=6a00e553e1689788330147e172522d970b#comment-6a00e553e1689788330147e172522d970b">
problems</a> 

which were finally 

<a href="http://thebuildingcoder.typepad.com/blog/2010/07/devtv-addin-templates.html?cid=6a00e553e1689788330148c784cbbc970c#comment-6a00e553e1689788330148c784cbbc970c">
resolved</a>:

<p>OK, I did eventually find a solution to my problem where I do not have 32-bit Revit version installed but only 64-bit Revit.

<p>After downloading the winzip file above...

<ol>
<li>extract the contents
<li>within the .csproj file...
<ol type="a">
<li>do a find/replace on $(ProgramFiles) and replace with C:\Program Files
<li>find any instances where processorArchitecture=x86 and replace with processorArchitecture=x64
</ol>
<li>delete the old winzip file
<li>create a new .zip that combines all the files completed.
</ol>

<p>The initial problem was trivial, forgetting to delete the original winzip even though the appropriate changes were made.

<p>Here are Stephen's 64 bit templates:

<ul>
<li>

<span class="asset  asset-generic at-xid-6a00e553e1689788330148c7f0186b970c"><a href="http://thebuildingcoder.typepad.com/files/template64bitonlyrevitarchaddincs.zip">Template64BitOnlyRevitArchAddInCs.zip</a></span>

<li>

<span class="asset  asset-generic at-xid-6a00e553e1689788330148c7f01968970c"><a href="http://thebuildingcoder.typepad.com/files/template64bitonlyrevitarchaddinvb.zip">Template64BitOnlyRevitArchAddInVb.zip</a></span>

</ul>

<p>They have been set up so that if  you download the first templates and they do not work, you can still simply download this second version without deleting the old ones and see the new ones clearly as Revit Architecture 2011 64-bit only Addin.

<p>Many thanks to Stephen for solving the issue and sharing his files with us!
