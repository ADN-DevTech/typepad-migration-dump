---
layout: "post"
title: "Using AJAX to generate pages hosting Freewheel views"
date: "2007-11-09 17:16:55"
author: "Kean Walmsley"
categories:
  - "AJAX"
  - "DWF"
  - "Freewheel"
  - "HTML"
  - "JavaScript"
  - "SaaS"
original_url: "https://www.keanw.com/2007/11/using-client-si.html "
typepad_basename: "using-client-si"
typepad_status: "Publish"
---

<p>I touched on this subject a few weeks ago in <a href="http://through-the-interface.typepad.com/through_the_interface/2007/10/au-handouts-e-1.html">part 2 of my DWF-related AU handout</a>, but thought I'd come back and describe in more depth some of the fun (although a more accurate word might be &quot;difficulty&quot; :-) I had solving this problem.</p>

<p>I've been playing around with web services and XML since SaaS was still known as ASP, and have tried to stay up-to-date with the technology as best I can. I'm really a client-side programmer, all things considered: I've created some server-side code, but have mostly involved myself with desktop-oriented programming and creation of samples that developers can execute locally, rather than having to set up a host environment.</p>

<p>So when working with web services, I really want to provide a simple application that can be run locally and connects to these services via simple, understandable client code.</p>

<p>The problem I wanted to solve with my AU demo was fairly simple (at least so I thought): for a particular DWF file I wanted to create a page linking to - and embedding - Freewheel views of the DWF's various sheets. The logical way to do this was to use the <a href="http://freewheel.autodesk.com/developers.aspx">DWFRender web service</a> to query the number of sheets contained in the DWF, and then create a page using DHTML that had some content for each of these sheets.</p>

