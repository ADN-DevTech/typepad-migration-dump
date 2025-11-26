---
layout: "post"
title: "Forge Picture, Debugging, Snooping Appearances"
date: "2019-03-26 06:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Architecture"
  - "Data Access"
  - "Debugging"
  - "Forge"
  - "RevitLookup"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2019/03/-architecture-edit-and-continue-snooping-appearance-assets.html "
typepad_basename: "-architecture-edit-and-continue-snooping-appearance-assets"
typepad_status: "Publish"
---

<p>Today, let's look at the Forge architecture, Revit add-in debug, edit and continue, and yet another RevitLookup enhancement:</p>

<ul>
<li><a href="#2">High-level picture of Forge</a> </li>
<li><a href="#3">Debug and continue in a Revit add-in</a> </li>
<li><a href="#4">Snooping appearance assets</a> </li>
</ul>

<h4><a name="2"></a> High-Level Picture of Forge</h4>

<p>Would you like to quickly understand
the <a href="https://forge.autodesk.com">Forge</a> architecture,
including all relevant aspects, without getting mired in its nitty-gritty details?</p>

<p>Check out Scott Sheppard's very cool executive overview in
the <a href="https://labs.blogs.com/its_alive_in_the_lab/2019/03/whats-so-hot-about-this-forge-thing-the-high-level-picture-for-software-development-managers.html">Forge high-level picture for software development managers</a>.</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a44b1658200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a44b1658200c image-full img-responsive" alt="Forge high-level picture" title="Forge high-level picture" src="/assets/image_fcda93.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<h4><a name="3"></a> Debug and Continue in a Revit Add-In</h4>

<p>Developers are continuously seeking reliable, efficient development approaches.
Some ways have been described in the past implementing the functionality
to <a href="https://thebuildingcoder.typepad.com/blog/about-the-author.html#5.49">edit and continue, and debug without restarting</a>.</p>

<p>This question arose again in the StackOverflow question
asking <a href="https://stackoverflow.com/questions/55256817/why-is-my-dll-still-being-used-by-revit-after-execution">why is my DLL still being used by Revit after execution?</a>.</p>

<p>Konrad Sobon jumped in and pointed out his solution:</p>

<blockquote>
  <p>I did a write-up on my blog that explains how you can use the Revit Add-In Manager to achieve the result you are after:</p>
  
  <ul>
  <li><a href="http://archi-lab.net/debugging-revit-add-ins">debugging revit add-ins</a></li>
  </ul>
  
  <p>The difference between this and a standard method of debugging is that Revit loads the DLL using the <code>LoadFrom</code> method, locking it up for as long as the Revit.exe process is running, while the Add-In Manager uses the <code>Load</code> method that only reads the <code>byte[]</code> stream of the DLL which means it remains available, and you can re-build your solution in VS, and reload in Revit without closing it. It does have drawbacks, obviously, so please read the post.</p>
</blockquote>

<h4><a name="4"></a> Snooping Appearance Assets</h4>

<p>In further support of efficient debugging and Revit database exploration, here is
another <a href="https://github.com/jeremytammik/RevitLookup">RevitLookup</a> enhancement
enabling snooping of appearance assets, based on three pull requests 
by <a href="http://www.facebook.com/profile.php?id=100003616852588">Victor Chekalin</a>, aka Виктор Чекалин:</p>

<ul>
<li><a href="https://github.com/jeremytammik/RevitLookup/pull/48">#48 &ndash; snoop rendering <code>AssetProperty</code></a></li>
<li><a href="https://github.com/jeremytammik/RevitLookup/pull/49">#49 &ndash; pushed the missed files</a></li>
<li><a href="https://github.com/jeremytammik/RevitLookup/pull/50">#50 &ndash; handle <code>AssetPropertyDoubleArray4d</code></a></li>
</ul>

<p>The description is sweet and simple:</p>

<ul>
<li>Snoop rendering <code>AssetProperty</code> &ndash; <code>Material</code> &rarr; <code>AppearanceAssetId</code> &rarr; <code>GetRenderingAssset</code></li>
</ul>

<p>This is supported by more than a thousand words:</p>

<p><center></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a44b1668200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a44b1668200c image-full img-responsive" alt="Snooping appearance assets" title="Snooping appearance assets" src="/assets/image_6ab487.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4744f35200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4744f35200d image-full img-responsive" alt="Snooping appearance assets" title="Snooping appearance assets" src="/assets/image_f3052b.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a4744f43200d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a4744f43200d image-full img-responsive" alt="Snooping appearance assets" title="Snooping appearance assets" src="/assets/image_6fd027.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a44b1683200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a44b1683200c image-full img-responsive" alt="Snooping appearance assets" title="Snooping appearance assets" src="/assets/image_3fce53.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p><a class="asset-img-link"  href="https://thebuildingcoder.typepad.com/.a/6a00e553e1689788330240a44b1cb5200c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330240a44b1cb5200c image-full img-responsive" alt="Snooping appearance assets" title="Snooping appearance assets" src="/assets/image_4321cb.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>I integrated Victor's pull requests
in <a href="https://github.com/jeremytammik/RevitLookup/releases/tag/2019.0.0.11">RevitLookup release 2019.0.0.11</a>.</p>

<p>Many thanks to Victor for this useful enhancement!</p>
