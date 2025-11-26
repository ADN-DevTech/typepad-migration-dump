---
layout: "post"
title: "Importing named Layout in an AutoCAD DWG file using LISP"
date: "2013-06-10 02:09:57"
author: "Partha Sarkar"
categories:
  - "2011"
  - "2012"
  - "2013"
  - "2014"
  - "AutoCAD"
  - "LISP"
  - "Partha Sarkar"
original_url: "https://adndevblog.typepad.com/autocad/2013/06/importing-named-layout-in-an-autocad-dwg-file-using-lisp.html "
typepad_basename: "importing-named-layout-in-an-autocad-dwg-file-using-lisp"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/infrastructure/partha-sarkar.html" target="_self">Partha Sarkar</a></p>
<p>If you are
wondering how to import a named Layout into the current drawing file using LISP
in AutoCAD, here you go.</p>
<p>LISP code
snippet below, demonstrates how to import a layout named &quot;ABC&quot; from a
selected DWG file to the current drawing file.</p>
<pre>(defun C:LayoutImport()<br /><br />  ;; Select a DWG file to import a Layout<br />	(setq myFile (getfiled &quot;Select your DWG File:&quot; &quot; &quot; &quot;dwg&quot; 8)) <br />	<span style="background-color: #ffff00;">(command&quot;layout&quot; &quot;t&quot; myFile &quot;ABC&quot;)</span> <br />	(alert &quot;New Layout is inserted!&quot;) <br />	(princ) <br />)</pre>
<p>&#0160;</p>
<p>After
executing the above LISP code, you would see a new Layout named “ABC” is
imported to the current DWG –</p>
<p>
<a class="asset-img-link" href="http://adndevblog.typepad.com/.a/6a0167607c2431970b0191032c5841970c-pi" style="float: left;"><img alt="AutoCAD_LISP_Layout" class="asset  asset-image at-xid-6a0167607c2431970b0191032c5841970c" src="/assets/image_30241.jpg" style="margin: 0px 5px 5px 0px;" title="AutoCAD_LISP_Layout" /></a></p>
<p>&#0160;</p>
