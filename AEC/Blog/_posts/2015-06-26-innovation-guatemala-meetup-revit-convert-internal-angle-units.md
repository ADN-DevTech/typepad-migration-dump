---
layout: "post"
title: "Innovation Guatemala Meetup, Revit convert internal angle units."
date: "2015-06-26 14:49:05"
author: "Jaime Rosales"
categories:
  - ".NET"
  - "Cloud"
  - "Events"
  - "Jaime Rosales"
  - "Revit"
  - "Revit Architecture"
  - "Revit MEP"
  - "Revit Structure"
original_url: "https://adndevblog.typepad.com/aec/2015/06/innovation-guatemala-meetup-revit-convert-internal-angle-units.html "
typepad_basename: "innovation-guatemala-meetup-revit-convert-internal-angle-units"
typepad_status: "Publish"
---

<p><a href="http://adndevblog.typepad.com/aec/jaime-rosales.html">By Jaime Rosales</a> (@<a href="https://twitter.com/AfroJme">afrojme</a>)</p>  <p>On Tuesday the 23rd I had the opportunity to present to an incredible variety of attendees at, a Meetup group called Innovation Guatemala, where different topics of Technology Innovation are discussed. I spent a couple of weeks talking to the organizers and had the chance to become the main speaker for that day, and show a bunch of Architects, Developers, Civil Engineers and Designers who code, what the Autodesk View and Data API is, and how cool would it be to start thinking in 3D for their mobile and web applications. The most interesting part was, even that I’m a native Spanish speaker, to give the presentation in Spanish was a bit challenging but they had such a great response to the technology I shared with them that night.</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d12eb730970c-pi"><img title="image" style="border-left-width: 0px; border-right-width: 0px; border-bottom-width: 0px; display: inline; border-top-width: 0px" border="0" alt="image" src="/assets/image_79576.jpg" width="480" height="360" /></a> </p>  <p>I started the presentation showing them who Autodesk was, since people don’t tend to be aware of all the great involvement Autodesk has in today’s technological industry. Their attention raised when I started showing what the capabilities the LMV (large model viewer) currently has. I showed and talked about the different uses of the API, from VR samples, Construction web applications, to real time collaboration using some of the attendees mobile devices so they can interact with it.</p>  <p><a href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01b8d12eb739970c-pi"><img title="image" style="border-left-width: 0px; border-right-width: 0px; border-bottom-width: 0px; display: inline; border-top-width: 0px" border="0" alt="image" src="/assets/image_632735.jpg" width="480" height="360" /></a>&#160;</p>  <p>Filled of questions, the night went by so fast, after a talk that got carried away for 1 hour and a half, the networking with the different attendees started and after sharing a drink and some food the event was a wrap and a total success. Many thanks to the organizers of the Innovation Guatemala Meetup for making this possible. </p>  <p>Back to Revit API, I had a chance to work on a case a couple of weeks back and I wanted to share with you guys the solution for the developer.</p>  <p><strong>Question</strong>: I'm trying to use the ConvertToInternalUnits() method in the Revit 2015 API to convert angles in degrees to internal units, which I assume are radians. </p>  <pre style="font-size: 13px; font-family: consolas; background: #1e1e1e; color: gainsboro"><span style="color: #569cd6">double</span>&#160;<span style="color: white">degAngle</span>&#160;<span style="color: #b4b4b4">=</span>&#160;<span style="color: #b5cea8">45</span>;<br /><span style="color: #b8d7a3">UnitType</span>&#160;<span style="color: white">uType</span>&#160;<span style="color: #b4b4b4">=</span>&#160;<span style="color: #b8d7a3">UnitType</span><span style="color: #b4b4b4">.</span><span style="color: white">UT_Angle</span>;<br /><span style="color: #569cd6">double</span>&#160;<span style="color: white">intAngle</span>&#160;<span style="color: #b4b4b4">=</span>&#160;<span style="color: white">ConvertToInternalUnits</span>(<span style="color: white">doc</span>, <span style="color: white">degAngle</span>, <span style="color: white">uType</span>);<br />&#160;<br /></pre>

<p>When I run the code above intAngle is set to 0.14763779527559054, but converting 45 degrees to radians should turn into 0.785398163. Where is this 0.14 number coming from? </p>

<p><strong>Answer</strong>:Thank you for your query, For obtaining the Radians something like this will work better. </p>

<pre style="font-size: 13px; font-family: consolas; background: #1e1e1e; color: gainsboro"><span style="color: #569cd6">double</span>&#160;<span style="color: white">degAngle</span>&#160;<span style="color: #b4b4b4">=</span>&#160;<span style="color: #b5cea8">45</span>;<br /><span style="color: #b8d7a3">DisplayUnitType</span>&#160;<span style="color: white">uType</span>&#160;<span style="color: #b4b4b4">=</span>&#160;<span style="color: #b8d7a3">DisplayUnitType</span><span style="color: #b4b4b4">.</span><span style="color: white">DUT_DECIMAL_DEGREES</span>;<br /><span style="color: #569cd6">double</span>&#160;<span style="color: white">intAngle</span>&#160;<span style="color: #b4b4b4">=</span>&#160;<span style="color: #4ec9b0">UnitUtils</span><span style="color: #b4b4b4">.</span><span style="color: white">ConvertToInternalUnits</span>(<span style="color: white">degAngle</span>, <span style="color: white">uType</span>);</pre>

<p>Let me know how it goes.</p>

<p><strong>Response</strong>: Thanks a lot, that works perfect.</p>

<p>Thank for reading guys and until next time. </p>
