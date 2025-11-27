---
layout: "post"
title: "Creating an associative fillet operation using ObjectARX"
date: "2014-12-15 09:30:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "Custom objects"
  - "Notification / Events"
  - "ObjectARX"
original_url: "https://www.keanw.com/2014/12/creating-an-associative-fillet-operation-using-objectarx.html "
typepad_basename: "creating-an-associative-fillet-operation-using-objectarx"
typepad_status: "Publish"
---

<p>This is really cool. Fellow architect on the AutoCAD team, Jiri Kripac – who originally wrote AutoCAD’s “AModeler” facet modeler and is the driving force behind AutoCAD’s Associative Framework – has written a really interesting ObjectARX sample to perform an associative fillet between two curves. Given Jiri’s background, this is as close to a canonical sample for implementing an operation using the Associative Framework – in this case by creating a custom AcDbAssocActionBody – as you’re likely to find.</p>
<p>Here’s a video showing this custom fillet in action, and how it can be used with parameters and expressions to do some really impressive things (for instance, Jiri demonstrates basing an extruded surface on curves linked via an associative fillet… really nice).</p>
<p><br /><iframe allowfullscreen="allowfullscreen" frameborder="0" height="264" src="//www.youtube.com/embed/6Czfaz6AXt4?rel=0&amp;showinfo=0" width="470"></iframe> <br /> </p>
<p>Jiri <a href="https://events.au.autodesk.com/connect/sessionDetail.ww?SESSION_ID=5217" target="_blank">presented this implementation at AU 2014</a> and has also <a href="https://github.com/ADN-DevTech/Associative-Fillet-sample-application" target="_blank">posted all his code to GitHub</a>.</p>
<p>When building the app, be sure to follow Jiri’s instructions in <a href="https://github.com/ADN-DevTech/Associative-Fillet-sample-application/blob/master/README.md" target="_blank">the ReadMe</a>: you’ll need to place the code in the <em>samples\entity</em> folder of the ObjectARX SDK for AutoCAD 2015 and then build it using Visual Studio 2012 (or at least the v110 “platform toolset” build tools, allowing you to use the VS 2013 IDE with the 2012 compiler). You will need to load the <em>.vcxproj</em> – no .sln is provided – and do remember to set the build configuration appropriately (the default is Win32 rather than x64, and this often isn’t directly obvious through the Visual Studio UI).</p>
<p>Many thanks to Jiri for providing this excellent sample!</p>
