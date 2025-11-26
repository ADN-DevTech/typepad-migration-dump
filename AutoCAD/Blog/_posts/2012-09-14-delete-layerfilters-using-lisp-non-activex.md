---
layout: "post"
title: "Delete LayerFilters using LISP (non-ActiveX)"
date: "2012-09-14 15:56:15"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2012/09/delete-layerfilters-using-lisp-non-activex.html "
typepad_basename: "delete-layerfilters-using-lisp-non-activex"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p>So maybe you want to write an AutoLISP function (not ActiveX) to delete layerFilters. </p>  <p>With ActiveX (vla-xxx) functions, you can obtain the <strong>ACADLAYERS_DICTIONARY</strong> dictionary by calling</p>  <p><strong>(vla-getextensiondictionary &lt;layerscollection&gt;) </strong></p>  <p>but how can we access <strong>ACAD_LAYERFILTERS</strong> and delete the layerfilters, using just plain old AutoLISP?</p>  <p>Here’s how…</p>  <pre style="line-height: 18pt"><font face="Consolas">(defun C:DelLyrFlt ( / tbl lyr xlist xrec filt)<br />&#160;&#160; (setq tbl (tblnext &quot;LAYER&quot; T)<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; lyr (tblobjname &quot;LAYER&quot; (cdr(assoc 2 tbl)))<br />&#160;&#160; ) ;setq<br />&#160;&#160; ;;Find the&quot;ACAD_LAYERFILTERS&quot; xrecord in the extension dictionary of the layer<br />&#160;&#160; (setq xlist (dictsearch<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; (cdr (assoc 360 (entget (cdr (assoc 330 (entget lyr))))))<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; &quot;ACAD_LAYERFILTERS&quot;)<br />&#160;&#160; ) ;setq<br />&#160;&#160; (if xlist<br />&#160;&#160;&#160;&#160;&#160; (progn<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; ;;Get the first layer_filter<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; (setq xrec (cdr (assoc -1 xlist))<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; filt (dictnext xrec t)<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; ) ;setq<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; ;;Remove each layer_filter<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; (while filt<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; (dictremove xrec (cdr (assoc 1 filt)))<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; (setq filt (dictnext xrec t))<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; ) ;while<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; ;;Find the&quot;ACAD_LAYERFILTERS&quot; xrecord<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; (dictremove<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; (cdr (assoc 360 (entget (cdr (assoc 330 (entget lyr))))))<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; &quot;ACAD_LAYERFILTERS&quot;<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; )<br />&#160;&#160;&#160;&#160;&#160; ) ;progn<br />&#160;&#160; ) ;if<br />&#160;&#160; (princ)<br />)</font></pre>
