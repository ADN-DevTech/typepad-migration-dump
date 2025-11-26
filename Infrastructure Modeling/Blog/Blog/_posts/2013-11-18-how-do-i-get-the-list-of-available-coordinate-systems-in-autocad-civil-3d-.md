---
layout: "post"
title: "How do I get the list of available Coordinate Systems in AutoCAD Civil 3D ?"
date: "2013-11-18 22:15:51"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "AutoCAD Civil 3D 2014"
  - "Civil 3D"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/11/how-do-i-get-the-list-of-available-coordinate-systems-in-autocad-civil-3d-.html "
typepad_basename: "how-do-i-get-the-list-of-available-coordinate-systems-in-autocad-civil-3d-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>&#0160;</p>
<p>Is there a way to get a list of the coordinate systems that are available to the Civil 3D drawing i.e. how can I access the list found in the <strong>Drawing Settings&gt;Units and Zone&gt;Zone</strong> section as shown in the screenshot below ?</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b0150b267970c-pi" style="display: inline;"><img alt="Civil3D_Coordinate_Systems_1" class="asset  asset-image at-xid-6a0167607c2431970b019b0150b267970c" src="/assets/image_8c88bf.jpg" title="Civil3D_Coordinate_Systems_1" /></a><br />&#0160;&#0160;</p>
<p>&#0160;</p>
<p>We can access the list of available coordinate systems using Geospatial Platform API which is part of AutoCAD Map 3D. As you know, AutoCAD Civil 3D is built on top of AutoCAD Map 3D and we can use the available Map 3D functionalities and API features in building custom application running on AutoCAD Civil 3D as well. To get the list of all the available Coordinate systems in a Drawing file in AutoCAD Civil 3D, we need to use <strong>MgCoordinateSystemFactory</strong> from <strong>OSGeo.MapGuide</strong> <em><strong>namespace</strong></em>. And for that you need to reference the following assemblies from &#39;AutoCAD Civil 3D 2014&#39; installation folder -&#0160;</p>
<ul>
<li><strong>OSGeo.MapGuide.Foundation.dll</strong></li>
<li><strong>OSGeo.MapGuide.PlatformBase.dll</strong></li>
<li><strong>OSGeo.MapGuide.Geometry.dll</strong></li>
</ul>
<p>&#0160;&#0160;</p>
<p>Here is the C# .NET code snippet -&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">MgCoordinateSystemFactory</span><span style="line-height: 140%;"> coordSysFactory = </span><span style="color: blue; line-height: 140%;">new</span><span style="color: #2b91af; line-height: 140%;">MgCoordinateSystemFactory</span><span style="line-height: 140%;">();</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">MgCoordinateSystemCatalog</span><span style="line-height: 140%;"> csCatalog = coordSysFactory.GetCatalog();</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">MgCoordinateSystemDictionary</span><span style="line-height: 140%;"> csDict = csCatalog.GetCoordinateSystemDictionary(); </span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">MgCoordinateSystemEnum</span><span style="line-height: 140%;"> csDictEnum = csDict.GetEnum();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> csCount = csDict.GetSize();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nCoordinate System Count : &quot;</span><span style="line-height: 140%;"> + csCount.ToString());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\n-------------------------------------------------&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">MgStringCollection</span><span style="line-height: 140%;"> csNames = csDictEnum.NextName(csCount);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> csName = </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">;&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">MgCoordinateSystem</span><span style="line-height: 140%;"> cs = </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">;&#0160;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"> &#0160; &#0160; &#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> i = 0; i &lt; csCount; i++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; csName = csNames.GetItem(i);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; cs = csDict.GetCoordinateSystem(csName);&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nCoordinate System Name : &quot;</span><span style="line-height: 140%;"> + csName.ToString() + </span><span style="color: #a31515; line-height: 140%;">&quot;&#0160; &quot;</span><span style="line-height: 140%;"> + </span><span style="color: #a31515; line-height: 140%;">&quot;CS Code :&#0160; &quot;</span><span style="line-height: 140%;"> + cs.CsCode.ToString());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\n-------------------------------------------------&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>and Result in AutoCAD Civil 3D -</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b019b0150b397970c-pi" style="display: inline;"><img alt="Civil3D_Coordinate_Systems" class="asset  asset-image at-xid-6a0167607c2431970b019b0150b397970c" src="/assets/image_c6a1a7.jpg" title="Civil3D_Coordinate_Systems" /></a></p>
