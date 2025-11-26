---
layout: "post"
title: "Accessing AutoCAD Civil 3D Alignment Design Speeds using .NET API"
date: "2013-02-25 00:25:06"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2013/02/accessing-autocad-civil-3d-alignment-design-speeds-using-net-api.html "
typepad_basename: "accessing-autocad-civil-3d-alignment-design-speeds-using-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>For a given
<strong>Alignment</strong> object in AutoCAD Civil 3D, we want to find out the all the
&#39;<em><strong>Station</strong></em>&#39; values and the corresponding &#39;<em><strong>Design Speed</strong></em>&#39; as shown in the
screenshot below -</p>
<p>&#0160;</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017ee8b7d3e3970d-pi" style="display: inline;"><img alt="AlignmentDesignSpeed01" class="asset  asset-image at-xid-6a0167607c2431970b017ee8b7d3e3970d" src="/assets/image_1b7f16.jpg" title="AlignmentDesignSpeed01" /></a></p>
<p>We can access
these values using <strong>DesignSpeed</strong> class in Civil 3D .NET API. </p>
<p>Here is a relevant code snippet in C# -</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Alignment</span><span style="line-height: 140%;"> objAlignment = trans.GetObject(alignmentId, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForRead) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Alignment</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">DesignSpeed</span><span style="line-height: 140%;"> objDS </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> objAlignment.DesignSpeeds)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\n\tDesign Speed Number :{0} -- Start Station: {1} --- Design Speed Value :{2}&quot;</span><span style="line-height: 140%;">, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; objDS.SpeedNumber.ToString(), objDS.Station.ToString(), objDS.Value.ToString() );&#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b017c3714ae3d970b-pi" style="display: inline;"><img alt="AlignmentDesignSpeed02" class="asset  asset-image at-xid-6a0167607c2431970b017c3714ae3d970b" src="/assets/image_b72bec.jpg" title="AlignmentDesignSpeed02" /></a><br /><br /></p>
<p>Hope this is useful to you!</p>
