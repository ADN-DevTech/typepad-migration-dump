---
layout: "post"
title: "Unpublished AutoCAD 2009 APIs to improve application, OS and system performance"
date: "2008-04-01 11:58:39"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "ObjectARX"
original_url: "https://www.keanw.com/2008/04/unpublished-aut.html "
typepad_basename: "unpublished-aut"
typepad_status: "Publish"
---

<p>I stumbled across some new ObjectARX APIs that have been exposed from acad.exe in AutoCAD 2009.</p>

<p>These functions are unpublished - just declare the functions in your ObjectARX project, and call them from your application initialization routine. I think you'll find the results surprising!</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: courier new"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">extern</span> <span style="COLOR: blue">fool</span> acedMakeAutoCADMuchQuicker(<span style="COLOR: blue">bool</span> enable);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">extern</span> <span style="COLOR: blue">fool</span> acedPreventWindowsBlueScreen(<span style="COLOR: blue">bool</span> enable);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">extern</span> <span style="COLOR: blue">fool</span> acedDoubleSystemClockSpeed(<span style="COLOR: blue">bool</span> enable);</p></div><p>OK, before anyone actually wastes time on this, happy <a href="http://en.wikipedia.org/wiki/April_Fools%27_Day">April Fool's Day</a>. :-)</p>
