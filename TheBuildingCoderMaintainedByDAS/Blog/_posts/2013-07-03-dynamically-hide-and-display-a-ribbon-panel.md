---
layout: "post"
title: "Dynamically Hide and Display a Ribbon Panel"
date: "2013-07-03 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Discipline"
  - "Fun"
  - "Ribbon"
  - "RME"
  - "RST"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/07/dynamically-hide-and-display-a-ribbon-panel.html "
typepad_basename: "dynamically-hide-and-display-a-ribbon-panel"
typepad_status: "Publish"
---

<p>Here is a quick little question on dynamically toggling the display of a custom ribbon panel, e.g. depending on the currently activated Revit disciplines.</p>

<p>First, however, let me share this nice and wise little quote regarding the current weather situation and actually life and happiness in general from the German humourist

<a href="http://en.wikipedia.org/wiki/Karl_Valentin">Karl Valentin</a> (1882-1948):

<p><i>I am happy if it rains &ndash; because if I am unhappy, it still goes on raining

<br/>(Ich freue mich, wenn es regnet &ndash; denn wenn ich mich nicht freue, regnet es auch).</i></p>

<a name="2"></a>

<h4>Toggle Ribbon Panel Visibility</h4>

<p><strong>Question:</strong> Is there an easy way to dynamically turn a specific user ribbon panel on or off depending on the currently active Revit disciplines?</p>

<p>For instance, I have implemented these two separate Structure and MEP ribbon panels within our company's ribbon tab, and I would like them to automatically reflect the changes in discipline option.</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301901e19e5b1970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301901e19e5b1970b image-full" alt="Structure and MEP ribbon panels" title="Structure and MEP ribbon panels" src="/assets/image_a1af2f.jpg" border="0" /></a><br />

</center>

<p>When the structural and systems disciplines are disabled in the Revit user interface, these two panels should disappear:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e1689788330191040fe76e970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e1689788330191040fe76e970c image-full" alt="Structure and systems inactive" title="Structure and systems inactive" src="/assets/image_64aadb.jpg" border="0" /></a><br />

</center>

<p>I am aware of the dedicated

<a href="http://thebuildingcoder.typepad.com/blog/2013/02/whats-new-in-the-revit-2011-api.html">
VisibilityMode tag</a> defined

by the add-in manifest XML format, but that only enables and disables individual buttons depending on the discipline.

<p>The command

<a href="http://thebuildingcoder.typepad.com/blog/2011/02/enable-ribbon-items-in-zero-document-state.html">
availability class</a> provides

even greater control and also only affects individual buttons.

<p>I already made use of both these two options for other purposes.

<p>The Revit 2013 API provides new properties like Application.IsStructureEnabled, etc.
I would like the information returned by that property to dynamically turn the entire corresponding ribbon panel on and off, not just a single command.</p>


<p><strong>Answer:</strong> Yes, the Revit API actually does provide complete and simple support for this functionality, as you can discover by exploring the methods and properties provided by the respective classes.
The Revit API documentation always has some new little surprise to offer, if you study it carefully.</p>

<p>In this case, you can make use of the read-write RibbonPanel.Visible property, which can be set to false to hide a panel.</p>

<p>You can save the RibbonPanel instance in a global variable in your application class to switch its state at will.</p>

<p>You can also make use of a self-implemented

<a href="http://thebuildingcoder.typepad.com/blog/2012/11/roll-your-own-toggle-button.html">
toggle button</a> in

one panel to control another panel's visibility.</p>
