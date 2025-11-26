---
layout: "post"
title: "Load time and startup time tasks using VisualLisp"
date: "2012-04-13 00:03:49"
author: "Balaji"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Balaji Ramamoorthy"
original_url: "https://adndevblog.typepad.com/autocad/2012/04/load-time-and-startup-time-tasks-using-visuallisp.html "
typepad_basename: "load-time-and-startup-time-tasks-using-visuallisp"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/Balaji-Ramamoorthy.html" target="_self">Balaji Ramamoorthy</a></p>
<p>There is an easy way to setup your Lisp functions to run when any drawing is opened in AutoCAD.</p>
<p>Before we get to the details, lets me explain what I will be referring to as the &quot;load time&quot; and &quot;startup time&quot; in this post.</p>
<p>When a drawing is opened in AutoCAD, while it is still being loaded you may be interested in running your lisp function. I refer to this as the &quot;load time&quot;.</p>
<p>Just after the drawing is fully opened, you may be interested in running your lisp function. I refer to this as the &quot;startup time&quot;</p>
<p>AutoCAD loads the &quot;acaddoc.lsp&quot; when a drawing is opened. If this file does not exist, then just create it and ensure that it is in one of the support paths configured in AutoCAD. This lsp file will be automatically loaded by AutoCAD for each drawing that is opened.</p>
<p>Here is a sample code that you can copy and paste at the end of your &quot;acaddoc.lsp&quot; :</p>
<div style="font-family: Courier New; font-size: 8pt; color: black; background: white;">
<p style="margin: 0px;"><span style="line-height: 140%;">(defun load-time-operations ()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(princ &quot;\nThis executes at load time. Before s::startup.&quot;)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(princ &quot;\nMy load time tasks...&quot;)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">)</span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">; Ensure that load-time-operations gets a call</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(load-time-operations) </span></p>
<p style="margin: 0px;">&#0160;</p>
<p style="margin: 0px;"><span style="line-height: 140%;">(defun s::startup ()</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(princ &quot;\nDocument is opened and AutoCAD is now ready.&quot;)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">(princ &quot;\nMy Startup tasks...&quot;)</span></p>
<p style="margin: 0px;"><span style="line-height: 140%;">)</span></p>
</div>
<p>Save the acaddoc.lsp and start AutoCAD and open a drawing. You can find the load time and startup messages displayed.</p>
<p>If your Lisp code is big and you do not want to add all the code to the acaddoc.lsp, you may also load your lsp file using the &quot;(load &quot;C:\\Test\\MyFuncs.lsp&quot;)&quot;</p>
<p>Thanks to Owen Wengerd for highlighting this important point.</p>
<p>AutoCAD provides the acadXXXXdoc.lsp (For ex : acad2012doc.lsp) that gets loaded even before the acaddoc.lsp for every drawing that is opened. Inserting your lisp code in this file will also work but is definitely not recommended because this file is reserved for use by AutoCAD. Instead you will need to create the acaddoc.lsp file if it does not already exist.</p>
<p>&#0160;</p>
<p>&#0160;</p>
