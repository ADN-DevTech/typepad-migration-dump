---
layout: "post"
title: "Swallowing StairsAutomation Warnings"
date: "2018-09-25 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Accelerator"
  - "Automation"
  - "DA4R"
  - "Failure"
  - "Forge"
  - "SDK Samples"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/09/swallowing-stairsautomation-warnings.html "
typepad_basename: "swallowing-stairsautomation-warnings"
typepad_status: "Publish"
---

<p>Here at the Forge Accelerator in Rome, I am starting to take some a first look at
the <a href="https://autodesk-forge.github.io">Forge</a>
<a href="https://forge.autodesk.com/en/docs/design-automation/v2/overview">Design Automation API</a> for Revit.</p>

<p>It is not yet available or documented, except to a closely restricted private beta that I am not a member of, so I cannot go into any details.
For more information on its current status, please refer to
<a href="https://fieldofviewblog.wordpress.com/revit">Mikako Harada's discussion of Design Automation for Revit</a>.</p>

<p>However, you can prepare for the day when it comes by handling your add-in warnings properly.</p>

<p>To make use of it, you obviously need to know the Revit API, and it becomes very easy indeed if you also have some experience with Forge apps.</p>

<p>Revit API code can be run in a Forge app by using the <code>IExternalDBApplication</code> interface, already listed in
the <a href="https://apidocs.co/apps/revit/2019/97318be3-45c4-d93b-ee7b-174fa80ab951.htm">Revit API documentation</a>.</p>

<p>This interface supports addition of DB-level external applications to Revit, to subscribe to DB-level events and updaters.</p>

<p>DB-level applications cannot create or modify UI.</p>

<p>Therefore, if your add-in pops up any warnings, it cannot be converted to a Forge Design Automation for Revit app &ndash; or, worse still, it will simply silently terminate as soon as it misbehaves.</p>

<p>Therefore, today, let's take a look at suppressing warnings caused by a typical Revit add-in.</p>

<p>As an example, we'll choose the StairsAutomation Revit SDK sample.</p>

<p>It generates five different types of stairs:</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833022ad393f6b4200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833022ad393f6b4200d image-full img-responsive" alt="StairsAutomation result" title="StairsAutomation result" src="/assets/image_64fddc.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Two of them generate Revit warning messages:</p>

<ul>
<li>Stair #3 generates
<a href="http://thebuildingcoder.typepad.com/files/stairsautomation_warnings_stair_3_8.html">8 warnings about overlapping handrail model line elements</a>.</li>
<li>Stair #4 generates <a href="http://thebuildingcoder.typepad.com/files/stairsautomation_warnings_stair_4_1.html">1 warning about a missing riser</a>.</li>
</ul>

<p>Happily, Revit warnings can easily be handled automatically making use of
the <a href="http://thebuildingcoder.typepad.com/blog/about-the-author.html#5.32">Failure API</a>.</p>

<p>Specifically, we presented
a <a href="http://thebuildingcoder.typepad.com/blog/2016/09/warning-swallower-and-roomedit3d-viewer-extension.html#2">generic warning swallower</a> that
can handle just about any warning message that crops up.</p>

<p>For the StairsAutomation sample, nothing much is required.</p>

<p>The code generating the stairs obviously runs inside a <code>Transaction</code>, and that, in turn, is enclosed in a <code>StairsEditScope</code>.</p>

<p>The call to <code>Commit</code> the stair editing scope is called with a custom failures preprocessor instance:</p>

<pre class="code">
&nbsp;&nbsp;editScope.Commit(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">StairsEditScopeFailuresPreprocessor</span>()&nbsp;);
</pre>

<p>In the original sample, the failures preprocessor does next to nothing:</p>

<pre class="code">
<span style="color:blue;">class</span>&nbsp;<span style="color:#2b91af;">StairsEditScopeFailuresPreprocessor</span>&nbsp;
&nbsp;&nbsp;:&nbsp;<span style="color:#2b91af;">IFailuresPreprocessor</span>
{
&nbsp;&nbsp;<span style="color:blue;">public</span>&nbsp;<span style="color:#2b91af;">FailureProcessingResult</span>&nbsp;PreprocessFailures(&nbsp;
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FailuresAccessor</span>&nbsp;a&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">return</span>&nbsp;<span style="color:#2b91af;">FailureProcessingResult</span>.Continue;
&nbsp;&nbsp;}
}
</pre>

<p>I simply added the following lines of code to it, to delete all warnings before returning:</p>

<pre class="code">
&nbsp;&nbsp;<span style="color:#2b91af;">IList</span>&lt;<span style="color:#2b91af;">FailureMessageAccessor</span>&gt;&nbsp;failures
&nbsp;&nbsp;=&nbsp;a.GetFailureMessages();

&nbsp;&nbsp;<span style="color:blue;">foreach</span>(&nbsp;<span style="color:#2b91af;">FailureMessageAccessor</span>&nbsp;f&nbsp;<span style="color:blue;">in</span>&nbsp;failures&nbsp;)
&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#2b91af;">FailureSeverity</span>&nbsp;fseverity&nbsp;=&nbsp;a.GetSeverity();

&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:blue;">if</span>(&nbsp;fseverity&nbsp;==&nbsp;<span style="color:#2b91af;">FailureSeverity</span>.Warning&nbsp;)
&nbsp;&nbsp;&nbsp;&nbsp;{
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;a.DeleteWarning(&nbsp;f&nbsp;);
&nbsp;&nbsp;&nbsp;&nbsp;}
&nbsp;&nbsp;}
</pre>

<p>Now, all five stair variations are created without any warning messages being displayed.</p>

<p>Of course, in your own more complex add-ins, you may need to handle other failures beside simple warnings that can be ignored.</p>

<p>For the most general case, you can make use of
the <a href="http://thebuildingcoder.typepad.com/blog/2016/09/warning-swallower-and-roomedit3d-viewer-extension.html#2">generic warning swallower</a> mentioned
above.</p>

<p>To document the steps I took to achieve this and track all the changes I made, I extracted the sample to an
own <a href="https://github.com/jeremytammik/StairsAutomation">StairsAutomation GitHub repository</a>.</p>

<p>It ended up being so simple that I need actually not have bothered, though...</p>

<p>Looking forward to making further explorations and digging deeper into this area anon.</p>
