---
layout: "post"
title: "Lookup Foundation, RevitLookup and DA4R 2026"
date: "2025-04-03 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "2026"
  - "AI"
  - "Cloud"
  - "DA4R"
  - "Data Access"
  - "Macro"
  - "Open Source"
  - "RevitLookup"
  - "Vibe"
original_url: "https://thebuildingcoder.typepad.com/blog/2025/04/lookup-foundation-revitlookup-and-da4r-2026.html "
typepad_basename: "lookup-foundation-revitlookup-and-da4r-2026"
typepad_status: "Publish"
---

<p>In immediately reaction to the release of Revit 2026, DA4R and RevitLookup follow suite:</p>

<ul>
<li><a href="#2">The Lookup Foundation</a></li>
<li><a href="#3">RevitLookup 2026</a>
<ul>
<li><a href="#3.1">General</a></li>
<li><a href="#3.2">Dashboard page</a></li>
<li><a href="#3.3">Summary page</a></li>
<li><a href="#3.4">Settings page</a></li>
<li><a href="#3.5">Unit dialogue</a></li>
<li><a href="#3.6">Modules dialogue</a></li>
<li><a href="#3.7">Application</a></li>
<li><a href="#3.7">LookupEngine</a></li>
<li><a href="#3.9">Improvements</a></li>
<li><a href="#3.10">Development:</a></li>
</ul></li>
<li><a href="#4">Design Automation API for Revit 2026</a></li>
<li><a href="#5">Vibe programming Revit macros</a></li>
<li><a href="#6">Direwolf fast Revit data extraction</a></li>
<li><a href="#7">From floppies to a memory stick</a></li>
</ul>

<h4><a name="2"></a> The Lookup Foundation</h4>

<p>Roman <a href="https://t.me/nice3point">@Nice3point</a> Karpovich, aka Роман Карпович,
has been heroically maintaining and enhancing RevitLookup for several years now.</p>

<p>He would like to expand and generalise it to implement a whole suite of tools for exploring the .NET objects.</p>

<p>For that purpose, we transferred ownership of the RevitLookup repository from my personal account to
the new <a href="https://github.com/lookup-foundation">Lookup Foundation</a>:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302e860e6ba1c200b-pi"><img class="asset  asset-image at-xid-6a00e553e16897883302e860e6ba1c200b image-full img-responsive" alt="Lookup Foundation" title="Lookup Foundation"  src="/assets/image_5774c4.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>GitHub redirects from the old URL to the new one, so existing links on the web will continue working:</p>

<blockquote>
  <p>All links to the previous repository location are automatically redirected to the new location. When you use git clone, git fetch, or git push on a transferred repository, these commands will redirect to the new repository location or URL. However, to avoid confusion, we strongly recommend updating any existing local clones to point to the new repository URL</p>
</blockquote>

<p>RevitLookup has been actively developed for many years, shaped by the contributions of a dedicated open-source community. Today, we mark an important milestone: the project transitions from its original home under Jeremy Tammik’s leadership to a new organization — the Lookup Foundation — where its development will be guided by the community that relies on it.</p>

<p>This transition ensures RevitLookup's continued growth as a community-driven project . RevitLookup is no longer just one person’s project; it belongs to the community that builds, tests, and improves it every day.</p>

<p>The Lookup Foundation will ensure the project remains open, sustainable, and driven by its users. Whether you're a developer, a Revit expert, or simply someone who benefits from the tool, you have a role to play in its evolution.</p>

<p>Please also refer to <a href="https://www.linkedin.com/posts/nice3point_github-lookup-foundationrevitlookup-interactive-activity-7313236257206079488-JxpU?utm_source=share&amp;utm_medium=member_desktop&amp;rcm=ACoAAACogiYBX6lBbCXzDJBU_OWT9MYGMYk5X6g">Roman's LinkedIn announcement</a>.</p>

<p>Once again, many thanks to Roman for all his hard work setting this up, and for the <a href="#3">numerous enhancements and migration to Revit 2026 discussed below</a>.</p>

<h4><a name="3"></a> RevitLookup 2026</h4>

