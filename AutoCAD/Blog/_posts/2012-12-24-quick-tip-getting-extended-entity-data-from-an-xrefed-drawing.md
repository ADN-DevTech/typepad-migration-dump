---
layout: "post"
title: "Quick tip: Getting extended entity data from an xref'ed drawing"
date: "2012-12-24 16:33:19"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "ActiveX"
  - "AutoCAD"
  - "Gopinath Taget"
  - "LISP"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/quick-tip-getting-extended-entity-data-from-an-xrefed-drawing.html "
typepad_basename: "quick-tip-getting-extended-entity-data-from-an-xrefed-drawing"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>An xref'ed DWG file is like an INSERT for AutoCAD. This means that AutoCAD loads the xref'ed DWG file into a AcDbBlockTableRecord (block definition) in the current drawing. Therefore, if XDATA was attached to a line in the XREF drawing   <br />file, a copy of that line (with XDATA) is present in the block definition. In this case, iterate the block definition to retrieve the XDATA information.</p>
