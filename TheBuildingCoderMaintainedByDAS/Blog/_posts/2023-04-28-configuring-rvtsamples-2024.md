---
layout: "post"
title: "Configuring RvtSamples 2024 and Big Numbers"
date: "2023-04-28 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2024"
  - "Element Relationships"
  - "Migration"
  - "SDK Samples"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2023/04/configuring-rvtsamples-2024.html "
typepad_basename: "configuring-rvtsamples-2024"
typepad_status: "Publish"
---

<p>I left the Nice APS accelerator <a href="https://aps.autodesk.com/accelerator-program">APS cloud accelerator</a> and
am returning to Switzerland, using the long train ride time to continue my Revit 2024 migration process,
now addressing the RvtSamples external application ad-in:</p>

<ul>
<li><a href="#3">Configuring RvtSamples 2024</a>
<ul>
<li><a href="#4">DatumsModification</a></li>
<li><a href="#5">ContextualAnalyticalModel</a></li>
<li><a href="#6">Infrastructure Alignments</a></li>
<li><a href="#7">Toposolid</a></li>
<li><a href="#8">Conclusion</a></li>
</ul></li>
<li><a href="#9">Consuming huge numbers of element ids</a></li>
</ul>

<h4><a name="3"></a> Configuring RvtSamples 2024</h4>

<p>Now that I completed installing Revit 2024,
<a href="https://thebuildingcoder.typepad.com/blog/2023/04/nice-accelerator-and-compiling-the-revit-2024-sdk.html">successfully compiled the Revit 2024 SDK samples</a>
and updated the <a href="https://github.com/jeremytammik/RevitSdkSamples">RevitSdkSamples repository</a>,
the time is ripe to configure the RvtSamples external application to load all 246 Revit 2024 SDK sample external commands.
Yes, 246 of them.
Pretty hard to manage one by one.</p>

<p>Mainly, this consists of editing RvtSamples.txt, the input text file specifying the name and location of the commands and the .NET assembly DLLs implementing them.</p>

<p>Here is an overview of (most of) the history of RvtSamples, including its initial implementation and similar migration efforts in the past:</p>

<ul>
<li><a href="https://thebuildingcoder.typepad.com/blog/2008/09/loading-sdk-sam.html">Loading SDK Samples</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2008/11/loading-the-building-coder-samples.html">Adding <code>&#35;include</code> functionality</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2009/05/porting-the-building-coder-samples.html">RvtSamples Conversion from 2009 to 2010</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2010/04/debugging-with-visual-studio-2010-and-rvtsamples.html">Debugging with Visual Studio 2010 and RvtSamples</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2011/04/migrating-the-building-coder-samples-to-revit-2012.html">Migrating the Building Coder Samples to Revit 2012</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/04/compiling-the-revit-2014-sdk.html">Compiling the Revit 2014 SDK</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/04/compiling-the-revit-2015-sdk-and-migrating-bc-samples.html">Compiling the Revit 2015 SDK and Migrating Bc Samples</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2015/05/migrating-the-building-coder-samples-to-revit-2016.html">Migrating The Building Coder Samples to Revit 2016</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/04/rvtsamples-for-revit-2017.html">RvtSamples for Revit 2017</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/05/the-building-coder-samples-2017.html">The Building Coder Samples 2017</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/05/sdk-update-rvtsamples-and-modifying-grid-end-point.html">RvtSamples for Revit 2018</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2018/04/rvtsamples-2019.html">RvtSamples 2019</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2018/05/installing-the-revit-2019-sdk-april-update.html">RvtSamples 2019 Update</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2019/04/the-revit-2020-fcs-api-and-sdk.html">RvtSamples 2020</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2019/04/close-doc-and-zero-doc-rvtsamples.html">Close Doc and Zero Doc RvtSamples</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2019/09/whats-new-in-the-revit-20201-api.html#4">RvtSamples 2020.1</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2020/05/setting-up-rvtsamples-for-revit-2021.html">Setting up RvtSamples for Revit 2021</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2021/04/revit-2022-sdk-and-the-building-coder-samples.html">Revit 2022 SDK and The Building Coder Samples</a></li>
<li><a href="https://thebuildingcoder.typepad.com/blog/2022/04/compiling-the-revit-2023-sdk-samples.html">Compiling the Revit 2023 SDK Samples</a></li>
</ul>