<p>The Lookup Foundation released <a href="https://github.com/lookup-foundation/RevitLookup/releases/tag/2026.0.0">RevitLookup 2026</a>.</p>

<p>A new major RevitLookup update with official Revit 2026 support and a lot of enhancements 🎉</p>

<p>This release focuses on performance, a renewed UI, expanded functionality, redesigned application architecture, clearer separation of core components and user flow improvements. Let's move to the details.</p>

<h5><a name="3.1"></a> General</h5>

<ul>
<li><strong>Revit 2026</strong> support.</li>
<li><strong>Added shortcuts</strong>. <code>SS</code> shortcut opens the Snoop Selection window.</li>
<li><strong>Unlocked Ribbon buttons</strong> for document-independent actions. Some dialogs can be opened without opening a document.</li>
<li><strong>Improved context actions</strong>. Now <code>Delete</code> command shows the number of deleted Elements from the document.</li>
<li><strong>Improved exception handling</strong>. Now shows more informational messages, added more supported error types. Fixed some application crashes.</li>
<li><strong>Fixed dependency conflicts</strong> for .NET Framework versions of RevitLookup, according to tests, you should no longer encounter startup issues.</li>
<li><strong>Added AppBundle</strong>, that can be installed with all available versions in the release.</li>
<li><strong>Added user manuals</strong>, visit the <a href="https://github.com/lookup-foundation/RevitLookup/wiki">Wiki</a> for RevitLookup features.</li>
<li><strong>Added digital signature</strong>. Thanks <a href="https://kinship.io/">Kinship</a> for providing cloud services!</li>
</ul>

<h5><a name="3.2"></a> Dashboard Page</h5>

<p>The Dashboard has been significantly redesigned to make it easier to understand for users unfamiliar with the Revit API.</p>

<ul>
<li><strong>Added description</strong> of all actions, navigation became more intuitive and clearer.</li>
<li><strong>Added UiControlledApplication</strong> action to analyze properties and methods available when running the add-in.</li>
<li><strong>Actions now grouped</strong> according to the window size.</li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302e860fdb755200d-pi"><img class="asset  asset-image at-xid-6a00e553e16897883302e860fdb755200d image-full img-responsive" alt="RevitLookup 2026" title="RevitLookup 2026" src="/assets/image_3851b4.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h5><a name="3.3"></a> Summary Page</h5>

<p>Updated and improved Summary page components.</p>

<ul>
<li><strong>Improved responsiveness</strong> of the whole page. RevitLookup now supports displaying large amounts of data, 100k rows will not cause freezes and glitches.</li>
<li><strong>Improved shortcuts</strong>, now the <strong>F5</strong> key works in all cases, previously it was required to focus on an element.</li>
<li><strong>Improved filter menu</strong>. Now applying <strong>Show/hide</strong> filters does not close the menu.</li>
<li><strong>Improved tooltips</strong>. Tooltips now display more useful information, number of elements contained in the type, full type name.</li>
<li><strong>Improved search</strong>. It is now more intuitive and faster. Priority is given to the members table, the table is filtered first, then if nothing is found, the list of items is filtered. When searching the table of members, the elements with the same type will be filtered as well.</li>
<li><strong>Show Extension</strong> filter is now enabled by default.</li>
<li><strong>Patched WPF</strong> issues causing non-obvious member's table behavior.</li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3d00a1a200c-pi"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3d00a1a200c image-full img-responsive" alt="RevitLookup 2026" title="RevitLookup 2026"  src="/assets/image_c5706f.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h5><a name="3.4"></a> Settings Page</h5>

<ul>
<li><strong>Added description</strong> of all settings, navigation became more intuitive and clearer.</li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302e860e6ba44200b-pi"><img class="asset  asset-image at-xid-6a00e553e16897883302e860e6ba44200b img-responsive" alt="RevitLookup 2026" title="RevitLookup 2026"  src="/assets/image_296105.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h5><a name="3.5"></a> Unit Dialogue</h5>

<ul>
<li><strong>Improved context menu</strong>, Copy commands are grouped into sub-menu.</li>
</ul>

<h5><a name="3.6"></a> Modules Dialogue</h5>

