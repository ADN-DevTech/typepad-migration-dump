---
layout: "post"
title: "How to access the 'Include Path' checkbox in the Xref dialog?"
date: "2013-01-01 19:08:49"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/how-to-access-the-include-path-checkbox-in-the-xref-dialog.html "
typepad_basename: "how-to-access-the-include-path-checkbox-in-the-xref-dialog"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>There is no variable or user setting that allows access to this checkbox other than what is provided with the XREF command. However, the code behind this is able to set the path of the xref directly.</p>  <p>For example, the ObjectARX method AcDbBlockTableRecord::setPathName() will do this. You can also use AcDbBlockTableRecord::pathName() to check on the current path setting for a given xref.</p>
