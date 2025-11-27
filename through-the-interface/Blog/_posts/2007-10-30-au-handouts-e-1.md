---
layout: "post"
title: "AU Handouts: Enriching Your DWF™ - Part 2"
date: "2007-10-30 11:07:44"
author: "Kean Walmsley"
categories:
  - "AJAX"
  - "AU"
  - "DWF"
  - "Freewheel"
  - "HTML"
  - "JavaScript"
  - "SaaS"
  - "Training"
original_url: "https://www.keanw.com/2007/10/au-handouts-e-1.html "
typepad_basename: "au-handouts-e-1"
typepad_status: "Publish"
---

<p><em>[This post continues from </em><a href="http://through-the-interface.typepad.com/through_the_interface/2007/10/au-handouts-enr.html"><em>part 1</em></a><em> of this series. The source for the below applications is available here: <a href="http://through-the-interface.typepad.com/through_the_interface/files/dwf_toolkit_application_source.zip">DWF Toolkit application source</a>, <a href="http://through-the-interface.typepad.com/through_the_interface/files/design_review_application_source.zip">Design Review application source</a> &amp; <a href="http://through-the-interface.typepad.com/through_the_interface/files/freewheel_application_source.zip">Freewheel application source</a>.]</em></p>

<p><strong>Mining the data with the DWF Toolkit</strong></p>

<p>Now we’re going to look at using the DWF Toolkit – the freely available, cross platform toolkit for creating and reading DWF files – to extract the “identity” and “material” metadata associated with our geometry.</p>

<p>The DWF Toolkit is a C++ toolkit, so we have two components to our project:</p>

<ol><li>A C++ DLL that uses the DWF Toolkit to read a DWF, storing the data we care about in XML</li>

<li>A VB.NET module that implements a user interface for viewing the XML content.</li></ol>

<p>By its nature, DWF Toolkit client code tends to be somewhat complex, as it closely follows the internal structure of the DWF format, so I’m not going to go into the code. I will just say that you need to be a competent C++ programmer to work with the DWF Toolkit – it is not for the casual developer to dip into. There are clearly cases where using the DWF Toolkit makes a lot of sense – such as using it to modify or post-process DWFs non-graphically - but where possible I would recommend using a “higher level” API such as the API to Design Review or the publishing APIs within AutoCAD (just as we saw earlier).</p>

