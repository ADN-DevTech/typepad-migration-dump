---
layout: "post"
title: "What entities support a non-uniform scale matrix?"
date: "2013-01-08 17:26:36"
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
original_url: "https://adndevblog.typepad.com/autocad/2013/01/what-entities-support-a-non-uniform-scale-matrix.html "
typepad_basename: "what-entities-support-a-non-uniform-scale-matrix"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/gopinath-taget.html">Gopinath Taget</a></p>  <p>AutoCAD has a number of entity types and you might wonder which entities support non-uniform scaling.</p>  <p>As a general rule, the AutoCAD built-in entity classes for entity types that existed before R13 (such as AcDbCircle, AcDbLine, AcDbArc, AcDb2dPolyline, etc.) require that the transformation matrix represent a uniformly scaling orthogonal transformation (if it is not, then Acad::eCannotScaleNonUniformly will be returned). Other AutoCAD built-in classes typically does not have this restrictions.</p>  <p>Also, here is more specific information on commonly used AutoCAD entities:</p>  <p>The following are entities that have a scaling restriction:</p>  <p>AcDb2dPolyine, AcDb3dPolyine, AcDbDimension and derived classes, AcDbArc, AcDbCircle, AcDbBlockReference, AcDbMInsert, AcDbFace, AcDbLine, AcDbPloyline, AcDbPoint, AcDbPoint, AcDbHatch, AcDbShape, AcDbText and derived classes, AcDbTrace, AcDbViewport, AcDbRegion, AcDb3dSolid, and AcDbBody.</p>  <p>The entities that support non-uniform scale matrix are:</p>  <p>AcDbLeader, AcDbMLine, AcDbMText, AcDbOle2Frame, AcDbPloyFaceMesh, AcDbPolygonMesh, AcDbRay, AcDbXline, AcDbFcf, AcDbSolid, AcDbEllipse, AcDbSpline, AcDbImage</p>
