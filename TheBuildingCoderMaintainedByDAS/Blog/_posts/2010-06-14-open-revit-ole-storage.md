---
layout: "post"
title: "Open Revit OLE Storage"
date: "2010-06-14 06:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "External"
  - "Utilities"
  - "Win32"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/06/open-revit-ole-storage.html "
typepad_basename: "open-revit-ole-storage"
typepad_status: "Publish"
---

<p>One of the valuable spoils of the AEC DevCamp last week in Waltham is the following stand-alone utility created by David S. Echols of 

<a href="ha-inc.com">
H&A Architects & Engineers</a> for

opening and analysing the contents of the Revit structured OLE storage file streams.

<p>We initially discussed this internal RVT and RFA file structure when presenting the Python utility to read the 

<!-- 023_rvt_file_version.htm -->

<a href="http://thebuildingcoder.typepad.com/blog/2008/10/rvt-file-version.html">
RVT or RFA file version</a> without

starting up and loading the file into Revit.

It is also of interest to read the 

<!-- C:\a\doc\revit\blog\162_rfa_thumbnail.htm -->

<a href="http://thebuildingcoder.typepad.com/blog/2009/06/rvt-and-rfa-thumbnail-image.html">
RVT and RFA thumbnail image</a> stored 

in the file.

<p>Now Dave has submitted this great little utility that goes much further in reading and parsing the information available in the structured file data, providing access to the thumbnail image and items such as the following from the sample file rac_advanced_sample_project.rvt provided with Revit Architecture 2011:

<ul>
<li>DocType: Project
<li>WorkSharing: NotEnabled
<li>IsCentralFile: False
<li>UserName: tammikj
<li>CentralFilePath: 
<li>RevitBuild: Autodesk Revit Architecture 2011 (Build: 20100208_2115)
<li>Product: Architecture
<li>Platform: x86
<li>BuildTimeStamp: 20100208_2115
<li>LastSavedpath: C:\My Documents\BIM_UX_Team_Projects_2011\First Exp FT\Advanced Projects\RTM Files\rac_advanced_sample_project.rvt
<li>OpenWorksetDefault: 3
</ul>

<p>Here is an example of the result of parsing the RAC sample file rac_basic_sample_project.rvt:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330133f0ec7ed3970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330133f0ec7ed3970b image-full" alt="OpenRevitOleStorage utility showing structured file storage data from rac_basic_sample_project.rvt" title="OpenRevitOleStorage utility showing structured file storage data from rac_basic_sample_project.rvt" src="/assets/image_4548a0.jpg" border="0"  /></a> <br />

</center>

<p>Here is Dave's description of this utility and the associated source code:

<p style="color:darkblue">Attached is a zip file of my OLE Storage test project. 
It does not have any references to the Revit API assemblies, so you should be able to extract the files, open the project and run it. 
Select the file you want to open and the BasicFileInfo will be displayed in the Text area.
If there is a preview image, it will be displayed to the right of the text area. 
This is course provided as-is.

<p>Here is 

<span class="asset  asset-generic at-xid-6a00e553e16897883301348416bfbb970c"><a href="http://thebuildingcoder.typepad.com/files/openrevitolestorage.zip">OpenRevitOleStorage.zip</a></span>

containing the complete source code and Visual Studio solution for this stand-alone utility.

<p>Obviously, as Dave points out, you make use of this utility or the provided code at your own risk.
Its use is completely unsupported, has nothing to do with the Revit API, and can change at any time.

<h4>Addendum</h4>

<p>Here is a question that came up just a couple of days later, so I thought I would add it to this post, for completeness' sake:

<p><strong>Question:</strong> Here is a longstanding issue I have had with Revit &ndash; how to test externally for file version?
 
<p>We automate some processes and start Revit to do it, therefore it would be very helpful to understand what version the file is that we are attempting to open!
 
<p>One problem with Revit is that there is no way to determine the version of a Revit project file outside (or inside) of Revit. On opening the file, Revit immediately upgrades an older file with no way to stop the process from happening. We even thought of storing information in an external  SQL database about what version of Revit each file is utilizing. We moved away from a similar method with AutoCAD projects, though, because of the issues it presented.

<p><strong>Answer:</strong> A Revit project is stored as a compound storage file, which consists of streams and storages. Storages are like folders, and can contain their own substreams and substorages.
 
<p>At the top level, as of a few releases ago, one stream is BasicFileInfo.  If interpreted as Unicode, it is roughly human-readable and lists some information about the model.  This includes the build of Revit used to save the model.
 
<p>This information was added so that out own support and development teams could quickly take a peek at a model and see what build it was saved in, and also whether it has worksharing enabled, since that information is also stored.
