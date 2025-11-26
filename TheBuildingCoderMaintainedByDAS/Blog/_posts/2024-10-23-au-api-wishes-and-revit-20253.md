---
layout: "post"
title: "AU API Wishes and Revit 2025.3"
date: "2024-10-23 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2025"
  - "AI"
  - "AU"
  - "SDK Samples"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2024/10/au-api-wishes-and-revit-20253.html "
typepad_basename: "au-api-wishes-and-revit-20253"
typepad_status: "Publish"
---

<p>AU is over and Revit 2025.3 has been released:</p>

<ul>
<li><a href="#2">Revit 2025.3</a></li>
<li><a href="#3">Revit 2025.3 SDK</a></li>
<li><a href="#4">Boost Your BIM AU API wishes</a></li>
<li><a href="#5">Claude computer use does stuff</a></li>
</ul>

<h4><a name="2"></a> Revit 2025.3</h4>

<p>Revit 2025.3 has been released.
An overview of the new features is provided by The Factory
in <a href="https://www.autodesk.com/blogs/aec/2024/10/15/autodesk-revit-2025-3/">What’s new and improved in Revit 2025.3</a>.
Highlights at a glance:</p>

<ul>
<li>Productivity enhancements for documentation production</li>
<li>Performance and user experience improvements, including your UI readability feedback</li>
<li>Export ceiling grids to IFC</li>
<li>Modernized UI for load cases and combinations</li>
<li>Rebar group sets when exporting to IFC</li>
<li>Duplicate layers for system families</li>
<li>Add-in manager</li>
<li>Background PDF export initialization dialog</li>
<li>In-context spell check</li>
<li>New updates for Autodesk Insight, including direct data exchange for more automation in AIA2030 reporting</li>
<li>Recentre room reference lines and room tags</li>
</ul>

<p>Of special interest to users and developers alike, the updated Add-In Manager enables you to identify all installed add-ins for each version of Revit, simply enable or disable individual add-ins before opening Revit and provides more visibility on critical insights like the load time of each add-in so that you can make easy and informed decisions about which add-ins may be reducing the time for you to get to work in Revit:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302e860eef917200d-pi"><img class="asset  asset-image at-xid-6a00e553e16897883302e860eef917200d image-full img-responsive" alt="Add-in manager" title="Add-in manager" src="/assets/image_06c8a0.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="3"></a> Revit 2025.3 SDK</h4>

<!-- REVIT_2025_3_SDK.msi -->

<p>The Revit SDK has been updated for Revit 2025.3, and the new version is available from
the <a href="https://aps.autodesk.com/developer/overview/revit">Revit developer page</a>.</p>

<p>I compared the Revit 2025 SDK with the new version and created <a href="doc/2025_3_file_diff.txt">a list of differing files</a>.
Note the following new files:</p>

<ul>
<li>./Samples/AnalysisVisualizationFramework/DistanceToSurfaces/CS/
<ul>
<li>Command.cs</li>
<li>DistanceToSurfaces.addin</li>
<li>DistanceToSurfaces.csproj</li>
<li>ReadMe_DistanceToSurfaces.rtf</li>
</ul></li>
<li>./Samples/ExternalResourceServer/ExternalResourceDBServer/CS/
<ul>
<li>ServerInterfaceExtensionsForRevitLinks.cs</li>
</ul></li>
<li>./Samples/Massing/DividedSurfaceByIntersects/CS/
<ul>
<li>Command.cs</li>
<li>DividedSurfaceByIntersects.addin</li>
<li>DividedSurfaceByIntersects.csproj</li>
<li>Properties/AssemblyInfo.cs</li>
<li>ReadMe_DividedSurfaceByIntersects.rtf</li>
</ul></li>
<li>./Structural Analysis SDK/Examples/Concrete/CodeCheckingConcreteExample/
<ul>
<li>Concrete/InternalForcesSurface.cs</li>
<li>Main/Calculation/SurfaceSection.cs</li>
<li>Main/ResultSurfaceElement.cs</li>
<li>Server/ServerResultsSurface.cs</li>
<li>Utility/ResultInPointSurface.cs</li>
</ul></li>
</ul>

<p>So, we seem to have gained a new external command or two and some other enhancements.</p>

<h4><a name="4"></a> Boost Your BIM AU API Wishes</h4>

<p>Once again,
Harry Mattison of <a href="https://x.com/BoostYourBIM">Boost Your BIM</a> fulfilled
several AU API wishes that you might want to check out:</p>

<ol>
<li><a href="https://boostyourbim.wordpress.com/2024/10/16/au2024-revit-api-wish-1/">Turn off annotations for all links in a view</a></li>
<li><a href="https://boostyourbim.wordpress.com/2024/10/16/au2024-wish-2/">Select objects by picking subcategory</a></li>
</ol>

<p>Many thanks to Harry for sharing these and once again proving that a lot can be achieved with the Revit API.</p>

<h4><a name="5"></a> Claude Computer Use Does Stuff</h4>

<p>Anthropic announced
<a href="https://www.anthropic.com/news/3-5-models-and-computer-use">computer use, a new Claude 3.5 Sonnet, and Claude 3.5 Haiku</a>.</p>

<p>To understand what this can mean, watch the two-minute video
on <a href="https://youtu.be/ODaHJzOyVCQ">Claude computer use for automating operations</a>:</p>

<p><center>
<iframe width="480" height="270" src="https://www.youtube.com/embed/ODaHJzOyVCQ?si=xCV1TQmyech5AxRp" title="Claude computer use for automating operations" frameborder="0" allow="accelerometer; autoplay; clipboard-write; encrypted-media; gyroscope; picture-in-picture; web-share" referrerpolicy="strict-origin-when-cross-origin" allowfullscreen></iframe>
</center></p>

<p>It is already generating quite a stir in social media and
the <a href="https://duckduckgo.com/?q=claude+computer+use">Internet in general</a>.</p>

<p>For more details, check out the Claude documentation
on <a href="https://docs.anthropic.com/en/docs/build-with-claude/computer-use">computer use (beta)</a>.</p>
