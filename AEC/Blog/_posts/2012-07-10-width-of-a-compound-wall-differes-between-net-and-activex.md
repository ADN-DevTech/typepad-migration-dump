---
layout: "post"
title: "Width of a compound wall differs between .NET and ActiveX"
date: "2012-07-10 23:31:52"
author: "Mikako Harada"
categories:
  - ".NET"
  - "AutoCAD Architecture"
  - "COM"
  - "Mikako Harada"
original_url: "https://adndevblog.typepad.com/aec/2012/07/width-of-a-compound-wall-differes-between-net-and-activex.html "
typepad_basename: "width-of-a-compound-wall-differes-between-net-and-activex"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/mikako-harada.html" target="_self" title="Mikako Harada">Mikako Harada</a></p>
<p><strong>Issue</strong></p>
<p>I’m in the process of migrating my VBA application to .NET in ACA.&#0160; I noticed that the values of Width are different between the two API.&#0160; For example, the width value displayed in the snoop tool (MgdDbgAec) shows as 6”, while on the property palette and in VBA, the width is 5 1/2”.&#0160; I have a variable width component (5 1/2 “ in this case) represents a wood stud.&#0160; This is the value that I need.&#0160; The 1/2“ static component represents the gyp board.</p>
<p>Is there a way that I can get the variable component width value like I was able to in VBA?&#0160;</p>
<p><strong>Solution</strong></p>
<p>There are two types of width properties for a wall in .NET: &quot;InstanceWidth&quot; and &quot;Width&quot;.&#0160; &quot;InstanceWidth&quot; is a base width, and &quot;Width&quot; is the total/overall width.&#0160; In ActiveX, we have &quot;Width&quot;, which is always base width.&#0160; When you have a formula or variable width &quot;BW&quot; (base width) in your definition of compound structure, you will need to distinguish between the usages of these two widths.</p>
