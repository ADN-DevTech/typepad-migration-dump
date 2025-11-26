---
layout: "post"
title: "External Application Attributes"
date: "2011-02-24 05:00:00"
author: "Jeremy Tammik"
categories:
  - "2011"
  - "Getting Started"
  - "Installation"
  - "Migration"
  - "Settings"
original_url: "https://thebuildingcoder.typepad.com/blog/2011/02/external-application-attributes.html "
typepad_basename: "external-application-attributes"
typepad_status: "Publish"
---

<p>Arno&scaron;t L&ouml;bel added a 

<a href="http://thebuildingcoder.typepad.com/blog/2011/02/enable-ribbon-items-in-zero-document-state.html?cid=6a00e553e168978833014e5f658859970c#comment-6a00e553e168978833014e5f658859970c">
comment</a> on

the little 

<a href="http://thebuildingcoder.typepad.com/blog/2011/02/enable-ribbon-items-in-zero-document-state.html">
zero document external application</a> that 

I presented last week, pointing out that the transaction attribute (e.g. TransactionMode.Manual) is only applicable to external commands, not applications.
As he says, it should be treated as an add-in declaration error, but since it is harmless, it is quietly ignored. 
(It could be treated as an error in the future though.)

<p>The question is also how the regeneration attribute is handled.
It was originally intended for both commands and applications, but sometime in the future it will not be needed for either.

<p>Currently it is required, however, and you will get an error if you remove it.

<p>To ensure absolute clarity on this issue, I tested removing both of the attributes.

<p>That causes no warning or error messages during compilation, obviously, since Visual Studio does not care what attributes we set on the classes we define.

The application cannot be loaded, however:

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833014e8648558f970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833014e8648558f970d" alt="External application required Regeneration option attribute" title="External application required Regeneration option attribute" src="/assets/image_422122.jpg" border="0" /></a> <br />

</center>

<p>So the regeneration attribute really is required, and the transaction one is not.

<p>This error message is maybe bit confusing, since it states 'Could not run', which does not really make sense for an external application. 
In theory, it should probably say 'Could not load' instead.

<p>I updated the code and the zip file provided in the 

<a href="http://thebuildingcoder.typepad.com/blog/2011/02/enable-ribbon-items-in-zero-document-state.html">
original post</a>

to remove the extraneous transaction attribute.


<h4>DevTV Template Update</h4>

<p>I mentioned how 

<a href="http://thebuildingcoder.typepad.com/blog/2010/10/revit-2011-devtv.html#2">
easy it is to update</a> the 

<a href="http://thebuildingcoder.typepad.com/blog/2010/07/devtv-addin-templates.html">
DevTV Visual Studio Wizard Revit add-in templates</a>.

<p>So I updated my personalised C# version, which previously still generated the extraneous transaction attribute on the external application.
That is actually the reason why it was included in the original application that Arno&scaron;t commented on, since I used my version of the DevTV template to create it...

<p>Here is the new updated version,

<span class="asset  asset-generic at-xid-6a00e553e1689788330147e2c93858970b"><a href="http://thebuildingcoder.typepad.com/files/templaterevitarchaddincsjt4.zip">TemplateRevitArchAddinCsJt4.zip</a></span>.