<!--

C:\Users\jta\AppData\Roaming\Autodesk\Revit\Addins\2024
copy Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\RvtSamples\CS\RvtSamples.txt
copy Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\RvtSamples\CS\RvtSamples.addin

-->

<p>I already described how to handle some of the errors encountered in previous migration cycles listed above.</p>

<p>Here is an overview of the problematic add-ins this time around:</p>

<h4><a name="4"></a> DatumsModification</h4>

<p>Correct list of external commands for the DatumsModification add-in:</p>

<ul>
<li>DatumAlignment</li>
<li>DatumPropagation</li>
<li>DatumStyleModification</li>
</ul>

<h4><a name="5"></a> ContextualAnalyticalModel</h4>

<p>The SDK source code implements the following 21 ContextualAnalyticalModel external commands:</p>

<ul>
<li>Use <code>grep "class.*IExternalCom" *cs</code></li>
<li>AddAssociation</li>
<li>AddCustomAssociation</li>
<li>AnalyticalNodeConnStatus</li>
<li>CreateAnalyticalPanel</li>
<li>CreateAnalyticalCurvedPanel</li>
<li>CreateAnalyticalMember</li>
<li>CreateAreaLoadWithRefPoint</li>
<li>CreateCustomAreaLoad</li>
<li>CreateCustomLineLoad</li>
<li>CreateCustomPointLoad</li>
<li>FlipAnalyticalMember</li>
<li>MemberForcesAnalyticalMember</li>
<li>ModifyPanelContour</li>
<li>MoveAnalyticalMemberUsingElementTransformUtils</li>
<li>MoveAnalyticalMemberUsingSetCurve</li>
<li>MoveAnalyticalNodeUsingElementTransformUtils</li>
<li>MoveAnalyticalPanelUsingElementTransformUtils</li>
<li>MoveAnalyticalPanelUsingSketchEditScope</li>
<li>ReleaseConditionsAnalyticalMember</li>
<li>RemoveAssociation</li>
<li>SetOuterContourForPanels</li>
</ul>

<p>These are the ContextualAnalyticalModel external commands listed in RvtSamples.txt:</p>

<ul>
<li>Use <code>grep "^ContextualAnalyticalModel" RvtSamples.txt | sort</code></li>
<li>AddRelation</li>
<li>AnalyticalNodeConnStatus</li>
<li>BreakRelation</li>
<li>CreateAnalyticalCurvedPanel</li>
<li>CreateAnalyticalMember</li>
<li>CreateAnalyticalPanel</li>
<li>FlipAnalyticalMember</li>
<li>MemberForcesAnalyticalMember</li>
<li>ModifyPanelContour</li>
<li>MoveAnalyticalMemberUsingElementTransformUtils</li>
<li>MoveAnalyticalMemberUsingSetCurve</li>
<li>MoveAnalyticalNodeUsingElementTransformUtils</li>
<li>MoveAnalyticalPanelUsingElementTransformUtils</li>
<li>MoveAnalyticalPanelUsingSketchEditScope</li>
<li>ReleaseConditionsAnalyticalMember</li>
<li>SetOuterContourForPanels</li>
<li>UpdateRelation</li>
</ul>

<h4><a name="6"></a> Infrastructure Alignments</h4>

<ul>
<li>Infrastructure Alignment Station Label</li>
<li>Infrastructure Alignment Properties</li>
</ul>

<h4><a name="7"></a> Toposolid</h4>

<p>The Toposolid sample only has one entry in RvtSamples.txt specifying an external command named:</p>

<ul>
<li>Revit.SDK.Samples.Toposolid.CS.Command</li>
</ul>

<p>This command does not exist.
Instead, the sample implements the following external commands:</p>

<ul>
<li>ToposolidCreation</li>
<li>ToposolidFromDWG</li>
<li>ContourSettingCreation</li>
<li>ContourSettingModification</li>
<li>ToposolidFromSurface</li>
<li>SSEPointVisibility</li>
<li>SplitToposolid</li>
<li>SimplifyToposolid</li>
</ul>

<h4><a name="8"></a> Conclusion</h4>