<p>The logic seemed sound, and I created a nice prototype with hardcoded sections for the two sheets of my DWF file. Here's the HTML code:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">html</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: green">&lt;!-- saved from url=(0017)http://localhost/ --&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">head</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">title</span><span style="COLOR: blue">&gt;</span>Freewheel<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">title</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">style</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">legend</span>&nbsp; &nbsp;&nbsp; &nbsp;{ <span style="COLOR: red">font-size</span>: <span style="COLOR: blue">10pt</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: red">font-family</span>: <span style="COLOR: blue">Calibri,</span> <span style="COLOR: blue">Arial</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: red">color</span>: <span style="COLOR: blue">black</span>; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">button</span>&nbsp; &nbsp;&nbsp; &nbsp;{ <span style="COLOR: red">font-size</span>: <span style="COLOR: blue">10pt</span>; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">input</span>&nbsp; &nbsp;&nbsp; &nbsp;{ <span style="COLOR: red">font-size</span>: <span style="COLOR: blue">10pt</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: red">font-family</span>: <span style="COLOR: blue">Calibri,</span> <span style="COLOR: blue">Arial</span>; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">style</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">head</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">body</span> <span style="COLOR: red">bgcolor</span><span style="COLOR: blue">=&quot;#E6E6E6&quot;&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">table</span> <span style="COLOR: red">width</span><span style="COLOR: blue">=&quot;620&quot;&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">tr</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">td</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">fieldset</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">legend</span><span style="COLOR: blue">&gt;</span>Sheet 1<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">legend</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">table</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">tr</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">td</span> <span style="COLOR: red">valign</span><span style="COLOR: blue">=&quot;middle&quot;&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">fieldset</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">legend</span><span style="COLOR: blue">&gt;</span>Clickable Thumbnail<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">legend</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">a</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: red">href</span><span style="COLOR: blue">=&quot;http://freewheel.labs.autodesk.com/dwf.aspx?sec=1&amp;dwf=http://through-the-interface.typepad.com/presentations/DWF/solids.dwf&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: red">target</span><span style="COLOR: blue">=&quot;New&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">img</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: red">frameborder</span><span style="COLOR: blue">=&quot;0&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: red">height</span><span style="COLOR: blue">=&quot;150&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: red">width</span><span style="COLOR: blue">=&quot;200&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: red">scrolling</span><span style="COLOR: blue">=&quot;no&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: red">src</span><span style="COLOR: blue">=&quot;http://freewheel.labs.autodesk.com/dwfImage.aspx?page=1&amp;width=200&amp;height=150&amp;path=http://through-the-interface.typepad.com/presentations/DWF/solids.dwf&quot;</span> /</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">a</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">fieldset</span><span style="COLOR: blue">&gt;</span>';</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">td</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">td</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">iframe</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: red">frameborder</span><span style="COLOR: blue">=&quot;0&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: red">height</span><span style="COLOR: blue">=&quot;300&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: red">width</span><span style="COLOR: blue">=&quot;400&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: red">scrolling</span><span style="COLOR: blue">=&quot;no&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: red">src</span><span style="COLOR: blue">=&quot;http://freewheel.labs.autodesk.com/dwf.aspx?sec=1&amp;dwf=http://through-the-interface.typepad.com/presentations/DWF/solids.dwf&quot;&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">iframe</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">td</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">tr</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">table</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">fieldset</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">td</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">tr</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">table</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">table</span> <span style="COLOR: red">width</span><span style="COLOR: blue">=&quot;620&quot;&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">tr</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">td</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">fieldset</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">legend</span><span style="COLOR: blue">&gt;</span>Sheet 2<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">legend</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">table</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">tr</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">td</span> <span style="COLOR: red">valign</span><span style="COLOR: blue">=&quot;middle&quot;&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">fieldset</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">legend</span><span style="COLOR: blue">&gt;</span>Clickable Thumbnail<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">legend</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">a</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: red">href</span><span style="COLOR: blue">=&quot;http://freewheel.labs.autodesk.com/dwf.aspx?sec=2&amp;dwf=http://through-the-interface.typepad.com/presentations/DWF/solids.dwf&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: red">target</span><span style="COLOR: blue">=&quot;New&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">img</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: red">frameborder</span><span style="COLOR: blue">=&quot;0&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: red">height</span><span style="COLOR: blue">=&quot;150&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: red">width</span><span style="COLOR: blue">=&quot;200&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: red">scrolling</span><span style="COLOR: blue">=&quot;no&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: red">src</span><span style="COLOR: blue">=&quot;http://freewheel.labs.autodesk.com/dwfImage.aspx?page=2&amp;width=200&amp;height=150&amp;path=http://through-the-interface.typepad.com/presentations/DWF/solids.dwf&quot;/&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">a</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">fieldset</span><span style="COLOR: blue">&gt;</span>';</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">td</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">td</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">iframe</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: red">frameborder</span><span style="COLOR: blue">=&quot;0&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: red">height</span><span style="COLOR: blue">=&quot;300&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: red">width</span><span style="COLOR: blue">=&quot;400&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: red">scrolling</span><span style="COLOR: blue">=&quot;no&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: red">src</span><span style="COLOR: blue">=&quot;http://freewheel.labs.autodesk.com/dwf.aspx?sec=2&amp;dwf=http://through-the-interface.typepad.com/presentations/DWF/solids.dwf&quot;&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">iframe</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">td</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">tr</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">table</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">fieldset</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">td</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">tr</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">table</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">body</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">html</span><span style="COLOR: blue">&gt;</span></p></div>

<p>And here's what it looked like when displayed:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=722,height=833,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/30/embedding_and_linking_freewheel.png"><img title="Embedding_and_linking_freewheel" height="346" alt="Embedding_and_linking_freewheel" src="/assets/embedding_and_linking_freewheel.png" width="300" border="0" /></a> </p>

<p>So far, so good.</p>

