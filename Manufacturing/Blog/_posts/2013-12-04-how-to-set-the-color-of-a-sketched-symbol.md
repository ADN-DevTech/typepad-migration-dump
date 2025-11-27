---
layout: "post"
title: "How to Set the Color of a Sketched Symbol"
date: "2013-12-04 07:32:06"
author: "Adam Nagy"
categories: []
original_url: "https://adndevblog.typepad.com/manufacturing/2013/12/how-to-set-the-color-of-a-sketched-symbol.html "
typepad_basename: "how-to-set-the-color-of-a-sketched-symbol"
typepad_status: "Publish"
---

<p>Q: I’m using the following code to set the color of a sketched symbol but nothing happens. How do I set the color of a sketched symbol?</p>  <p><font face="Consolas">&#160;&#160; sketchedSym.Color.SetColor(255, 0, 0)</font></p>  <p>A: Properties that return transient objects return a &quot;tear off&quot; object that's not associated to the original object it was obtained from.&#160; For example, if you have a work plane and call the Plane property, it returns a Plane object that provides the geometric information of the plane.&#160; Or if you call the Geometry property of a cylindrical face it will return a Cylinder object which provides the geometric information for a cylinder.&#160; These objects serve as a way to conveniently pass a set of related information.&#160; A Color object is also a transient that provides a convenient way to pass color information in the API. </p>  <p>When Color property of the SketchedSymbol object is called the API creates a new Color object using the color information associated with the symbol and returns it.&#160; The Color object returned is not associated with the symbol.&#160; When you change any of the properties of the Color object, you are changing the color object but since it's not associated with the symbol, it doesn’t affect the sketched symbol and you don't see anything happen.&#160; The way to set the color of the symbol is to use a Color object to set the Color property. A common way is to create a new Color object to pass in, as shown below.</p>  <p><font face="Consolas">&#160;&#160; Dim transObjs As TransientObjects     <br />&#160;&#160; Set transObjs = </font><font face="Consolas">ThisApplication.TransientObjects     <br /></font><font face="Consolas">&#160;&#160; sketchedSym.Color = transObjs.CreateColor(255, 0, 0)</font></p>
