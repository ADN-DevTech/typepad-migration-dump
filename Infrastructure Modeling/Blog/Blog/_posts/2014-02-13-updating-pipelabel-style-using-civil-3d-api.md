---
layout: "post"
title: "Updating PipeLabel Style using Civil 3D API"
date: "2014-02-13 01:08:17"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2013"
  - "AutoCAD Civil 3D 2014"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2014/02/updating-pipelabel-style-using-civil-3d-api.html "
typepad_basename: "updating-pipelabel-style-using-civil-3d-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>&#0160;</p>
<p><strong>Label.StyleName</strong> API allows to <strong>get</strong> or <strong>set</strong> the Entity object&#39;s style name and we can use this to update PipeLabel Style.</p>
<p>Here is a C# .NET code snippet :</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PipeLabel</span><span style="line-height: 140%;"> pipeLbl = ts.GetObject(pipeLblID, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="color: #2b91af; line-height: 140%;">PipeLabel</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Label.StyleName Property -&gt; Gets or sets the Entity object&#39;s style name.&#0160; &#0160; &#0160; &#0160; &#0160; </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nStyle Name Before Change : &quot;</span><span style="line-height: 140%;"> + pipeLbl.StyleName.ToString());</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Now update the Style</span></p>
<p style="margin: 0px;"><span style="background-color: #ffff00;"><strong><span style="line-height: 140%;">pipeLbl.StyleName = </span><span style="color: #a31515; line-height: 140%;">&quot;2D Length - Total Span&quot;</span><span style="line-height: 140%;">;</span></strong></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ed.WriteMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nStyle Name After Change : &quot;</span><span style="line-height: 140%;"> + pipeLbl.StyleName.ToString());</span></p>
</div>
<p>&#0160;</p>
<p>In the following screenshot we can see a particular Label Style being used for Pipe Object in Civil 3D.</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a3fcbbc47d970b-pi" style="display: inline;"><img alt="Pipe_Label_Before_Change" class="asset  asset-image at-xid-6a0167607c2431970b01a3fcbbc47d970b img-responsive" src="/assets/image_56c97d.jpg" title="Pipe_Label_Before_Change" /></a><br />&#0160;</p>
<p>And here we see by updating the StyleName how the Label style is changed :</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a5116b789b970c-pi" style="display: inline;"><img alt="Pipe_Label_After_Change" class="asset  asset-image at-xid-6a0167607c2431970b01a5116b789b970c img-responsive" src="/assets/image_50c335.jpg" title="Pipe_Label_After_Change" /></a></p>
