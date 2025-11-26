---
layout: "post"
title: "Handy Utility Classes"
date: "2013-04-09 05:00:00"
author: "Jeremy Tammik"
categories:
  - "Utilities"
original_url: "https://thebuildingcoder.typepad.com/blog/2013/04/handy-utility-classes.html "
typepad_basename: "handy-utility-classes"
typepad_status: "Publish"
---

<p>Rudolf Honke of

<a href="http://www.acadgraph.de">
Mensch und Maschine acadGraph GmbH</a> has

repeatedly encouraged me to raise awareness of the numerous utility classes available in the Revit API and now provided the following starting point for a discussion of them.</p>

<p>One way to find a number of utility classes is to search the Revit API help file RevitAPI.chm for the string "utils":</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017eea19fac9970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017eea19fac9970d" alt="Revit API utility classes" title="Revit API utility classes" src="/assets/image_fe7d5f.jpg" border="0" /></a><br />

</center>

<p>In general, these classes provide static methods that can be called from any valid context with no need for an object instance.

<p>One of the better-known examples is the

<a href="http://thebuildingcoder.typepad.com/blog/2011/08/built-in-parameter-name-and-labelutils.html">
LabelUtils class</a> that

returns localised display strings for built-in parameters and unit types:</p>

<center>

<a class="asset-img-link"  style="display: inline;" href="http://thebuildingcoder.typepad.com/.a/6a00e553e168978833017eea19fa0d970d-popup" onclick="window.open( this.href, '_blank', 'width=640,height=480,scrollbars=no,resizable=no,toolbar=no,directories=no,location=no,menubar=no,status=no,left=0,top=0' ); return false"><img class="asset  asset-image at-xid-6a00e553e168978833017eea19fa0d970d image-full" alt="LabelUtils methods" title="LabelUtils methods" src="/assets/image_d0452c.jpg" border="0" /></a><br />

</center>

<p>By the way, Rudolf misses a method for built-in categories in this class...

<p>If might be possible to implement some of these methods yourself, but using the utility methods obviously saves effort and duplication of code.</p>

<p>Another utility class that has been mentioned here in the past is

<a href="http://thebuildingcoder.typepad.com/blog/2011/08/wall-joins-and-geometry.html">WallUtils</a>.</p>

<p>It is important to be aware of their existence, or at least know where to look for them.</p>

<p>They are mostly quite well described in the help file, and yet many developers fail to notice them.
As said, sometimes you can get around using them, albeit with more effort on your own part.</p>

<p>For example, you can retrieve the element id of a referenced document using an appropriate element filter, or, much more simply, via the ExternalFileUtils GetAllExternalFileReferences method.

<p>On the other hand, some things cannot be achieved except by using these methods.</p>

<p>For instance, after placing a couple of detail instances, their display order and visibility can be modified using the DetailElementOrderUtils class methods BringToFront, BringForward, SendBackward oder SendToBack.

<p>Here is an occurrence count of the string "utils" in the different versions of the help file, showing the growth of this group of methods:

<ul>
<li>2011: 43</li>
<li>2012: 265</li>
<li>2013: 405</li>
<li>2014: 455</li>
</ul>

<p>I hope this whets your appetite and look forward to hearing about more examples of unexpected and powerful uses of these methods.</p>
