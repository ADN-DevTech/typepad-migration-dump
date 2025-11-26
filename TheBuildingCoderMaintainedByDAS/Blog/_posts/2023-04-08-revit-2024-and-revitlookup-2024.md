---
layout: "post"
title: "Revit 2024 and RevitLookup 2024"
date: "2023-04-08 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2024"
  - "RevitLookup"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2023/04/revit-2024-and-revitlookup-2024.html "
typepad_basename: "revit-2024-and-revitlookup-2024"
typepad_status: "Publish"
---

<p><a href="https://blogs.autodesk.com/revit/2023/04/04/whats-new-in-autodesk-revit-2024/">Revit 2024 was released this week</a>.</p>

<p>Further improving on last year's ground-breaking speed record,
Roman <a href="https://github.com/Nice3point">Nice3point</a> was prepared well in advance to
release the corresponding <a href="https://github.com/jeremytammik/RevitLookup">RevitLookup</a>
<a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2024.0.0">2024 update</a>
faster still this time around:</p>

<!-- https://github.com/jeremytammik/RevitLookup/releases/edit/2024.0.0 -->

<div align="center">

<a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b685319833200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b685319833200d img-responsive" style="width: 300px; display: block; margin-left: auto; margin-right: auto;" alt="RevitLookup" title="RevitLookup" src="/assets/image_36dc86.jpg" /></a><br />

</div>

<p>In this release, the entire code base has been completely rewritten from scratch with a redesigned user interface.
New tools, OTA update, Windows 11 support!</p>

<p>Here is an overview of the enhancements:</p>

<ul>
<li><a href="#2">UI</a>
<ul>
<li>A brand-new user interface</li>
<li>Themes</li>
<li>Extended context menu</li>
<li>Tooltips</li>
<li>Snoop Selection on Modify tab</li>
<li>Smooth navigation</li>
<li>Windows 11 Mica effect support</li>
<li>Windows 11 Snap Layouts support</li>
<li>Accent colour synced with OS</li>
<li>New logo</li>
<li>Searchbar</li>
</ul></li>
<li><a href="#3">Engine</a>
<ul>
<li>A brand-new core</li>
<li>Extensions</li>
<li>Display all methods</li>
<li>Generic names support</li>
<li>Multiple results for methods with overloads</li>
<li>Extensible storage moved to the <code>GetEntity()</code> method</li>
<li>Extending Functionality</li>
</ul></li>
<li><a href="#4">New features</a>
<ul>
<li>Snoop Point</li>
<li>Snoop Sub-Element</li>
<li>Snoop UI Application</li>
<li>Component manager</li>
<li>PerformanceAdviser on document performance</li>
<li>Registry research: schemas, services, updaters</li>
<li>Explore built-in and APS (formerly Forge) units</li>
<li>Event monitor</li>
<li>Reworked search</li>
<li>Visual search in a project</li>
<li>OTA update</li>
</ul></li>
<li><a href="#5">RevitLookup ideas</a></li>
<li><a href="#6">Where would you like to snoop?</a></li>
</ul>

<h4><a name="2"></a> UI</h4>

<h5>A brand-new user interface</h5>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b7519ed2dd200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b7519ed2dd200c image-full img-responsive" alt="RevitLookup2024_02" title="RevitLookup2024_02" src="/assets/image_7b1301.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<h5>Themes</h5>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b685319844200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b685319844200d image-full img-responsive" alt="RevitLookup2024_03" title="RevitLookup2024_03" src="/assets/image_983f14.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<h5>Extended context menu</h5>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b685319848200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b685319848200d image-full img-responsive" alt="RevitLookup2024_04" title="RevitLookup2024_04" src="/assets/image_28e5d3.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p>Wiki page: <a href="https://github.com/jeremytammik/RevitLookup/wiki/Context-actions">Context actions</a></p>

<h5>Tooltips</h5>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b7519ed2e3200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b7519ed2e3200c image-full img-responsive" alt="RevitLookup2024_05" title="RevitLookup2024_05" src="/assets/image_573e30.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<h5>The Snoop Selection button has been moved to the Modify tab</h5>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b7517a7bff200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b7517a7bff200b image-full img-responsive" alt="RevitLookup2024_06" title="RevitLookup2024_06" src="/assets/image_9b9ea9.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<h5>Smooth navigation. Enable acceleration in Revit settings if you are having trouble with this option</h5>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b7519ed2f6200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b7519ed2f6200c img-responsive" alt="RevitLookup2024_07" title="RevitLookup2024_07" src="/assets/image_2f4703.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<h5>Windows 11 Mica effect support</h5>

<h5>Windows 11 Snap Layouts support</h5>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b7517a7c20200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b7517a7c20200b img-responsive" alt="RevitLookup2024_08" title="RevitLookup2024_08" src="/assets/image_cc4394.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<h5>Accent colour synced with OS</h5>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b7517a7c25200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b7517a7c25200b image-full img-responsive" alt="RevitLookup2024_09" title="RevitLookup2024_09" src="/assets/image_85d81b.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<h5>New logo</h5>

<h5>Searchbar</h5>

<p>Focus is triggered by pressing any key on the keyboard:</p>

<h4><a name="3"></a>  Engine</h4>

<h5>A brand-new core</h5>

<h5>Extensions</h5>

<p>Support new methods from the API and other libraries:</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b7517a7c29200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b7517a7c29200b img-responsive" alt="RevitLookup2024_10" title="RevitLookup2024_10" src="/assets/image_4a887f.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p><a href="https://github.com/jeremytammik/RevitLookup/wiki/Extensions">Available Extensions</a></p>

<h5>Display all methods</h5>

