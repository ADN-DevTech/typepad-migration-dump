---
layout: "post"
title: "Length Query and RevitLookup Heralds DLL Paradise"
date: "2024-06-27 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Deployment"
  - "Geometry"
  - "Installation"
  - "Precision"
  - "RevitLookup"
original_url: "https://thebuildingcoder.typepad.com/blog/2024/06/length-query-and-revitlookup-heralds-dll-paradise.html "
typepad_basename: "length-query-and-revitlookup-heralds-dll-paradise"
typepad_status: "Publish"
---

<p>Exciting new RevitLookup release solves Revit add-in DLL hell, and a clarification on the arc length properties provided by curve elements:</p>

<ul>
<li><a href="#2">Curve Length versus ApproximateLength</a></li>
<li><a href="#3">RevitLookup dependency isolation ends DLL hell</a></li>
<li><a href="#4">Add-in dependencies isolation</a></li>
</ul>

<h4><a name="2"></a> Curve Length versus ApproximateLength</h4>

<p>Last week, the Revit development team helped to provide a useful clarification
of <a href="https://forums.autodesk.com/t5/revit-api-forum/the-difference-between-length-and-approximatelength-in-curve/m-p/12841543/">the difference between <code>Length</code> and <code>ApproximateLength</code> in <code>Curve</code> class</a>:</p>

<p><strong>Question:</strong>
The Curve class provides two properties to get the length of a curve, <code>Length</code> and <code>ApproximateLength</code>.
What is the difference?</p>

<p><strong>Answer:</strong>
For many types of curves, the results will probably be identical.
Differences probably only occur for complex curves.
For example, there is no closed-form expression for the arc length of an ellipse.
One efficient and fruitful way to address this kind of question is to implement a little test suite with benchmarking and try it out for yourself.</p>

<p>The development team clarify:</p>

<p>Seems pretty well documented in the remarks for the <code>ApproximateLength</code> property to me.
ApproximateLength is completely accurate for uniform curves (lines and arcs) but could be off by as much as 2x for non-uniform curves, and so it may be worth checking the curve’s class and deciding which method to call from there, or just using the <code>Length</code> property in all cases, if accuracy is a concern.
I recommend the latter, as the time savings hasn’t shown to be significant in my work.</p>

<p><code>ApproximateLength</code> uses a rough approximation that depends on the curve type.
There's no guarantee that the approximation method will be unchanged in future releases.</p>

<p><code>Length</code> performs a line integral to compute the curve length, which can be considered exact for all practical purposes.
Lines and arcs have closed-form expressions for their lengths that are used instead.
I agree that a user is unlikely to see a noticeable performance difference between the two methods.
The performance differences are mostly relevant for internal usage, e.g., in graphics functionality.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3b42439200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3b42439200c img-responsive" style="width: 300px; display: block; margin-left: auto; margin-right: auto;" alt="Arc length" title="Arc length"  src="/assets/image_424a07.jpg" /></a><br /></p>

<p></center></p>

<h4><a name="3"></a> RevitLookup Dependency Isolation Ends DLL Hell</h4>

<p>The new <a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2025.0.8">RevitLookup release 2025.0.8</a> runs
in an isolated container for addin dependencies.</p>

<p>This new capability prevents conflicts and compatibility issues arising from different library versions between plugins, ensuring a more stable and reliable environment for plugin execution.</p>

<p>This enhancement uses the <code>Nice3point.Revit.Toolkit</code> to manage the isolation process, effectively eliminating DLL conflicts.
By integrating this package, RevitLookup ensures a consistent and predictable user experience.</p>

<p>The detailed description how it works is provided in the release notes
for <a href="https://github.com/Nice3point/RevitToolkit/releases/tag/2025.0.1">RevitToolkit release 2025.0.1</a>,
also reproduced <a href="#4">below</a>.</p>

