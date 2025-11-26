---
layout: "post"
title: "Accessing Point File Format names and their extension using Civil 3D .NET API"
date: "2014-04-28 00:35:17"
author: "Partha Sarkar"
categories:
  - ".NET"
  - "AutoCAD Civil 3D 2015"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/infrastructure/2014/04/accessing-point-file-format-names-and-their-extension-using-civil-3d-net-api.html "
typepad_basename: "accessing-point-file-format-names-and-their-extension-using-civil-3d-net-api"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>Those of you tried to access the Point File Format names and their extension using Civil 3D .NET API earlier and found some issues in enumerating <strong>PointFileFormatCollection</strong> and extracting the name of the individual point file format in a DWG file, this is now resolved in Civil 3D 2015 release.</p>
<p>We can now iterate the <strong>PointFileFormatCollection</strong> object and extract the name as well as other associated properties like file extension etc.</p>
<p>&#0160;</p>
<p><a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b01a511aad4f3970c-pi" style="display: inline;"><img alt="Point_File_Formats" class="asset  asset-image at-xid-6a0167607c2431970b01a511aad4f3970c img-responsive" src="/assets/image_7570e8.jpg" title="Point_File_Formats" /></a><br />&#0160;</p>
<p>&#0160;</p>
<p>Here is the C# code snippet :</p>
<p>&#0160;</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> ed = </span><span style="color: #2b91af; line-height: 140%;">AcadApp</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Editor;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = </span><span style="color: #2b91af; line-height: 140%;">AcadApp</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument.Database;</span>&#0160;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PointFileFormatCollection</span><span style="line-height: 140%;"> ptFileFormatColls = </span><span style="color: #2b91af; line-height: 140%;">PointFileFormatCollection</span><span style="line-height: 140%;">.GetPointFileFormats(db);</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">String</span><span style="line-height: 140%;"> ptFileFormatstring = </span><span style="color: #a31515; line-height: 140%;">&quot;&quot;</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">//start a transaction </span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> trans = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; </span><span style="color: blue; line-height: 140%;">foreach</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">PointFileFormat</span><span style="line-height: 140%;"> pointFileFormat </span><span style="color: blue; line-height: 140%;">in</span><span style="line-height: 140%;"> ptFileFormatColls)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; {&#0160; &#0160; &#0160; &#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; ptFileFormatstring = ptFileFormatstring + </span><span style="color: #a31515; line-height: 140%;">&quot;\nPoint File Format Name : &quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; + pointFileFormat.Name.ToString() + </span><span style="color: #a31515; line-height: 140%;">&quot;&#0160;&#0160; &quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; + </span><span style="color: #a31515; line-height: 140%;">&quot;File Extension :&#0160; &quot;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; &#0160; &#0160; + pointFileFormat.FileExtension.ToString();&#0160; &#0160;&#0160; </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; }</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; ed.WriteMessage(ptFileFormatstring);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160; trans.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
<p>&#0160;</p>
<p>Hope this is useful to you.</p>
