---
layout: "post"
title: "RevitLookup Geometry Visualisation"
date: "2024-06-13 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Analysis"
  - "Data Access"
  - "Geometry"
  - "RevitLookup"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2024/06/revitlookup-geometry-visualisation.html "
typepad_basename: "revitlookup-geometry-visualisation"
typepad_status: "Publish"
---

<p>A small step for Roman, a giant leap for the Revit add-in developer community:</p>

<ul>
<li><a href="#2">1000 stars on GitHub</a></li>
<li><a href="#3">RevitLookup Geometry Visualization</a></li>
<li><a href="#4">RevitLookup 2025.0.5</a></li>
<li><a href="#5">RevitLookup 2025.0.6</a></li>
<li><a href="#6">RevitLookup 2025.0.7</a></li>
<li><a href="#7">Versions and Visualisation Wiki</a></li>
</ul>

<p>RevitLookup has been rewarded 1000 well-earned stars on GitHub.
To celebrate,
Roman <a href="https://t.me/nice3point">@Nice3point</a> Karpovich, aka Роман Карпович,
presents a huge new chunk of RevitLookup functionality enabling Revit BIM element geometry visualization.</p>

<h4><a name="2"></a> 1000 Stars and Geometry Visualisation</h4>

<p>We are proud to share that RevitLookup has achieved 1000 stars on GitHub!
This milestone is a testament to its value and the dedication of our community.
Thank you for helping us reach this landmark!</p>

<p><center>
<a href="https://star-history.com/#jeremytammik/RevitLookup&amp;Date">
    <picture>
        <source media="(prefers-color-scheme: dark)" srcset="https://api.star-history.com/svg?repos=jeremytammik/RevitLookup&amp;type=Date" />
        <source media="(prefers-color-scheme: light)" srcset="https://api.star-history.com/svg?repos=jeremytammik/RevitLookup&amp;type=Date" />
        <img alt="Star History Chart" src="https://api.star-history.com/svg?repos=jeremytammik/RevitLookup&amp;type=Date" />
    </picture>
</a>
</center></p>

<p>To celebrate it, we are excited to introduce a major new feature in this release that will transform your interaction with models, offering a deeper understanding of the geometric objects that constitute your models:</p>

<h4><a name="3"></a> RevitLookup Geometry Visualization</h4>

<blockquote>
  <p>Introducing the new Geometry Visualization feature in RevitLookup!
  Now you can visualize various geometry objects directly within the interface.
  Enhance your BIM workflow with this powerful tool!</p>
</blockquote>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3b689be200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3b689be200b image-full img-responsive" alt="RevitLookup geometry visualisation" title="RevitLookup geometry visualisation" src="/assets/image_ae834b.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>It is built using the Revit API <code>DirectContext3D</code> functionality and described in
the <a href="https://github.com/jeremytammik/RevitLookup/wiki/Visualization">wiki documentation of RevitLookup Geometry Visualization</a>.
It was mainly implemented in release 2025.0.5, with further enhancements following in 2025.0.6 and 2025.0.7.</p>

<p>Please feel free to submit your feedback, wishes and suggestions regarding visualization in 
the <a href="https://github.com/jeremytammik/RevitLookup/pull/245">comments on pull request 245</a>.</p>

<h4><a name="4"></a> RevitLookup 2025.0.5</h4>

<p><a href="https://github.com/jeremytammik/RevitLookup/releases/edit/2025.0.6">RevitLookup 2025.0.5</a> includes
comprehensive geometry visualization capabilities, enabling users to visualize various geometry objects directly within the RevitLookup interface.</p>

<p>In Revit, geometry is at the core of every model.
Whether you are dealing with simple shapes or intricate structures, having the ability to visualize geometric elements can significantly improve your workflow, analysis and understanding of the BIM.</p>

<p>To illustrate the power of these visualization capabilities, here are samples of the geometric objects you can now explore directly within RevitLookup:</p>

<p>Mesh:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3b2db16200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3b2db16200c image-full img-responsive" alt="Mesh" title="Mesh"   src="/assets/image_c92542.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Face:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3b2db1b200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3b2db1b200c image-full img-responsive" alt="Face" title="Face"  src="/assets/image_6bddae.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Solid:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3b689c4200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3b689c4200b image-full img-responsive" alt="Solid" title="Solid" src="/assets/image_8d5779.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Curve:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3b2db23200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3b2db23200c image-full img-responsive" alt="Curve" title="Curve" src="/assets/image_892f4f.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Edge:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3b2db2c200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3b2db2c200c image-full img-responsive" alt="Edge" title="Edge"   src="/assets/image_fc3c83.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>BoundingBox:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302dad0c6a7aa200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302dad0c6a7aa200d image-full img-responsive" alt="BoundingBox" title="BoundingBox" src="/assets/image_11647f.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>XYZ:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3b2db38200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3b2db38200c image-full img-responsive" alt="XYZ" title="XYZ"  src="/assets/image_b17a99.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>For detailed documentation, check
the <a href="https://github.com/jeremytammik/RevitLookup/wiki/Visualization">wiki documentation of RevitLookup Geometry Visualization</a>.</p>

