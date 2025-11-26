---
layout: "post"
title: "IFamilyLoadOptions and GEMini"
date: "2010-02-12 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Events"
  - "Family"
original_url: "https://thebuildingcoder.typepad.com/blog/2010/02/ifamilyloadoptions-and-gemini.html "
typepad_basename: "ifamilyloadoptions-and-gemini"
typepad_status: "Publish"
---

<p>First of all, a new lunar year is beginning, and with it the year of the tiger. Happy Chinese New Year to all!</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330120a8910484970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330120a8910484970b" alt="Year of the tiger" title="Year of the tiger" src="/assets/image_0b54c7.jpg" border="0"  /></a> <br />

</center>

<p>We discussed the new

<a href="http://thebuildingcoder.typepad.com/blog/2009/08/the-revit-family-api.html">
Revit Family API</a> in

some depth and presented

<a href="http://thebuildingcoder.typepad.com/blog/2009/10/revit-family-creation-api-labs.html">
labs</a> to

help start making efficient use of it.
One issue in the context of families and the loading of families remained unresolved, however, and we have some questions on this from

<a href="http://thebuildingcoder.typepad.com/blog/2009/10/revit-2010-subscription-pack.html?cid=6a00e553e1689788330120a67a6b82970c#comment-6a00e553e1689788330120a67a6b82970c">
Prasad</a> and

<a href="http://thebuildingcoder.typepad.com/blog/2009/08/the-revit-family-api.html?cid=6a00e553e16897883301287655e694970c#comment-6a00e553e16897883301287655e694970c">
Lars R&aring;dman</a> that

still remain unanswered, namely on how to use the IFamilyLoadOptions interface.
So far nobody was actually able to provide a working example.

<p>Now Gamal Kira of

<a href="http://www.team-solutions.de">
GEM Team Solutions</a>

solved this problem and very friendlily provided a sample application illustrating the solution.

<p>The IFamilyLoadOptions interface defines call-back functionality for specific family load situations through the two events OnFamilyFound and OnSharedFamilyFound, called when a family or a shared family being loaded was already present in the target document, respectively.

<p>They both take an output argument in which the event handler has the opportunity to specify whether the existing family's parameter values should be overwritten.

<p>Unfortunately, this does not currently work the way one might expect. Once a family has been loaded into the project, it will not be reloaded later, even if it was modified, and attempting to use this event handler throws an exception.

<p>One would mostly want a family to be reloaded if it has been edited, even if it is already loaded.

<p>The trick to this that Gamal has uncovered is to call the LoadFamily method outside of a command context.
Normally, the Revit API is only usable within the command context, although read-only access sometimes also works outside of it, e.g. when called from a modeless dialogue.
This is the first example that I know of where some functionality actually requires a non-command context.

<p>Here is what Gamal has to say about his example solution:

<p>I isolated the ILoadOptions solution in a separate project

<span class="asset  asset-generic at-xid-6a00e553e168978833012877939db0970c"><a href="http://thebuildingcoder.typepad.com/files/revittest.zip">RevitTest</a></span>

<h4>Installation and GEMini</h4>

To test, register the command in Revit.ini in the normal fashion.
If you like, you can use our GEMini.exe registration tool, which is included.
It works through an XML configuration file.
Simply adapt the path specified in the batch file Setup-Debug-RevitIni.bat and run it.
If you are interested in the source for GEMini, please feel free to ask.
It works with all versions of Revit, including 64-bit and 32-bit ones running on a 64-bit system.

<h4>IFamilyLoadOptions and RevitTest</h4>

<p>The external command GEM RevitTest displays a dialogue box to initiate the LoadFamily call.
This dialogue can be displayed either as a modal or modeless form.
From the form, LoadFamily can be called either with or without the IFamilyLoadOptions call-backs installed.
When modal, the LoadFamily operation is executed within the command context, whereas the modeless dialogue executes it outside of the command context.
The call to LoadFamily always throws an exception when called within the command context, regardless of whether the IFamilyLoadOptions call-backs are installed or not.
From the modeless version, the IFamilyLoadOptions call-back works.</p>

<p>Very many thanks to Gamal for all his research and for providing this valuable sample!</p>

<center>

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330120a89102bc970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330120a89102bc970b" alt="Year of the tiger" title="Year of the tiger" src="/assets/image_1583aa.jpg" border="0"  /></a> <br />

<a style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330128779399c6970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330128779399c6970c" alt="Year of the tiger" title="Year of the tiger" src="/assets/image_298759.jpg" border="0"  /></a> <br />

</center>
