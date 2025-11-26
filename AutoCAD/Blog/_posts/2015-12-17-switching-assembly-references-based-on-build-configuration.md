---
layout: "post"
title: "Switching assembly references based on build configuration"
date: "2015-12-17 23:28:58"
author: "Balaji"
categories:
  - ".NET"
  - "2013"
  - "2014"
  - "2015"
  - "2016"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2015/12/switching-assembly-references-based-on-build-configuration.html "
typepad_basename: "switching-assembly-references-based-on-build-configuration"
typepad_status: "Publish"
---

<p><a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html">By Balaji Ramamoorthy</a></p>
<p>Recently, a developer came up with this query :</p>
<blockquote>Is there a simple way to have a single copy of my code configured to compile to different folders with different references based on the desired version of AutoCAD to be run ?</blockquote>
<p>This is not strictly an AutoCAD API related query but it is very relevant to how we build plugins for AutoCAD. It is a common requirement that we add references from different paths based on the AutoCAD version for which we are building the plugin.</p>
<p>A simple way to get this working is to create separate build configurations in your Visual Studio solution.</p>
<p>Now, open the .csproj in a text editor and manually include the &quot;Condition&quot; for each of the references.</p>
<p>As an example, here is the change to include different versions of the interop assembly for Sheetset manager for each build configuration.</p>
<p>&lt;Reference Include=&quot;Interop.ACSMCOMPONENTS20Lib&quot; Condition=&quot;&#39;$(Configuration)&#39;==&#39;Debug2015&#39;&quot;&gt;<br /> &lt;HintPath&gt;..\..\..\..\ObjectARX 2015\inc-x64\ACSMCOMPONENTS20Lib.dll&lt;/HintPath&gt;<br /> &lt;EmbedInteropTypes&gt;True&lt;/EmbedInteropTypes&gt;<br />&lt;/Reference&gt;<br />&lt;Reference Include=&quot;Interop.ACSMCOMPONENTS20Lib&quot; Condition=&quot;&#39;$(Configuration)&#39;==&#39;Debug2016&#39;&quot;&gt;<br /> &lt;HintPath&gt;..\..\..\..\ObjectARX 2016\inc-x64\ACSMCOMPONENTS20Lib.dll&lt;/HintPath&gt;<br /> &lt;EmbedInteropTypes&gt;True&lt;/EmbedInteropTypes&gt;<br />&lt;/Reference&gt;</p>
<p>If you are using any other technique to achieve this already, please do share by posting your comments. I am sure that will help many other developers. Thank you.</p>
