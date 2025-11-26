---
layout: "post"
title: "Wblocking a Block by Specifying the Block Name Using ActiveX"
date: "2012-10-25 21:49:16"
author: "Balaji"
categories:
  - "ActiveX"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/10/wblocking-a-block-by-specifying-the-block-name-using-activex.html "
typepad_basename: "wblocking-a-block-by-specifying-the-block-name-using-activex"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/balaji-ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<div><strong>Issue</strong></div>
<p>Is there a way to WBLOCK a block by specifying the block name using ActiveX?</p>
<!--stopindex-->
<div><a name="section2"> </a><!--startindex-->
<div><strong>Solution</strong></div>
<p>There is not a method in ActiveX to WBLOCK a block by passing the block name as is done at the command line. A workaround is to pass a selection set which contains the entities of the desired block to the Wblock method. To do this, a selection set is created with the entities contained in the block. </p>
<p>Note: These examples do not include error handling for simplicity.</p>
<p>Here is a VBA example which illustrates the process of wblocking by using a block name:</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Sub</span><span style="line-height: 140%;"> TestWblockWithName()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> ss </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> AcadSelectionSet</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> blk </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> AcadBlock</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> i </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Integer</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> obj() </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Object</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Dim</span><span style="line-height: 140%;"> blkname </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">String</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; blkname = ThisDrawing.Utility.GetString(</span><span style="color: blue; line-height: 140%;">True</span><span style="line-height: 140%;">, </span><span style="color: #a31515; line-height: 140%;">&quot;Enter block name: &quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; fname = ThisDrawing.Utility.GetString(</span><span style="color: blue; line-height: 140%;">True</span><span style="line-height: 140%;">, </span><span style="color: #a31515; line-height: 140%;">&quot;Enter Wblock file name: &quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; blk = ThisDrawing.Blocks(blkname)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; cnt = blk.Count</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">ReDim</span><span style="line-height: 140%;"> obj(0 </span><span style="color: blue; line-height: 140%;">To</span><span style="line-height: 140%;"> cnt - 1) </span><span style="color: blue; line-height: 140%;">As</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Object</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">'Populate an object array with entities of the desired block</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">For</span><span style="line-height: 140%;"> i = 0 </span><span style="color: blue; line-height: 140%;">To</span><span style="line-height: 140%;"> cnt - 1</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; obj(i) = blk(i)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Next</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">'We are using CopyObjects as it replaces the need to insert and then explode the blockreference.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; obj_arr = ThisDrawing.CopyObjects(obj, ThisDrawing.ModelSpace)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">'Create a seleciton set and populate it with the desired entities.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ss = ThisDrawing.SelectionSets.Add(</span><span style="color: #a31515; line-height: 140%;">&quot;tt&quot;</span><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ss.AddItems(obj_arr)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">'Wblock the selection set.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ThisDrawing.Wblock(fname, ss)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; ss.Delete()</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: green; line-height: 140%;">'Delete those entities that CopyObjects created.</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">For</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Each</span><span style="line-height: 140%;"> itm </span><span style="color: blue; line-height: 140%;">In</span><span style="line-height: 140%;"> obj_arr</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; itm.Delete()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">Next</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">End</span><span style="line-height: 140%;"> </span><span style="color: blue; line-height: 140%;">Sub</span></p>
</div>
<p></p>
<p>Here is a VLisp example :</p>
<p></p>
<p><span style="line-height: 120%; font-family: 'courier new', courier; font-size: 8pt;"><span style="color:#ff0000">(</span><span style="color:#0000ff">defun</span>&nbsp;c:TestWblock&nbsp;<span style="color:#ff0000">(</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vl-load-com</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setq</span>&nbsp;a_app&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-get-acad-object</span><span style="color:#ff0000">)</span><br />
&nbsp;a_doc&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-get-ActiveDocument</span>&nbsp;a_app<span style="color:#ff0000">)</span><br />
&nbsp;a_msp&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-get-modelspace</span>&nbsp;a_doc<span style="color:#ff0000">)</span><br />
&nbsp;a_blks&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-get-blocks</span>&nbsp;a_doc<span style="color:#ff0000">)</span><br />
&nbsp;blknam&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">getstring</span>&nbsp;t&nbsp;<span style="color:#ff00ff">"Enter&nbsp;block&nbsp;name:&nbsp;"</span><span style="color:#ff0000">)</span><br />
&nbsp;fname&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">getstring</span>&nbsp;t&nbsp;<span style="color:#ff00ff">"Enter&nbsp;Wblock&nbsp;file&nbsp;name:&nbsp;"</span><span style="color:#ff0000">)</span><br />
&nbsp;a_blk&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-item</span>&nbsp;a_blks&nbsp;blknam<span style="color:#ff0000">)</span><br />
&nbsp;a_sss&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-get-SelectionSets</span>&nbsp;a_doc<span style="color:#ff0000">)</span><br />
&nbsp;a_ss&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-add</span>&nbsp;a_sss&nbsp;<span style="color:#ff00ff">"myset"</span><span style="color:#ff0000">)</span><br />
&nbsp;i&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;0<br />
&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setq</span>&nbsp;sa&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-make-safearray</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;vlax-vbobject<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span>cons&nbsp;<span style="color:#008000">0</span>&nbsp;<span style="color:#ff0000">(</span>-&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-get-count</span>&nbsp;a_blk<span style="color:#ff0000">)</span>&nbsp;<span style="color:#008000">1</span><span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-for</span>&nbsp;itm&nbsp;a_blk<br />
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-safearray-put-element</span>&nbsp;sa&nbsp;i&nbsp;itm<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setq</span>&nbsp;i&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">1+</span>&nbsp;i<span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
<br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">setq</span>&nbsp;obj_arr&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-copyobjects</span>&nbsp;a_doc&nbsp;sa&nbsp;a_msp<span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-additems</span>&nbsp;a_ss&nbsp;obj_arr<span style="color:#ff0000">)</span><br />
<br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-Wblock</span>&nbsp;a_doc&nbsp;fname&nbsp;a_ss<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-delete</span>&nbsp;a_ss<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span>foreach&nbsp;itm&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-safearray-</span>>list&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vlax-variant-value</span>&nbsp;obj_arr<span style="color:#ff0000">)</span><span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">vla-delete</span>&nbsp;itm<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">)</span><br />
&nbsp;&nbsp;<span style="color:#ff0000">(</span><span style="color:#0000ff">princ</span><span style="color:#ff0000">)</span><br />
<span style="color:#ff0000">)</span><br />
</span></p>