<p>Displaying all methods that objects have, even if RevitLookup does not support them:</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b7519ed302200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b7519ed302200c img-responsive" alt="RevitLookup2024_11" title="RevitLookup2024_11" src="/assets/image_0403d6.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b68531987c200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b68531987c200d image-full img-responsive" alt="RevitLookup2024_12" title="RevitLookup2024_12" src="/assets/image_9202d7.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<h5>Generic names support</h5>

<p><style> th, td { padding-right: 1em; } </style></p>

<table>
<tr><td>Before</td><td>Now</td></tr>
<tr><td>

<a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b7519ed310200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b7519ed310200c img-responsive" alt="RevitLookup2024_13" title="RevitLookup2024_13" src="/assets/image_23f300.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a>
</td><td>

<a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b7519ed327200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b7519ed327200c img-responsive" alt="RevitLookup2024_14" title="RevitLookup2024_14" src="/assets/image_fff674.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a>
</td></tr>
</table>

<h5>Multiple results for methods with overloads</h5>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b7517a7cd5200b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b7517a7cd5200b image-full img-responsive" alt="RevitLookup2024_15" title="RevitLookup2024_15" src="/assets/image_944788.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<h5>Extensible storage moved to the <code>GetEntity()</code> method</h5>

<h5>Extending Functionality</h5>

<p>Adding new features and extending the functionality of RevitLookup just got easier;
<a href="https://github.com/jeremytammik/RevitLookup/blob/dev/Contributing.md#architecture">check out the Developer's Guide</a>.</p>

<h4><a name="4"></a>  New features</h4>

<h5>Snoop Point</h5>

<h5>Snoop Sub-Element</h5>

<h5>Snoop UI Application</h5>

<h5>Component manager</h5>

<p>Explore <code>AdWindows.dll</code> and learn how the ribbon and user interface in Revit are arranged.</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b7519ed3af200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b7519ed3af200c image-full img-responsive" alt="RevitLookup2024_16" title="RevitLookup2024_16" src="/assets/image_9e671e.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<h5>PerformanceAdviser on document performance</h5>

<p><code>PerformanceAdviser</code> to Explore document performance issues.</p>

<h5>Registry research: schemas, services, updaters</h5>

<h5>Explore built0in and APS (formerly Forge) units</h5>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b685319930200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b685319930200d image-full img-responsive" alt="RevitLookup2024_17" title="RevitLookup2024_17" src="/assets/image_2ae195.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<h5>Event monitor</h5>

<p>Track all incoming events.
Events from the <code>RevitAPI.dll</code> and <code>RevitAPIUI.dll</code> libraries are available.
The search bar is used to filter results:</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b7519ed3b8200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b7519ed3b8200c image-full img-responsive" alt="RevitLookup2024_18" title="RevitLookup2024_18" src="/assets/image_6894cf.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<h5>Reworked search</h5>

<p>Now you can search for multiple values by <code>Name</code>, <code>Id</code>, <code>UniqueId</code>, <code>IfcGUID</code> and <code>Type IfcGUID</code> parameters:</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b7519ed3d8200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b7519ed3d8200c img-responsive" alt="RevitLookup2024_19" title="RevitLookup2024_19" src="/assets/image_330be1.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p>Wiki page: <a href="https://github.com/jeremytammik/RevitLookup/wiki/Search-elements">Search elements</a></p>

<h5>Visual search in a project</h5>

<p>Showing elements:</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b7519ed3dc200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b7519ed3dc200c image-full img-responsive" alt="RevitLookup2024_20" title="RevitLookup2024_20" src="/assets/image_db6c24.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p>Showing faces (Revit 2023 or higher):</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b7519ed3e3200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b7519ed3e3200c image-full img-responsive" alt="RevitLookup2024_21" title="RevitLookup2024_21" src="/assets/image_4d55ae.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p>Showing solids (Revit 2023 or higher):</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b7519ed3ec200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b7519ed3ec200c image-full img-responsive" alt="RevitLookup2024_22" title="RevitLookup2024_22" src="/assets/image_84f1a8.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p>Showing edges (Revit 2023 or higher):</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b7519ed404200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b7519ed404200c image-full img-responsive" alt="RevitLookup2024_23" title="RevitLookup2024_23" src="/assets/image_663074.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<h5>OTA update</h5>

<p>The RevitLookup update is now available directly from the plugin:</p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e16897883302b7519ed410200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883302b7519ed410200c image-full img-responsive" alt="RevitLookup2024_24" title="RevitLookup2024_24" src="/assets/image_973a5a.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p>Designed &amp; Developed by <a href="https://github.com/Nice3point">Nice3point</a> 🕊</p>

<p>Ever so many thanks to Roman <a href="https://github.com/Nice3point">Nice3point</a> for his tremendous work!</p>

<h4><a name="5"></a> RevitLookup Ideas</h4>

<p>Check out
the <a href="https://github.com/jeremytammik/RevitLookup/discussions">RevitLookup discussions page</a>
aka <a href="https://github.com/jeremytammik/RevitLookup/discussions">RevitLookup Ideas</a>
to discuss your RevitLookup wishes and dreams with the developer community.</p>

<p>Here is one of the open discussions including a poll asking for your preference:</p>

<h4><a name="6"></a> Where would you like to Snoop?</h4>

<p>Discussion about
the <a href="https://github.com/jeremytammik/RevitLookup/discussions/151">Snoop Selection button placement &#35;151</a>:</p>

<p>What do you think of the new location of the "Snoop Selection" button?
How do you feel about it and what is your opinion on it, should it be kept or made optional?
The idea is that Revit automatically opens "Modify" tab, making the Snoop button available at all times.</p>

<p><a href="https://github.com/jeremytammik/RevitLookup/discussions/151">Please vote</a>!</p>
