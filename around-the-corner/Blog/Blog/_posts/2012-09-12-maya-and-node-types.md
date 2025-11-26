---
layout: "post"
title: "Maya and Node Types"
date: "2012-09-12 00:45:00"
author: "Cyrille Fauvel"
categories:
  - "C++"
  - "Cyrille Fauvel"
  - "Maya"
  - "Python"
original_url: "https://around-the-corner.typepad.com/adn/2012/09/maya-and-node-types.html "
typepad_basename: "maya-and-node-types"
typepad_status: "Publish"
---

<p>In Maya, both intrinsic and user-defined Maya Objects are registered and recognized by their type identifier or type id. The basis of the type id system is a tag which is used at run-time to determine how to create and destroy Maya Objects, and how they are to be input/output from/to files. These tag-based identifiers are implemented by the class MTypeId.</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017d3bfd0f80970c-pi" style="display: inline;"><img alt="Darkvador" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017d3bfd0f80970c" src="/assets/image_ca8825.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Darkvador" /></a><br />Before going into more details, we need to come back briefly on that statement &quot;type identifier or type id&quot;. A type identifier is a string and actually the node name, like for example &#39;polySphere&#39; for the polygon sphere definition. The name is used for several things:</p>
<ul>
<li>be able to create the node via a MEL command. I.e. &#0160;&#0160;createNode polySphere;</li>
<li>save/read nodes in/from Maya ASCII file</li>
<li>give the default naming for a newly created node of that type</li>
</ul>
<p>A type id is a numerical 32 bits integer which is used by Maya internal to identify a node type at runtime. The Id is used for few other things:</p>
<ul>
<li>register/unregister the node into the Maya kernel</li>
<li>save/read nodes in/from Maya binary file</li>
</ul>
<p>Being a&#0160;32 bits integer, it is unlikely we have nodes with the same IDs in a file unless you do not register the ID for your nodes. This is why you will need to register a block before shipping a file with one custom node of yours inside.</p>
<p>
<a class="asset-img-link" href="http://around-the-corner.typepad.com/.a/6a0163057a21c8970d017744ac5f02970d-pi" style="display: inline;"><img alt="Fingerprint" border="0" class="asset  asset-image at-xid-6a0163057a21c8970d017744ac5f02970d" src="/assets/image_af9769.jpg" style="display: block; margin-left: auto; margin-right: auto;" title="Fingerprint" /></a><br />The numeric ID ranges have been divided like this. Ids in range</p>
<ul>
<li>0 - 0x0007FFFF has been reserved for&#0160;plug-ins that will forever be internal to your site. But while we reserved those for your internal usage, I would recommend having your own range just in case you share a file and a plug-in with a customer, vendor or contractor many years later.</li>
<li>0x00080000 - 0x000FFFFF has been reserved for Maya devkit samples.&#0160;If you customize one of these plug-in&#0160;examples, you should change the id to avoid future conflicts.</li>
</ul>
<p>To get a
new range assigned to you, just go on <a href="http://www.autodesk.com/developmaya">http://www.autodesk.com/developmaya</a>
and scroll to the Tool section at the bottom of the page. You&#39;ll find a link
there to self register your ID range.</p>
<p>Now how to use these IDs is where the documentation isn&#39;t always very clear. So let review the 2 options we have to build a new MTypeId. There is two&#0160;MTypeId constructors.</p>
<ol>
<li>MTypeId&#0160; (unsigned int&#0160; id)&#0160;</li>
<li>and MTypeId&#0160; (unsigned int&#0160; prefix, unsigned int&#0160; id)</li>
</ol>
<p>What is important to understand is that the second constructor does something like this:</p>
<pre class="brush: cpp; toolbar: false;">fId = id &amp; 0x000000ff;
fId |= (prefix &amp; 0xffffff) &lt;&lt; 8;</pre>
<p>All it does is to clamp only the &#39;low&#39; 24b off the first argument vs. high order, and bitwise left shift by 8 that parameter. In case you&#39;ve been assigned a range like: (I indeed chose something complicated for the sample)</p>
<p style="padding-left: 30px;">0x00072DC0 - 0x00072E3F (128 Ids)</p>
<p>It means that you cannot use the second constructor as is since the low order part is equal C0. So you need to be careful on how to use that constructor.</p>
<p>To start with the first constructor, you can either do this</p>
<pre class="brush: cpp; toolbar: false;">MTypeId tp0 (0x00072DC0);
MTypeId tp1 (0x00072DC1);
MTypeId tp2 (0x00072DC2);
...
MTypeId tpx (0x00072E3F)</pre>
<p>But you cannot use the 2nd constructor as is. I.e.</p>
<pre class="brush: cpp; toolbar: false;">MTypeId wrongtp0 (0x00072D, 0);
MTypeId wrongtp1 (0x00072D, 1);
MTypeId wrongtpx (0x00072D, 128); // or 0x80</pre>
<p>because you would define Ids like this, and your range really starts a 0xC0 only:</p>
<pre class="brush: cpp; toolbar: false;">wrongtp0 =0x00072D00
wrongtp1 =0x00072D01
wrongtpx&#0160; =0x00072D80</pre>
<p>but you can do this</p>
<pre class="brush: cpp; toolbar: false;">MTypeId tp0 (0x00072D, 0xC0);
MTypeId tp1 (0x00072D, 0xC1);
...
MTypeId tpx (0x00072D, 0xFF); // not your last Id, but the last one valid for the 0x00072D&#0160;prefix
MTypeId tpy (0x00072E, 0x00);
...
MTypeId lasttp (0x00072E, 0x3F); // this is the 128i of your range</pre>
<p>&#0160;</p>
