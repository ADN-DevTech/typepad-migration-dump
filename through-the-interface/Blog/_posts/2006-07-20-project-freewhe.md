---
layout: "post"
title: "Project Freewheel: Losing Control!"
date: "2006-07-20 14:43:48"
author: "Kean Walmsley"
categories:
  - "DWF"
  - "SaaS"
original_url: "https://www.keanw.com/2006/07/project_freewhe.html "
typepad_basename: "project_freewhe"
typepad_status: "Publish"
---

<p><a href="http://dwfit.com/">Project Freewheel</a> is a project that was recently launched via <a href="http://labs.autodesk.com/">Autodesk Labs</a>. It's a technology preview that allows you to share DWF files without the need for installing a client application that includes an ActiveX control for DWF display (such as the <a href="http://www.autodesk.com/dwfviewer">DWF Viewer</a> or <a href="http://www.autodesk.com/designreview">Design Review</a>).</p>

<p>There are three main features of Project Freewheel that are of interest to developers. Let's take a look at them below.</p>

<p><strong>Embedding a navigable DWF</strong></p>

<p>It's now really simple to embed a DWF file in your web page without forcing a download of the DWF Viewer application. As long as your DWF file is accessible somewhere on the Internet, you can use Project Freewheel's <a href="http://dwfit.com/dwf.aspx">Interactive Software Viewer</a> to allow navigation of your DWF without a component install. Very cool and very easy.</p>

<p>Once your DWF is in a publicly accessible location, you simply use a frame or inline frame tag in your HTML to embed the results of a URL passing the required parameters (in this case we're just passing the location of the DWF):</p>

<p>&lt;iframe src=&quot;http://dwfit.com/dwf.aspx?dwf=http://dwfit.com/sample/psp.dwf&quot; width=&quot;400&quot; height=&quot;300&quot; scrolling=&quot;no&quot; frameborder=&quot;0&quot; /&gt; </p>

<iframe src="http://dwfit.com/dwf.aspx?dwf=http://dwfit.com/sample/psp.dwf" frameborder="0" width="400" scrolling="no" height="300"> </iframe>

<p><strong>Embedding a static image of a DWF</strong></p>

<p>If you merely want a static image, that's easy too. You can use the <a href="http://dwfit.com/DWFImage.aspx">Rendering Service</a> to serve up a rendered view of a particular sheet from a DWF file. This time we're going to pass in a few more parameters regarding the size of the image and the view target/scale:</p>

<p>&lt;iframe src=&quot;http://dwfit.com/dwfImage.aspx?cx=0.5&amp;cy=0.5&amp;scale=1&amp;page=3&amp;width=370&amp;height=270&amp; path=http://dwfit.com/sample/psp.dwf&quot; width=&quot;400&quot; height=&quot;300&quot; scrolling=&quot;no&quot; frameborder=&quot;0&quot; /&gt;</p>

<iframe src="http://dwfit.com/dwfImage.aspx?cx=0.5&amp;cy=0.5&amp;scale=1&amp;page=3&amp;width=370&amp;height=270&amp;path=http://dwfit.com/sample/psp.dwf" frameborder="0" width="400" scrolling="no" height="300"> </iframe>

<p><strong>Getting some data regarding a DWF</strong></p>

<p>The last item of interest to developers is the <a href="http://dwfit.com/dwfRender.asmx">DWFRender Web Service</a> that allows you to query non-graphical information about a particular DWF file. If you're new to web services, I recommend this <a href="http://www.xml.com/pub/a/ws/2001/04/04/webservices/">primer</a> (although it's now slightly dated).</p>

<p>Most interestingly you can use the service to query the number of pages contained in a particular DWF and various information on those pages (name, paper-size, units).</p>

<p>There are a number of approaches you can use to call SOAP web services from HTML using JavaScript, and the one I've used myself in the past is documented here:</p>

<p><a href="http://msdn.microsoft.com/workshop/author/webservice/webservice.asp">http://msdn.microsoft.com/workshop/author/webservice/webservice.asp</a></p>

<p>It takes a little work to set up - you need to define some local script (typically JavaScript) and make use of an <a href="http://msdn.microsoft.com/archive/en-us/samples/internet/behaviors/library/webservice/webservice.htc">HTC file</a> provided by Microsoft. I would have done it directly from this page, but it's actually quite tricky to do from within a blog - you have to get the HTC file onto the server and try to get the hosted page to call the script properly. Anyway - you can imagine the results: the page would simply have some embedded text representing the number of pages and the page-names, all of which would have been queried from the web service (the calling code would have had to pass in the URL to the DWF, of course).</p>

<p>For more detailed information on how to use Project Freewheel as a developer, please check <a href="http://dwfit.com/webdevelopers.htm">this page</a>.</p>
