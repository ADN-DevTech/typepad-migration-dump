---
layout: "post"
title: "Inventor API: Face.AlternateBody"
date: "2013-07-22 01:12:30"
author: "Vladimir Ananyev"
categories:
  - "Inventor"
  - "Vladimir Ananyev"
original_url: "https://adndevblog.typepad.com/manufacturing/2013/07/inventor-api-facealternatebody.html "
typepad_basename: "inventor-api-facealternatebody"
typepad_status: "Publish"
---

<p>by <a href="http://adndevblog.typepad.com/manufacturing/vladimir-ananyev.html">Vladimir Ananyev</a></p>  <p><b>Q: </b>Could you provide more information on the Face.AlternateBody property?     <br />Sometimes I only get one loop when I query a face.</p>  <p><a name="section2"></a><b>A: </b>Let's say the Face.AlternateBody property is the context we're talking about.</p>  <p>The internal implementation is trying to query the SurfaceGeometryFormEnum_ClosedUVLoops and SurfaceGeometryFormEnum_NURBS as the AlternateForm. Obviously the UVLoops and NURBS are two alternative geometry representations for the face. However not every face can have both representations all the time. That's likely why you're getting one loop per face in your case. I understand that since most native Inventor surfaces are periodic surfaces (except cylinder, torus, etc.), you would want some alternative body info for your specific purpose.</p>  <p>Note that AlternateBody API feature is just a facility to output certain high-level geometric / topological properties that might be interesting to the API's client – such as, does the Faces Loops close in UV-space (a.k.a UVLoops, they may not for certain implementations of analytic surfaces)? Does the surface or curve have an underlying B-Spline (NURBS) basis? etc. In case, a client cannot consume the data with its existing set of properties, the Inventor engine may be capable of returning an alternate form of the object that is more palatable. In such an event, the Inventor engine can support an additional interface on the object that delivers the alternate form. Of course, the client is urged to stay within the original, preferred form and use the alternate route only as a last resort. There is a real danger of encountering down-graded data in the alternate state, since it is not the preferred / native data of the Inventor engine.</p>
