---
layout: "post"
title: "Webcast in the making"
date: "2006-06-23 23:24:00"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD OEM"
  - "DWF"
  - "RealDWG"
original_url: "https://www.keanw.com/2006/06/webcast_in_the_.html "
typepad_basename: "webcast_in_the_"
typepad_status: "Publish"
---

<p>I thought I’d mention one of the projects we’re working on in DevTech right now. We’re planning a webcast for later this year, to talk our developers through the various technologies Autodesk provides to create/access/view native DWG data and published DWF data.</p>

<p>Here’s the idea: we show the generation of native DWG data using AutoCAD 2007 (in this case creating a number of 3D solids). We then take show how various technologies can be used to make use of the information stored in the native DWG file. Afterwards, we’ll publish the data to DWF – showing the capacity to add custom metadata using AutoCAD’s DWF Metadata API – and then show the various technologies that can be used to access and view the published DWF data.</p>

<p>Here's a quick description of some of the sample applications we're building to demonstrate the various technologies.</p>

<p><em>Native DWG</em></p>

<ol><li>A <strong>RealDWG</strong> application that accesses the DWG to extract the volume information of the contained 3D solids, displaying the results in a table (but not working with the geometry of the model at all).<ul><li>Although a fairly basic sample, it demonstrates that precise analysis can be performed directly on the DWG contents without the need for a graphical display. It also shows how to make use of the .NET API to RealDWG.</li></ul></li>

<li>An application with <strong>DWG TrueView</strong> embedded, to show the basic capabilities of the use of this component. <ul><li>This sample will show to what extent it is possible to embed this control for basic DWG display (without any further analysis of the DWG contents).</li></ul></li>

<li>An enhanced viewing application based on <strong>AutoCAD OEM</strong>, showing how this can look like a traditional CAD application or be embedded in another window. <ul><li>This sample will show how it is possible to make use of AutoCAD's APIs within an enhanced viewing application. We'll be able to take the same .NET code from the RealDWG sample and show it working with an embedded viewer.</li></ul></li></ol>

<p><em>Published DWF</em></p>

<ol><li>A <strong>DWF Toolkit</strong> application that access the DWF, extracting metadata about the materials used in the model and displaying a non-graphical summary of this data.<ul><li>In a similar way to the RealDWG sample, this one looks at how it is possible to use a file access toolkit to get at the contents of DWF files. While RealDWG focuses on the precise data representing the model, the DWF toolkit allows us to get at the published graphics and metadata. In this case we'll extract material metadata we attached during the publishing process using AutoCAD's APIs.</li></ul></li>

<li>Applications with the <strong>DWF Viewer</strong> embedded, showing how it can be used to create both simple and complex viewing applications. <ul><li>This sample will show some of the cool stuff you can do with an embedded DWF Viewer - particularly around object selection events and the ability to visually isolate subsets of the graphics published out to the DWF file.</li></ul></li></ol>