<p>This time around, I submitted a ticket with the development team in the hope of avoiding having to repeat this entire process for the next SDK update:</p>

<ul>
<li>REVIT-206304 &ndash; Update RvtSamples.txt for Revit 2024 SDK</li>
</ul>

<p>Some of the menus generated by RvtSamples had too many entries to display them all on my screen, so I modified the sorting and added two new groups for <code>Analytical Model</code> and <code>Toposolid</code>.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b7517edfb8200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b7517edfb8200b image-full img-responsive" alt="RvtSamples 2024" title="RvtSamples 2024" src="/assets/image_519dea.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>My current running version of RvtSamples is captured
in <a href="https://github.com/jeremytammik/RevitSdkSamples/releases/tag/2024.0.0.3">RevitSdkSamples release 2024.0.0.3</a>.</p>

<h4><a name="9"></a> Consuming Huge Numbers of Element Ids</h4>

<p>The <a href="http://forums.autodesk.com/t5/revit-api-forum/bd-p/160">Revit API discussion forum</a> thread
on how to <a href="https://forums.autodesk.com/t5/revit-api-forum/draw-line-visible-on-screen/m-p/11922998">draw a line visible on screen</a> spawned
several useful ideas on how to generate and display transient graphics for rubber banding functionality similar to AutoCAD jigs.</p>

<p>It also raised the question of generating (and consuming) huge amounts of element ids, since each transient element in a loop consumes a new element id, even if the transaction is never commited.
Luckily, Revit 2024 <a href="https://thebuildingcoder.typepad.com/blog/2023/04/whats-new-in-the-revit-2024-api.html#4.1.2">upgraded <code>ElementId</code> storage to 64-bit</a>.</p>

<p>Anyway, that question led to the following amusing discussion:</p>

<ul>
<li>An interesting question was raised in the discussion on drawing transient geometry: if I create a new element in a transaction that is rolled back (not committed) and wrap that in a loop, a new element id is consumed in each iteration. That can consume all of the element ids space if I run it for long enough. How should this be handled, please?</li>
<li>This is a non-issue in Revit 2024.+. Element id is now 64 bit.</li>
<li>That does not make it a non-issue. It just means it takes a million or a billion time more time to consume all the possible tokens. Is that a real solution?</li>
<li><code>LLONG_MAX</code> is 9,223,372,036,854,775,807. We won't need to worry about it.
Should the next element id field in document be rolled back on transaction rollback is an independent issue though.
Another independent issue is should the editor rubber banding reuse the same element ids on every iteration?</li>
<li>Well, if I rubber band a line in a loop and create a new element id for every iteration, and assuming an extremely slow loop running 100 iterations per second, I will end up using 100*60*60*24*365 = 3.153.600.000 element ids in one year of rubber banding... that is not a completely insignificant number...</li>
<li>Jeremy, is that three trillion?</li>
<li>It's three thousand million. I don't know the exact definition of a trillion, but it's a big number :-)</li>
<li>Ok. the new Element id max is 9 quintillion. Dividing 9 quintillion by your number gives me 3 million, which would be the number of years required for your example to run through the ElementId space. That's why we've asserted that the 64-bit ids are unlikely to run out any time soon.</li>
<li>To do another example, if we had an operation running 24/7 which generated 1 million new ids per second:
<ul>
<li>1,000,000 * 60 * 60 * 24 * 365 = 3.1536e+13</li>
<li>9,223,372,036,854,775,807 / 3.1536e+13 = 292471 (plus a fraction)</li>
<li>which is just a wild number of years.</li>
</ul></li>
<li>It is worth noting that we tried doing this and estimated that it would take days to even get to the end of the 32-bit space by creating the simple transient elements. It would therefore take on the order 2^31 days to run out of 64-bit ids.
So, to confirm what is said above, it would take an extremely long time to run out of the newly extended elementid space</li>
<li>To pick up on the 3 million years mentioned above: Revit will survive 3 million years, but the rubber bands will surely lose some elasticity.</li>
<li>Can I just say all of you are awesome and this conversation makes me so happy I work here &nbsp; :-)</li>
</ul>

<p>For some related facts, consult the illuminating ten-minute video by Numberphile explaining <a href="https://youtu.be/C-52AI_ojyQ">what is a billion?</a>.</p>