<p>To generate the DHTML code for each of the sections, which could then be called in a loop once we knew the number of sheets, was also pretty straightforward:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">var</span> freewheel =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: maroon">'http://freewheel.labs.autodesk.com'</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">function</span> createViews(sheets)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">var</span> container = document.getElementById(<span style="COLOR: maroon">'views'</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">var</span> htm = <span style="COLOR: maroon">'&lt;table width=&quot;620&quot;&gt;&lt;tr&gt;&lt;td&gt;'</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">for</span>(i=1; i&lt;= sheets; i++)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; htm += <span style="COLOR: maroon">'&lt;fieldset&gt;&lt;legend&gt;Sheet '</span> + i + <span style="COLOR: maroon">'&lt;/legend&gt;'</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; htm += <span style="COLOR: maroon">'&lt;table&gt;&lt;tr&gt;&lt;td valign=&quot;middle&quot;&gt;'</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; htm += <span style="COLOR: maroon">'&lt;fieldset&gt;&lt;legend&gt;Clickable Thumbnail&lt;/legend&gt;'</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; htm += <span style="COLOR: maroon">'&lt;a href=&quot;'</span> + freewheel + <span style="COLOR: maroon">'/dwf.aspx?'</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; htm += <span style="COLOR: maroon">'sec='</span> + i + <span style="COLOR: maroon">'&amp;dwf='</span> + path + <span style="COLOR: maroon">'&quot; target=&quot;New&quot;&gt;'</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; htm += <span style="COLOR: maroon">'&lt;img frameborder=&quot;0&quot; height=&quot;150&quot; '</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; htm += <span style="COLOR: maroon">'width=&quot;200&quot; scrolling=&quot;no&quot; src=&quot;'</span> + freewheel;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; htm += <span style="COLOR: maroon">'/dwfImage.aspx?page='</span> + i + <span style="COLOR: maroon">'&amp;'</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; htm += <span style="COLOR: maroon">'width=200&amp;height=150&amp;path='</span> + path + <span style="COLOR: maroon">'&quot;/&gt;'</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; htm += <span style="COLOR: maroon">'&lt;/a&gt;&lt;/fieldset&gt;'</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; htm += <span style="COLOR: maroon">'&lt;/td&gt;&lt;td&gt;'</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; htm += <span style="COLOR: maroon">'&lt;iframe frameborder=&quot;0&quot; height=&quot;300&quot; '</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; htm += <span style="COLOR: maroon">'width=&quot;400&quot; scrolling=&quot;no&quot; src=&quot;'</span> + freewheel;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; htm += <span style="COLOR: maroon">'/dwf.aspx?sec='</span> + i + <span style="COLOR: maroon">'&amp;dwf='</span> + path + <span style="COLOR: maroon">'&quot;&gt;'</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; htm += <span style="COLOR: maroon">'&lt;/iframe&gt;'</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; htm += <span style="COLOR: maroon">'&lt;/td&gt;&lt;/tr&gt;'</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; htm += <span style="COLOR: maroon">'&lt;/table&gt;&lt;/fieldset&gt;'</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; htm += <span style="COLOR: maroon">'&lt;/td&gt;&lt;/tr&gt;&lt;table&gt;'</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; container.innerHTML = htm;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p>The only challenge remaining was to call our DWFRender web service from out JavaScript code.</p>

<p>Back in the old days I used a component from Microsoft to call web services from HTML &amp; JavaScript, called the <a href="http://msdn2.microsoft.com/en-us/library/ms531035.aspx">Web Service Behavior</a>, or webservice.htc. This seemed to work fine, but is no longer supported, so I thought I'd find a more modern approach, something from the AJAX bandwagon, perhaps. AJAX is &quot;Asynchronous JavaScript and XML&quot;, and is the latest &amp; greatest technology for developing smart yet thin applications. Or so the buzz goes.</p>

<p>After searching around a while, I ended up trying a couple of approaches. The first was based a <a href="http://www.ibm.com/developerworks/webservices/library/ws-wsajax/">technique provided by IBM</a>. It took me a while to get the code to run, and even then it wasn't getting the result I wanted. So I implemented another approach using a technique shown at <a href="http://www.codeproject.com/Ajax/JavaScriptSOAPClient.asp">The Code Project</a>.</p>

<p>Once again it didn't return anything helpful, but then I noticed this statement on the page:</p><blockquote dir="ltr"><p><em>Please note that many browsers do not allow cross-domain calls for security reasons.</em></p></blockquote><p>I researched this for some time, and realised that my JavaScript code was hitting a browser security problem. It seems that cross-domain SOAP requests are an issue: if your web-page is considered to be in one domain, it will not call out across to a web service hosted on another domain. Even the Web Service Behavior I'd used in the past didn't solve the problem (thinking back I'd been calling web services hosted in the same domain, so it hadn't been an issue I'd come across).</p>

