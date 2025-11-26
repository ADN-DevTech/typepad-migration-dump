---
layout: "post"
title: "Camera Settings, Doors Traversed, Script on the Fly"
date: "2024-03-19 06:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "AI"
  - "Geometry"
  - "Sustainability"
  - "Travel"
  - "View"
  - "Viewer"
original_url: "https://thebuildingcoder.typepad.com/blog/2024/03/camera-settings-doors-traversed-script-on-the-fly.html "
typepad_basename: "camera-settings-doors-traversed-script-on-the-fly"
typepad_status: "Publish"
---

<p>New exciting Revit API solutions and continued furious pace of LLM development:</p>

<ul>
<li><a href="#2">Bowerbird C&#35; scripting for Revit</a></li>
<li><a href="#3">Doors traversed by path of travel</a></li>
<li><a href="#4">Camera mapping between APS and Revit</a></li>
<li><a href="#5">Claude 3 can see</a></li>
<li><a href="#6">Devin, an AI software engineer</a></li>
<li><a href="#7">Meta Imagine generates images</a></li>
<li><a href="#8">An LLM for decompiling binary code</a></li>
<li><a href="#9">Simple climate change overview</a></li>
</ul>

<h4><a name="2"></a> Bowerbird C&#35; Scripting for Revit</h4>

<p><a href="https://github.com/cdiggins">Christopher Diggins</a> published
<a href="https://github.com/ara3d/bowerbird">Bowerbird</a> for
quick and easy C&#35; tool and plug-in development by dynamically compiling C# source files,
and a <a href="https://forums.autodesk.com/t5/revit-api-forum/feedback-requested-bowerbird-c-scripting-for-revit/td-p/12643568">request for feedback on it</a>
in the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a>,
saying:</p>

<blockquote>
  <p>I've released a new open-source project for Revit C# developers called Bowerbird.
  It uses the Roslyn C# compiler to allow users to create and edit new commands directly from C# source files,
  without having to go through the process of creating and deploying a plug-in, and re-launching Revit.</p>
  
  <p>It is inspired
  by <a href="https://github.com/eirannejad/pyRevit">pyRevit</a> by <a href="https://github.com/eirannejad">Ehsan Iran-Nejad</a>
  and <a href="https://github.com/sridharbaldava/Revit.ScriptCS">Revit.ScriptCS</a> by <a href="https://github.com/sridharbaldava">Sridhar Baldava</a>.</p>
  
  <p>I'd greatly appreciate any feedback or contributions in
  the <a href="https://github.com/ara3d/bowerbird/">Bowerbird GitHub project</a>.
  Thanks in advance!</p>
</blockquote>

<p>Many thanks to Christopher for creating and sharing this helpful tool!</p>

<h4><a name="3"></a> Doors Traversed by Path of Travel</h4>

<p>A while ago, I took a look at determining the doors traversed by a path of travel and shared some thoughts on that in
the <a href="https://github.com/jeremytammik/PathOfTravelDoors">PathOfTravelDoors GitHub repo</a>.</p>

<p>They were picked up again in
the <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on <a href="https://forums.autodesk.com/t5/revit-api-forum/doors-traversed-on-path-of-travel-lines/td-p/12616109">doors traversed on path of travel lines</a>.</p>

<p><strong>Question:</strong> I want to list all the doors that are crossed by a path of travel line.
I tried to code that, but it seems that the <code>ReferenceIntersector</code> finds more doors that are not on the path of travel, because the introduced ray is reaching them.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3ae4291200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3ae4291200d image-full img-responsive" alt="Path of travel doors" title="Path of travel doors" src="/assets/image_3adab9.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>I looked at the Revit SDK sample PathOfTravel, but that does not help.</p>

<p>The <a href="https://www.revitapidocs.com/2024/c4fb6c89-ca34-7c56-b730-98755d11fedf.htm">ReferenceIntersector documentation</a> is illuminating, and
the <a href="https://www.revitapidocs.com/2024/866e1f2b-c79a-4d9f-1db1-9e386dd42941.htm">FindNearest method</a> ought
to ensure that I only get a maximum of one single intersected door.</p>

<p><strong>Answer:</strong> Hmm. Maybe, this task can be addressed simpler.
How about this approach without using the reference intersector at all?</p>

<ul>
<li>Retrieve the path of travel curve tessellation</li>
<li>For each line segment, determine whether it intersects a door</li>
</ul>

<p>Afaict, that should solve the problem right there.
What do you think?</p>

<p><strong>Response:</strong> Just cracked it an hour ago!
I tackled it with this trick:
For each curve in the path of travel line I did once from the start point following the curve's direction, and once from the endpoint with the reversed direction.
Then, I accepted the points that appeared in both.</p>

<p>I also thought of another approach as you mentioned: generating an imaginary line at each door location and checking whether the path of travel line segments intersects that.
However, this method required finding the two points of each door, possibly by examining the geometry of a wall for its opening.
While it seems plausible, I decided against pursuing it initially due to its complexity.</p>