<!--
Please note that this is unrelated
to [the recent RevitLookup hotfix 2025.0.1](https://thebuildingcoder.typepad.com/blog/2024/04/revitlookup-hotfix-and-the-revit-2025-sdk.html#2).
-->

<p>The dependency isolation is available starting with Revit 2025.
Note that the isolation mechanism is implemented by an additional library that must be loaded into Revit at first startup for it to work.
Therefore, if your other plugins use <code>Nice3point.Revit.Toolkit</code>, it must be updated to version <code>2025.0.1</code>, which introduces this feature.</p>

<p>RevitLookup 2025.0.8 addresses the following issues:</p>

<ul>
<li>Dependency conflicts <a href="https://github.com/jeremytammik/RevitLookup/issues/210">latest release won't run #210</a> and
<a href="https://github.com/jeremytammik/RevitLookup/issues/252">I get an error #252</a></li>
<li><a href="https://github.com/jeremytammik/RevitLookup/issues/254">Request for adding <code>WorksharingTooltipInfo</code> properties #254</a></li>
<li>A discussion of the <code>AssemblyLoadContext</code> used to implement the dependency isolation,
<a href="https://github.com/jeremytammik/RevitLookup/issues/246">Build Automation Version is breaking Revit 2025 #246</a></li>
</ul>

<p>As further improvements, the following type extensions are added
for the <a href="https://github.com/jeremytammik/RevitLookup/pull/255"><code>Part</code> class, associated classes #255</a>
and <a href="https://github.com/jeremytammik/RevitLookup/pull/257"><code>WorksharingUtils</code> #257</a>:</p>

<p>Part:</p>

<ul>
<li>IsMergedPart: Is the Part the result of a merge.</li>
<li>IsPartDerivedFromLink: Is the Part derived from link geometry</li>
<li>GetChainLengthToOriginal: Calculates the length of the longest chain of divisions/ merges to reach to an original non-Part element that is the source of the tested part</li>
<li>GetMergedParts: Retrieves the element ids of the source elements of a merged part</li>
<li>ArePartsValidForDivide: Identifies if provided members are valid for dividing parts</li>
<li>FindMergeableClusters: Segregates a set of elements into subsets which are valid for merge</li>
<li>ArePartsValidForMerge: Identifies whether Part elements may be merged</li>
<li>GetAssociatedPartMaker: Gets associated PartMaker for an element</li>
<li>GetSplittingCurves: Identifies the curves that were used to create the part</li>
<li>GetSplittingElements: Identifies the elements ( reference planes, levels, grids ) that were used to create the part</li>
<li>HasAssociatedParts: Checks if an element has associated parts</li>
</ul>

<p>PartMaker:
- GetPartMakerMethodToDivideVolumeFW: Obtains the object allowing access to the divided volume properties of the PartMaker</p>

<p>Element:
- GetCheckoutStatus: Gets the ownership status of an element
- GetWorksharingTooltipInfo: Gets worksharing information about an element to display in an in-canvas tooltip
- GetModelUpdatesStatus: Gets the status of a single element in the central model
- AreElementsValidForCreateParts: Identifies if the given elements can be used to create parts</p>

<h4><a name="4"></a> Add-In Dependencies Isolation</h4>

<p>The RevitLookup isolated plugin dependency container is built using .NET <code>AssemblyLoadContext</code>.</p>

<p>This feature enables plugins to run in a separate, isolated context, ensuring independent execution and preventing conflicts from incompatible library versions.</p>

<p>This enhancement is available for Revit 2025 and higher, addressing the limitations of Revit's traditional plugin loading mechanism, which loads plugins by path without native support for isolation.</p>

<p>How it works:</p>

<p>The core functionality centres on <code>AssemblyLoadContext</code>, which creates an isolated container for each plugin.</p>

<p>When a plugin is loaded, it is assigned a unique <code>AssemblyLoadContext</code> instance, encapsulating the plugin and its dependencies to prevent interference with other plugins or the main application.</p>

<p>To use this isolation feature, developers must inherit their classes from:</p>

<ul>
<li>ExternalCommand</li>
<li>ExternalApplication</li>
<li>ExternalDbApplication</li>
<li>ExternalCommandAvailability</li>
</ul>

<p>These classes contain the built-in isolation mechanism under the hood.
Plugins using interfaces such as <code>IExternalCommand</code> will not benefit from this isolation and will run in the default context.</p>

<p>Limitations:</p>

<ul>
<li>The isolated plugin context feature is available starting with Revit 2025.</li>
<li>For older Revit versions, this library uses a ResolveHelper to help load dependencies from the plugin's folder, but does not protect against conflicts arising from incompatible packages.</li>
<li>Additionally, plugins that do not inherit from the specified classes will not be isolated and may experience compatibility issues if they rely on the default context.</li>
</ul>

<p>For further details, please refer to the discussion between ricaun and Nice3point
on <a href="https://github.com/jeremytammik/RevitLookup/issues/246">build automation version breaking Revit 2025 #246</a></p>

<p>They recommend that Autodesk and Revit adopt similar functionality and include it in the basic Revit API add-in handling architecture, so that all add-in dependencies are automatically isolated and DLL hell conflicts never occur.</p>

<p>ricaun recorded a nine-minute video
on <a href="https://youtu.be/cpy4J_6-8WY">RevitLookup - End of DLL hell - Revit API</a> explaining
and demonstrating exactly how RevitLookup for Revit 2025 can herald the end of DLL hell:</p>

<p><center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/cpy4J_6-8WY?si=05tXP34MEFOGqFGB" title="YouTube video player" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
</center></p>

<p>Many thanks to both of you for your thorough implementation, testing, discussion and documentation!</p>
