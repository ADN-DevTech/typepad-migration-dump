---
layout: "post"
title: "Rotation Adjusts and Fixes Conduit End"
date: "2018-02-02 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Element Creation"
  - "Geometry"
  - "Regen"
  - "Transaction"
  - "Update"
original_url: "https://thebuildingcoder.typepad.com/blog/2018/02/rotation-adjusts-conduit-end.html "
typepad_basename: "rotation-adjusts-conduit-end"
typepad_status: "Publish"
---

<p>Quite a while ago, I listed a bunch of possible approaches
to <a href="http://thebuildingcoder.typepad.com/blog/2014/06/refresh-element-graphics-display.html">refresh element graphics display</a>.</p>

<p>Here comes a new one to join the gang:</p>

<h4><a name="2"></a>Applying Element Rotation to Adjust and Fix Conduit End</h4>

<p><strong>Question:</strong> I am creating a series of several conduit elements.</p>

<p>One conduit is in a different size and conduit type.</p>

<p>When I modify the conduit diameters before the elbow is created, one of the conduit ends exceeds the elbow.</p>

<p>The observed result looks like this:</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c94c412a970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c94c412a970b image-full img-responsive" alt="Conduit end extending too far" title="Conduit end extending too far" src="/assets/image_b61d73.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br />
</center></p>

<p>I am expecting it to look like this:</p>

<p><center></p>

<p><a class="asset-img-link"  href="http://thebuildingcoder.typepad.com/.a/6a00e553e16897883301b7c94c4138970b-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e16897883301b7c94c4138970b image-full img-responsive" alt="Conduit end adjusted" title="Conduit end adjusted" src="/assets/image_e60238.jpg" border="0" style="display: block; margin-left: auto; margin-right: auto;" /></a><br /></p>

<p></center></p>

<p>If I remove the code to set the different size and type, and change diameters as usual, then this issue does not appear.</p>

<p><strong>Answer by Paolo Serra:</strong> This happened to me as well on pipes and valves.</p>

<p>I fixed this after the fitting creation by applying a call to <code>ElementTransformUtils.Rotate</code> to the family instance, passing in a zero radians angle.</p>

<pre class="code">
<span style="color:blue;">using</span>(&nbsp;<span style="color:#2b91af;">Transaction</span>&nbsp;trans&nbsp;=&nbsp;<span style="color:blue;">new</span>&nbsp;<span style="color:#2b91af;">Transaction</span>(&nbsp;doc&nbsp;)&nbsp;)
{
&nbsp;&nbsp;trans.Start(&nbsp;<span style="color:#a31515;">&quot;虚假旋转线管&quot;</span>&nbsp;);
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;ids&nbsp;=&nbsp;conduits.Select(&nbsp;c&nbsp;=&gt;&nbsp;c.Id&nbsp;).ToList();
&nbsp;&nbsp;<span style="color:blue;">var</span>&nbsp;axis&nbsp;=&nbsp;<span style="color:#2b91af;">Line</span>.CreateBound(&nbsp;<span style="color:#2b91af;">XYZ</span>.Zero,&nbsp;<span style="color:#2b91af;">XYZ</span>.BasisX&nbsp;);
&nbsp;&nbsp;<span style="color:#2b91af;">ElementTransformUtils</span>.RotateElements(&nbsp;doc,&nbsp;ids,&nbsp;axis,&nbsp;0&nbsp;);
&nbsp;&nbsp;trans.Commit();
}
</pre>
