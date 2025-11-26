---
layout: "post"
title: "Checking the status of a Toolbar using LISP or VBA"
date: "2013-01-17 15:01:57"
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
original_url: "https://adndevblog.typepad.com/autocad/2013/01/checking-the-status-of-a-toolbar-using-lisp-or-vba.html "
typepad_basename: "checking-the-status-of-a-toolbar-using-lisp-or-vba"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/fenton-webb.html">Fenton Webb</a></p>  <p><b>Issue</b></p>  <p>How to check, at any time, whether a toolbar is shown or not, using either Visual LISP or VBA?</p>  <p><a name="section2"></a></p>  <p><b>Solution</b></p>  <p>The following LISP and VBA code will tell you the current status of a specific toolbar.&#160; To modify it, substitute the appropriate menugroup, toolbar name, and properties.&#160; </p>  <p><strong>LISP</strong>:</p>  <pre>;;; For demonstration purposes only<br />;;; No error checking provided<br />(defun C:TBar_Chk()<br />&#160;&#160; (vl-load-com)<br />&#160;&#160; (setq oAcad (vlax-get-acad-object)<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; oDoc (vla-get-activedocument oAcad)<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; oMenuGrp (vla-item (vla-get-menugroups oAcad) &quot;ACAD&quot;)<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; oTBar (vla-item (vla-get-toolbars oMenuGrp) &quot;Viewports&quot;)<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; sTBarName (vlax-get-property oTBar 'name)<br />&#160;&#160; )&#160; <br />&#160;&#160; (if (= (vlax-get-property oTBar 'visible) :vlax-true)<br />&#160;&#160;&#160;&#160;&#160; (alert (strcat &quot;Toolbar: &quot; sTBarName &quot;\nStatus: Visible&quot;))<br />&#160;&#160;&#160;&#160;&#160; (alert (strcat &quot;Toolbar: &quot; sTBarName &quot;\nStatus: Hidden&quot;))<br />&#160;&#160; )<br />)<br /></pre>

<p><strong>VBA</strong>:</p>

<pre>';;; For demonstration purposes only<br />';;; No error checking provided<br />Sub TBar_Chk()<br />&#160;&#160;&#160; Dim oMenuGrp As AcadMenuGroup<br />&#160;&#160;&#160; Dim oTBar As AcadToolbar<br />&#160;&#160;&#160; Dim sTBarName As String<br />&#160;&#160;&#160; Set oMenuGrp = ThisDrawing.Application.MenuGroups.Item(&quot;ACAD&quot;)<br />&#160;&#160;&#160; Set oTBar = oMenuGrp.Toolbars.Item(&quot;Viewports&quot;)<br />&#160;&#160;&#160; sTBarName = oTBar.Name<br />&#160;&#160;&#160; If oTBar.Visible = True Then<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; MsgBox &quot;Toolbar: &quot; + sTBarName + vbCr + &quot;Status: Visible&quot;<br />&#160;&#160;&#160; Else<br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; MsgBox &quot;Toolbar: &quot; + sTBarName + vbCr + &quot;Status: Hidden&quot;<br />&#160;&#160;&#160; End If<br />End Sub<br /></pre>

<p><img alt="" src="/assets/cnt_topcorner.gif" width="7" height="5" /></p>
