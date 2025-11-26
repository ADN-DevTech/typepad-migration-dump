---
layout: "post"
title: "Happy Bhai Dooj!"
date: "2012-11-15 05:00:00"
author: "Jeremy Tammik"
categories:
  - ".NET"
  - "Automation"
  - "Events"
  - "Fun"
  - "Ribbon"
  - "User Interface"
original_url: "https://thebuildingcoder.typepad.com/blog/2012/11/happy-bhai-dooj.html "
typepad_basename: "happy-bhai-dooj"
typepad_status: "Publish"
---

<p>Happy

<a href="http://en.wikipedia.org/wiki/Bhau-beej">
Bhai Dooj</a> to you!</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017d3db20ff0970c-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017d3db20ff0970c" alt="Happy Bhai Dooj!" title="Happy Bhai Dooj!" src="/assets/image_a49a3e.jpg" border="0" /></a><br />

</center>

<p>This greeting comes from my colleague Sandeep Kumar in

<a href="http://en.wikipedia.org/wiki/Bangalore">Bangalore</a>, who explains:

<p>Bhai Dooj is a festival of prayers from sister to brother, brother's protection for her sister.
May we all celebrate this Bhai Dooj with even more love and protection for our sisters and brothers.
Best wishes on this Bhai Dooj.

<p>Thus fortified, let us turn to a Revit API issue, based on this excerpt from a useful little chat I had yesterday that might be of general use:</p>


<a name="2"></a>

<h4>Implementing a Single Command for Multiple Buttons</h4>

<p><strong>Question:</strong> My external application displays a large number of all kinds of ribbon items.
Now I thought I might implement one single handler class for all of the external commands these items can trigger, instead of creating a separate one for each.
I would like to create only one CommandHandler class  derived from IExternalCommand, and decide which function is required in its Execute method.
The problem I have is that the ExternalCommandData instance passed in does not provide any information about the source of the event, so there is no way of knowing which RibbonItem was clicked.
Is there a way to get that information, e.g. retrieve the name of the button clicked or something?


<p><strong>Answer:</strong> A good idea. It makes perfect sense. Unfortunately, no, this is not possible.

<p>At least, the Revit API does not give you this access.
You might possibly have more luck using .NET

<a href="http://thebuildingcoder.typepad.com/blog/automation">UI Automation</a>.

It probably provides

<a href="http://thebuildingcoder.typepad.com/blog/2011/01/subscribing-to-ui-automation-events.html">UI Automation events</a> that

can let you find out what button was clicked.</p>

<p>Using the standard Revit API functionality, you really do have to implement a separate command for each button.
You can easily funnel them all into the same handler yourself afterwards, if you like.
That would achieve almost what you want.
Implement a separate command for each button, determine which button was clicked, and then funnel all calls into the same command handler method.
The handler method can check the source of the event and branch out again appropriately.


<p><strong>Addendum:</strong> In his comment below, Rudolf Honke points to the very interesting discussion thread on

<a href="http://forums.autodesk.com/t5/Autodesk-Revit-API/Which-pushbutton-caused-the-ExternalCommand/td-p/3698908">
which pushbutton caused the ExternalCommand</a> where

<a href="http://forums.autodesk.com/t5/user/viewprofilepage/user-id/1103138">Revitalizer</a> and

<a href="http://forums.autodesk.com/t5/user/viewprofilepage/user-id/774564">ollikat</a> expound

on various alternative solutions, e.g. subscribing to the UIElementActivated event, creating commands at runtime and implementing one single base class inheriting IExternalCommand and other command classes derived from it.
