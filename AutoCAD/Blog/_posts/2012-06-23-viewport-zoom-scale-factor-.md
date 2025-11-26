---
layout: "post"
title: "Viewport zoom scale factor "
date: "2012-06-23 20:20:06"
author: "Balaji"
categories:
  - "AutoCAD"
  - "Balaji Ramamoorthy"
  - "LISP"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/06/viewport-zoom-scale-factor-.html "
typepad_basename: "viewport-zoom-scale-factor-"
typepad_status: "Publish"
---

<p>To get the viewport zoom scale factor, you can query for the 'CustomScale' property on the paper space viewport entity. Alternatively, get the viewport paperspace height, which is DXF group code 41. The model space viewport height is contained in the viewport's extended entity data and is the 2nd DXF group code1040. Extended entity data can be extracted into 'resbuf' result buffers, however, ObjectARX provides AcDbViewport::viewHeight() that gets this value directly. Therefore, the viewport entity (AcDbViewport) zoom scale factor is calculated as :</p>
<p>group_41 / 2nd_group_1040 (or pspace_height / mspace_height).</p>
<p>The following code fragments demonstrates how to calculate the zoom scale factor&nbsp;for an AcDbViewport entity:</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">(defun c:ZoomScaleFactor (/ a_app a_doc obj a_pvp msp_ht )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; (vl-load-com)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; (setq&nbsp;&nbsp;&nbsp; a_app (vlax-get-acad-object)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; a_doc (vla-get-ActiveDocument a_app)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; obj&nbsp;&nbsp; (vlax-ename-&gt;vla-object (car (entsel &quot;Select a pviewport entity: &quot;)))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; )</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; (vla-put-mspace a_doc :vlax-true)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; (setq&nbsp;&nbsp;&nbsp; a_pvp&nbsp; (vla-get-ActivePViewport a_doc)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; pvp_ht (vla-get-height a_pvp)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; msp_ht (GETVAR &quot;VIEWSIZE&quot;)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; zsf&nbsp;&nbsp; (vla-get-customscale a_pvp)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; )</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; (princ (strcat &quot;pvp height : &quot; (rtos pvp_ht)))</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; (princ (strcat &quot;msp height : &quot; (rtos msp_ht)))</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; (vla-put-mspace a_doc :vlax-false)</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; (print (strcat &quot;Viewport Zoom scale factor = &quot; (rtos (/ pvp_ht msp_ht) 2 2)))</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; (print (strcat &quot;Viewport Zoom scale factor = &quot; (rtos zsf)))</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; (princ)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">)</span></p>
</div>
<p></p>
<p>Here is the ObjectARX equivalent of the&nbsp;same code :</p>
<p></p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">ads_name ename;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">ads_point pt;</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">int</span><span style="line-height: 140%;"> rc = acedEntSel(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nSelect Viewport entity &quot;</span><span style="line-height: 140%;">), ename, pt);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(rc != RTNORM)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acutPrintf(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nError selecting entity &quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbObjectId entId;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">Acad::ErrorStatus es = acdbGetObjectId(entId, ename);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbEntity* pEnt;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">es = acdbOpenObject(pEnt, entId, AcDb::kForRead);</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">AcDbViewport* pVPEnt = AcDbViewport::cast(pEnt);</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">if</span><span style="line-height: 140%;">(!pVPEnt)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">{</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; acutPrintf(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nEntity is not a Viewport entity &quot;</span><span style="line-height: 140%;">));</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; pEnt-&gt;close();</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">&nbsp;&nbsp;&nbsp; </span><span style="color: blue; line-height: 140%;">return</span><span style="line-height: 140%;">;</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">}</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the paper space viewport's height</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// DXF group code 41</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> psht = pVPEnt-&gt;height();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Get the model space viewport's height</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Extended Entity Data 2nd 1040 DXF group code under 'ACAD'</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> msht = pVPEnt-&gt;viewHeight();</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// Calculate the viewport entity zoom factor</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// The ZOOM factor is calculated with the following formula:</span></p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// group_41 / 2nd_group_1040 (or pspace_height / mspace_height).</span></p>
<p style="margin: 0px;"><span style="color: blue; line-height: 140%;">double</span><span style="line-height: 140%;"> xpsf = psht / msht;</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">acutPrintf(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nViewport Zoom scale factor = %.2lf &quot;</span><span style="line-height: 140%;">), xpsf);</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="color: green; line-height: 140%;">// OR</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">acutPrintf(_T(</span><span style="color: #a31515; line-height: 140%;">&quot;\nViewport Zoom scale factor = %.2lf&quot;</span><span style="line-height: 140%;">), pVPEnt-&gt;customScale());</span></p>
<p style="margin: 0px;">&nbsp;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">pVPEnt-&gt;close();</span></p>
</div>
