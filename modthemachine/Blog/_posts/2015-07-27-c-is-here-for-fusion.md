---
layout: "post"
title: "C++ is Here for Fusion"
date: "2015-07-27 22:42:15"
author: "Adam Nagy"
categories:
  - "Announcements"
  - "Brian"
  - "Fusion 360"
original_url: "https://modthemachine.typepad.com/my_weblog/2015/07/c-is-here-for-fusion.html "
typepad_basename: "c-is-here-for-fusion"
typepad_status: "Publish"
---

<p>The latest update for Fusion went out this past weekend so if you’ve run Fusion since then you have it.&#0160; You’ll now see a new “C++” option when you create a new script or add-in, as shown below.</p>
<p><a href="http://modthemachine.typepad.com/.a/6a00e553fcbfc6883401b8d13e54a5970c-pi"><img alt="FusionC  " border="0" height="401" src="/assets/image_534047.jpg" style="background-image: none; float: none; padding-top: 0px; padding-left: 0px; margin-left: auto; display: block; padding-right: 0px; margin-right: auto; border: 0px;" title="FusionC  " width="350" /></a></p>
<p>That will create a new C++ project.&#0160; A C++ project consists of a .manifest file, (the same as Python and JavaScript), but also a .cpp file and two project files; a .vcxproj file for Windows and Visual Studio and a .xcodeproj file for Mac and XCode.</p>
<p>The API is the same as what’s been exposed for Python and JavaScript.&#0160; In fact the Python and JavaScript interfaces are layers on top of the C++ interface.&#0160; Each language has it’s own quirks so the API is used a bit differently for each one but it is the same API. You can read all about the C++ quirks in the <a href="http://fusion360.autodesk.com/learning/learning.html?guid=GUID-ECC0A398-4D89-4776-A054-F7B432F7FCF6">C++ Specific Issues</a> topic in the <strong>Fusion 360 API User’s Manual.</strong></p>
<p>Some highlights of the C++ API are:</p>
<ul>
<li>You can now deliver your applications as a binary file instead of source code.</li>
<li>You now have a typed language which means you’ll be able to user reliable Intellisense (code hints) when writing your program.&#0160; This is a huge benefit and means you’ll spend a lot less time in the documentation looking to see what methods and properties a certain object supports and what the arguments are for a particular function.</li>
<li>Performance is much better than Python and JavaScript.</li>
<li>You get to use a development environment you’re already familiar with.</li>
</ul>
<p>Probably the biggest downside is that because you’re delivering the runtime binary, you have to compile your code separately for Windows and Mac.&#0160; Except in cases where you’re using Windows or Mac specific libraries you should be able to have a single set of source but you’ll need to compile it on Windows to create the Windows compatible .dll file and then also compile it on a Mac to create a Mac compatible .dylib file.</p>
<p>You can post any comments (positive or negative) or problems you have to the <a href="http://forums.autodesk.com/t5/api-and-scripts/bd-p/22">Fusion 360 API forum</a>, where I’ll be watching.</p>
<p>-Brian</p>