<ul>
<li><strong>Added new context menu</strong> action. Added navigation to the module directory or directly to the module file.</li>
<li><strong>Added column sorting</strong>. Click on the column header to sort.</li>
<li><strong>Improved context menu</strong>, Copy commands are grouped into sub-menu.</li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302e860fdb763200d-pi"><img class="asset  asset-image at-xid-6a00e553e16897883302e860fdb763200d img-responsive" alt="RevitLookup 2026" title="RevitLookup 2026"  src="/assets/image_d9e3a7.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h5><a name="3.7"></a> Application</h5>

<ul>
<li><strong>Adjusted default window sizes</strong> for better display due to changes in Win.UI components.</li>
<li><strong>Synchronised UI library</strong> and styles with Wpf.UI.</li>
<li><strong>Improved theme synchronization</strong> with Windows.</li>
<li><strong>Added a new high contrast</strong> theme.</li>
</ul>

<h5><a name="3.7"></a> LookupEngine</h5>

<p>RevitLookup now runs on its own <a href="https://github.com/lookup-foundation/LookupEngine">engine</a>.
The engine can run outside Revit, and can be reused across the entire family of Autodesk products.
This is a pledge for the future for products like <strong>AutocadLookup</strong>, <strong>InventorLookup</strong> and others.</p>

<p>Isolating the engine also brings many new improvements:</p>

<ul>
<li><strong>The engine is much faster</strong> than the integrated version, it allocates less memory, and supports more features.</li>
<li><strong>Added context support</strong>, now allows additional metadata to be used to resolve members.</li>
<li><strong>Fixed CLR exceptions</strong> for critical .NET Core components when evaluating members.</li>
<li><strong>New Lookup Engine API</strong>. Isolation encouraged independent use of the API in any application.</li>
<li><strong>Improved context detection</strong>, objects now understand what context they are in and provide values specific to their context, not just the active context.</li>
<li><strong>Added navigation stack</strong>, now it will be easier to add support for new unsupported parent-dependent methods.</li>
</ul>

<h5><a name="3.9"></a> Improvements</h5>

<ul>
<li><strong>Revit 2025.4 patch</strong> brings some fixes to the API, now some methods and properties are available again for use in RevitLookup:
<ul>
<li><code>DatumPlane.CanBeVisibleInView</code></li>
<li><code>DatumPlane.GetPropagationViews</code></li>
<li><code>IndependentTag.TagText</code></li>
</ul></li>
<li>Added new <strong>View</strong> extensions:
<ul>
<li><code>GetAllPlacedInstances</code></li>
</ul></li>
<li>Added new <strong>Wall</strong> method overloads by @SergeyNefyodov:
<ul>
<li><code>IsWallCrossSectionValid</code></li>
</ul></li>
<li>Added new <strong>CompoundStructure</strong> method overloads by @SergeyNefyodov:
<ul>
<li><code>CanLayerBeStructuralMaterial</code></li>
<li><code>CanLayerBeVariable</code></li>
<li><code>CanLayerWidthBeNonZero</code></li>
<li><code>GetAdjacentRegions</code></li>
<li><code>GetCoreBoundaryLayerIndex</code></li>
<li><code>GetDeckEmbeddingType</code></li>
<li><code>GetDeckProfileId</code></li>
<li><code>GetLayerAssociatedToRegion</code></li>
<li><code>GetLayerFunction</code></li>
<li><code>GetLayerWidth</code></li>
<li><code>GetMaterialId</code></li>
<li><code>GetNumberOfShellLayers</code></li>
<li><code>GetOffsetForLocationLine</code></li>
<li><code>GetPreviousNonZeroLayerIndex</code></li>
<li><code>GetRegionEnvelope</code></li>
<li><code>GetRegionsAssociatedToLayer</code></li>
<li><code>GetSegmentCoordinate</code></li>
<li><code>GetSegmentOrientation</code></li>
<li><code>GetWallSweepsInfo</code></li>
<li><code>GetWidth</code></li>
<li><code>IsCoreLayer</code></li>
<li><code>IsRectangularRegion</code></li>
<li><code>IsSimpleRegion</code></li>
<li><code>IsStructuralDeck</code></li>
<li><code>ParticipatesInWrapping</code></li>
</ul></li>
<li>Visualisation now supports the new <a href="https://github.com/lookup-foundation/RevitLookup/wiki/Visualization#curveloop-visualization">CurveLoop</a> type, thanks @JieGou for the implementation!</li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3d00a24200c-pi"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3d00a24200c image-full img-responsive" alt="RevitLookup 2026" title="RevitLookup 2026" src="/assets/image_d83789.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h5><a name="3.10"></a> Development:</h5>

