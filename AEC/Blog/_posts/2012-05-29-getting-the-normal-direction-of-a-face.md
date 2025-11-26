---
layout: "post"
title: "Getting the normal direction of a face"
date: "2012-05-29 21:30:50"
author: "Katsuaki Takamizawa"
categories:
  - "Katsuaki Takamizawa"
original_url: "https://adndevblog.typepad.com/aec/2012/05/getting-the-normal-direction-of-a-face.html "
typepad_basename: "getting-the-normal-direction-of-a-face"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/aec/katsuaki-takamizawa.html" target="_self">Katsuaki Takamizawa</a></p>
<p>When I used PlanarFace.Normal property to get normals of faces, the direction seems to be opposite for some faces.</p>
<p><span style="background-color: #fcfae1; color: #0000ff;">foreach </span>(Face face <span style="color: #0000ff;">in </span>solid.Faces)<br />{<br />&#0160; PlanarFace planarFace = face as PlanarFace;</p>
<p>&#0160;&#0160;&#0160; if (planarFace != null)<br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; <span style="color: #60bf00;">// Get a normal direction of the face from Normal property</span><br /><span style="color: #60bf00;">&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; //</span><br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; Trace.WriteLine(<span style="color: #c00000;">&quot;Normal=&quot;</span> + planarFace.Normal.ToString());<br />}</p>
<p>Actually, the right way to find the normal of a face is to use PlanarFace.ComputeNormal method. The following code computes a normal at the origin of the face by passing the UV coordinate as following.</p>
<p>XYZ normal = planarFace.ComputeNormal(new UV(planarFace.Origin.X, planarFace.Origin.Y));</p>
<p>&#0160;</p>
