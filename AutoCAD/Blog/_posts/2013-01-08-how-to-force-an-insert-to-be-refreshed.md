---
layout: "post"
title: "How to force an insert to be refreshed?"
date: "2013-01-08 16:45:40"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/how-to-force-an-insert-to-be-refreshed.html "
typepad_basename: "how-to-force-an-insert-to-be-refreshed"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>Lets say you are looking for a way to force an update of the AcDbBlockReferences after modifying its AcDbBlockTableRecord without having to call REGEN?</p>  <p>The simplest and fastest way to do this is to iterate the current space, open all the AcDbBlockReference entities for write and call the assertWriteEnabled() method. Because your block definition could be nested in another block definition, you should call assertWriteEnabled() on all AcDbBlockReferences that are present.</p>  <p>As an alternative method (which may decrease the time spent on the process) and to avoid unnecessary 'Undo' information recording, you can temporarily turn off the Undo recording and restore it after the process. To do this, use the 'AcDbDatabase::disableUndoRecording()' method.</p>
