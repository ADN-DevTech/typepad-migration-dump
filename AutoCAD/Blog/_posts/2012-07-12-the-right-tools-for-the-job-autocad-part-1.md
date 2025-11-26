---
layout: "post"
title: "The Right Tools for the Job &ndash; AutoCAD Part 1"
date: "2012-07-12 17:51:38"
author: "Fenton Webb"
categories:
  - ".NET"
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "LISP"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/the-right-tools-for-the-job-autocad-part-1.html "
typepad_basename: "the-right-tools-for-the-job-autocad-part-1"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>
<p>Recently, Stephen posted this great blog entry on <a href="http://adndevblog.typepad.com/autocad/2012/07/performance-perception-versus-reality.html"><strong>Performance – perception versus reality</strong></a><strong>&#0160;</strong>and then asked me if I could tell you all the tricks I have learnt over the years to produce the fastest, leanest AutoCAD code on the planet!! <img alt="Smile" class="wlEmoticon wlEmoticon-smile" src="/assets/image_480726.jpg" style="border-style: none;" />&#0160;</p>
<p>In fact, as Stephen mentioned in his post, I presented this exact topic at Autodesk University so I’ll present the topics inside of that presentation here in a few different blog entry parts.</p>
<p>So for Part 1, way before I start talking about the performance topic, let’s start with a quick look at which APIs are currently available inside of AutoCAD, along with what I consider to be their Pros and Cons…</p>
<p>First we have:</p>
<p><strong><a href="http://en.wikipedia.org/wiki/AutoLISP">AutoLISP</a>/<a href="http://usa.autodesk.com/adsk/servlet/index?siteID=123112&amp;id=770237">Visual LISP</a></strong>… For those that don’t know, LISP is a scripting language inside of AutoCAD</p>
<p><strong>Pros</strong></p>
<ul>
<li>Quick and Easy to write code with
<ul>
<li>There is an integrated Development IDE accessed via the “VLIDE” command</li>
</ul>
</li>
<li>Lots of Sample Code</li>
<li>Can Access all parts of the DWG file </li>
<li>Runs inside of Menus/CUI Commands
<ul>
<li>This is extremely useful in my opinion</li>
</ul>
</li>
<li>Integrated Supported Language
<ul>
<li>It’s not going away because the AutoCAD development team use LISP in their test harnesses</li>
</ul>
</li>
<li>Automatic memory management</li>
<li>Works same every year
<ul>
<li>Not affected by product changes and updates</li>
</ul>
</li>
<li>You can compile LISP modules
<ul>
<li>thus making your source code secure</li>
</ul>
</li>
</ul>
<p><strong>Cons</strong></p>
<ul>
<li>Limited ongoing investment from Autodesk 
<ul>
<li>We are focusing more on the newer languages these days as they provide the most bang for your buck</li>
</ul>
</li>
<li>Limited/Outdated UI
<ul>
<li>All we have is the aging DCL (Dialog Control Language)</li>
</ul>
</li>
<li>Not easy to read and/or maintain the code
<ul>
<li>obviously up for discussion, but in my opinion, it’s true.</li>
</ul>
</li>
<li>Not Object Oriented</li>
<li>Slow Performance
<ul>
<li>This is really task dependent. Some operations can be extremely fast though, for instance, creating objects (due to most of the work being done internally by AutoCAD)</li>
</ul>
</li>
<li>Limited Interoperability
<ul>
<li>I would say that LISP’s interoperability capabilities aren’t bad though, you can very easily interoperate with ObjectARX and .NET, and visa versa using the acedEvaluateLisp() function.</li>
</ul>
</li>
<li>Not a skill you can really use outside of the AutoCAD world</li>
</ul>
<p>Next we have:</p>
<p><strong><a href="http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&amp;id=12900036&amp;linkID=9240617">VBA</a>…&#0160; </strong>Microsoft VBA Macros inside of AutoCAD, similar to Outlook Macros</p>
<p><strong>Pros</strong></p>
<ul>
<li>Quick and Easy to write code with
<ul>
<li>Like LISP, there is an integrated Development IDE which can be accessed via the command “VBAIDE”</li>
</ul>
</li>
<li>Lots of Sample Code</li>
<li>Object Based</li>
<li>Easy and Nice UI APIs</li>
<li>Works same every year
<ul>
<li>Generally, not affected by product changes and updates</li>