<p>The full source code for the project is posted to my blog, and here is what the application looks like:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=392,height=520,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/30/dwf_toolkit_application.png"><img title="Dwf_toolkit_application" height="397" alt="Dwf_toolkit_application" src="/assets/dwf_toolkit_application.png" width="299" border="0" /></a> <a onclick="window.open(this.href, '_blank', 'width=431,height=520,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/30/dwf_toolkit_application_2.png"><img title="Dwf_toolkit_application_2" height="361" alt="Dwf_toolkit_application_2" src="/assets/dwf_toolkit_application_2.png" width="300" border="0" /></a> </p>

<p><em>Figure 12 – a DWF Toolkit application to extract our metadata</em></p>

<p><strong>Linking 2D with 3D DWF data in an application embedding Design Review</strong></p>

<p>In the next part of our demo, we’re going to make use of the metadata embedded in our DWF file to link the 2D and 3D sheets of our DWF.</p>

<p>Emedded 2D and 3D view onto DWF data actually have quite different API capabilities: 2D views have a set of events that you can respond to, to determine when objects are hovered over or selected (which you cannot receive for objects in 3D views), while 3D views allow you to manipulate object visibility and transparency (something you cannot control for objects in 2D views).</p>

<p>So we’re actually going to play to the strengths of the two API sets: we’re going to respond to an event when objects are hovered over in the 2D view, and during that callback we’re going to manipulate the 3D view to “isolate” the object in that view. And the way we’re going to link the two views is via the metadata that’s attached to our objects.</p>

<p>The HTML code is too long to include in its entirety here, so I’m going to describe the overall functionality and leave it to the reader to work through the code at their own pace. I will post the full source in a blog entry, though (as well as making it available for download there).</p>

<p>One of the key things is that in our “OnOverObject” callback (which we respond to with JavaScript code in our HTML), is that we receive a geometry index (a node ID) for the object that’s being hovered over. This, in itself, isn’t directly useful, so we need to find a way to map it to the objects in question. I managed to do this by hosting the object identity (handle) metadata in a combobox, populated in the same order as the geometry IDs we receive. So it’s very important that we populate the combobox appropriately, even if it’s hidden (which in our case it isn’t – we also make it available for the user to select objects by their handle).</p>

<p>Let’s look at how we embed our two views – both onto the same DWF file. Here’s the object tag for the 3D view:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">object</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: red">id</span><span style="COLOR: blue">=&quot;ObjectView&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: red">width</span><span style="COLOR: blue">=&quot;100%&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: red">height</span><span style="COLOR: blue">=&quot;100%&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: red">classid</span><span style="COLOR: blue">=&quot;clsid:A662DA7E-CCB7-4743-B71A-D817F6D575DF&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: red">codebase</span><span style="COLOR: blue">=</span> <span style="COLOR: blue">&quot;http://www.autodesk.com/global/dwfviewer/installer/DwfViewerSetup.cab#version=7,0,0,928&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: red">VIEWASTEXT</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">param</span> <span style="COLOR: red">name</span><span style="COLOR: blue">=&quot;src&quot;</span> <span style="COLOR: red">value</span><span style="COLOR: blue">=&quot;solids.dwf?section=1&quot;&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">object</span><span style="COLOR: blue">&gt;</span></p></div>

<p>We have used the src property to set the appropriate section by using “?section=1” to specify the first one.</p>

<p>With the 2D sheet we’ve taken a slightly different approach:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">object</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: red">id</span><span style="COLOR: blue">=&quot;Canvas&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: red">width</span><span style="COLOR: blue">=&quot;100%&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: red">height</span><span style="COLOR: blue">=&quot;100%&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: red">classid</span><span style="COLOR: blue">=&quot;clsid:A662DA7E-CCB7-4743-B71A-D817F6D575DF&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: red">codebase</span><span style="COLOR: blue">=</span> <span style="COLOR: blue">&quot;http://www.autodesk.com/global/dwfviewer/installer/DwfViewerSetup.cab#version=7,0,0,928&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: red">VIEWASTEXT</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">param</span> <span style="COLOR: red">name</span><span style="COLOR: blue">=&quot;src&quot;</span> <span style="COLOR: red">value</span><span style="COLOR: blue">=&quot;solids.dwf&quot;&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">object</span><span style="COLOR: blue">&gt;</span> </p></div>

<p>We have not selected the sheet here – to avoid timing issues we’ve done so in the EndLoadItem JavaScript callback of the 2D view’s document:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">if</span> (Canvas.SourcePath != <span style="COLOR: maroon">&quot;solids.dwf?section=2&quot;</span>)</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">{</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; Canvas.SourcePath = <span style="COLOR: maroon">&quot;solids.dwf?section=2&quot;</span>;</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">}</p></div>

<p>Much of the rest of the code is around user interface tweaks, such as forcing the “Orbit” mode by default in the 3D view, and the “Select Object” mode in the 2D view. We also disable the toolbars etc., to reduce dialog clutter.</p>

<p><a onclick="window.open(this.href, '_blank', 'width=800,height=577,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/30/design_review_application_2.png"><img title="Design_review_application_2" height="216" alt="Design_review_application_2" src="/assets/design_review_application_2.png" width="300" border="0" /></a> </p>

<p><em>Figure 13 - embedding and linking multiple Design Review windows in an HTML application</em></p>

<p><strong>Hosting an embedded Freewheel view in a custom web-page</strong></p>

<p>Freewheel – and its Labs sibling, Project Freewheel – is a zero-client AJAX (Asynchronous JavaScript and XML) technology for working with both 2D &amp; 3D DWF content. The DWF file resides on a server – whether uploaded securely by the user or hosted on a publicly-accessible web-page – and the graphics for the current view on the DWF content are piped down to the HTML client via raster. It’s impressively responsive, even when orbiting in 3D.</p>

<p>We don’t get the same ability to view and work with metadata (at least not right now), but it is still a compelling experience if you want to embed lightweight navigation for DWF without forcing an install of Design Review on the client machine. It’s also getting some very cool collaborative review capabilities, to allow multiple people to mark 2D or 3D designs up in real-time. Very cool stuff, but not today’s subject, I’m afraid.</p>

<p>The first thing is to get our design into Freewheel. One option is to use the “Share Now” utility available from Autodesk Labs, which publishes to DWF and uploads to Freewheel, or we could upload it via the Freewheel user-interface, as we have a DWF file ready-to-go.<br />In our case, however, I’ve already posted a version of the DWF file to a publicly-accessible internet location, so we’re going to use that:</p>

<p><a href="http://through-the-interface.typepad.com/presentations/DWF/solids.dwf">http://through-the-interface.typepad.com/presentations/DWF/solids.dwf</a></p>

<p>A quick note on using Autodesk Freewheel vs. Project Freewheel. Autodesk Freewheel (<a href="http://freewheel.autodesk.com/">http://freewheel.autodesk.com</a>) is the production environment for Freewheel, and is hosted on a high-availability, load-balanced server farm. Project Freewheel (<a href="http://freewheel.labs.autodesk.com/">http://freewheel.labs.autodesk.com</a>) is hosted on a PC under someone’s desk (OK, that’s an exaggeration :-), but contains the latest &amp; greatest functionality. I’ve found that the DWF files I’ve been creating for this demo are best viewed with the <em>Project</em> Freewheel server, so I’m using that, today, but I recommend you first try with the production system when implementing your own Freewheel-powered sites.</p>

<p>The two basic approaches for including a Freewheel view in your website are to link or to embed. Linking is simple: you create a standard HTML link with a URL pointing Freewheel to your DWF file:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">a</span> <span style="COLOR: red">href</span><span style="COLOR: blue">=&quot;http://freewheel.labs.autodesk.com/dwf.aspx?dwf=http://through-the-interface.typepad.com/presentations/DWF/solids.dwf&quot;&gt;</span>My link<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">a</span><span style="COLOR: blue">&gt;</span></p></div>

<p>This creates a straight link to the Freewheel view. As with any HTML link, you can choose to launch this in a new window using the target property. You might also want to show a different page – you do this using the “sec” argument, which is the page/sheet number starting from 1:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">a</span> <span style="COLOR: red">href</span><span style="COLOR: blue">=&quot;http://freewheel.labs.autodesk.com/dwf.aspx?sec=2&amp;dwf=http://through-the-interface.typepad.com/presentations/DWF/solids.dwf&quot;&gt;</span>My link<span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">a</span><span style="COLOR: blue">&gt;</span></p></div>

<p>Next we’re going to embed a view. We do this using an HTML iframe:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">iframe</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: red">frameborder</span><span style="COLOR: blue">=&quot;0&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: red">height</span><span style="COLOR: blue">=&quot;300&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: red">width</span><span style="COLOR: blue">=&quot;400&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: red">scrolling</span><span style="COLOR: blue">=&quot;no&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: red">src</span><span style="COLOR: blue">=&quot;http://freewheel.labs.autodesk.com/dwf.aspx?sec=2&amp;dwf=http://through-the-interface.typepad.com/presentations/DWF/solids.dwf&quot;&gt;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;/</span><span style="COLOR: maroon">iframe</span><span style="COLOR: blue">&gt;</span></p></div>

<p>Once again, you can use the “sec” parameter to select a particular sheet. This creates a navigable view on your DWF file.</p>

<p>Aside from linking or embedding, Freewheel exposes an ever-growing set of web services APIs, to allow you to render a particular view of a DWF to raster or to get information about a DWF, such as the number of pages, etc.</p>

<p>We can use the dwfImage web service to get an image of a DWF to use as a thumbnail:</p>

<div style="FONT-SIZE: 8pt; BACKGROUND: white; COLOR: black; FONT-FAMILY: Courier New"><p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&lt;</span><span style="COLOR: maroon">img</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: red">frameborder</span><span style="COLOR: blue">=&quot;0&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: red">height</span><span style="COLOR: blue">=&quot;150&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: red">width</span><span style="COLOR: blue">=&quot;200&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: red">scrolling</span><span style="COLOR: blue">=&quot;no&quot;</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px">&nbsp; <span style="COLOR: red">src</span><span style="COLOR: blue">=</span></p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&quot;http://freewheel.labs.autodesk.com/dwfImage.aspx?page=1&amp;width=200&amp;height=150&amp;path=http://through-the-interface.typepad.com/presentations/DWF/solids.dwf&quot;</span> /</p>

<p style="FONT-SIZE: 8pt; MARGIN: 0px"><span style="COLOR: blue">&gt;</span></p></div>

<p>We can throw these together and create a web-page that links from the sheet thumbnails to full instances of Freewheel, as well as embedding interactive views of the same sheets. (The HTML source for this page is posted on my blog – it’s not complicated, but too long to include here.)</p>

<p><a onclick="window.open(this.href, '_blank', 'width=722,height=833,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/30/embedding_and_linking_freewheel.png"><img title="Embedding_and_linking_freewheel" height="346" alt="Embedding_and_linking_freewheel" src="/assets/embedding_and_linking_freewheel.png" width="300" border="0" /></a> </p>

<p><em>Figure 14 – embedding and linking to Freewheel in a custom web-page</em></p>

<p>The code that defines the above page could actually be generated manually – and that’s the next bit of fun… :-)</p>

<p>Using AJAX you can call web services from your client code: there are limitations as to what you can do (especially with regards to security limitations accessing web services across domains - which will make a great advanced topic for my blog), but this should give you a rough idea of what’s possible. In the below sample we query the number of sheets for a particular DWF, and use DHTML to generate the tags that allow us to view the content, as in the above hard-coded sample:</p>

<p><a onclick="window.open(this.href, '_blank', 'width=706,height=285,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/30/blank_freewheel_generator.png"><img title="Blank_freewheel_generator" height="121" alt="Blank_freewheel_generator" src="/assets/blank_freewheel_generator.png" width="300" border="0" /></a> </p>

<p><em>Figure 15 – our Freewheel generator on load</em></p>

<p><a onclick="window.open(this.href, '_blank', 'width=710,height=849,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0'); return false" href="http://through-the-interface.typepad.com/.shared/image.html?/photos/uncategorized/2007/10/30/completed_freewheel_generator.png"><img title="Completed_freewheel_generator" height="358" alt="Completed_freewheel_generator" src="/assets/completed_freewheel_generator.png" width="299" border="0" /></a> </p>

<p><em>Figure 16 – our completed Freewheel generator</em></p>
