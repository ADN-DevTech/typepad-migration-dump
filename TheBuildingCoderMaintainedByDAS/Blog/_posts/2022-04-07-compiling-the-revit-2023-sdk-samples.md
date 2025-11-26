---
layout: "post"
title: "Compiling the Revit 2023 SDK Samples"
date: "2022-04-07 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2023"
  - "Installation"
  - "Migration"
  - "SDK Samples"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2022/04/compiling-the-revit-2023-sdk-samples.html "
typepad_basename: "compiling-the-revit-2023-sdk-samples"
typepad_status: "Publish"
---

<p>The first thing I do after installing the new release of Revit is compile the Revit SDK and set up RvtSamples to load the external commands.
So, here we go again:</p>

<ul>
<li><a href="#2">Update the RevitSdkSamples repo</a></li>
<li><a href="#3">Set the Revit API references</a></li>
<li><a href="#4">Eliminate processor architecture mismatch warning</a></li>
<li><a href="#5">Missing XML comments</a></li>
<li><a href="#6">Targets and rule sets</a></li>
<li><a href="#7">Set up RvtSamples</a>
<ul>
<li><a href="#7.2">DatumsModification commands</a></li>
<li><a href="#7.3">ContextualAnalyticalModel commands</a></li>
<li><a href="#7.4">CivilAlignments commands</a></li>
</ul></li>
<li><a href="#8">Conclusion</a></li>
</ul>

<h4><a name="2"></a> Update the RevitSdkSamples Repo</h4>

<p>I installed the new SDK and performed a pretty exhaustive comparison with the previous version to ensure that I do not overwrite anything important in
the <a href="https://github.com/jeremytammik/RevitSdkSamples">RevitSdkSamples repository</a>.</p>

<p>The advantage of maintaining a public repository for this is that changes can easily be tracked and I can share the steps that I take to compile the SDK samples and set up <code>RvtSamples</code> to load them.</p>

<p>I enjoyed the support and conversation with <a href="https://github.com/jmcouffin">jmcouffin</a> during this process, in
his <a href="https://github.com/jeremytammik/RevitSdkSamples/pull/1">pull request #1 2023SDK samples</a>.
That was the first time anyone offered to help with this repository, by the way, so thank you very much indeed for that!</p>

<p>I compiled <a href="https://thebuildingcoder.typepad.com/files/diff_2022_2023.txt">diff_2022_2023.txt</a> listing
the differences between the Revit 2023 SDK samples and previous versions, both in the directory structure and individual files.</p>

<p>Afaict from that analysis, three new samples were added:</p>

<ul>
<li>SheetToView3D</li>
<li>SelectionChanged</li>
<li>ContextualAnalyticalModel</li>
</ul>

<p>We need to take a look at those asap.
First things first, though: compile the SDK and set up <code>RvtSamples</code>.</p>

<h4><a name="3"></a> Set the Revit API References</h4>

<p>The first attempt at compiling <code>SDKSamples.sln</code> right out of the box produced 9544 error and 1168 warnings;
the namespace <code>Autodesk</code> could not be found.</p>

<p>SDKSamples includes 199 solution projects, and I am not going to edit their references one by one, so I need to implement a batch update somehow.</p>

<p>Luckily, there is no need to do so either; searching globally for <code>RevitAPI.dll</code>, I find only 8 occurrences, in the <code>targets</code> files in the <code>VSProps</code> folder and in the <code>ContextualAnalyticalModel.csproj</code>.
Apparently, the latter is an exception.</p>

<p>I ended up fixing the Revit API references by replacing variable expressions by a constant <em>C:\Program Files\Autodesk\Revit 2023</em> in the following files:</p>

<ul>
<li>ContextualAnalyticalModel.csproj</li>
<li>SDKSamples.CivilAlignments.targets</li>
<li>SDKSamples.VB.targets</li>
<li>SDKSamples.targets</li>
<li>SDKSamples.Steel.targets</li>
</ul>

<p>With those modifications, I was able to successfully compile all 199 projects with zero errors. </p>

<h4><a name="4"></a> Eliminate Processor Architecture Mismatch Warning</h4>

<p>95 warnings remain. Most of those concern the everlasting processor architecture mismatch, e.g.:</p>

<ul>
<li>Warning: There was a mismatch between the processor architecture of the project being built "MSIL" and the processor architecture of the reference "RevitAPIUI", "AMD64". This mismatch may cause runtime failures. Please consider changing the targeted processor architecture of your project through the Configuration Manager so as to align the processor architectures between your project and references, or take a dependency on references with a processor architecture that matches the targeted processor architecture of your project.</li>
</ul>

<p>I mentioned this warning when it appeared and have been fixing it in every release ever since:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/06/processor-architecture-mismatch-warning.html">Processor Architecture Mismatch Warning and Key Hook</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/07/recursively-disable-architecture-mismatch-warning.html">Recursively Disable Architecture Mismatch Warning</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2014/09/architecture-mismatch-warning-disabler-update.html">Architecture Mismatch Warning Disabler Update</a></li>
</ul>

