---
layout: "post"
title: "Use EXPORTPDF / EXPORTDWF / EXPORTDWFX programmatically"
date: "2012-07-26 04:31:00"
author: "Adam Nagy"
categories:
  - "Adam Nagy"
  - "AutoCAD"
original_url: "https://adndevblog.typepad.com/autocad/2012/07/use-exportpdf-exportdwf-exportdwfx-programmatically.html "
typepad_basename: "use-exportpdf-exportdwf-exportdwfx-programmatically"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/adam-nagy.html" target="_self">Adam Nagy</a></p>
<p>I&#39;m wondering if I could use EXPORTPDF directly to create a PDF file.</p>
<p><strong>Solution</strong></p>
<p>EXPORTPDF does not have a command line version, but just like EXPORTDWF and EXPORTDWFX as well, this command also builds on the EXPORT command functionality, which does have a command line version.</p>
<p>Here is a sample showing how to use it:</p>
<p><span style="background-color: #e6e6e6; font-family: &#39;courier new&#39;, courier;">(command &quot;_-EXPORT&quot; &quot;_PDF&quot; &quot;_D&quot; &quot;_NO&quot; &quot;C:\\my.pdf&quot;)</span></p>
<p>Also, the EXPORT command is using the Publish API in the background so you might be interested in using that - have a look at <a href="http://adndevblog.typepad.com/autocad/2012/05/how-to-use-autodeskautocadpublishingpublisherpublishexecute.html" target="_self">blog post &quot;How to use Autodesk.AutoCAD.Publishing.Publisher.PublishExecute?&quot;</a></p>
