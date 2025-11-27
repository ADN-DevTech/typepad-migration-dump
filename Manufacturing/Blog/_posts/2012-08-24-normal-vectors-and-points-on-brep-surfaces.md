---
layout: "post"
title: "Normal Vectors and Points on BREP surfaces"
date: "2012-08-24 12:34:46"
author: "Augusto Goncalves"
categories:
  - "Augusto Goncalves"
  - "Inventor"
original_url: "https://adndevblog.typepad.com/manufacturing/2012/08/normal-vectors-and-points-on-brep-surfaces.html "
typepad_basename: "normal-vectors-and-points-on-brep-surfaces"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/manufacturing/augusto-goncalves.html">Augusto Goncalves</a></p>  <p><em>(1) Does the Normal Vector returned by Face.Evaluator.GetNormal() point into, or out of the material in a solid?</em></p>  <p>When used on a solid - Face.Evaluator.GetNormal() returns normal vectors pointing away from the solid material, into void. The same is true of FaceProxy.Evaluator.GetNormal()</p>  <p><em>(2) What does Face.IsParamReversed = True mean?</em></p>  <p>Geometry obtained from BRep entity maybe aligned or opposed with this BRep entity, IsParamReversed property indicates whether the underlying geometry is consistent or reversed with the BRep. So if you use Face.Geometry.Evaluator.GetNormal(), and Face.IsParamReversed is true, then the normals returned only need to be reversed to point out of the solid.</p>  <p><em>(3) Do calls return the same value, and does Face.IsParamReversed affect the result?     <br />Face.Evaluator.GetPointAtParam(u, v)      <br />Face.Geometry.Evaluator.GetPointAtParam(u, v);</em></p>  <p>Face.Evaluator.GetPointAtParam() and Face.Geometry.Evaluator.GetPointAtParam() return a same point for a given u,v coordinate.The same is also true when considering FaceProxy objects. Face.IsParamReversed do affect on the result. </p>
