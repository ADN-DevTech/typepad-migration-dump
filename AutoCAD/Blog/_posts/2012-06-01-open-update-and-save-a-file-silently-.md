---
layout: "post"
title: "open, update and save a file silently "
date: "2012-06-01 03:47:29"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/open-update-and-save-a-file-silently-.html "
typepad_basename: "open-update-and-save-a-file-silently-"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue<br /> </strong>How to open a drawing and save it to another location without displaying it in the Acad window ?</p>
<p><strong>Solution<br /> </strong>You can use AcDbDatabase::readDwgFile() and AcDbDatabase::saveAs() to accomplish this. When you do saveAs(), you can specify any local or mapped network drive and directory as you desire, given that you have write-access to the destination and the file is not opened by AutoCAD or any other application. If the file exists, saveAs() will overwrite it quietly without any warning if no one else is operating on it. Therefore, precaution must be taken so that data is not mistakenly lost.</p>
<p>The code snippet of an ARX below assumes a drawing &quot;c:\temp\In.dwg&quot; exists. It reads the dwg, adds one circle and save as to a new drawing silently.</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;</span><span style="color: green; line-height: 140%;">// save as silently</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">static</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">void</span><span style="line-height: 140%;"> mySaveAs()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; AcDbDatabase* pDb = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> AcDbDatabase(Adesk::kFalse); <br /></span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; Acad::ErrorStatus es =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; pDb-&gt;readDwgFile(L</span><span style="color: #a31515; line-height: 140%;">&quot;c:\\temp\\In.dwg&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; assert(es == Acad::eOk);</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160; </span><span style="color: green; line-height: 140%;">// get the block table</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; AcDbBlockTable *pBlockTable;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160; &#0160; &#0160;&#0160;&#0160;&#0160; es = pDb-&gt;getBlockTable(pBlockTable,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; AcDb::kForRead);</span></p>
<span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (es != Acad::eOk)</span>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; }</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// get model space</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; AcDbBlockTableRecord *pBlockTableRec;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; es = pBlockTable-&gt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; getAt(ACDB_MODEL_SPACE, pBlockTableRec,</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; AcDb::kForWrite);</span></p>
<br />
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (es != Acad::eOk)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; {</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; pBlockTable-&gt;close();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160;&#0160; }</span></p>
<br />
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; pBlockTable-&gt;close();</span></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// create a new entity</span></div>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; AcDbCircle *pCircle =</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> AcDbCircle(AcGePoint3d(0,0,0),</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; &#0160;&#0160;&#0160; AcGeVector3d(0,0,1),100);</span></p>
<br />
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// add the new entity to the model space</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; AcDbObjectId objId;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; pBlockTableRec-&gt;appendAcDbEntity(objId, pCircle);</span></p>
<br />
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// close the entity</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; pCircle-&gt;close();</span></p>
<span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; </span><span style="color: green; line-height: 140%;">// close the model space block</span>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160; &#0160;&#0160;&#0160; pBlockTableRec-&gt;close(); </span></p>
<p style="margin: 0px;">&#0160;</p>
&#0160;&#0160;&#0160;&#0160;&#0160;<span style="color: #00bf00;">&#0160;&#0160; // save as to the new drawing</span><br />
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; es = pDb-&gt;saveAs(L</span><span style="color: #a31515; line-height: 140%;">&quot;c:\\temp\\Out.dwg&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; assert(es == Acad::eOk);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; </span><span style="color: blue; line-height: 140%;">delete</span><span style="line-height: 140%;"> pDb;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;"><br /></span></p>
</div>
<p>Notice if you use AcDbDatabase::saveAs() and the drawing exists and is currently open by AutoCAD, it will fail. The error code is eFileAccessErr. The ObjectARX documentation regarding this is incomplete. In addition, the document warns that if the database executing the saveAs() function is not the current database in the AutoCAD editor, then the thumbnail preview image is not saved to the file name you specified.</p>
<p>With the introduction of <a href="http://adndevblog.typepad.com/autocad/2012/04/getting-started-with-accoreconsole.html" target="_self">Accoreconsole</a> , this becomes much easier. The following is a small demo to achieve the same effect as the above ARX does. It is just a script! And I found the thumbnail can also be saved with the drawing.</p>
<p>content of src file</p>
<p><span style="color: #0080ff;">;Command:</span><br /><span style="color: #0080ff;">FILEDIA</span><br /><span style="color: #0080ff;">;Enter new value for FILEDIA &lt;1&gt;:</span><br /><span style="color: #0080ff;">0</span><br /><span style="color: #0080ff;">;Command:</span><br /><span style="color: #0080ff;">Circle</span><br /><span style="color: #0080ff;">(100,100,0)</span><br /><span style="color: #0080ff;">100</span><br /><span style="color: #0080ff;">SaveAs</span><br /><span style="color: #0080ff;">&quot;C:\temp\Out.DWG&quot;</span><br /><span style="color: #0080ff;">;Command:</span><br /><span style="color: #0080ff;">FILEDIA</span><br /><span style="color: #0080ff;">;;;Enter new value for FILEDIA &lt;1&gt;:</span><br /><span style="color: #0080ff;">1</span></p>
<p>It assumes a drawing &quot;c:\temp\In.dwg&quot; exists, the scr file is put to c:\temp and you have swtiched to the path of <span style="color: #111111;">Accoreconsole, the command line of Windows could be:</span></p>
<p><span style="color: #0080ff;">C:\Program Files\Autodesk\AutoCAD 2013&gt;accoreconsole /i &quot;c:\temp\In.dwg&quot;</span><br /><span style="color: #0080ff;">/s &quot;c:\temp\update.scr&quot;</span></p>
<p><span style="color: #111111;">You can write a bat file to make it more flexible. Please refer to the<a href="http://adndevblog.typepad.com/autocad/Downloads/DevTV-AccoreConsole.zip" target="_self"> DevTV on </a></span><a href="http://adndevblog.typepad.com/autocad/Downloads/DevTV-AccoreConsole.zip" target="_self">Accoreconsole</a></p>
<p>&#0160;</p>
<p><span style="color: #111111;"><br /></span></p>
<p>&#0160;</p>
<p>&#0160;</p>
