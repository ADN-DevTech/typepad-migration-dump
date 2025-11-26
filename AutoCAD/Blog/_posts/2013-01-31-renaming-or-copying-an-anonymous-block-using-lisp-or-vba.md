---
layout: "post"
title: "Renaming or copying an anonymous block using LISP or VBA"
date: "2013-01-31 15:52:13"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "ActiveX"
  - "AutoCAD"
  - "Fenton Webb"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/renaming-or-copying-an-anonymous-block-using-lisp-or-vba.html "
typepad_basename: "renaming-or-copying-an-anonymous-block-using-lisp-or-vba"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p><b>Issue</b></p>  <p>How do I rename or copy an anonymous block to another name, such as TESTBLOCK?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>It is possible to rename an anonymous block. For example, you can rename a block named *T1 to TESTBLOCK. When listed, the available blocks in a drawing use commands such as BLOCK or INSERT. TESTBLOCK will not be listed because it is an   <br />anonymous block (but it exists). Changing an anonymous block's name will not make it a named block. The group code 70 decides if the block is anonymous, and this group code cannot be edited.</p>  <p>For example, create an anonymous block &quot;*T&quot; using any_blk function, and then execute the ren_blk function to rename it to &quot;TESTBLOCK&quot;.</p>  <pre><pre><br /><font size="1" face="Consolas">(defun c:any_blk ()<br /> (vl-load-com)<br /> (setq a_app  (VLAX-GET-ACAD-OBJECT)<br />    a_doc  (vla-get-ActiveDocument a_app)<br />    a_blks (vla-get-blocks a_doc)<br />    blk    (vla-add a_blks (vlax-3d-point '(0 0 0)) &quot;*T&quot;)<br /> )<br /> (vla-addcircle blk (vlax-3d-point '(0 0 0)) 3)<br />)<br /><br />(defun c:ren_blk ()<br /> (vl-load-com)<br /> (setq a_app  (VLAX-GET-ACAD-OBJECT)<br />    a_doc  (vla-get-ActiveDocument a_app)<br />    a_blks (vla-get-blocks a_doc)<br />    blk    (vla-item a_blks &quot;*T1&quot;)<br />)<br /> (vla-put-name blk &quot;TESTBLOCK&quot;)<br />)</font><br /></pre></pre>

<p>Now invoke command INSERT and you will find that TESTBLOCK is not listed, but you are allowed to type the blockname and then to insert it. The block TESTBLOCK is not listed as it is an anonymous block.</p>

<p>As a workaround, you could make a copy of the anonymous block instead of renaming it, as shown in the following sample code:</p>

<pre><pre><br /><font size="1" face="Consolas">(defun c:cop_blk ()<br /> (vl-load-com)<br /> (setq a_app  (VLAX-GET-ACAD-OBJECT)<br />    a_doc  (vla-get-ActiveDocument a_app)<br />    a_blks (vla-get-blocks a_doc)<br />    i      0<br /> )<br /> (if (tblsearch &quot;BLOCK&quot; &quot;*T1&quot;)<br />      (progn<br />     (setq blk (vla-item a_blks &quot;*T1&quot;))<br />     (setq inspt  (vla-get-origin blk)<br />           cnt    (- (vla-get-count blk) 1)<br />           newfil (vlax-make-safearray vlax-vbobject (cons 0 cnt))<br />     )<br />     (vlax-for ent    blk<br />         (vlax-safearray-put-element newfil i ent)<br />         (setq i (1+ i))<br />     )<br />     (if (null (tblsearch &quot;BLOCK&quot; &quot;TESTBLOCK&quot;))<br />          (setq newblk (vla-add a_blks inspt &quot;TESTBLOCK&quot;))<br />          (setq newblk (vla-add a_blks inspt &quot;TESTBLOCKX&quot;))<br />     )<br />     (vla-copyobjects a_doc newfil newblk nil)<br />      )<br />      (princ &quot;\nBlock *T is not available. Unable to make a Copy.&quot;)<br /> )<br /> (princ)<br />)<br /></font></pre></pre>

<p><font size="1" face="Consolas">Here is a sample VBA code to copy an anonymous block:</font></p>

<pre><pre><br /><font size="1" face="Consolas">Sub copyblk()<br />   Dim objects() As Object<br />   Dim oldblk As AcadBlock<br />   Dim newblk As AcadBlock<br />   Dim inspt As Variant<br />   Dim obj As Object<br />   Dim i As Integer<br />   <br />   i = 0<br />   Set oldblk = ThisDrawing.Blocks.Item(&quot;*T1&quot;)  'Replace &quot;*T1&quot; with the<br />appropriate anonymous block name<br />   inspt = oldblk.Origin<br />   Cnt = oldblk.Count - 1<br />   ReDim objects(Cnt) As Object<br />   <br />   For Each obj In oldblk<br />    Set objects(i) = obj<br />    i = i + 1<br />   Next obj<br />   <br />   Set newblk = ThisDrawing.Blocks.Add(inspt, &quot;NEWTEST&quot;)<br />   ThisDrawing.CopyObjects objects, newblk<br />End Sub</font></pre></pre>