<p>Later: Unfortunately, the described technique fails to yield the intended results across certain models, resulting in a null output from the <code>ReferenceIntersector</code>.</p>

<p>As a workaround for those specific models, an alternative approach was employed:</p>

<ul>
<li>Extract the start and end points of each line from the door geometry instance</li>
<li>Construct an imaginary line corresponding to the door location (the bounding box includes door swing, so not proper for my case)</li>
<li>Examine the intersection of this imaginary line with the curves of the path of travel lines</li>
</ul>

<p><strong>Answer:</strong>
Glad to hear that you found an approach that works reliably for all door instances.</p>

<p>I cannot say for sure why the reference intersector fails in some cases.
One thing to consider, though, is that a content creator has complete freedom in the family definition.
So, some content creators might choose to represent doors in a completely unconventional manner.
They might define the door geometry so that no solids or faces exist for the reference intersector to detect, which might lead to such failures.
The infinite flexibility provided for Revit family definitions can make it hard to ensure that an approach always covers all cases.
This makes unit testing on a large collection of possible BIM variations all the more important.</p>

<p>The approach you describe is very generic: every door opening is defined by one single line from start to end point, and that line must be crossed to pass through the door.
That sounds pretty fool-proof to me.</p>

<!--

% bl 1740 1744 1781 1836 1871 1917 2028


859199650394ddcd8a508ed39b70c68d



-->

<h4><a name="4"></a> Camera Mapping Between APS and Revit</h4>

<p>In 2019, Eason Kang shared a very helpful explanation on how
to <a href="https://aps.autodesk.com/blog/map-forge-viewer-camera-back-revit">map Forge viewer camera back to Revit</a>.</p>

<p>However, some aspects changed, and some were left uncovered back then, as discussed in
the new <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a>
on <a href="https://forums.autodesk.com/t5/revit-api-forum/revit-3d-view-camera-settings/m-p/12629132">Revit 3D view camera settings</a>.</p>

<p>So, Eason took another deep dive into the topic, researched, tested, organized all the material and published it in two blog posts:</p>

<ul>
<li><a href="https://aps.autodesk.com/blog/camera-mapping-between-aps-viewer-and-revit-part-i-restore-viewer-camera-revit">From APS viewer to Revit</a></li>
<li><a href="https://aps.autodesk.com/blog/camera-mapping-between-aps-viewer-and-revit-part-ii-restore-revit-camera-viewer">From Revit to APS viewer</a></li>
</ul>

<p>The associated sample code lives in the</p>

<ul>
<li><a href="https://github.com/yiskang/aps-viewer-revit-camera-sync">aps-viewer-revit-camera-sync GitHub repo</a></li>
</ul>

<!--
If you’re interested, here are the old discussions with another customer about doing something similar.
Cf. https://forge.zendesk.com/agent/tickets/14155
-->

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3aa225c200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3aa225c200c image-full img-responsive" alt="APS perspective view camera" title="APS perspective view camera" src="/assets/image_f87ee2.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Ever so many thanks to Eason for his very careful research and documentation.</p>

<h4><a name="5"></a> Claude 3 can See</h4>

<p>Claude 3 LLM AI model was released, now vision-enabled and with scores in several intelligence tests:</p>

<ul>
<li><a href="https://www.anthropic.com/news/claude-3-family">Claude 3 announcement</a></li>
<li><a href="https://claude.ai/">Claude 3 entry point</a></li>
</ul>

<h4><a name="6"></a> Devin, an AI Software Engineer</h4>

<p>Another announcement
introduces <a href="https://www.cognition-labs.com/blog">Devin, the first AI software engineer</a>,
a fully autonomous AI software engineer.</p>

<h4><a name="7"></a> Meta Imagine Generates Images</h4>

<p><a href="https://imagine.meta.com/">Meta Imagine</a> generates images,
cf., <a href="https://uk.pcmag.com/ai/150034/meta-launches-web-based-ai-image-generator-ai-updates-across-its-apps">Meta launches web-based AI image generator trained on your Instagram pics</a>.</p>

<p>I briefly tested it myself, trying to approximate an image of the real-world scene in front of me, and was unable to tweak the prompt to generate a satisfactory result.
My impression was that it very quickly ignored important aspects of my prompt, e.g., specific colour requests, etc., even when I repeated them, so I quickly gave up, unsatisfied.</p>

<h4><a name="8"></a> An LLM for Decompiling Binary Code</h4>

<p>Yet another use of LLM,
<a href="https://arxiv.org/abs/2403.05286">LLM4Decompile: decompiling binary code with large language models</a>,
with its <a href="https://github.com/albertan017/LLM4Decompile">LLM4Decompile GitHub repo</a>.</p>

<h4><a name="9"></a> Simple Climate Change Overview</h4>

<p>On another ever-present and looming topic of our days,
BBC shares a nice and simple comprehensive article
explaining <a href="https://www.bbc.com/news/science-environment-24021772">What is climate change? A really simple guide</a>.</p>
