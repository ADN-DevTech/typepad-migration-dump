---
layout: "post"
title: "Making non-database resident objects work with undo"
date: "2013-01-22 16:42:09"
author: "Gopinath Taget"
categories:
  - "2010"
  - "2011"
  - "2012"
  - "2013"
  - "AutoCAD"
  - "Gopinath Taget"
  - "ObjectARX"
original_url: "https://adndevblog.typepad.com/autocad/2013/01/making-non-database-resident-objects-work-with-undo.html "
typepad_basename: "making-non-database-resident-objects-work-with-undo"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>Non-database resident objects do not participate in an Undo/Redo. However, it is fairly easy to take advantage of the built-in Undo/Redo feature with the following procedure:</p>  <p>1. Derive your objects from AcDbObject as if you were creating a custom database-resident object. (You will only need to override the dwgInFields/dwgOutFields methods.)   <br />2. Append the objects to the database using AcDbDatabase::addAcDbObject</p>  <p>The addAcDbObject function appends the object to the database but does not set up an owner for the object so it will not be saved when the database is saved.</p>  <p>You will need to use proper open/close protocol on this object to ensure undo/redo works as expected.</p>
