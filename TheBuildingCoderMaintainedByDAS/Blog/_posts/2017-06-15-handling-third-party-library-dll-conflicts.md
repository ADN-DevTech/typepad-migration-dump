---
layout: "post"
title: "Handling Third Party Library DLL Conflicts"
date: "2017-06-15 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Win32"
original_url: "https://thebuildingcoder.typepad.com/blog/2017/06/handling-third-party-library-dll-conflicts.html "
typepad_basename: "handling-third-party-library-dll-conflicts"
typepad_status: "Publish"
---

<p>A recurring question just came up again, on handling conflicts between DLLs loaded by Revit add-ins.</p>

<p>For instance, this can be caused by a scenario in which add-ins A and B both make use of library DLL C, but specify different versions. A soon as the first add-in has loaded its version of C, the second add-in is prevented from loading the version it requires and cannot run.</p>

<p>I have no official solution to suggest for this, but here are several discussion threads and articles suggesting various workarounds:</p>

<ul>
<li><a href="http://forums.autodesk.com/t5/revit-api/proper-way-to-handle-app-config-bindingredirects-in-revit-add-in/m-p/5692149">Proper way to handle App.config bindingRedirects in Revit add-in</a></li>
<li><a href="http://forums.autodesk.com/t5/revit-api/loading-different-versions-of-same-third-party-library/m-p/6023644">Loading different versions of same third party library</a></li>
<li><a href="http://adndevblog.typepad.com/aec/2012/06/loading-multiple-versions-of-the-same-dll-used-in-revit-plug-ins.html">Loading multiple versions of the same DLL used in Revit plug-ins</a></li>
<li>The <a href="https://blogs.msdn.microsoft.com/kcwalina/2008/04/25/managed-extensibility-framework">Managed Extensibility Framework MEF</a> offers an option but requires a lot of changes.</li>
<li><a href="http://www.brad-smith.info/blog/archives/500">A Plug-In System Using Reflection, AppDomain and ISponsor</a></li>
<li>You can also use <a href="https://www.microsoft.com/en-us/download/details.aspx?id=17630">ILMerge</a> to merge all of your DLLs into one single .NET assembly, cf. the CodeProject article on <a href="https://www.codeproject.com/articles/9364/merging-net-assemblies-using-ilmerge">Merging .NET assemblies using ILMerge</a></li>
</ul>

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c903027a970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c903027a970b img-responsive" style="width: 300px; " alt="DLL hell?" title="DLL hell?" src="/assets/image_0764ee.jpg" /></a><br /></p>

<p></center></p>

<h4>Addendum &ndash; Updated ILMerge Link</h4>

<p>As noted by Micah Gray in
his <a href="https://thebuildingcoder.typepad.com/blog/2017/06/handling-third-party-library-dll-conflicts.html#comment-4983002843">comment below</a>:</p>

<blockquote>
  <p>The ILMerge link above no longer works.
  It appears to have moved over to
  the <a href="https://github.com/dotnet/ILMerge">GitHub ILMerge repository</a>.</p>
</blockquote>
