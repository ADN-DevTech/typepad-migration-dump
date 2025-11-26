---
layout: "post"
title: "Strip XData from the Layer Table using LISP"
date: "2013-01-17 11:48:30"
author: "Fenton Webb"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Fenton Webb"
  - "LISP"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/strip-xdata-from-the-layer-table-using-lisp.html "
typepad_basename: "strip-xdata-from-the-layer-table-using-lisp"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html"><font color="#0066cc">Fenton Webb</font></a></p>  <p><b>Issue</b></p>  <p>How do I remove the XDATA that is attached to my Layer Table ?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>The following AutoLISP code will remove all the XData attached to the Layer Table:</p>  <pre><br />(defun StripLayerXdata ()
<p> (setq aLayerLst (tblnext &quot;LAYER&quot; T))<br /> (while aLayerLst<br />&#160;&#160; (setq aLayerName (cdr (assoc '2 aLayerLst)))<br />&#160;&#160; (setq aLayerELst (entget (tblobjname &quot;LAYER&quot; aLayerName) '(&quot;*&quot;)))<br />&#160;&#160; (if (setq xdata (assoc '-3 aLayerELst))<br />&#160;&#160;&#160;&#160; (progn<br />&#160;&#160;&#160; (setq newLayerLst (subst (cons (car xdata) (list (list (caadr xdata))))<br />xdata aLayerELst))<br />&#160;&#160;&#160; (entmod newLayerLst)<br />&#160;&#160;&#160;&#160; )<br />&#160;&#160; )<br />&#160;&#160; (setq aLayerLst (tblnext &quot;LAYER&quot;))<br /> )<br /> (princ)<br />)<br />(vl-doc-export 'StripLayerXdata)<br />(princ &quot;\nStripLayerXdata loaded, type (StripLayerXdata) to run.&quot;)<br />(princ)<br /></p></pre>
