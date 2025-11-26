---
layout: "post"
title: "Civil 3D .NET API: Is there any simple way to copy a style to the same drawing?"
date: "2012-04-05 01:24:59"
author: "Marat Mirgaleev"
categories:
  - "AutoCAD Civil 3D 2011"
  - "AutoCAD Civil 3D 2012"
  - "AutoCAD Civil 3D 2013"
original_url: "https://adndevblog.typepad.com/infrastructure/2012/04/civil-3d-net-api-is-there-any-simple-way-to-copy-a-style-to-the-same-drawing.html "
typepad_basename: "civil-3d-net-api-is-there-any-simple-way-to-copy-a-style-to-the-same-drawing"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/marat-mirgaleev.html" target="_self">Marat Mirgaleev</a></p>
<p>There is a method which copies a Civil 3D style object from one drawing to another – this is StyleBase::ExportTo(). It is helpful for copying any kind of styles, such as Label styles, Surface styles or others. But when you use this method to copy a style to the same drawing, it may fail.</p>
<p>The reason for this is that the ExportTo() method was not designed to export styles to the same drawings.</p>
<p>Instead of this, we can use StyleBase::CopyAsSibling(). Here is a code snippet :</p>
<div style="font-family: courier new; background: white; color: black; font-size: 8pt;">
<p style="margin: 0px;"><span style="line-height: 140%; color: #2b91af;">Database</span><span style="line-height: 140%;"> db = </span><span style="line-height: 140%; color: #2b91af;">HostApplicationServices</span><span style="line-height: 140%;">.WorkingDatabase;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%; color: blue;">using</span><span style="line-height: 140%;"> (</span><span style="line-height: 140%; color: #2b91af;">Transaction</span><span style="line-height: 140%;"> ts = </span><span style="line-height: 140%; color: #2b91af;">HostApplicationServices</span><span style="line-height: 140%;">.WorkingDatabase.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: #2b91af;">ObjectId</span><span style="line-height: 140%;"> id = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </span><span style="line-height: 140%; color: #2b91af;">CivilApplication</span><span style="line-height: 140%;">.ActiveDocument.Styles.AlignmentStyles[0];</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; </span><span style="line-height: 140%; color: #2b91af;">AlignmentStyle</span><span style="line-height: 140%;"> alignStyle = </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; id.GetObject(</span><span style="line-height: 140%; color: #2b91af;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="line-height: 140%; color: blue;">as</span><span style="line-height: 140%;"> </span><span style="line-height: 140%; color: #2b91af;">AlignmentStyle</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; alignStyle.CopyAsSibling(</span><span style="line-height: 140%; color: #a31515;">"newStyle_CopyAlignmentStyle"</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp; ts.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
</div>
