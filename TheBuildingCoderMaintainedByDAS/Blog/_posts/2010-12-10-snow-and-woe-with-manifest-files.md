---
layout: "post"
title: "Snow and Woe with Manifest Files"
date: "2010-12-10 04:00:00"
author: "Jeremy Tammik"
categories:
  - "2011"
  - "AU 2010"
  - "Debugging"
  - "Getting Started"
  - "Installation"
  - "News"
  - "Travel"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/12/snow-and-woe-with-manifest-files.html "
typepad_basename: "snow-and-woe-with-manifest-files"
typepad_status: "Publish"
---

<p>I am still on the road toward Paris.
The departure of our flight in Moscow was delayed, so we missed the connection to Paris in Vienna.
In addition, I heard that the Paris airport was closed all day until five in the afternoon due to snow.
We are still booked to fly and arrive Thursday night, though, so the Paris conference scheduled for Friday is not yet endangered.
Right now, the ETA is quarter to twelve, so it will be a short night before the conference begins...

<p>Actually, by the time I get to post this, it is early Friday morning.
We arrived safely in the hotel at two in the morning, slept for a couple of hours, and reached the office, still completely empty at this time of the day.

<p>Before I continue, here are a couple of pictures from the developer conference in Moscow, taken by my colleague Partha Sarkar.
Here is the view from the Autodesk office over snowy Moscow:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e08d60cf970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e08d60cf970b image-full" alt="View from the Autodesk office in Moscow" title="View from the Autodesk office in Moscow" src="/assets/image_7cea4c.jpg" border="0" /></a> <br />

</center>

<p>A view of the conference opening session by Jim Quanci with one of our translators at work:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e08d5fcd970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e08d5fcd970b image-full" alt="Jim Quanci presenting with translator" title="Jim Quanci presenting with translator" src="/assets/image_b8bd32.jpg" border="0" /></a> <br />

</center>

<p>And here the DevTech group having dinner after a hard day's work and lots of interesting discussions;
from left to right, Marat Mirgaleev, Philippe Leefsma, Adam Nagy, Karl Osti, Jim Quanci, Partha Sarkar, and me:</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330147e08d5e64970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330147e08d5e64970b image-full" alt="DevTech dinner" title="DevTech dinner" src="/assets/image_2777df.jpg" border="0" /></a> <br />

</center>

<h3>Updated Revit API Track at Autodesk University 2010</h3>

<p>I updated the list of 

<a href="http://thebuildingcoder.typepad.com/blog/2010/12/the-revit-api-track-at-au-2010.html">
Revit API related classes at Autodesk University 2010</a>,

adding new classes and uploading more materials for direct download.
You really must check out this material, there are several real gems in there!
Yesterday I looked through Iffat Mai's Diary of a Wimpy BIM Manager CP430-1, and I love it!
Looking through all this material is absolutely worthwhile and should keep you occupied for a while...

<a name="woe"></a>

<h3>Add-in Manifest Woes</h3>

<p>In the last couple of days I have talked with several people who had problems installing Revit add-ins, mainly because they ran into issues with the add-in manifest file.
One Russian developer says he has been struggling with these problems for months now.

<p>Please do read and follow the instructions in the developer guide 'Revit 2011 API Developer Guide.pdf' carefully and precisely!
Section 2.2.5 'Create a .addin manifest file' describes the simple case for the Hello World walkthrough, and 3.4 'Add-in Registration' provides the full detailed description.
We already discussed these issues when looking at the 

<a href="http://thebuildingcoder.typepad.com/blog/2010/04/addin-manifest-and-guidize.html#1">
manifest file in general</a> and specifically for the 

<a href="http://thebuildingcoder.typepad.com/blog/2010/05/pipe-to-conduit-converter.html#3">
pipe to conduit converter</a>.

An overview of other manifest related topics is given in the discussion of 

<a href="http://thebuildingcoder.typepad.com/blog/2010/08/network-access-to-add-in-manifest-and-icons.html">
network access and ribbon icons</a>.

<p>Some of the problems people seem to be running into and that are not highlighted in the documentation:

<ul>
<li>The ProgramData folder is hidden by default on Windows 7.
<li>The ProgramData folder is <strong>not</strong> "Program Data"; there is <strong>no space</strong> in the folder name.
<li>The encoding of the XML format add-in manifest file must correspond to what you specify in the XML header tag.
<li>The assembly path that you specify for your add-in DLL must be correct.
<li>The full class name consists of the namespace prefix with a point separator and the class name appended to it.
</ul>

<h4>ProgramData Folder Issues</h4>

<p>Some Windows 7 systems seem to have another folder "Program Data" with a space in it in addition to the ProgramData folder without a space.
Placing the manifest file in a subdirectory under the one with a space will not work!
You can either use the command line cmd.exe to change directories to the correct location, or configure the Windows Explorer not to hide system folders by selecting Organize &gt; Folder and search options &gt; View &gt; Hidden files and folders &gt; Show hidden files, folders, and drives.

<a name="fents"></a>

<p><strong>Addendum:</strong> A safer way to ensure you get to the right place is to use the DOS environment variables in the Windows explorer address bar, e.g. type %programdata% for C:\ProgramData Or %Appdata% for C:\Users\&lt;user&gt;\AppData\Roaming.</p>

<h4>Add-in Manifest File Encoding</h4>

<p>Several of the developer guide add-in manifest examples use UTF-8 encoding. 
To reuse them as shown, you need to ensure that your editor really does save them in that format.
Alternatively, you can use ANSI encoding, if you have no need for non-ASCII characters, or any other encoding acceptable for XML files.
Whatever you do, please ensure that the encoding used matches the one you specify in the XML encoding attribute.
Several people ran into this issue right in the beginning, when add-in manifest files were first introduced, and I discussed it in detail in the post on

<a href="http://thebuildingcoder.typepad.com/blog/2010/04/addin-manifest-and-guidize.html#3">
manifest files and Guidize</a>.

<h4>Assembly Path</h4>

<p>Please do not try to type in your assembly path manually.
Use copy and paste instead!
The assembly path is listed in the Visual Studio output window when compilation completes, so you can copy it from there.

<h4>Full Class Name</h4>

<p>In one example, I saw someone entering a file path here...
Go to the source code of your external command class implementation, determine what the namespace and class name is, and concatenate the two using a '.' separator to define the full class name.
Here again, please use copy and paste, do not try to type things in manually.

<h4>DevTV Add-in Template</h4>

<p>Actually, the simplest way to avoid all these problems is to use the Visual Studio

<a href="http://thebuildingcoder.typepad.com/blog/2010/10/revit-2011-devtv.html#template_update">
DevTV add-in template</a>,

which creates a valid add-in manifest file for you and automatically adds a post-build event to the generated Visual Studio solution which copies it to the right destination folder and also ensures that it remains up to date.
