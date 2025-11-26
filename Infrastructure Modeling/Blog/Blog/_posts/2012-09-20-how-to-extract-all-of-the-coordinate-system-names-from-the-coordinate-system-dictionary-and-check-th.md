---
layout: "post"
title: "How to extract all of the coordinate system names from the coordinate system dictionary and check their types?"
date: "2012-09-20 00:59:53"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AIMS 2012"
  - "AIMS 2013"
  - "AutoCAD Map 3D 2011"
  - "AutoCAD Map 3D 2012"
  - "AutoCAD Map 3D 2013"
  - "MapGuide Enterprise 2011"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/09/how-to-extract-all-of-the-coordinate-system-names-from-the-coordinate-system-dictionary-and-check-th.html "
typepad_basename: "how-to-extract-all-of-the-coordinate-system-names-from-the-coordinate-system-dictionary-and-check-th"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>The
<strong>OSGeo.MapGuide</strong> namespace contains a set of classes which provide access to the
CS-Map coordinate system functionality in both the AutoCAD Map 3D and AIMS
environments.</p>
<p> <strong>MgCoordinateSystemFactory</strong> is used to get the coordinate system
categories and the coordinate system catalog. <strong>MgCoordinateSystemCatalog</strong> is used
to get the various dictionaries. <strong>MgCoordinateSystemDictionary
</strong>contains definitions for earth-based and Euclidean coordinate systems.</p>
<p>&#0160;</p>
<p>The following code extracts all of the coordinate system names from the coordinate system dictionary, uses those names to get all of the coordinate system definitions from the dictionary and determines the type (projected, geographic, or
arbitrary) of the coordinate system from its definition, and stores the name of
the coordinate system in a list according to its type.&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//MgCoordinateSystemFactory coordSysFactory = new MgCoordinateSystemFactory();</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//MgCoordinateSystemCatalog csCatalog = coordSysFactory.GetCatalog();</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//MgCoordinateSystemDictionary csDict = csCatalog.GetCoordinateSystemDictionary();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">List</span><span style="line-height: 140%;">&lt;</span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;">&gt; arbitraryCs = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">List</span><span style="line-height: 140%;">&lt;</span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;">&gt;();</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">List</span><span style="line-height: 140%;">&lt;</span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;">&gt; geographicCs = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">List</span><span style="line-height: 140%;">&lt;</span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;">&gt;();</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">List</span><span style="line-height: 140%;">&lt;</span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;">&gt; projectedCs = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">List</span><span style="line-height: 140%;">&lt;</span><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;">&gt;();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">MgCoordinateSystemEnum</span><span style="line-height: 140%;"> csDictEnum = csDict.GetEnum();</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// the following line gets all of the names in the dictionary</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// uses those names to get the coordinate </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> csCount = csDict.GetSize();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nCoordinate System Count : &quot;</span><span style="line-height: 140%;"> + csCount.ToString());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\n-------------------------------------------------&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">MgStringCollection</span><span style="line-height: 140%;"> csNames = csDictEnum.NextName(csCount);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">string</span><span style="line-height: 140%;"> csName = </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">MgCoordinateSystem</span><span style="line-height: 140%;"> cs = </span><span style="color: blue; line-height: 140%;">null</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> csType = 0;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">for</span><span style="line-height: 140%;"> (</span><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> i = 0; i &lt; csCount; i++)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;csName = csNames.GetItem(i);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;cs = csDict.GetCoordinateSystem(csName);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;csType = cs.GetType();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (csType == </span><span style="color: #2b91af; line-height: 140%;">MgCoordinateSystemType</span><span style="line-height: 140%;">.Arbitrary)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; arbitraryCs.Add(csName);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: blue; line-height: 140%;">else</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (csType == </span><span style="color: #2b91af; line-height: 140%;">MgCoordinateSystemType</span><span style="line-height: 140%;">.Geographic)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; geographicCs.Add(csName);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: blue; line-height: 140%;">else</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (csType == </span><span style="color: #2b91af; line-height: 140%;">MgCoordinateSystemType</span><span style="line-height: 140%;">.Projected)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; projectedCs.Add(csName);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;}&#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nCoordinate System Name : &quot;</span><span style="line-height: 140%;"> + csName.ToString());</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c32002232970b-pi" style="display: inline;"><img alt="CS_Name" class="asset  asset-image at-xid-6a0167607c2431970b017c32002232970b" src="/assets/image_3449c3.jpg" title="CS_Name" /></a><br /><br /></p>
<p>Hope this is useful to you !</p>
