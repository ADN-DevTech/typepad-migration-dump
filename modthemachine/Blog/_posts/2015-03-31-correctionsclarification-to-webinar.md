---
layout: "post"
title: "Corrections/Clarification to Webinar"
date: "2015-03-31 17:40:02"
author: "Adam Nagy"
categories:
  - "Brian"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2015/03/correctionsclarification-to-webinar.html "
typepad_basename: "correctionsclarification-to-webinar"
typepad_status: "Publish"
---

<p>I recently posted about the <a href="http://modthemachine.typepad.com/my_weblog/2015/03/fusion-360s-api-webinar.html">Fusion API Webinar</a> and want to correct and clarify a few things I said during the presentation and in the questions after.</p>
<ol>
<li>When showing the slides comparing the JavaScript and Python code I mentioned that JavaScript is pickier about types and sometimes requires type casting.&#0160; This is not true.&#0160; There was an issue in a very early version of the API where it did require type casting but that was a Fusion API issue and not a JavaScript issue and has long since been addressed.&#0160; I’ve updated the PowerPoint slides to remove the casting line.</li>
<li>It was asked if you could begin a debug session into a running JavaScript add-in and replied that I didn’t believe it was possible.&#0160; I’ve since learned about a capability that I didn’t know about that does allow you to debug into a running JavaScript add-in or script.&#0160; From the <strong>Scripts and Add-Ins</strong> dialog you can see which scripts and add-ins are currently running by looking for the running symbol next to the name as I’ve highlighted below.&#0160; When you select a running JavaScript program, the Debug button will be enabled, as shown below.&#0160; This will open a window and allow you to begin debugging the running script. <br /> <br /><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b7c76f5f08970b-pi"><img alt="EditJavaScript" border="0" height="342" src="/assets/image_195843.jpg" style="background-image: none; padding-top: 0px; padding-left: 0px; display: inline; padding-right: 0px; border-width: 0px;" title="EditJavaScript" width="420" /></a></li>
<li>I also mentioned TkInter as a way to create custom dialogs when using Python but that there are currently problems when using it on a Mac.&#0160; This is true but I want to clarify that it’s not a Fusion 360 API issue in this case but is a <a href="https://www.python.org/download/mac/tcltk/">general problem with TkInter (Tcl/Tk specifically) on Mac</a>.&#0160; We’ve attempted to find a combination of libraries that works but so far have been unsuccessful.</li>
</ol>
<p>-Brian</p>
