---
layout: "post"
title: "Collaboration, Dockables, Railings, Pop-Ups, ILSpy"
date: "2021-07-14 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "360"
  - "BIM"
  - "Geometry"
  - "Getting Started"
  - "User Interface"
  - "Win32"
original_url: "https://thebuildingcoder.typepad.com/blog/2021/07/collaboration-dockables-railings-pop-ups-ilspy.html "
typepad_basename: "collaboration-dockables-railings-pop-ups-ilspy"
typepad_status: "Publish"
---

<p>Here is an invitation to the upcoming AEC collaboration webinar and overviews over dockable panels, dialogue handling, decompilation and railing geometry:</p>

<ul>
<li><a href="#2">AEC collaboration webinar</a></li>
<li><a href="#3">Dockable panels and <code>WebView2</code></a></li>
<li><a href="#4">Dismissing Revit pop-ups</a></li>
<li><a href="#5">Check API changes using decompilation</a></li>
<li><a href="#6">Railing geometry</a></li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302788037dd13200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302788037dd13200d image-full img-responsive" alt="AEC Collaboration: BIM Collaborate for Project Managers" title="AEC Collaboration: BIM Collaborate for Project Managers" src="/assets/image_e322f5.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="2"></a> AEC Collaboration Webinar</h4>

<p>Interested in AEC collaboration and coordination?</p>

<p>We have a one-hour live webinar
on <a href="https://www.autodesk.com/webinars/aec/bim-collaborate-for-project-managers">AEC Collaboration: BIM Collaborate for Project Managers</a> coming
up on <a href="https://www.timeanddate.com/worldclock/converter.html?iso=20210728T170000&amp;p1=tz_pt&amp;p2=tz_et&amp;p3=tz_cest">July 28, 2021 at 10am PT / 1pm ET / 19:00 CET</a>.</p>

<p>Take your work anywhere, see your design progress clearly and work flexibly without disruption.</p>

<p>Join Autodesk Technical Specialists as they guide you into workflows supported by Autodesk BIM Collaborate. Learn to use analytical tools and reports to make data driven decisions for your project, see an overview of Insight and see how AI can help keep projects on track.</p>

<p>Learn the power of automated clash detection of Model Coordination, how to create and manage issues for coordination and explore other tools that save time and improve change visibility in your models; no Revit skills necessary.</p>

<p>Our experts will cover:</p>

<ul>
<li>Reports</li>
<li>Insight and Design Risk dashboard</li>
<li>Model Coordination</li>
<li>Meetings</li>
<li>Change visualization tracker</li>
</ul>

<p><a href="https://www.autodesk.com/webinars/aec/bim-collaborate-for-project-managers">Link to registration</a>.</p>

<p>Can't attend live?
<a href="https://www.autodesk.com/webinars/aec/bim-collaborate-for-project-managers">Register</a> anyway
and we'll send you the recording after the webinar.</p>

<h4><a name="3"></a> Dockable Panels and WebView2</h4>

<p>Konrad Sobon presents a very nice general introduction get started with dockable panels in his article
on <a href="https://archi-lab.net/webview2-and-revits-dockable-panel">WebView2 and Revit’s Dockable Panel</a>.</p>

<p>Unfortunately, he runs into a problem using <code>WebView2</code> to host a browser in them.</p>

<p>Many thanks to Konrad for the nice introduction to dockable panels and subsequent problem analysis!</p>

<p>Jason Masters explained <a href="https://archi-lab.net/webview2-and-revits-dockable-panel/#comment-2813">how he solved the conflict using named pipes to connect to a separate UI process</a>, similar to the suggestion to achieve
<a href="https://thebuildingcoder.typepad.com/blog/2019/04/set-floor-level-and-use-ipc-for-disentanglement.html#6">disentanglement and independence via IPC</a>:</p>

