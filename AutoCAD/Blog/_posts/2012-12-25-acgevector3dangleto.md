---
layout: "post"
title: "AcGeVector3d::angleTo()"
date: "2012-12-25 02:05:00"
author: "Xiaodong Liang"
categories:
  - "AutoCAD"
  - "ObjectARX"
  - "Xiaodong Liang"
original_url: "https://adndevblog.typepad.com/autocad/2012/12/acgevector3dangleto.html "
typepad_basename: "acgevector3dangleto"
typepad_status: "Publish"
---

<p>By <a href="http://adndevblog.typepad.com/autocad/xiaodong-liang.html" target="_self">Xiaodong Liang</a></p>
<p><strong>Issue     <br /></strong>The method AcGeVector3d::angleTo(    <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; const AcGeVector3d &amp;vec,     <br />&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160;&#0160; const AcGeVector3d&amp;&#0160; refVec)&#0160; is described as follows in the ARXREF.HLP:</p>
<p>&quot;<em>Returns the angle between this vector and the vector &#39;vec&#39; in the range [0, 2 * PI]; If ( refVec.dotProduct(crossProduct(vec)) &gt;= 0.0 ) the return value coincides with the return value of the function angleTo( vec ). Otherwise the return value is 2*PI minus the return value of the function angleTo( vec ).&quot;</em></p>
<p>What exactly is meant when passing as refVec?</p>
<p><a name="section2"></a></p>
<p><strong>Solution</strong></p>
<p>According to the online documentation, there are two overloaded functions for angleTo().</p>
<p>One is:</p>
<pre><pre><br />double AcGeVector3d::angleTo( const AcGeVector3d&amp; vec) const;<br /></pre>
</pre>
<p>Returns the angle between this vector and the vector vec in the range [0, Pi].</p>
<p>Another is:</p>
<pre><pre><br />double AcGeVector3d::angleTo(<br />                    const AcGeVector3d&amp; vec,<br />                    const AcGeVector3d&amp; refVec) const;<br /></pre>
</pre>
<p>Returns the angle between this vector and the vector vec in the range [0, 2 x&#0160; Pi]. If (refVec.dotProduct(crossProduct(vec)) &gt;= 0.0), then the return value&#0160; coincides with the return value of the function angleTo(vec). Otherwise, the&#0160; return value is 2 x Pi minus the return value of the function angleTo(vec). Therefore, the return angle&#39;s range is different. That&#39;s exactly why the refVec&#0160; is needed. Dot Product, as you know, is a method to find out how parallel two 
  <br />lines are. The result of the calculation is a scalar. This is most useful if you&#0160; use unit vectors (get a number between -1 and 1), such as our usage here&#0160; (vectors).</p>
<pre><pre>|x1| |x2|<br /><br />|y1| . |y2| = (x1 * x2) + (y1 * y2) + (z1 * z2)<br /><br />|z1| |z2|<br /></pre>
</pre>
<p>Cross Product is the process of multiplying two vectors together to get a&#0160; normal to the plane they describe with the Vector (0,0,0).</p>
<pre><pre><br />|x1| |x2| |y1*z2 - z1*y2|<br /><br />|y1| x |y2| = |z1*x2 - x1*z2|<br /><br />|z1| |z2| |x1*y2 - y1*x2|<br /></pre>
</pre>
<p>Therefore, in the code,</p>
<pre><pre>refVec.dotProduct(crossProduct(vec))</pre>
</pre>
<p>it first checks the angle between the normal (for the plane which &#39;this&#39; vector&#0160; and &#39;vec&#39; vector is on) and the refVec. As it states, if 
  <br />(refVec.dotProduct(crossProduct(vec)) &gt;= 0.0), then the return value coincides with the return value of the function angleTo(vec). Otherwise the return value is 2 x Pi minus the return value of the function angleTo(vec). You can actually use the normal vector for the plane that &#39;this&#39; vector is on,&#0160; as the refVec, to check the &#39;vec&#39;. Of course, you can choose other vectors that&#0160; are not the same as the normal vector depends on your application needs.</p>
