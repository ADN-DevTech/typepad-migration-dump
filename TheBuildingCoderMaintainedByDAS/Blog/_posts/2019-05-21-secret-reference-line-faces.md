---
layout: "post"
title: "Secret Reference Line Faces"
date: "2019-05-21 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Algorithm"
  - "Content"
  - "Debugging"
  - "Family"
  - "Geometry"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/05/secret-reference-line-faces.html "
typepad_basename: "secret-reference-line-faces"
typepad_status: "Publish"
---

<p>The open source space around the Revit API is continuously growing richer, solutions are shared and exciting discoveries are made, both in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> and elsewhere:</p>

<ul>
<li><a href="#2">Debugging, code signing and HOK Mission Control</a></li>
<li><a href="#3">Creating connectors on a reference line</a></li>
</ul>

<h4><a name="2"></a> Debugging, Code Signing and HOK Mission Control</h4>

<p>Yesterday I happened to notice a tweet
by Konrad Sobon, <a href="https://twitter.com/arch_laboratory">@arch_laboratory</a>, pointing
out a couple of exciting blog posts
on <a href="http://archi-lab.net">archi+lab</a>, e.g.:</p>

<ul>
<li><a href="http://archi-lab.net/debugging-revit-add-ins">Debugging Revit add-ins</a></li>
<li><a href="http://archi-lab.net/code-signing-of-your-revit-plug-ins">Code signing of your Revit plug-ins</a></li>
<li><a href="http://archi-lab.net/playing-with-power-shell-commands-and-post-build-events">Playing with Power Shell commands and Post Build events</a></li>
</ul>

<p>The latter one discusses some aspects of
the <a href="https://github.com/HOKGroup/HOK-Revit-Addins/tree/master/HOK.MissionControl">Mission Control repository</a> and
making it easier to pull, build, and deploy.</p>

<p>In fact, it is worth while taking a look at all
the open source <a href="https://github.com/HOKGroup/HOK-Revit-Addins">HOK Revit Addins</a>.</p>

<p>This is all very exciting stuff indeed.</p>

<p>Browsing further, I also discovered that Konrad just started
the <a href="http://archi-lab.net/next-chapter">next chapter</a> of his career.</p>

<p>Congratulations, Konrad!</p>

<p>The best of luck to you, and have fun!</p>

<h4><a name="3"></a> Creating Connectors on a Reference Line</h4>

<p><a href="https://forums.autodesk.com/t5/user/viewprofilepage/user-id/3894260">MarryTookMyCoffe</a> points
out a surprising and useful possibility 
for <a href="https://forums.autodesk.com/t5/revit-api-forum/creating-connectors-on-reference-line/m-p/8707761">creating connectors on a reference line</a>:</p>

<p>I made some tests and thought I will share my results with the forum:</p>

<p>The problem with putting connectors on a reference line is that the API doesn’t give us any references to any surfaces, no matter for what view we ask.</p>

<p>There is however a way to get around this one by adding a secret suffix to the unique id and calling the <code>ParseFromStableRepresentation</code> method on it:</p>

<pre>
  Reference.ParseFromStableRepresentation(
    document, uniqueID );
</pre>

<p>With the right secret suffix, this can be used to retrieve a reference to a face on the reference line.</p>

<p>With lots of trial and error, we learned that the codes for specific faces will be always the same:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4887de0200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4887de0200d image-full img-responsive" alt="Creating connectors on a reference line" title="Creating connectors on a reference line" src="/assets/image_560fbb.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>For example, let's look at this stable representation:</p>

<pre>
  3ded2a48-367f-42c7-83bd-9fd4f659891a-00000fd0:2
</pre>

<p>Here is a table summarising the possible secret suffixes:</p>

<table>
<tr><td style="text-align: right">3ded2a48-367f-42c7-83bd&nbsp;&nbsp;&nbsp;<br/>-9fd4f659891a-00000fd0&nbsp;&nbsp;&nbsp;</td><td>The Reference Line<br/>unique id</td></tr>
<tr><td style="text-align: right">:0&nbsp;&nbsp;&nbsp;</td><td>ref to line</td></tr>
<tr><td style="text-align: right">:1&nbsp;&nbsp;&nbsp;</td><td>ref to solid</td></tr>
<tr><td style="text-align: right">:2&nbsp;&nbsp;&nbsp;</td><td>ref to face</td></tr>
<tr><td style="text-align: right">:7&nbsp;&nbsp;&nbsp;</td><td>ref to face</td></tr>
<tr><td style="text-align: right">:12&nbsp;&nbsp;&nbsp;</td><td>ref to face</td></tr>
<tr><td style="text-align: right">:17&nbsp;&nbsp;&nbsp;</td><td>ref to face</td></tr>
</table>

<p>The gaps between the numbers may seems strange, but I think that after every face, we have hidden edges of this face; what would explain the numeration.</p>

<p>[Q] Does that apply to each and every reference line?</p>

<p>[A] Yes, it applies to every reference line, always the same numbers.</p>

<p>[Q] So each reference line implicitly defines a solid and four faces?</p>

<p>[A] Faces are in the solid, but yes.</p>

<p>[Q] What solid is that?</p>

<p>[A] Similar to an extrusion, you can get a solid of the object with faces and lines, with the only difference that its faces don't have references. I guess the programmer forgot to set up this property.</p>

<p>[Q] Can you share a concrete use case for this?</p>

<p>[A] I use reference line with connectors in all fittings and accessories; that enables my family definition to work with minimal graphics. That way, you can easily define a family with low details (that is important in GB) and high details (that is important in Russia), without messing up how they work.</p>

<p>I plan to go even further and implement an auto-creation tool for fittings, accessories, and mechanical equipment. </p>

<p>The idea was to add to any family made on the market the same functionality that our families have &ndash; all you have to do is set length and type of connection, like: thread, pressed, etc.</p>

<p>Very many thanks to MarryTookMyCoffe for discovering and sharing this valuable insight!</p>

<p><center>
<i>once downhill, once uphill, but always on foot.</i>
</center></p>
