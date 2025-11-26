---
layout: "post"
title: "RVT and RFA Thumbnail Image"
date: "2009-06-30 08:48:38"
author: "Jeremy Tammik"
categories:
  - "External"
  - "User Interface"
  - "Win32"
original_url: "https://thebuildingcoder.typepad.com/blog/2009/06/rvt-and-rfa-thumbnail-image.html "
typepad_basename: "rvt-and-rfa-thumbnail-image"
typepad_status: "Publish"
---

<p>Here is a question that reappears pretty regularly.</p>

<p><strong>Question:</strong>
How can I obtain the preview bitmap image or thumbnail from a Revit project or family file using the Revit API?</p>

<p>This is an example of the kind of image I am interested in:</p>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301157099d684970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="at-xid-6a00e553e16897883301157099d684970c" alt="RFA thumbnail preview bitmap image" title="RFA thumbnail preview bitmap image" src="/assets/image_5497a8.jpg" border="0"  /></a>

<p><strong>Answer:</strong>
The topic of 

<a href="http://redbolts.com/blog/post/2008/12/01/Getting-your-Revit-thumbnails.aspx">
getting your Revit thumbnails</a>

was already been discussed by

<a href="http://redbolts.com/blog">
Guy Robinson</a>

in

<a href="http://redbolts.com/blog">
Bolt out of the Red</a>, 

but for completeness sake I will partially reiterate it here as well, since the query keep reappearing.</p>

<p>The Revit API does not provide any built-in support for this, but you can make use of generic Windows API functionality instead.
Revit project files use Windows structured storage to manage resources internally. 
You can use the DocFile Viewer utility dfview.exe to look at the structured storage file contents. 
Here is an example of a Revit project file with the preview image highlighted:</p>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301157099d7b9970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="at-xid-6a00e553e16897883301157099d7b9970c" alt="RVT file structured storage and preview image in dfview.exe" title="RVT file structured storage and preview image in dfview.exe" src="/assets/image_5b0f57.jpg" border="0"  /></a>

<p>We had a short look at the internal RVT file structure when exploring how to extract the Revit build version from 

<a href="http://thebuildingcoder.typepad.com/blog/2008/10/rvt-file-version.html">
RVT</a> 

and 

<a href="http://thebuildingcoder.typepad.com/blog/2009/06/rfa-version-grey-commands-family-context-and-rdb-link.html#1">
RFA</a> 

files.</p>

<p>The thumbnail is a standard PNG file inserted into the Revit structured storage document.</p>

<p>To extract the preview thumbnail image, you can use the Windows

<a href="http://msdn.microsoft.com/en-us/library/bb761848(VS.85).aspx">
IExtractImage interface</a>. 

Preview.dll is a shell plug-in, i.e. an object that implements this interface. 
It is used by the Windows Shell Folders to extract preview images for "known" file types.
The preview extractor needs to register itself in the registry and implement the two following standard API functions:</p>

<pre class="code">
  STDMETHOD(GetLocation)(
    LPWSTR pszPathBuffer,
    DWORD cchMax,
    DWORD *pdwPriority,
    const SIZE *prgSize,
    DWORD dwRecClrDepth,
    DWORD *pdwFlags);

  STDMETHOD(Extract)(HBITMAP*);
</pre>

<p>More details and sample code are available from 

<a href="http://redbolts.com/blog/post/2008/12/01/Getting-your-Revit-thumbnails.aspx">
Guy's post</a>.</p>
