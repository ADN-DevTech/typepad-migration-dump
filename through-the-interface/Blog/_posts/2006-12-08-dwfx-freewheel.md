---
layout: "post"
title: "DWFx & Freewheel"
date: "2006-12-08 09:24:16"
author: "Kean Walmsley"
categories:
  - "DWF"
original_url: "https://www.keanw.com/2006/12/dwfx_freewheel.html "
typepad_basename: "dwfx_freewheel"
typepad_status: "Publish"
---

<p> Many of you will have seen the recent announcement from Autodesk and Microsoft regarding DWFx. I thought I’d spend some time talking about the technology behind the announcement and the impact on developers using DWF. I also wanted to mention Project Freewheel briefly once again.</p>

<p><strong><span style="font-size: 1.2em;">DWFx</span></strong></p>

<p>Here are the recent DWFx-related announcements, from Scott Sheppard’s Beyond the Paper blog:</p>

<p><a href="http://dwf.blogs.com/beyond_the_paper/2006/11/press_release_a.html">PRESS RELEASE: Autodesk Unlocks Design Data for Windows Vista and XPS Users</a></p>

<p><a href="http://dwf.blogs.com/beyond_the_paper/2006/11/microsoft_annou.html">AU Update: Microsoft Announces Support for DWF via XPS</a></p>

<p><a href="http://dwf.blogs.com/beyond_the_paper/2006/12/microsoft_net_3.html">Microsoft .NET 3.0 Framework includes DWF (.dwfx) Viewing Capability</a></p>

<p>I’ve put the below information in a Q&amp;A format (which seemed appropriate as I was writing it). If you have further questions about this, be sure to post a comment or send me an email.</p>

<p><strong>What is DWFx?</strong></p>

<p>DWFx is the next generation of the DWF format – one that is more closely aligned with emerging technology standards sponsored and supported by Microsoft. The near-term impact is around 2D publishing: as soon as next year, many of our design applications will publish to the DWFx format. The new 2D portion of DWFx is based on (or in tech-speak “is a specialization of”) the XML Paper Specification (XPS) format, an open format for published document interchange. </p>

<p><strong>Why is Autodesk doing this?</strong></p>

<p>For a number of reasons, but this is a big one: DWFx is supported natively on Windows Vista – no additional viewer needs to be installed to view these files – which greatly increases the attraction and reach of the format.</p>

<p><strong>What is XPS?</strong></p>

<p>From the <a href="http://www.microsoft.com/whdc/xps/default.mspx">Microsoft website</a>:</p><blockquote dir="ltr"><p><em>“The XML Paper Specification (XPS) makes modern documents possible for all. Simply put, XPS describes electronic paper in a way that can be read by hardware, read by software, and read by humans. With XPS, documents print better, can be shared easier, be archived with confidence, and are more secure.</em></p>

<p><em>Microsoft has integrated XPS-based technologies into the 2007 Microsoft Office system and the Microsoft Windows Vista operating system, but XPS itself is platform independent, openly published, and available royalty-free. Microsoft is using XPS to bring additional document value to its customers, its partners, and the computing industry.”</em></p></blockquote><p><strong>What about non-Vista platforms?</strong></p>

<p>The XPS Viewer is also part of the .NET Framework 3.0 install, and the “XPS Essentials Pack” download, available for Windows 2000 &amp; XP.</p>

<p><strong>What about the existing DWF format?</strong></p>

<p>DWFx is the eventual – not immediate – replacement for DWF. It is basically the next version of the DWF standard. For now, largely due to the fact it will not immediately support the existing features of DWF, it will be another publishing option.</p>

<p>For example, DWFx does not support 3D, for now. It does support metadata, but this will only be viewable in the separately downloadable DWF Viewer. In time, DWFx will support all existing DWF functionality.</p>

<p><strong>What’s the best way to read &amp; write the new DWFx, from a programmatic perspective?</strong></p>

<p>As DWFx is based on XPS, which is XML packaged up according to the Open Packaging Conventions (OPC – which is basically the same as having the various files ZIPped up, but also includes support for digital signatures and rights management), you could use your favourite text editor and WinZIP to create a DWFx file.</p>

<p>That said, in order to cope with the complexity of the relationships between the various components of a DWFx file, it’ll be much easier to make use of the DWF Toolkit (of which version 7.4 will support reading/writing DWFx). We’re adding a compatibility layer between the existing DWF 6 APIs that are part of the current DWF Toolkit, so that – hopefully – existing code will be 95% compatible with the new DWFx format.</p>

<p>Also, in time you will also be able to use all our existing DWF publishers to create DWFx content (including AutoCAD, AutoCAD LT, Inventor, Revit, Map 3D, and so on - as well as free tools such as DWG TrueView, DWG TrueConvert and DWF Writer).</p>

<p><strong>What about the ObjectARX metadata publishing APIs?</strong></p>

<p>The AutoCAD APIs for adding custom metadata to DWFs during publication will continue to function with DWFx (although, as mentioned, the metadata will not be visible in the XPS Viewer).</p>

<p><strong><span style="font-size: 1.2em;">Project Freewheel</span></strong></p>

<p>Another key part of Autodesk’s strategy around proliferation of DWF as a published design format is Project Freewheel. I discussed this in previous posts (<a href="http://through-the-interface.typepad.com/through_the_interface/2006/07/project_freewhe.html">Project Freewheel: Losing Control!</a> &amp; <a href="http://through-the-interface.typepad.com/through_the_interface/2006/07/calling_the_dwf.html">Calling the DWFRender Web Service from HTML</a>), but I wanted to mention it once again. This technology allows DWFs to be viewed on any platform that supports internet browsing, viewing of raster images and some level of javascript. Which is basically any platform (this includes many telephones and BlackBerry devices, for instance).</p>

<p>And something truly incredible: Project Freewheel also now supports 3D - <a href="http://dwf.blogs.com/beyond_the_paper/2006/12/project_freewhe.html">Project Freewheel: 3D Support Added</a>. You have to try it – it really needs to be seen to be believed.</p>
