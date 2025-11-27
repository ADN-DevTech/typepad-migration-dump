---
layout: "post"
title: "Why are Point2d and Point3d objects immutable?"
date: "2012-10-19 18:00:33"
author: "Kean Walmsley"
categories:
  - "AutoCAD"
  - "AutoCAD .NET"
  - "Geometry"
original_url: "https://www.keanw.com/2012/10/why-are-point2d-and-point3d-objects-immutable.html "
typepad_basename: "why-are-point2d-and-point3d-objects-immutable"
typepad_status: "Publish"
---

<p>It’s been a hectic week and I haven’t been able to find much time to work on a final post for today, so I had a quick delve in my “interesting” folder and found this little gem.</p>  <p>Not long ago, someone asked me by email about the reason for making the various co-ordinate properties (X/Y/Z) of Autodesk.AutoCAD.Geometry.Point2d and Point3d read-only, essentially making these classes <a href="http://en.wikipedia.org/wiki/Immutable_object" target="_blank">immutable</a>.</p>  <p>Being a big fan of functional programming, I can think of lots of good reasons for immutability, most of which (and more) <a href="http://stackoverflow.com/questions/2365272/why-net-string-is-immutable" target="_blank">have also been given to justify the logic behind making System.String an immutable type</a> over on Stack Overflow.</p>  <p>But not having designed the .NET API in AutoCAD, I wasn’t aware of the specific design decision behind it. As it was an internal question – i.e. coming from an Autodesk employee – I suggested they contact the person who did in fact design the API, my good friend and colleague Albert Szilvasy. The response Albert gave (and was passed back to me) was the following:</p>  <blockquote>   <p><em>The same reason why System.String is immutable.</em></p>    <p><em>The problem is that a mutable type is confusing to use when it appears as a property return value. For example:</em></p>    <div style="font-family: courier new; background: white; color: black; font-size: 8pt">     <p style="margin: 0px"><span style="line-height: 140%; color: blue">class</span><span style="line-height: 140%"> A </span></p>      <p style="margin: 0px"><span style="line-height: 140%">{</span></p>      <p style="margin: 0px"><span style="line-height: 140%">&#160; </span><span style="line-height: 140%; color: blue">public</span><span style="line-height: 140%"> </span><span style="line-height: 140%; color: #2b91af">Point3d</span><span style="line-height: 140%"> prop {</span><span style="line-height: 140%; color: blue">get</span><span style="line-height: 140%">;}</span></p>      <p style="margin: 0px"><span style="line-height: 140%">};</span></p>      <p style="margin: 0px">&#160;</p>      <p style="margin: 0px"><span style="line-height: 140%">A a;</span></p>      <p style="margin: 0px"><span style="line-height: 140%">a.prop.X = 1.0;</span></p>   </div>    <p><em>Would this syntax mean that I can set the X value of a read-only property? Or would this simply mean that I set the X value of a temporary point object returned by prop?</em></p> </blockquote>  <p>So there you have it. :-)</p>
