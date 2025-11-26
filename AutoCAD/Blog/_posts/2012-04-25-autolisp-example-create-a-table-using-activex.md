---
layout: "post"
title: "AutoLISP example - create a Table (using ActiveX)"
date: "2012-04-25 10:40:24"
author: "Wayne Brill"
categories:
  - "LISP"
  - "Wayne Brill"
original_url: "https://adndevblog.typepad.com/autocad/2012/04/autolisp-example-create-a-table-using-activex.html "
typepad_basename: "autolisp-example-create-a-table-using-activex"
typepad_status: "Publish"
---

<p>Here is a Visual Lisp example that uses the ActiveX interface to create a table:</p>  <p>By <a href="http://adndevblog.typepad.com/autocad/wayne-brill.html" target="_self">Wayne Brill</a></p>  <p>&#160;</p>  <p>(defun c:addMyTable ( / ActiveDocument mSpace pt    <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; myTable nRows nCols row cell )     <br />&#160; (vl-load-com)     <br />&#160; (setq ActiveDocument (vla-get-activedocument     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; (vlax-get-acad-object)))     <br />&#160; (setq mSpace(vla-get-modelspace ActiveDocument))     <br />&#160; (setq pt (vlax-make-safearray vlax-vbDouble     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160; '(0 . 2)))     <br />&#160; ;insertion point for the table     <br />&#160; (vlax-safearray-fill pt '(2.0 2.0 0.0))     <br />&#160; (setq myTable     <br />&#160;&#160;&#160;&#160; (vla-addtable mSpace pt 5 5 10 30))     <br />&#160; (vla-setcelltextheight myTable 0 0 5)     <br />&#160; (vla-settext myTable 0 0 &quot;myTable&quot;)     <br />&#160; <br />&#160; ;rows and columns zero based     <br />&#160; (setq nRows(- (vla-get-rows myTable) 1))     <br />&#160; (setq nCols(- (vla-get-columns myTable) 1))     <br />&#160; <br />&#160; ; rows and columns after row 0, column 0     <br />&#160; (setq row 1)     <br />&#160; (setQ cell 0)     <br />&#160; <br />&#160; ; loop through cells     <br />&#160; (while (&lt;= row nRows)     <br />&#160;&#160;&#160; (while (&lt;= cell nCols)     <br />&#160;&#160; (vla-setCelltextHeight myTable row cell 5.0)     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; (vla-settext myTable row cell &quot;test&quot;)     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; ; make cell alignment middle center     <br />&#160;&#160;&#160;&#160;&#160;&#160;&#160; (vla-setCellAlignment myTable row cell 5)&#160;&#160;&#160;&#160;&#160; <br />&#160;&#160; (setq cell (1+ cell))     <br />&#160;&#160; );while     <br />&#160; (setq row (1+ row))     <br />&#160; (setq cell 0)&#160; <br />);while     <br />(princ)     <br />);defun</p>