<p>Feel free to leave comments and suggestions regarding visualization in
the <a href="https://github.com/jeremytammik/RevitLookup/pull/245">pill request 245</a>.
Your input help improve this tool for everyone in the Revit community.</p>

<p>Other improvements include:</p>

<ul>
<li><strong>BoundingBoxXYZ</strong> class support
<ul>
<li>Added <code>Bounds</code> method support</li>
<li>Added <code>MinEnabled</code> method support</li>
<li>Added <code>MaxEnabled</code> method support</li>
<li>Added <code>BoundEnabled</code> method support</li>
</ul></li>
<li>Added <strong>Edit parameter</strong> icon</li>
<li>Added <strong>Select</strong> context menu action for Reference type</li>
<li>Added <strong>Export family size table</strong> for FamilySizeTableManager type by @SergeyNefyodov in https://github.com/jeremytammik/RevitLookup/pull/244</li>
</ul>

<p>Added new extensions:</p>

<ul>
<li>Application: GetFormulaFunctions &ndash; Gets list of function names supported by formula engine</li>
<li>Application: GetFormulaOperators &ndash; Gets list of operator names supported by formula engine</li>
<li>BoundingBoxXYZ: Centroid &ndash; Gets the bounding box center point</li>
<li>BoundingBoxXYZ: Vertices &ndash; Gets list of bounding box vertices</li>
<li>BoundingBoxXYZ: Volume &ndash; Evaluate bounding box volume</li>
<li>BoundingBoxXYZ: SurfaceArea &ndash; Evaluate bounding box surface area</li>
<li>Document: GetAllGlobalParameters &ndash; Returns all global parameters available in the given document</li>
<li>Document: GetLightGroupManager &ndash; Gets a light group manager object from the given document</li>
<li>Document: GetTemporaryGraphicsManager &ndash; Gets a TemporaryGraphicsManager reference of the document</li>
<li>Document: GetAnalyticalToPhysicalAssociationManager &ndash; Gets a AnalyticalToPhysicalAssociationManager for this document</li>
<li>Document: GetFamilySizeTableManager &ndash; Gets a FamilySizeTableManager from a Family</li>
<li>UIApplication: CurrentTheme &ndash; Gets a current theme</li>
<li>UIApplication: CurrentCanvasTheme &ndash; Gets a current canvas theme</li>
<li>UIApplication: FollowSystemColorTheme &ndash; Indicate if the overall theme follows operating system color theme</li>
<li>View: GetSpatialFieldManager &ndash; Retrieves manager object for the given view</li>
</ul>

<p>Hope everyone enjoys the new release.
Thanks!</p>

<p>Made with love by <a href="https://t.me/nice3point">@Nice3point</a>.</p>

<h4><a name="5"></a> RevitLookup 2025.0.6</h4>

<p><a href="https://github.com/jeremytammik/RevitLookup/releases/edit/2025.0.6">RevitLookup 2025.0.6</a> implements:</p>

<ul>
<li><a href="https://github.com/jeremytammik/RevitLookup/issues/250">Visualization dark theme support</a></li>
<li><a href="https://github.com/jeremytammik/RevitLookup/compare/2025.0.5...2025.0.6">Full changelog</a></li>
</ul>

<h4><a name="6"></a> RevitLookup 2025.0.7</h4>

<p><a href="https://github.com/jeremytammik/RevitLookup/releases/edit/2025.0.7">RevitLookup 2025.0.7</a> implements
solid scaling, theme synchronisation with Revit and other improvements:</p>

<p>Solid scaling:</p>

<p>Visualisation now supports scaling a solid, relative to its centre. 
Exploring small objects is now even easier, cf.,
<a href="https://github.com/jeremytammik/RevitLookup/issues/251">issue 251</a>:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3b689dd200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3b689dd200b image-full img-responsive" alt="Solid scaling" title="Solid scaling" src="/assets/image_538879.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Theme synchronisation with Revit:</p>

<p>Starting with Revit 2024, you can choose to automatically change the RevitLookup theme.
Fans of darker colors will no longer have to dig through the settings every time:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302c8d3b2db40200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302c8d3b2db40200c image-full img-responsive" alt="Theme synchronisation" title="Theme synchronisation" src="/assets/image_e0f39a.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>Other improvements:</p>

<ul>
<li>Improved arrow position for vertical edges on visualization</li>
<li>Multithreading visualization support. Changing settings now does not affect rendering. Previously there were artifacts due to fast settings changes</li>
</ul>

<p>New <code>Element</code> extensions:</p>

<ul>
<li>GetCuttingSolids &ndash; Gets all the solids which cut the input element</li>
<li>GetSolidsBeingCut &ndash; Get all the solids which are cut by the input element</li>
<li>IsAllowedForSolidCut &ndash; Validates that the element is eligible for a solid-solid cut</li>
<li>IsElementFromAppropriateContext &ndash; Validates that the element is from an appropriate document</li>
</ul>

<h4><a name="7"></a> Versions and Visualisation Wiki</h4>

<ul>
<li><a href="https://github.com/jeremytammik/RevitLookup/wiki/Versions">RevitLookup versioning</a></li>
<li><a href="https://github.com/jeremytammik/RevitLookup/wiki/Visualization">RevitLookup visualization</a></li>
</ul>