<blockquote>
  <p>It’s so frustrating, because DLL hell was solved by Microsoft, like 15 years ago, using strong naming, but Autodesk just doesn’t support it.</p>
  
  <p>Personally, I just use Electron, built my whole client application there, and just shuttle <code>json</code> data back and forth to a thin Revit wrapper over named pipes.
  Still, the potential for DLL conflicts with different versions of <code>Newtonsoft.json</code> remains, but thankfully its core API has stayed pretty stable and consistent.</p>
</blockquote>

<h4><a name="4"></a> Dismissing Revit Pop-Ups</h4>

<p>Another article by Konrad
discusses <a href="https://archi-lab.net/dismissing-revit-pop-ups-the-easy-and-not-so-easy-ways">dismissing Revit pop-ups &ndash; the easy and not so easy ways</a> and
explains</p>

<ul>
<li>How to set up and use the <code>DialogBoxShowing</code> event for the Revit-API-style solution</li>
<li>Using the Win32Api <code>FindWindow</code> and <code>GetWindowText</code> methods to find the right button and simulate a user click on it</li>
</ul>

<p>Yet again many thanks to Konrad for this helpful overview!</p>

<p>This explanation nicely complements the existing articles in the topic group
5.32 on <a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.32">detecting and handling dialogues and failures</a>.</p>

<h4><a name="5"></a> Check API Changes using Decompilation</h4>

<p>Comparing changes between different versions of the Revit API is possible using
the free <a href="https://github.com/icsharpcode/ILSpy">ILSpy tool</a> (e.g., version 5.0.0.4861-preview3)
to:</p>

<ul>
<li>Open the <code>RevitAPI.dll</code> assembly</li>
<li>Select the root node</li>
<li>Select File &gt; Save Code...</li>
<li>Select an empty folder to save all the decompiled source code</li>
<li>Create a git repo </li>
<li>Save the API source code</li>
</ul>

<p>Repeat this process with another version of <code>RevitAPI.dll</code> to generate the decompiled source code, commit into the git repo to compare the different releases and the changes between them.</p>

<h4><a name="6"></a> Railing Geometry</h4>

<p>Some useful aspects of the tricky task of accessing railing geometry can be gleaned from these snippets of an internal discussion:</p>

<p>Q: How can I extract the geometry from a <code>RailingType</code> element?</p>

<p>A: Last time I tried, the best way to get geometry was using the <code>IModelExportContext</code>, but I forget whether a railing type actually has geometry one can get from API or if it's all on the railing.</p>

<p>There is also the <code>Element.Geometry</code> property, <code>GeometryElement</code> and <code>GeometryInstance</code> objects that can be obtained directly, but in case of railings it could have some specific complications.</p>

<p>Exactly, railings have a complicated representation and I have no idea what Element.get_Geometry actually gives for a RailingType.
So, I'd need to look into an example and see what they give and what's best to use.</p>

<p>For <code>RailingType</code> elements, <code>get_Geometry</code> returns null.
I then used <code>GetDependentElements</code> with a filter for <code>Railing</code> elements.
I can then call <code>get_Geometry</code> and/or <code>GetGeometryInstances</code> on the <code>Railing</code> element.
Is this a valid workflow?
Also, I would like to use <code>GetGeometryInstances</code> on the <code>Railing</code> element, but it is returning an identity transform; so, it is not using instanced geometry?</p>

<p>Well, a railing has its own RailingType, so the railing being an identity transform of something does make sense regardless of where the railing is positioned.</p>

<p>The issue is that the railing element is instanced 600 places in the model.
I thought I would be able to get the instance geometry and it's transform.</p>

<p>I'm confused &ndash; railings aren't instanced.</p>

<p>I think I answered my own question.
The RailingType has a dependent Railing element.
This Railing element has a GeometryInstance in its GeometryElement.
I can get the geometry and its transform from this element.</p>

<p>That sounds right &ndash; a Railing geometrically should be small, one or a few GeometryInstances would be expected.</p>

<p>There is some potential difference with the continuous rails; these are separate Elements but you may see them as part of the Railing's geometry.
In that case, it's also easy to encounter the continuous rails twice; in fact, Revit draws them twice, which is why you can tab select a top rail, for example, to toggle between the top rail alone and the railing which its attached to.</p>