<p>I implemented the <a href="https://github.com/jeremytammik/DisableMismatchWarning">DisableMismatchWarning</a> utility to help me do so, and make use of that now as well:</p>

<p>After running that over all samples,
only <a href="https://thebuildingcoder.typepad.com/files/revit_2023_sdk_samples_errors_warnings_1.txt">6 warnings</a> remain.</p>

<h4><a name="5"></a> Missing XML Comments</h4>

<p>Two of the remaining six warnings are easy to fix, so let's do so:</p>

<ul>
<li>Warning CS1591: Missing XML comment for publicly visible type or member 'ExportPDFData.Combine' in ImportExport,
<em>...\ImportExport\CS\Export\ExportPDFData.cs</em> line 41</li>
<li>Warning CS1591: Missing XML comment for publicly visible type or member 'ExportPDFOptionsForm.ExportPDFOptionsForm(ExportPDFData)' in ImportExport,
<em>...\ImportExport\CS\Export\ExportPDFOptionsForm.cs</em>, line 45</li>
</ul>

<p>I simply added placeholder XML documentation to both methods.</p>

<h4><a name="6"></a> Targets and Rule Sets</h4>

<p>The four remaining warnings seem unimportant and can be ignored:</p>

<ul>
<li><em>C:\Program Files (x86)\Microsoft Visual Studio\2019\Enterprise\MSBuild\Current\Bin\Microsoft.Csharp.targets</em> cannot be imported again.
It was already imported at <em>Y:\a\src\rvt\RevitSdkSamples\SDK\Samples\VSProps\SDKSamples.targets (29,3)</em>.
This is most likely a build authoring error.
This subsequent import will be ignored.
SampleCommandsSteelElements   </li>
<li>Warning  Could not find rule set file "Migrated rules for FrameBuilder.ruleset". FrameBuilder   </li>
<li>Warning  Could not find rule set file "GeometryCreation_BooleanOperation.ruleset". GeometryCreation_BooleanOperation   </li>
<li>Warning  Could not find rule set file "ProximityDetection_WallJoinControl.ruleset". ProximityDetection_WallJoinControl   </li>
</ul>

<h4><a name="7"></a> Set Up RvtSamples</h4>

<p>I added my local system path <em>Y:/a/src/rvt/RevitSdkSamples/SDK/Samples/</em> in both <code>RvtSamples.addin</code> and <code>RvtSamples.txt</code>.</p>

<p>Some add-in file paths are not found, so I set the debugging flag in <code>RvtSamples</code> <code>Application.cs</code>:</p>

<pre class="code">
  bool testClassName = true; // jeremy
</pre>

<p>The culprits are the usual suspects, all of them VB.NET samples, but strangely enough not all VB.NET samples,
cf. <a href="https://thebuildingcoder.typepad.com/files/revit_2023_sdk_samples_errors_warnings_2.txt">revit_2023_sdk_samples_errors_warnings_2.txt</a>.</p>

<h4><a name="7.2"></a> DatumsModification Commands</h4>

<p>RvtSamples.txt erroneously listed an external command named <code>Command</code> for the <code>DatumsModification</code> SDK sample.
In fact, it defines no such command; instead, it implements three others:</p>

<ul>
<li>DatumAlignment</li>
<li>DatumPropagation</li>
<li>DatumStyleModification</li>
</ul>

<p>I updated RvtSamples.txt accordingly.</p>

<h4><a name="7.3"></a> ContextualAnalyticalModel Commands</h4>

<p>ContextualAnalyticalModel defines 15 external commands:</p>

<ul>
<li>AddAssociation</li>
<li>AnalyticalNodeConnStatus</li>
<li>CreateAnalyticalPanel</li>
<li>CreateAnalyticalMember</li>
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

<p>RvtSamples.txt erroneously lists three others: AddRelation, UpdateRelation and BreakRelation.</p>

<p>I replaced them by AddAssociation and RemoveAssociation.</p>

<h4><a name="7.4"></a> CivilAlignments Commands</h4>

<p>The CivilAlignments sample defines two commands and tries to create menu entries named "Infrastructure Alignments" or both of them.</p>

<p>This causes Revit to throw an exception.</p>

<p>I fixed that by naming them 'Infrastructure Alignments CreateAlignmentStationLabelsCmd' and 'Infrastructure Alignments ShowPropertiesCmd', respectively.</p>

<h4><a name="8"></a> Conclusion</h4>

<p>I finished off by resetting the <code>testClassName</code> debug switch and creating the final
<a href="https://github.com/jeremytammik/RevitSdkSamples/releases/tag/2023.0.0.3">RevitSdkSamples release 2023.0.0.3</a>.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330282e14e3832200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330282e14e3832200b image-full img-responsive" alt="RvtSamples 2023" title="RvtSamples 2023" src="/assets/image_d5a11d.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>