</ul>
</li>
<li>Code Saved with DWG file</li>
<li>Automatic Memory Management</li>
<li>VBA skills are recognized outside of Autodesk</li>
</ul>
<p><strong>Cons</strong></p>
<ul>
<li>Limited ongoing investment from Autodesk</li>
<li>Slow Performance
<ul>
<li>task dependent, of course</li>
</ul>
</li>
<li>Bad Source code security 
<ul>
<li>password encrypted macros can easily be hacked</li>
</ul>
</li>
<li>64bit version runs Out of Process
<ul>
<li>Tricky and extremely slow</li>
</ul>
</li>
<li>Limited Access to AutoCAD</li>
<li>No built in Interoperability </li>
</ul>
<p>Now onto:</p>
<p><a href="http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&amp;id=12900036&amp;linkID=9240617"><strong>ObjectARX</strong></a><strong> – </strong>our awesome C++ API</p>
<p><strong>Pros</strong></p>
<ul>
<li>Totally Interoperable!
<ul>
<li>Mix and match APIs </li>
<li>Not easy to implement though</li>
</ul>
</li>
<li>Lots of sample code</li>
<li>Object Oriented</li>
<li>Very powerful
<ul>
<li>the most powerful API we have</li>
</ul>
</li>
<li>Low level
<ul>
<li>gain access to almost anything going on inside of AutoCAD</li>
</ul>
</li>
<li>Fastest API we have</li>
<li>Secure
<ul>
<li>Compiled machine code</li>
</ul>
</li>
<li>Advanced technology features
<ul>
<li>still being updated by the likes of Microsoft</li>
</ul>
</li>
<li>Full Access to AutoCAD</li>
<li>Powerful UI API capabilities</li>
<li>Recognized inside of AutoCAD world as a top notch programming skill</li>
<li>Recognized outside of AutoCAD world too</li>
</ul>
<p><strong>Cons</strong></p>
<ul>
<li>Pro software engineers needed to write it
<ul>
<li>Expensive!</li>
</ul>
</li>
<li>No built in Memory Management </li>
<li>UI capabilities are powerful, but error prone and difficult</li>
<li>Powerful, Low level – is this really an advantage???
<ul>
<li>Its power can be its downfall</li>
</ul>
</li>
<li>Tied to platform 32bit and 64bit</li>
<li>Binary break every 3 years
<ul>
<li>Is affected by product changes and updates</li>
</ul>
</li>
</ul>
<p><strong>AutoCAD <a href="http://usa.autodesk.com/adsk/servlet/ps/dl/item?siteID=123112&amp;id=12900036&amp;linkID=9240617">.NET</a></strong> <strong>– </strong>our even more awesome AutoCAD .NET API</p>
<p><strong>Pros</strong></p>
<ul>
<li>Quick and Easy to write code with
<ul>
<li>VB.NET provides an easy way to <a href="http://download.autodesk.com/media/adn/DevTV_VBA_Migration/english/DevTV_VBA_To_VBdotNet_Migration_English.html">port over from VBA</a></li>
</ul>
</li>
<li>Easy and Nice UI APIs
<ul>
<li>Built into the latest Visual Studio tools</li>
</ul>
</li>
<li>Totally interoperable
<ul>
<li>and easy with it</li>
</ul>
</li>
<li>Lots of sample code</li>
<li>Not tied to platform
<ul>
<li>as long as you steer away from ObjectARX and COM interoperability</li>
</ul>
</li>
<li>Object Oriented</li>
<li>Very powerful</li>
<li>Low Level, but at the same time High level!</li>
<li>Very Fast
<ul>
<li>but not compared with ObjectARX</li>
</ul>
</li>
<li>Latest and most Advanced programming language on Windows
<ul>
<li>New features being added all the time</li>
</ul>
</li>
<li>Automatic memory management</li>
<li>Recognized inside of AutoCAD world as a very good programming skill</li>
<li>Recognized outside of AutoCAD world too</li>
</ul>
<p><strong>Cons</strong></p>
<ul>
<li>Code Security can be an issue, be careful</li>
<li>Difficult for LISP and VBA people to migrate to pure .NET
<ul>
<li>Using VB.NET via COM Interop makes life easy though</li>
</ul>
</li>
<li>Cannot unload DLLs very easily while AutoCAD is running</li>
</ul>
<p>Read <a href="http://adndevblog.typepad.com/autocad/2012/07/the-right-tools-for-the-job-autocad-part-2.html">The Right Tools for the Job - Part 2</a></p>
