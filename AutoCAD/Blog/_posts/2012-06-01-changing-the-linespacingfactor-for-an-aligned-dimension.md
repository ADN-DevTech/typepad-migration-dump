---
layout: "post"
title: "Changing the LineSpacingFactor for an Aligned Dimension"
date: "2012-06-01 21:25:22"
author: "Balaji"
categories:
  - ".NET"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "LISP"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/changing-the-linespacingfactor-for-an-aligned-dimension.html "
typepad_basename: "changing-the-linespacingfactor-for-an-aligned-dimension"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>When placing an Aligned Dimension using the AutoCAD command, you can choose the MTEXT option and create multiple line text. Here is the sample code to change the Line Spacing Factor.</p>
<div>
<p>Lisp :</p>
<p>An existing dimension could be modifed using Lisp. In this sample code, we reduce the line spacing factor by 25%.<br />
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">(setq e (car(entsel)))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(setq ed (entget e))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(entmod (subst (cons 41 0.75) (assoc 41 ed) ed))</span></p>
</div>
<p></p>
<br />ObjectARX :<br />
<p></p>
It is also possible to change this setting with ObjectARX.  The following example changes pDim-&gt;textLineSpacingFactor to .75x of its original value, and applies it to the selected dimension.</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">AcGePoint3d ptPick;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ads_name eName;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (acedEntSel(</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ACRX_T(</span><span style="color: #a31515; line-height: 140%;">&quot;Select a dimension: &quot;</span><span style="line-height: 140%;">), </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; eName, </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; asDblArray (ptPick)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; ) != RTNORM )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbObjectId id;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acdbGetObjectId (id, eName);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbEntity *pEnt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">acdbOpenAcDbEntity (pEnt, id, AcDb::kForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbDimension *pDim = AcDbDimension::cast (pEnt);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pEnt-&gt;close ();</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (pDim == NULL)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (Acad::eOk != acdbOpenObject(pDim, id, AcDb::kForWrite))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// get the current line spacing factor</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> dimLSpcFac = pDim-&gt;textLineSpacingFactor();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Reduce the line spacing factor</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pDim-&gt;setTextLineSpacingFactor(dimLSpcFac * 0.75);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// update the dimension's block table record</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">pDim-&gt;recomputeDimBlock();</span></p>
<p style="margin: 0px;">&nbsp;</p>
</div>
<p></p>
<p>AutoCAD .Net API :<br /> <br /></p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Document</span><span style="line-height: 140%;"> doc = </span><span style="color: #2b91af; line-height: 140%;">Application</span><span style="line-height: 140%;">.DocumentManager.MdiActiveDocument;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Database</span><span style="line-height: 140%;"> db = doc.Database;</span></p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">Editor</span><span style="line-height: 140%;"> ed = doc.Editor;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PromptEntityOptions</span><span style="line-height: 140%;"> peo </span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; = </span><span style="color: blue; line-height: 140%;">new</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">PromptEntityOptions</span><span style="line-height: 140%;">(</span><span style="color: #a31515; line-height: 140%;">&quot;Select a dimension : &quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">peo.SetRejectMessage(</span><span style="color: #a31515; line-height: 140%;">&quot;\nPlease select the dimension to modify&quot;</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">peo.AddAllowedClass(</span><span style="color: blue; line-height: 140%;">typeof</span><span style="line-height: 140%;">(</span><span style="color: #2b91af; line-height: 140%;">Dimension</span><span style="line-height: 140%;">), </span><span style="color: blue; line-height: 140%;">false</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">PromptEntityResult</span><span style="line-height: 140%;"> per = ed.GetEntity(peo);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;"> (per.Status != </span><span style="color: #2b91af; line-height: 140%;">PromptStatus</span><span style="line-height: 140%;">.OK)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: #2b91af; line-height: 140%;">ObjectId</span><span style="line-height: 140%;"> id = per.ObjectId;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">using</span><span style="line-height: 140%;"> (</span><span style="color: #2b91af; line-height: 140%;">Transaction</span><span style="line-height: 140%;"> tr = db.TransactionManager.StartTransaction())</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: #2b91af; line-height: 140%;">Dimension</span><span style="line-height: 140%;"> dim = tr.GetObject(id, </span><span style="color: #2b91af; line-height: 140%;">OpenMode</span><span style="line-height: 140%;">.ForWrite) </span><span style="color: blue; line-height: 140%;">as</span><span style="line-height: 140%;"> </span><span style="color: #2b91af; line-height: 140%;">Dimension</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// get the current line spacing factor</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> dimLSpcFac = dim.TextLineSpacingFactor;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// Reduce the line spacing factor</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; dim.TextLineSpacingFactor = dimLSpcFac * 0.75;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">// update the dimension's block table record</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; dim.RecomputeDimensionBlock(</span><span style="color: blue; line-height: 140%;">true</span><span style="line-height: 140%;">);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; tr.Commit();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
</div>