<p>The security problem is actually with the XMLHTTP component, which is at the core of AJAX and is now implemented by various (probably all, but I'm no expert) browsers.</p>

<p>There are a few ways around this limitation: it seems that most commonly developers implement server-based code that calls across to the problematic domain, basically provided a local cache of the results. In terms of clarifying the problem and spelling out the alternatives, I found <a href="http://fettig.net/weblog/2005/11/28/how-to-make-xmlhttprequest-connections-to-another-server-in-your-domain/">this site</a> extremely helpful, as well as <a href="http://fettig.net/weblog/2005/11/30/xmlhttprequest-subdomain-update/">this follow-up post</a>.</p>

<p>Anyway, it turns out that for the purposes of my demo I could cobble something that worked together based on fairly simply client-side code. I didn't go to the effort of implementing a cross-browser solution, as the main purpose is to demonstrate calling the Freewheel web service and not to solve the problem of World (well, Internet) Peace.</p>

<p>There were two points that I hadn't realised were issues. The first was the <a href="http://msdn2.microsoft.com/en-us/library/ms537628.aspx">Mark of the Web (MOTW)</a>. I had added this to my HTML header sometime in the past, to gain Internet Exporer's trust for it not to bother me with requests to approve the running of ActiveX Controls:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: green">&lt;!-- saved from url=(0017)http://localhost/ --&gt;</span></p></div>

<p>Removing this was part of the trick - the other part was to adjust my browser settings to allow cross-domain access:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=417,height=479,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/11/09/ie_security.png"><img title="Ie_security" height="344" alt="Ie_security" src="/assets/ie_security.png" width="299" border="0" /></a> </p>

<p>That then allowed my client code to run and actually get results. Here's the HTML code for the page:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">html</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">head</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">title</span><span style="COLOR: blue">&gt;</span>Freewheel<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">title</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">style</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">legend</span>&nbsp; &nbsp;&nbsp; &nbsp;{ <span style="COLOR: red">font-size</span>: <span style="COLOR: blue">10pt</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: red">font-family</span>: <span style="COLOR: blue">Calibri,</span> <span style="COLOR: blue">Arial</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: red">color</span>: <span style="COLOR: blue">black</span>; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">button</span>&nbsp; &nbsp;&nbsp; &nbsp;{ <span style="COLOR: red">font-size</span>: <span style="COLOR: blue">10pt</span>; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: maroon">input</span>&nbsp; &nbsp;&nbsp; &nbsp;{ <span style="COLOR: red">font-size</span>: <span style="COLOR: blue">10pt</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: red">font-family</span>: <span style="COLOR: blue">Calibri,</span> <span style="COLOR: blue">Arial</span>; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">style</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">script</span> <span style="COLOR: red">type</span><span style="COLOR: blue">=&quot;text/javascript&quot;&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">var</span> freewheel =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: maroon">'http://freewheel.labs.autodesk.com'</span>;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">var</span> http_request = <span style="COLOR: blue">false</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">var</span> xmldoc;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">var</span> path;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">function</span> makeRequest(url)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; http_request =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">new</span> ActiveXObject(<span style="COLOR: maroon">&quot;Msxml2.XMLHTTP&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">catch</span> (e)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">try</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;http_request =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">new</span> ActiveXObject(<span style="COLOR: maroon">&quot;Microsoft.XMLHTTP&quot;</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">catch</span> (e)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; { }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (http_request)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; http_request.onreadystatechange = alertContents;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; http_request.open(<span style="COLOR: maroon">'GET'</span>, url, <span style="COLOR: blue">true</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; http_request.send(<span style="COLOR: blue">null</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">function</span> alertContents()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">if</span> (http_request.readyState == 4)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">if</span> (http_request.status == 200)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">var</span> string = http_request.responseText;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">var</span> xmldoc;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;xmldoc = http_request.responseXML;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">var</span> secs =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; xmldoc.getElementsByTagName(</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: maroon">&quot;unsignedInt&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; )[0].firstChild.nodeValue;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;createViews(secs);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; <span style="COLOR: blue">else</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;alert(<span style="COLOR: maroon">'There was a problem with the request.'</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">function</span> replaceAll(strText, strTarget, strSubString)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">var</span> intIndexOfMatch = strText.indexOf( strTarget );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">while</span> (intIndexOfMatch != -1)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; strText = strText.replace( strTarget, strSubString )</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; intIndexOfMatch = strText.indexOf( strTarget );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">return</span>( strText );</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">function</span> createViews(sheets)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">var</span> container = document.getElementById(<span style="COLOR: maroon">'views'</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">var</span> htm = <span style="COLOR: maroon">'&lt;table width=&quot;620&quot;&gt;&lt;tr&gt;&lt;td&gt;'</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">for</span>(i=1; i&lt;= sheets; i++)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; {</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; htm += <span style="COLOR: maroon">'&lt;fieldset&gt;&lt;legend&gt;Sheet '</span> + i + <span style="COLOR: maroon">'&lt;/legend&gt;'</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; htm += <span style="COLOR: maroon">'&lt;table&gt;&lt;tr&gt;&lt;td valign=&quot;middle&quot;&gt;'</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; htm += <span style="COLOR: maroon">'&lt;fieldset&gt;&lt;legend&gt;Clickable Thumbnail&lt;/legend&gt;'</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; htm += <span style="COLOR: maroon">'&lt;a href=&quot;'</span> + freewheel + <span style="COLOR: maroon">'/dwf.aspx?'</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; htm += <span style="COLOR: maroon">'sec='</span> + i + <span style="COLOR: maroon">'&amp;dwf='</span> + path + <span style="COLOR: maroon">'&quot; target=&quot;New&quot;&gt;'</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; htm += <span style="COLOR: maroon">'&lt;img frameborder=&quot;0&quot; height=&quot;150&quot; '</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; htm += <span style="COLOR: maroon">'width=&quot;200&quot; scrolling=&quot;no&quot; src=&quot;'</span> + freewheel;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; htm += <span style="COLOR: maroon">'/dwfImage.aspx?page='</span> + i + <span style="COLOR: maroon">'&amp;'</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; htm += <span style="COLOR: maroon">'width=200&amp;height=150&amp;path='</span> + path + <span style="COLOR: maroon">'&quot;/&gt;'</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; htm += <span style="COLOR: maroon">'&lt;/a&gt;&lt;/fieldset&gt;'</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; htm += <span style="COLOR: maroon">'&lt;/td&gt;&lt;td&gt;'</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; htm += <span style="COLOR: maroon">'&lt;iframe frameborder=&quot;0&quot; height=&quot;300&quot; '</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; htm += <span style="COLOR: maroon">'width=&quot;400&quot; scrolling=&quot;no&quot; src=&quot;'</span> + freewheel;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; htm += <span style="COLOR: maroon">'/dwf.aspx?sec='</span> + i + <span style="COLOR: maroon">'&amp;dwf='</span> + path + <span style="COLOR: maroon">'&quot;&gt;'</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; htm += <span style="COLOR: maroon">'&lt;/iframe&gt;'</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; htm += <span style="COLOR: maroon">'&lt;/td&gt;&lt;/tr&gt;'</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp; htm += <span style="COLOR: maroon">'&lt;/table&gt;&lt;/fieldset&gt;'</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; }</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; htm += <span style="COLOR: maroon">'&lt;/td&gt;&lt;/tr&gt;&lt;table&gt;'</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; container.innerHTML = htm;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: blue">function</span> OnGenerateSheetViews()</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; path = document.getElementById(<span style="COLOR: maroon">'URL'</span>).value;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">var</span> url = freewheel + <span style="COLOR: maroon">'/dwfrender.asmx/sectionCount?path='</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; <span style="COLOR: blue">var</span> dwf = path;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; dwf = replaceAll(dwf, <span style="COLOR: maroon">'/'</span>, <span style="COLOR: maroon">'%2F'</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; dwf = replaceAll(dwf, <span style="COLOR: maroon">':'</span>, <span style="COLOR: maroon">'%3A'</span>);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; url += dwf;&nbsp; &nbsp;&nbsp; &nbsp;</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; makeRequest(url);</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;}</p><br /><p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">script</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">head</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">body</span> <span style="COLOR: red">bgcolor</span><span style="COLOR: blue">=&quot;#E6E6E6&quot;&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">input</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: red">type</span><span style="COLOR: blue">=&quot;text&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: red">id</span><span style="COLOR: blue">=&quot;URL&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: red">size</span><span style="COLOR: blue">=&quot;60&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: red">value</span><span style="COLOR: blue">=&quot;http://through-the-interface.typepad.com/presentations/DWF/solids.dwf&quot;</span> /</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">input</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: red">id</span><span style="COLOR: blue">=&quot;GenerateSheetViews&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: red">type</span><span style="COLOR: blue">=&quot;button&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: red">value</span><span style="COLOR: blue">=&quot;Generate Sheet Views&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp;&nbsp; &nbsp;<span style="COLOR: red">onclick</span><span style="COLOR: blue">=&quot;return OnGenerateSheetViews()&quot;</span> /</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; &nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">div</span> <span style="COLOR: red">id</span><span style="COLOR: blue">=&quot;views&quot;&gt;&lt;/</span><span style="COLOR: maroon">div</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">body</span><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">html</span><span style="COLOR: blue">&gt;</span></p></div>

<p>You'll notice that the page is nearly all script: the static elements are minimal - just somewhere to enter the URL of our DWF file and a &lt;div&gt; for our DHTML tags to be squirted into.</p>

<p>When the page loads there's not a great deal to see:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=706,height=285,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/30/blank_freewheel_generator.png"><img title="Blank_freewheel_generator" height="121" alt="Blank_freewheel_generator" src="/assets/blank_freewheel_generator.png" width="300" border="0" /></a> </p>

<p>And it generates our sample pretty successfully:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=710,height=849,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/30/completed_freewheel_generator.png"><img title="Completed_freewheel_generator" height="358" alt="Completed_freewheel_generator" src="/assets/completed_freewheel_generator.png" width="299" border="0" /></a> </p>

<p>The application comes into its own when we run it with a DWF with lots of sheets: the sample file, <a href="http://freewheel.autodesk.com/sample/Hotel5.dwf">http://freewheel.autodesk.com/sample/Hotel5.dwf</a>, has 29 sheets in case you really want to put it through its paces. :-)</p>

<p><strong>Additional note:</strong></p>

<p>A colleague kindly reminded me about something I had meant to mention. The code in this post makes use of the <em>Autodesk Labs</em> Freewheel server (<a href="http://freewheel.labs.autodesk.com/">http://freewheel.labs.autodesk.com</a>). When you're developing applications based on Freewheel, you should really use the <em>production</em> Freewheel server (<a href="http://freewheel.autodesk.com/">http://freewheel.autodesk.com</a>), wherever possible. I used the Labs version (which is hosted on a single server, rather than being on a load-balanced, production-capable server farm) simply because it was able to render the specific DWF I was hosted in my site: fixes are generally rolled out first on the Labs server and migrate over time to the production server, as you'd expect, and in this case my DWF wasn't yet viewable via the production system.</p>

<p>To adjust the above code to use the production server, simply change the value of the &quot;freewheel&quot; variable:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">var</span> freewheel =</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: maroon">'http://freewheel.autodesk.com'</span>;</p></div>