<ul>
<li><strong>Redesigned project structure</strong>:
<ul>
<li>Standalone <a href="https://github.com/lookup-foundation/LookupEngine">LookupEngine</a>. The LookupEngine is now a separate project, independent of RevitLookup.</li>
<li>Standalone <a href="https://github.com/lookup-foundation/LookupEngine.UI">LookupEngine.UI</a>. The LookupEngine UI is now a separate project, independent of RevitLookup.</li>
<li>UI Playground for development and testing the UI outside the Revit, independently.</li>
</ul></li>
<li><strong>Moving to Central Package Management</strong>, all dependencies are now managed in a single file.</li>
<li><strong>Moving to Central Build Management</strong>, common build configurations are now managed in a single file.</li>
<li><strong>Automatic registration of MVVM components</strong> in the service provider, no longer need to register each component manually.</li>
<li><strong>.NET 9 SDK support</strong> for development.</li>
<li><strong>Migrated to SLNX solution format</strong>, to avoid Git merge conflicts.</li>
<li><strong>Changed API for ContextMenu</strong>. Now it provides dependencies and execution context.</li>
<li><strong>Disabled PresentationFramework traces</strong> for the entire domain to address UI performance issues and freezing under an attached debugger, leaving only Critical events enabled.</li>
<li><strong>Updated</strong> <a href="https://github.com/lookup-foundation/RevitLookup/blob/dev/Contributing.md">Contribution</a> guide.</li>
</ul>

<h4><a name="4"></a> Design Automation API for Revit 2026</h4>

<p>The <a href="https://aps.autodesk.com/">APS</a> <a href="https://aps.autodesk.com/design-automation-apis">Design Automation API</a> for
Revit 2026 has been released, DA4R 2026.</p>

<h4><a name="5"></a> Vibe Programming Revit Macros</h4>

<p>Michael Kilkelly shared
an <a href="https://www.linkedin.com/posts/michaelkilkelly_revitapi-vibecoding-archsmarter-ugcPost-7311019788833546240-8Fho?utm_source=share&amp;utm_medium=member_desktop&amp;rcm=ACoAAACogiYBX6lBbCXzDJBU_OWT9MYGMYk5X6g">8-and-a-half minute video on LinkedIn</a> using
the Revit API launchpad
with <a href="https://en.wikipedia.org/wiki/Vibe_coding">vibe coding</a> to
generate from scratch and execute Revit API macros.</p>

<p>Michael has been working with the Revit API and macros for a long time, cf. his 2015 tutorial
on <a href="https://thebuildingcoder.typepad.com/blog/2015/10/rtc-classes-and-getting-started-with-revit-macros.html#7">getting started with Revit macros</a>.</p>

<h4><a name="6"></a> Direwolf Fast Revit Data Extraction</h4>

<p><a href="https://github.com/Framebuffers/Direwolf">Direwolf</a> implements a data analysis framework for Autodesk Revit.
It extracts, serialises, and stores parameters from BIM models in fractions of a second.</p>

<h4><a name="7"></a> From Floppies to a Memory Stick</h4>

<p>I bought a 1 TB memory stick, and it got me thinking.
In the beginning of my programming career, we used <a href="https://en.wikipedia.org/wiki/Floppy_disk">floppy disks</a>.
They originally held 360 KB.</p>

<pre><code class="language-py">kb = 1024 byte
floppy = 360 * kb = 368640 byte
mb = 1024 * kb = 1048576 byte
tb = 1024 * mb = 1073741824 byte
tb / floppy = 2912.711111</code></pre>

<p>Assuming a floppy is 3 millimetres thick, my stick would be able to store that data from a stack of floppies about 2900 &middot; 0.003 = 8.7 metres high.</p>
