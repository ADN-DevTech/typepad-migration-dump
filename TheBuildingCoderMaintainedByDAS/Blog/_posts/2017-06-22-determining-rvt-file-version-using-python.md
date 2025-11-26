---
layout: "post"
title: "Determining RVT File Version Using Python"
date: "2017-06-22 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Data Access"
  - "External"
  - "Python"
  - "Storage"
original_url: "https://thebuildingcoder.typepad.com/blog/2017/06/determining-rvt-file-version-using-python.html "
typepad_basename: "determining-rvt-file-version-using-python"
typepad_status: "Publish"
---

<script src="https://cdn.rawgit.com/google/code-prettify/master/loader/run_prettify.js" type="text/javascript"></script>

<p>Before diving into a programming topic, albeit a non-Revit-API one, let me highlight this interesting read 
on <a href="http://blogs.autodesk.com/revit/2017/05/18/whats-next-revit-2018">The View from Inside the Factory: What’s Next for Revit 2018</a>,
well worthwhile for programmers and non-programmers alike, discussing stuff like:</p>

<blockquote>
  <p>The way we develop and deliver Revit software...  what that means to you, and to us, the folks 'inside the factory'...
  agile development and delivery... more frequent releases...
  <a href="https://forums.autodesk.com/t5/revit-roadmaps/bg-p/307">Revit Roadmap</a>
  and <a href="https://forums.autodesk.com/t5/revit-ideas/idb-p/302">Revit Ideas Page</a>...</p>
</blockquote>

<p>Returning to programming issues, we discussed several approaches to read the <code>BasicFileInfo</code> and RVT OLE storage,
aka <a href="https://en.wikipedia.org/wiki/COM_Structured_Storage">COM Structured Storage</a>,
to retrieve stuff like the file version and preview image, and, more lately, alternative access to BIM data via Forge:</p>

<ul>
<li><a href="http://thebuildingcoder.typepad.com/blog/2008/10/rvt-file-version.html">RVT file version using Python <code>rvtver.py</code></a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2009/06/rfa-version-grey-commands-family-context-and-rdb-link.html#1">RFA file version using Python <code>rvtver.py</code></a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2010/06/open-revit-ole-storage.html">C# Revit OLE Storage viewer</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2013/01/basic-file-info-and-rvt-file-version.html#5">Basic File Info and RVT file version via C# console application</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2016/02/reading-an-rvt-file-without-revit.html">Reading an RVT File without Revit</a></li>
<li><a href="http://thebuildingcoder.typepad.com/blog/2017/05/external-access-to-the-revit-api.html">External Access to the Revit API</a></li>
</ul>

<!---
0887_rvt_file_version.htm
1407_read_rvt_without_revit.md
--->

<p><center></p>

<p><a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c904bee4970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c904bee4970b image-full img-responsive" alt="Revit OLE storage" title="Revit OLE storage" src="/assets/image_3bade1.jpg" border="0" /></a><br /></p>

<p></center></p>

<p>Frederic now presented another more efficient Python solution for accessing the RVT file version in
his <a href="http://thebuildingcoder.typepad.com/blog/2008/10/rvt-file-version.html#comment-3378524407">comment</a> on the first post above:</p>

<blockquote>
  <p>I recently needed the same functionality, but in a large project file the <code>BasicFileInfo</code> was in line 900000 of 3000000 if I remember correctly.</p>
  
  <p>So, I needed something that accesses the <code>BasicFileInfo</code> directly.</p>
  
  <p>With the external <a href="https://pypi.org/project/olefile"><code>olefile</code> Python package</a> from <a href="https://pypi.org">pypi.org</a>, that was very easy and readable &ndash;
  <a href="https://gist.github.com/hdm-dt-fb/46aa41f5394ed5e8e7055bc7258d2ff1">check out my gist</a>:</p>
</blockquote>

<pre class="prettyprint">
import os.path as op
import olefile
import re

def get_rvt_file_version(rvt_file):
  if op.exists(rvt_file):
    if olefile.isOleFile(rvt_file):
      rvt_ole = olefile.OleFileIO(rvt_file)
      bfi = rvt_ole.openstream("BasicFileInfo")
      file_info = bfi.read().decode("utf-16le", "ignore")
      pattern = re.compile(r"\d{4}")
      rvt_file_version = re.search(pattern, file_info)[0]
      return rvt_file_version
    else:
      print("file does not apper to be an ole file: {}".format(rvt_file))
  else:
    print("file not found: {}".format(rvt_file))
</pre>

<p>Thank you very much for sharing this, Frederic!</p>
