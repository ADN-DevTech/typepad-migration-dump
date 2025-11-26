---
layout: "post"
title: "Get Project Parameter Id and Prevent Updater Loop"
date: "2019-10-01 05:00:00"
author: "Jeremy Tammik"
categories:
  - "DA4R"
  - "DMU"
  - "Filters"
  - "Forge"
  - "Parameters"
  - "View"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/10/get-project-parameter-id-and-prevent-updater-loop.html "
typepad_basename: "get-project-parameter-id-and-prevent-updater-loop"
typepad_status: "Publish"
---

<p>Let me once again highlight two helpful answers
by Frank <a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/2083518">@Fair59</a> Aarssen in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a>
and the newest pair of Forge Design Automation samples:</p>

<ul>
<li><a href="#2">Get project parameter id from its name</a></li>
<li><a href="#3">Preventing an updater loop</a></li>
<li><a href="#4">New Forge and Design Automation samples</a></li>
</ul>

<h4><a name="2"></a> Get Project Parameter Id from its Name</h4>

<p>Frank shows how to retrieve a project parameter id given its name in the thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/create-view-filters-for-project-parameter/m-p/9051132">creating view filters for a project parameter</a>:</p>

<p><strong>Question:</strong> Occasionally, we have to make multiple view filters in a model based on a project parameter.
I am trying to put together a macro that I can modify when needed to help me create these filters.
However, I am struggling with being able to select any parameters that are not built-in to create to filters.
How can I find the parameter id when I only have the name?</p>

<p><strong>Answer:</strong> Project parameters are 'document-based; parameters and are stored in a (shared) <code>ParameterElement</code>.</p>

<p>Given a name, this is how you find the <code>Id</code>:</p>

<pre class="code">
  <span style="color:#2b91af;">ElementId</span>&nbsp;GetProjectParameterId(
  &nbsp;&nbsp;<span style="color:#2b91af;">Document</span>&nbsp;doc,&nbsp;
  &nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;name&nbsp;)
  {
  &nbsp;&nbsp;<span style="color:#2b91af;">ParameterElement</span>&nbsp;pElem&nbsp;
  &nbsp;&nbsp;&nbsp;&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">FilteredElementCollector</span>(&nbsp;doc&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.OfClass(&nbsp;<span style="color:blue;">typeof</span>(&nbsp;<span style="color:#2b91af;">ParameterElement</span>&nbsp;)&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Cast&lt;<span style="color:#2b91af;">ParameterElement</span>&gt;()
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.Where(&nbsp;e&nbsp;=&gt;&nbsp;e.Name.Equals(name)&nbsp;)
  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;.FirstOrDefault();

  &nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;pElem&nbsp;?.Id;
  }
</pre>

<p>On closer inspection, I noticed that this is basically a repetition of part of an earlier answer by Frank to a previous thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/deleting-a-non-shared-project-parameter/td-p/5975020">deleting a non-shared project parameter</a>.</p>

<h4><a name="3"></a> Preventing an Updater Loop</h4>

<p>Frank solves an infinite looping problem in the thread 
on <a href="https://forums.autodesk.com/t5/revit-api-forum/iupdater-get-family-instance-and-update-values/m-p/9053785">IUpdater get family instance and update values</a>:</p>

<p><strong>Question:</strong> My goal: Update family parameter values from the API when shape handles are pulled. </p>

<p>What works: <code>IUpdater</code> class that fires off my custom code when a user manipulates the shape handles on a family.</p>

<p>What doesn't work: updating the family parameters.</p>

<p>I have an <code>IUpdater</code> class that is fired only when family instances are changed.</p>

<p>My problem is that it doesn't seem to update the parameter, and it loops infinitely (until Revit crashes).
My end goal here is to get the family whose shape handle has been pulled, loop through each of its parameters and set a value, in this example just setting "4", but I have more advanced match being applied that is currently disabled while I figure this out.</p>

<p><strong>Answer:</strong> When you say, 'Revit is looping', it might be that the modification you apply in the updater Execute method triggers the same updater again, starting an infinite loop.</p>

<p>There are different levels of triggers and updaters. You may want to modify those so that the changes you make do not re-trigger your updater.</p>

<p>Alternatively, you might be able to add a switch to your updater Execute so that it is only active once and deactivated when it gets called a second time in the same transaction, to step out of the infinite loop.</p>

<p>Maybe some of
the <a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.31">other DMU samples</a> will
help resolve the problem.</p>

<p>Frank shares a simpler suggestion:</p>

<p>To prevent the loop, I usually test to see if the new value differs from the old value:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:blue;">string</span>&nbsp;newValue&nbsp;=&nbsp;<span style="color:#a31515;">&quot;4&quot;</span>;
&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;param.AsValueString()&nbsp;!=&nbsp;newValue&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;param.SetValueString(&nbsp;newValue&nbsp;);
&nbsp;&nbsp;}
</pre>

<p>Ever so many thanks to Frank for his endless flow of extremely insightful answers!</p>

<h4><a name="4"></a> New Forge and Design Automation Samples</h4>

<p>I do not discuss <a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.55">Forge Design Automation for Revit</a> or
DA4R as much as I would like, since there are still more than enough issues to be handled dealing with the pure Revit desktop API, and my other Forge Platform Development colleagues are more specialised on the web-based issues.</p>

<p>I would still like to highlight the continuous succession
of <a href="https://forge.autodesk.com/categories/code-samples">new Forge samples</a>,
recently including <a href="https://forge.autodesk.com/blog/communicate-servers-inside-design-automation">communication with servers from inside Design Automation</a>
and a <a href="https://forge.autodesk.com/blog/design-automation-revit-parameters-export-import-sample-excel">Design Automation for Revit sample demonstrating parameter export and import with Excel</a>.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a48b20f7200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a48b20f7200c image-full img-responsive" alt="Design Automation server communication" title="Design Automation server communication" src="/assets/image_9bcf53.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>
