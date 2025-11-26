---
layout: "post"
title: "Quick tip: Get the dwg file path from AcDbDatabase object"
date: "2013-02-15 12:56:35"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/02/quick-tip-get-the-dwg-file-path-from-acdbdatabase-object.html "
typepad_basename: "quick-tip-get-the-dwg-file-path-from-acdbdatabase-object"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>You might have noticed that on occasion the &quot;acdbHostApplicationServices()-&gt;workingDatabase()-&gt;getFilename( pFileName )&quot; returns the name of the temporary save file rather than the name of actual file that is open. This is as designed.</p>  <p> AcDbDatabase::originalFileName() function retrieves the file name under which the drawing is originally opened. In addition, AcApDocument::fileName() function also can retrieve the actual file full path.</p>
